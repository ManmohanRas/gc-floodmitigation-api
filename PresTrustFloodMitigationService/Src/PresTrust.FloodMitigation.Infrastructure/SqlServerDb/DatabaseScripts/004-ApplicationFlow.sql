use prestrusttemp
BEGIN TRY
	BEGIN TRANSACTION
	--==============================================================================================================--

		DECLARE
			@v_LEGACY_RECORD_COUNT INT,
			@v_LEGACY_RECORD_INDEX INT;

		DROP TABLE IF EXISTS [#LegacyApplicationIds];

		CREATE TABLE [#LegacyApplicationIds] (
			Id INT IDENTITY(1,1),
			LegacyApplicationId INT
		);

		INSERT INTO [#LegacyApplicationIds]
		(
			LegacyApplicationId
		)
		SELECT
			LegacyApplicationId
		FROM [Flood].[FloodApplicationLegacy]
		WHERE ISNULL(FloodApplicationId,0) = 0;

		SET	@v_LEGACY_RECORD_COUNT = @@ROWCOUNT;
		SET	@v_LEGACY_RECORD_INDEX = 1;

		WHILE (@v_LEGACY_RECORD_INDEX <= @v_LEGACY_RECORD_COUNT)
		BEGIN

			DECLARE
				@v_LEGACY_APPLICATION_ID INT,
				@v_NEW_APPLICATION_ID INT;

			SELECT
				@v_LEGACY_APPLICATION_ID = LegacyApplicationId 
			FROM		[#LegacyApplicationIds]
			WHERE		ID = @v_LEGACY_RECORD_INDEX;

			INSERT INTO [Flood].[FloodApplication]
			(
				Title,
				AgencyId,
				ApplicationTypeId,
				ApplicationSubTypeId,
				StatusId,
				ExpirationDate,
				CreatedByProgramAdmin,
				LastUpdatedBy,
				LastUpdatedOn,
				IsActive
			)
			SELECT
				ProjectArea,
				MunicipalID,
				CASE
					WHEN ProgramType = 'Core' THEN 1
					WHEN ProgramType = 'Match' THEN 2
				END AS ProgramType,
				CASE
					WHEN SubProgramType = 'Disaster' THEN 1
					WHEN SubProgramType = 'Fast Track' THEN 2
					WHEN SubProgramType = 'Ongoing Flooding' THEN 4
					ELSE 3
				END AS SubProgramType,
				CASE
					WHEN ProjectAreaStatus = 'Preliminary' THEN 3
					WHEN ProjectAreaStatus = 'Active' THEN 6
					WHEN ProjectAreaStatus = 'Closed' THEN 7
					WHEN ProjectAreaStatus = 'Rejected' THEN 8
					WHEN ProjectAreaStatus = 'Withdrawn' THEN 9
					ELSE 1
				END AS ProjectAreaStatus,
				FundingExpirationDate,
				1 AS CreatedByProgramAdmin,
				'flood-admin' AS LastUpdatedBy,
				GetDate() AS LastUpdatedOn,
				1 AS IsActive
			FROM [FloodMitigation].[floodmp].[tblProjectArea]
			WHERE MunicipalID IS NOT NULL AND ProjectAreaID = @v_LEGACY_APPLICATION_ID;

			SET @v_NEW_APPLICATION_ID = @@IDENTITY;

			UPDATE	[Flood].[FloodApplicationLegacy]
			SET FloodApplicationId = @v_NEW_APPLICATION_ID
			WHERE	LegacyApplicationId = @v_LEGACY_APPLICATION_ID;

			INSERT INTO [Flood].[FloodApplicationOverview]
			(
				[ApplicationId],
				[NoOfHomes],
				[NoOfContiguousHomes],
				[NatlDisaster],
				[NatlDisasterId],	 
				[NatlDisasterName],	 
				[NatlDisasterYear],
				[NatlDisasterMonth],
				[LOI],
				[LOIStatus],
				[LOIApprovedDate],
				[FEMA_OR_NJDEP_Applied],
				[FEMAApplied],
				[FEMAStatus],
				[FEMAApprovedDate],
				[FEMADenialReason],
				[GreenAcresApplied],
				[GreenAcresStatus],
				[GreenAcresApprovedDate],
				[BlueAcresApplied],
				[BlueAcresStatus],
				[BlueAcresApprovedDate],
				[FundingAgenciesApplied],
				[LastUpdatedBy],
				[LastUpdatedOn]
			)
			SELECT TOP 1
				@v_NEW_APPLICATION_ID,
				PA.[FactorHomes],
				PA.[FactorContiguousHomes],
				NULL AS NatlDisaster,
				PA.[NatlDisasterId],
				NULL AS NatlDisasterName,
				NULL AS NatlDisasterYear,
				NULL AS NatlDisasterMonth,
				PA.[LOI],
				FP.[LOIApproved],
				PA.[LOIDate],
				NULL AS FEMA_OR_NJDEP_Applied,
				PA.[FEMAApplied],
				PA.[FEMAApproved],
				PA.[FEMAApprovedDate],
				PA.[FEMAAppDenyReason],
				PA.[GreenAcresApplied],
				PA.[GreenAcresApproved],
				PA.[GreenAcresApprovedDate],
				PA.[BlueAcresApplied],
				PA.[BlueAcresApproved],
				PA.[BlueAcresApprovedDate],
				NULL AS FundingAgenciesApplied,
				'flood-admin'  AS LastUpdatedBy,
				GETDATE() AS LastUpdatedOn
			FROM [FloodMitigation].[floodmp].tblFloodParcel AS FP 
			LEFT JOIN [FloodMitigation].[floodmp].tblProjectArea AS PA ON (FP.ProjectAreaID = PA.ProjectAreaID)
			WHERE PA.ProjectAreaID = @v_LEGACY_APPLICATION_ID;

			INSERT INTO [Flood].[FloodApplicationFinance]
			(
				[ApplicationId],
				[MatchPercent],
				[LastUpdatedBy],
				[LastUpdatedOn]
			)
			SELECT TOP 1
				@v_NEW_APPLICATION_ID,
                 CASE 
					WHEN FP.ProgramMatchPercent = FLOOR(FP.ProgramMatchPercent) 
					THEN CAST(FP.ProgramMatchPercent AS INT)
					ELSE CAST((FP.ProgramMatchPercent - FLOOR(FP.ProgramMatchPercent)) * 100 AS INT)
                 END AS ProgramMatchPercentValue,
				'flood-admin'  AS LastUpdatedBy,
				GETDATE() AS LastUpdatedOn
			FROM [FloodMitigation].[floodmp].tblFloodParcel AS FP 
			LEFT JOIN [FloodMitigation].[floodmp].tblProjectArea AS PA ON (FP.ProjectAreaID = PA.ProjectAreaID)
			WHERE PA.ProjectAreaID = @v_LEGACY_APPLICATION_ID;

			-- Insert FEMA Award data
INSERT INTO [Flood].[FloodApplicationFinanceFund] (ApplicationId,FundingSourceTypeId, Title, Amount, AwardDate,LastUpdatedBy,LastUpdatedOn)
SELECT
    @v_NEW_APPLICATION_ID,
    1 AS FundingSourceTypeId,  -- Assuming 1 corresponds to FEMA in your funding source type table
    'FEMA Award' AS Title,
    FEMAAwardAmt AS Amount,
    ISNULL (FEMAAwardDate,NULL) AS AwardDate,
	'flood-admin'  AS LastUpdatedBy,
	GETDATE() AS LastUpdatedOn
FROM [FloodMitigation].[floodmp].tblProjectArea PA
WHERE PA.ProjectAreaID = @v_LEGACY_APPLICATION_ID
AND FEMAAwardAmt IS NOT NULL --AND FEMAAwardDate IS NOT NULL;

-- Insert Green Acres data
INSERT INTO [Flood].[FloodApplicationFinanceFund] (ApplicationId,FundingSourceTypeId, Title, Amount, AwardDate,LastUpdatedBy,LastUpdatedOn)
SELECT 
@v_NEW_APPLICATION_ID,
    2 AS FundingSourceTypeId,  -- Assuming 2 corresponds to Green Acres
    'Green Acres' AS Title,
    GreenAcresAmt AS Amount,
    ISNULL (GreenAcresAmtDate,NULL) AS AwardDate,
	'flood-admin'  AS LastUpdatedBy,
	GETDATE() AS LastUpdatedOn
FROM [FloodMitigation].[floodmp].tblProjectArea PA
WHERE PA.ProjectAreaID = @v_LEGACY_APPLICATION_ID
AND GreenAcresAmt IS NOT NULL --AND GreenAcresAmtDate IS NOT NULL;

-- Insert Blue Acres data
INSERT INTO [Flood].[FloodApplicationFinanceFund] (ApplicationId,FundingSourceTypeId, Title, Amount, AwardDate,LastUpdatedBy,LastUpdatedOn)
SELECT
@v_NEW_APPLICATION_ID,
    3 AS FundingSourceTypeId,  -- Assuming 3 corresponds to Blue Acres
    'Blue Acres' AS Title,
    BlueAcresAmt AS Amount,
    ISNULL (BlueAcresAmtDate,NULL) AS AwardDate,
	'flood-admin'  AS LastUpdatedBy,
	GETDATE() AS LastUpdatedOn
FROM [FloodMitigation].[floodmp].tblProjectArea PA
WHERE PA.ProjectAreaID = @v_LEGACY_APPLICATION_ID
AND BlueAcresAmt IS NOT NULL --AND BlueAcresAmtDate IS NOT NULL;

-- Insert Muni OSTF data
INSERT INTO [Flood].[FloodApplicationFinanceFund] (ApplicationId,FundingSourceTypeId, Title, Amount, AwardDate,LastUpdatedBy,LastUpdatedOn)
SELECT 
@v_NEW_APPLICATION_ID,
    4 AS FundingSourceTypeId,  -- Assuming 4 corresponds to Muni OSTF
    'Muni OSTF' AS Title,
    MuniOSTFAmt AS Amount,
    ISNULL (MuniOSTFAmtDate,NULL) AS AwardDate,
	'flood-admin'  AS LastUpdatedBy,
	GETDATE() AS LastUpdatedOn
FROM [FloodMitigation].[floodmp].tblProjectArea PA
WHERE PA.ProjectAreaID = @v_LEGACY_APPLICATION_ID
AND MuniOSTFAmt IS NOT NULL --AND MuniOSTFAmtDate IS NOT NULL;

-- Insert Muni Funds data
INSERT INTO [Flood].[FloodApplicationFinanceFund] (ApplicationId,FundingSourceTypeId, Title, Amount, AwardDate,LastUpdatedBy,LastUpdatedOn)
SELECT 
@v_NEW_APPLICATION_ID,
    5 AS FundingSourceTypeId,  -- Assuming 5 corresponds to Muni Funds
    'Muni Funds' AS Title,
    MuniFundsAmt AS Amount,
    ISNULL (MuniFundsAmtDate,NULL) AS AwardDate,
	'flood-admin'  AS LastUpdatedBy,
	GETDATE() AS LastUpdatedOn
FROM [FloodMitigation].[floodmp].tblProjectArea PA
WHERE PA.ProjectAreaID = @v_LEGACY_APPLICATION_ID
AND MuniFundsAmt IS NOT NULL --AND MuniFundsAmtDate IS NOT NULL;

-- Insert Landowner Donation data
INSERT INTO [Flood].[FloodApplicationFinanceFund] (ApplicationId,FundingSourceTypeId, Title, Amount, AwardDate,LastUpdatedBy,LastUpdatedOn)
SELECT 
@v_NEW_APPLICATION_ID,
    6 AS FundingSourceTypeId,  -- Assuming 6 corresponds to Landowner Donation
    'Landowner Donation' AS Title,
    LandownerDonationAmt AS Amount,
    ISNULL (LandownerDonationAmtDate,NULL) AS AwardDate,
	'flood-admin'  AS LastUpdatedBy,
	GETDATE() AS LastUpdatedOn
FROM [FloodMitigation].[floodmp].tblProjectArea PA
WHERE PA.ProjectAreaID = @v_LEGACY_APPLICATION_ID 
AND LandownerDonationAmt IS NOT NULL --AND LandownerDonationAmtDate IS NOT NULL;

-- Insert other funding data
INSERT INTO [Flood].[FloodApplicationFinanceFund] (ApplicationId,FundingSourceTypeId, Title, Amount, AwardDate,LastUpdatedBy,LastUpdatedOn)
SELECT 
    @v_NEW_APPLICATION_ID,
    7 AS FundingSourceTypeId,  -- Assuming 7 corresponds to other funding
    OtherAmtName,
    OtherAmt,
    ISNULL (OtherAmtDate,NULL) AS OtherAmtDate,
	'flood-admin'  AS LastUpdatedBy,
	GETDATE() AS LastUpdatedOn
FROM [FloodMitigation].[floodmp].tblProjectArea PA
WHERE PA.ProjectAreaID = @v_LEGACY_APPLICATION_ID 
AND OtherAmtName IS NOT NULL AND OtherAmt IS NOT NULL --AND OtherAmtDate IS NOT NULL;


			--INSERT INTO [Flood].[FloodApplicationFinanceFund]
			--(
			--	[ApplicationId],
			--	[FundingSourceTypeId],
			--	[Title],
			--	[Amount],
			--	[AwardDate],
			--	[LastUpdatedBy],
		    --	[LastUpdatedOn]
			--)
			--SELECT
			--	@v_NEW_APPLICATION_ID,
			--	1 AS FundingSourceTypeId, -- need to connect with id,amount and date fileds
			--	NULL AS Title,
			--	ISNULL(PA.[FEMAAwardAmt],0) AS AMOUNT,
			--	PA.[FEMAAwardDate],
			--	'flood-admin'  AS LastUpdatedBy,
			--	GETDATE() AS LastUpdatedOn
			--FROM  [FloodMitigation].[floodmp].tblProjectArea AS PA 
			--WHERE PA.ProjectAreaID = @v_LEGACY_APPLICATION_ID;

			INSERT INTO [Flood].[FloodApplicationFinanceLineItems]
			(
				[ApplicationId],
				[PamsPin],
				[ValueEstimate],	
				[LastUpdatedBy], 
				[LastUpdatedOn]
			)
			SELECT TOP 1
				@v_NEW_APPLICATION_ID,
				FP.[PAMS_PIN],
				NULL AS ValueEstimate,
				'flood-admin'  AS LastUpdatedBy,
				GETDATE() AS LastUpdatedOn
			   FROM [FloodMitigation].[floodmp].tblFloodParcel AS FP 
			   WHERE FP.[PAMS_PIN] is not null and FP.ProjectAreaID = @v_LEGACY_APPLICATION_ID;

		    INSERT INTO [Flood].[FloodApplicationFundingAgency]
			(
				   [ApplicationId],
				   [FundingAgencyName],
				   [CurrentStatus],
                   [DateOfApproval]
			)
			SELECT
				@v_NEW_APPLICATION_ID,
                'Funding Agency' AS FundingAgencyName,
                NULL AS CurrentStatus,
                NULL AS DateOfApproval
			FROM [FloodMitigation].[floodmp].[tblProjectArea] PA
			WHERE PA.ProjectAreaID = @v_LEGACY_APPLICATION_ID;

			INSERT INTO [Flood].[FloodApplicationSignatory]
			(
				[ApplicationId],
				[Designation],
				[Title], 
				[SignedOn], 
				[SignatoryType],
				[LastUpdatedBy], 
				[LastUpdatedOn]
			)
			SELECT TOP 1
				@v_NEW_APPLICATION_ID,
				NULL AS Designation,
				NULL AS Title,
				'1900-06-29' AS SignedOn,
				'CERTIFY_APPLICATION' AS SignatoryType,
				'flood-admin'  AS LastUpdatedBy,
				GETDATE() AS LastUpdatedOn
			FROM [FloodMitigation].[floodmp].tblProjectArea AS PA 
			WHERE PA.ProjectAreaID = @v_LEGACY_APPLICATION_ID;

			INSERT INTO [Flood].[FloodApplicationAdminDetails]
			(
				ApplicationId,
				MunicipalResolutionDate,
				MunicipalResolutionNumber,
				FMCPreliminaryApprovalDate,
				FMCPreliminaryNumber,
				BCCPreliminaryApprovalDate,
				BCCPreliminaryNumber,
				ProjectDescription,
				FundingExpirationDate,
				FirstFundingExpirationDate,
				SecondFundingExpirationDate,
				CommissionerMeetingDate,
				FirstCommitteeMeetingDate,
				SecondCommitteeMeetingDate,
				LastUpdatedBy,
				LastUpdatedOn
			)
			SELECT TOP 1
				@v_NEW_APPLICATION_ID,
				PA.MuniResoSupportDate,
				PA.MuniResoSupportNo,
				PA.FMCPrelimDate,
				PA.FMCPrelimNo,
				PA.BCFPrelimDate,
				PA.BCFPrelimNo,
                TRY_CAST (PA.ProjectDescription AS NVARCHAR (max)) AS ProjectDescription,
				PA.FundingExpirationDate,
				PA.FundingExtension_6Mo,
                PA.FundingExtension_12Mo,
				NULL As CommissionerMeetingDate,
				NULL As FirstCommitteeMeetingDate,
				NULL As SecondCommitteeMeetingDate,
				'flood-admin' AS LastUpdatedBy,
				GetDate() AS LastUpdatedOn
			FROM [FloodMitigation].[floodmp].[tblFloodParcel] AS FP 
			LEFT JOIN [FloodMitigation].[floodmp].[tblProjectArea] AS PA ON FP.ProjectAreaID = PA.ProjectAreaID
			WHERE PA.ProjectAreaID = @v_LEGACY_APPLICATION_ID;

			INSERT INTO [Flood].[FloodContacts]
			(
				ApplicationId,
				ContactName,
				Agency,
				Email,
				MainNumber,
				AlternateNumber,
				SelectContact,
				LastUpdatedBy,
				LastUpdatedOn
			)
			SELECT TOP 1
				@v_NEW_APPLICATION_ID,
				M.ContactPerson,
				NULL AS Agency,
				M.Email,
				M.CellPhone,
				M.AltCellPhone,
				NULL AS SelectContact,
				'flood-admin' AS LastUpdatedBy,
				GetDate() AS LastUpdatedOn
			FROM [FloodMitigation].[floodmp].[tblMunicipality] AS M
			LEFT JOIN [FloodMitigation].[floodmp].[tblProjectArea] AS PA ON M.MunicipalID = PA.MunicipalID
			WHERE PA.ProjectAreaID = @v_LEGACY_APPLICATION_ID;

			INSERT INTO [Flood].[FloodApplicationPayment]
			(
				ApplicationId,
				CAFNumber,
				CAFClosed,
				LastUpdatedBy,
				LastUpdatedOn
			)
			SELECT TOP 1
				@v_NEW_APPLICATION_ID,
				ISNULL(CAFNumber, 0) AS CAFNumber,
				CAFClosedYN,
				'flood-admin' AS LastUpdatedBy,
				GetDate() AS LastUpdatedOn
			FROM [FloodMitigation].[floodmp].[tblProjectArea]
			WHERE ProjectAreaID = @v_LEGACY_APPLICATION_ID;

			INSERT INTO [Flood].[FloodApplicationParcel]
			(
				ApplicationId,
				PamsPin,
				StatusId,
				IsLocked,
				IsSubmitted,
				IsApproved
			)
			SELECT 
				@v_NEW_APPLICATION_ID,
				PAMS_PIN,
				CASE
					WHEN ParcelStatus = 'In Review' THEN 2
					WHEN ParcelStatus = 'Pending' THEN 3
					WHEN ParcelStatus = 'Preserved' THEN 5
					WHEN ParcelStatus = 'Rejected' THEN 7
					WHEN ParcelStatus = 'Withdrew' THEN 8
					ELSE 0
				END AS ParcelStatus,
				CASE
					WHEN ParcelStatus = 'Preserved' THEN 1
					WHEN ParcelStatus = 'Rejected' THEN 1
					WHEN ParcelStatus = 'Withdrew' THEN 1
					ELSE 0
				END AS IsLocked,
				CASE
					WHEN ParcelStatus = 'Preserved' THEN 1
					ELSE 0
				END AS IsSubmitted,
				CASE
					WHEN ParcelStatus = 'Preserved' THEN 1
					ELSE 0
				END AS IsApproved
			FROM [FloodMitigation].[floodmp].[tblFloodParcel]
			WHERE ProjectAreaID IS NOT NULL AND PAMS_PIN IS NOT NULL AND ProjectAreaID = @v_LEGACY_APPLICATION_ID;
			
			--======= Application Parcel Tabs - Start =======--

			INSERT INTO [Flood].[FloodLockedParcel]
			    (
				[ApplicationId],
				[PamsPin],
				[AgencyID],
				[Block],
				[Lot],
				[QualificationCode],
				[Latitude],
				[Longitude],
				[StreetNo],
				[StreetAddress],
				[Acreage],
				[OwnersName],
				[OwnersAddress1],
				[OwnersAddress2],
				[OwnersCity],
				[OwnersState],
				[OwnersZipcode],
				[SquareFootage],
				[YearOfConstruction],
				[TotalAssessedValue],
				[LandValue],
				[ImprovementValue],
				[AnnualTaxes],
				[IsValidPamsPin],
				[TargetAreaId],
				[DateOfFLAP],
				[IsElevated],
				[IsActive],
			    [LastUpdatedBy],
				[LastUpdatedOn]
				)
				SELECT
				@v_NEW_APPLICATION_ID,
				TPL.[PAMS_PIN],
				TPL.[MunicipalID],
				TPL.[Block],
				TPL.[Lot],
				TPL.[Qcode],
				TPL.[Latitude],
				TPL.[Longitude],
				TPL.[StreetNumber],
				TPL.[StreetAddress],
				TPL.[Acreage],
				TPL.[Landowner],
				TPL.[AddressLine1],
				TPL.[AddressLine2],
				TPL.[City],
				UPPER(TPL.[State]) AS State,
				TPL.[Zip],
				FP.[SquareFootage],
				TPL.[ConstructionYear],
				TPL.[TotalAssessedValue],
				TPL.[LandValue],
				TPL.[ImprovementValue],
				TPL.[AnnualTaxes],
				ISNULL(FP.[IsValidPamsPin],0) AS IsValidPamsPin,
				FP.[TargetAreaId],
				FP.[DateOfFLAP],
				NULL AS [IsElevated],
				ISNULL(FP.[IsActive],1) AS [IsActive],
				'flood-admin'  AS LastUpdatedBy,
				GETDATE() AS LastUpdatedOn
				FROM [FloodMitigation].[floodmp].[tblFloodParcel] TPL
				left join [Flood].[FloodParcel] FP on FP.PamsPin = tpl.PAMS_PIN
				WHERE PAMS_PIN IS NOT NULL AND [ProjectAreaID] =@v_LEGACY_APPLICATION_ID;

			INSERT INTO [Flood].[FloodParcelProperty]
				(
				[ApplicationId],
				[PamsPin],
				[Priority],
				[ValueEstimate],
				[EstimatedPurchasePrice],
				[BRV],
				[NfipPolicyNo],
				[SourceOfValueEstimate],
				[FirstFloorElevation],
				[StructureType],
				[FoundationType],
				[OccupancyClass],
				[PercentageOfDamage],
				[HasContaminants],
				[IsLowIncomeHousing],
				[HasHistoricSignificance],
				[IsRentalProperty],
				[RentPerMonth],
				[NeedSoftCost],
				[IsPreIrenePropertyOwner],
				[LastUpdatedBy],
				[LastUpdatedOn]
				)
				SELECT
				@v_NEW_APPLICATION_ID,
				FP.[PAMS_PIN],
				CASE
				WHEN FP.IsAlternate = 0 THEN 1
				ELSE 2
				END AS IsAlternate,
				0 AS [ValueEstimate],
				TRY_CAST(FP.[EstPurchasePrice] AS DECIMAL(18,2)) AS EstimatedPurchasePrice, -- Conversion to DECIMAL
				TRY_CAST(FP.[BRV] AS DECIMAL(18,2)) AS BRV, -- Conversion to DECIMAL
				FP.[NFIPPolicy] AS NFIPPolicy,
				--TRY_CAST(FP.EstPurchasePriceSource AS DECIMAL(18,2)) AS SourceOfValueEstimate, -- Conversion to DECIMALFP.EstPurchasePriceSource,
				CASE 
				WHEN FP.EstPurchasePriceSource = 'Appraisal' THEN 1 
				WHEN FP.EstPurchasePriceSource = 'Architect' THEN 2 
				WHEN FP.EstPurchasePriceSource = 'Engineer' THEN 3 
				WHEN FP.EstPurchasePriceSource = 'House Sale' THEN 4
				WHEN FP.EstPurchasePriceSource = 'Housevalues.com' THEN 5 
				WHEN FP.EstPurchasePriceSource = 'Realestate.com' THEN 6 
				WHEN FP.EstPurchasePriceSource = 'Tax Assessor' THEN 7
				WHEN FP.EstPurchasePriceSource = 'Zillow.com' THEN 8
				ELSE NULL
				END AS SourceOfValueEstimate,
				TRY_CAST(FP.[FISFFE] AS DECIMAL(18,2)) AS FirstFloorElevation, -- Conversion to DECIMALFP.[FISFFE],
				CASE
					WHEN FP.StructureType = '1 story - no basement' THEN 1
					WHEN FP.StructureType = '1-2 story - with basement' THEN 2
					WHEN FP.StructureType = '2 story - no basement' THEN 3
					WHEN FP.StructureType = 'mobile home' THEN 4
					WHEN FP.StructureType = 'split level - no basement' THEN 5
					WHEN FP.StructureType = 'split level - with basement' THEN 6
					ELSE 7
                END AS StructureType,
				CASE
					WHEN FP.FoundationType = 'PIER' THEN 1
					WHEN FP.FoundationType = 'PILE' THEN 2
					WHEN FP.FoundationType = 'SLAB' THEN 3
					ELSE 0
				END AS FoundationType,
				CASE
					WHEN FP.[OccupancyClass] = 'RES 1 - SINGLE FAMILY' THEN 1
					WHEN FP.[OccupancyClass] = 'RES 2 - MOBILE HOME' THEN 2
					WHEN FP.[OccupancyClass] = 'RES 3 - MULTIPLE FAMILY DUPLEX 50 UNITS' THEN 4
					ELSE 0
				END AS OccupancyClass,
				NULL AS PercentageOfDamage,
				TRY_CAST(FP.Contaminants AS DECIMAL(18,2)) AS HasContaminants, -- Conversion to DECIMALFP.[Contaminants],
				TRY_CAST(FP.LowIncomeHousing AS DECIMAL(18,2)) AS IsLowIncomeHousing, -- Conversion to DECIMALFP.[LowIncomeHousing],
				TRY_CAST(FP.Historic AS DECIMAL(18,2)) AS Historic, -- Conversion to DECIMALFP.[Historic],
				TRY_CAST(FP.RentalProperty AS DECIMAL(18,2)) AS RentalProperty, -- Conversion to DECIMALFP.[RentalProperty],
				TRY_CAST(FP.RentMos AS DECIMAL(18,2)) AS RentPerMonth, -- Conversion to DECIMALFP.[RentMos],
				0 AS NeedSoftCost,
				TRY_CAST(FP.IsPreIrenePropertyOwner AS DECIMAL(18,2)) AS IsPreIrenePropertyOwner, -- Conversion to DECIMALFP.[IsPreIrenePropertyOwner],
				'flood-admin'  AS LastUpdatedBy,
				GETDATE() AS LastUpdatedOn
				FROM [FloodMitigation].[floodmp].[tblFloodParcel] AS FP 
				WHERE FP.[PAMS_PIN] is not null and FP.[ProjectAreaID] =@v_LEGACY_APPLICATION_ID;


			INSERT INTO [Flood].[FloodParcelTech]
			(
			[ApplicationId],
			[PamsPin],
			[FEMASevereRepetitiveLossList],
			[FEMARepetitiveLossList],
			[IsthepropertywithinthePassaicRiverBasin],
			[IsthepropertywithinFloodway],
			[IsthepropertywithinFloodplain],
			[Claim10Years],
			[TotalOfClaims],
			[BenefitCostRatio],
			[FEMACommunityId],
			[FirmEffectiveDate],
			[FirmPanel],
			[FirmPanelFinal],
			[FloodZoneDesignation],
			[BaseFloodElevation],
			[BaseFloodElevationFinal],
			[RiverId],
			[RiverIdFinal],
			[FisEffectiveDate],
			[FloodProfile],
			[FloodProfileFinal],
			[FloodSource],
			[FirstFloodElevation],
			[FirstFloodElevationFinal],
			[StreambedElevation],
			[StreambedElevationFinal],
			[ElevationBeforeMitigation],
			[ElevationBeforeMitigationFinal],
			[FloodType],
			[TenPercent],
			[TwoPercent],
			[OnePercent],
			[PointOnePercent],	
			[LastUpdatedBy],
			[LastUpdatedOn]
			)
			SELECT
			@v_NEW_APPLICATION_ID,
			FP.[PAMS_PIN],
			FP.SRL,
			FP.RL,
			FP.PassaicRiverBasin,
			FP.[Floodway],
			FP.[Floodplain],
			FP.Claims,
			FP.TotalClaimsAmt,
			FP.[BCR],
			FP.[FEMACommunityID],
			FP.[FIRMEffectiveDate],
			FP.[FIRMPanel],
			NULL AS FirmPanelFinal,
			FP.[FIRMFloodZone],
			FP.FIRMGeneralBFE,
			NULL AS BaseFloodElevationFinal,
			FP.[RiverID],
			NULL AS RiverIdFinal,
			FP.[FISEffectiveDate],
			FP.[FISFloodProfile],
			NULL AS FloodProfileFinal,
			FP.[FISFloodSource],
			FP.FISFFE,
			NULL AS FirstFloodElevationFinal,
			FP.[FISStreambedElevation],
			NULL AS StreambedElevationFinal,
			FP.[FISElevationBeforeMitigation],
			NULL AS ElevationBeforeMitigationFinal,
			CASE 
		    WHEN FP.[FISFloodType] = 'Mud Flow' THEN 'Mud Flow'
			WHEN FP.[FISFloodType] = 'Overland Flow' THEN 'Overland Flow'
			WHEN FP.[FISFloodType] = 'Slope Failure' THEN 'Slope Failure'
			END AS [FISFloodType],
			FP.[Discharge10],
			FP.[Discharge2],
			FP.[Discharge1],
			FP.[DischargePoint1],
			'flood-admin'  AS LastUpdatedBy,
			GETDATE() AS LastUpdatedOn
			FROM [FloodMitigation].[floodmp].[tblFloodParcel] AS FP 
			WHERE FP.[PAMS_PIN] IS NOT NULL AND FP.[ProjectAreaID] = @v_LEGACY_APPLICATION_ID;

			INSERT INTO [Flood].[FloodParcelFinance]
			(
			[ApplicationId],
			[PamsPin],
			[EstimatePurchasePrice],
			[AdditionalSoftCostEstimate],
			[AppraisedValue],
			[AMV],
			[TotalFEMABenifits],
			[DOBAffidavitType],
			[DOBAffidavitAmt],
			[HardCostFMPAmt],
			[SoftCostFMPAmt],
			[AppraisersFee],
			[SurveyorsFee],
			[LastUpdatedBy],
			[LastUpdatedOn]
			)
			SELECT
			@v_NEW_APPLICATION_ID,
			FP.[PAMS_PIN],
			ISNULL(FP.[EstPurchasePrice],0) AS EstimatePurchasePrice,
			0 AS AdditionalSoftCostEstimate,
			FP.[AppraisedValue],
			FP.[AMV],
			NULL AS TotalFEMABenifits,
			NULL AS DOBAffidavitType,
			FP.[DOBAmount],
			FP.[HardCostReimbursed],
			FP.[SoftCostReimbursed],
			FP.[AppraisersFee],
			FP.[SurveyorsFee],
			'flood-admin'  AS LastUpdatedBy,
			GETDATE() AS LastUpdatedOn
			FROM [FloodMitigation].[floodmp].[tblFloodParcel] AS FP 
			WHERE FP.[PAMS_PIN] IS NOT NULL AND FP.[ProjectAreaID] = @v_LEGACY_APPLICATION_ID;

			Insert into [Flood].[FloodParcelSurvey]
				(
				ApplicationId,
				PamsPin,
				Surveyor,
				SurveyDate,
				LastRevision,
				DateCorrected,
				LastUpdatedBy,
				LastUpdatedOn
				)
				SELECT
				@v_NEW_APPLICATION_ID,
				FP.PAMS_PIN,
				NULL as Surveyor,
				NULL as SurveyDate,
				NULL as LastRevision,
				NULL as DateCorrected,
				'flood-admin' AS LastUpdatedBy,
				GetDate() AS LastUpdatedOn
				FROM [FloodMitigation].[floodmp].[tblFloodParcel] AS FP
				WHERE FP.[PAMS_PIN] IS NOT NULL AND FP.[ProjectAreaID] = @v_LEGACY_APPLICATION_ID;

				Insert into [Flood].[FloodParcelAdminDetails]
					(
					ApplicationId,
					PamsPin,
					FMCFinalApprovalDate,
					FMCFinalNumber,
					BCCFinalApprovalDate,
					BCCFinalNumber,
					MunicipalPurchaseDate,
					MunicipalPurchaseNumber,
					GrantAgreementDate,
					GrantAgreementExpirationDate,
					DOBDocumentsMissingDate,
					DueDiligenceDocumentsMissingDate,
					ScheduleClosingDate,
					SoftCostReimbursementRequestDate,
					FMCSoftCostReimbApprovalDate,
					FMCSoftCostReimbApprovalNumber,
					BCCSoftCostReimbApprovalDate,         											
					BCCSoftCostReimbApprovalNumber,        									
					DoesHomeOwnerHaveNFIPInsurance,        												
					IsDEPInvolved,                        												
					IsPARRequestedbyFunder,              												
					LastUpdatedBy,																
					LastUpdatedOn
					)
					SELECT
					@v_NEW_APPLICATION_ID,
					FP.PAMS_PIN,
					FP.FMCFinalDate,
					FP.FMCFinal#,
					FP.BCFFinalDate,
					FP.BCFFinal#,
					FP.MuniOrdPurchaseDate,
					FP.MuniOrdPurchase#,
					FP.GrantAgreement,
					FP.GrantAgreementExp,
					NULL AS DOBDocumentsMissingDate,
					NULL AS DueDiligenceDocumentsMissingDate,
					NULL AS ScheduleClosingDate,
					NULL AS SoftCostReimbursementRequestDate,
					NULL AS FMCSoftCostReimbApprovalDate,
					NULL AS FMCSoftCostReimbApprovalNumber,
					NULL AS BCCSoftCostReimbApprovalDate,         											
					NULL AS BCCSoftCostReimbApprovalNumber,        									
					NULL AS DoesHomeOwnerHaveNFIPInsurance,        												
					NULL AS IsDEPInvolved,                        												
					NULL AS IsPARRequestedbyFunder,
					'flood-admin' AS LastUpdatedBy,
					GetDate() AS LastUpdatedOn
					from [FloodMitigation].[floodmp].[tblFloodParcel] AS FP 
					WHERE FP.PAMS_PIN IS NOT NULL AND FP.[ProjectAreaID] = @v_LEGACY_APPLICATION_ID;


					Insert Into [Flood].[FloodParcelPayment]
						(
						ApplicationId,
						PamsPin,
						HardCostPaymentTypeId,
						HardCostPaymentDate,
						HardCostPaymentStatusId,
						SoftCostPaymentTypeId,
						SoftCostPaymentDate,
						SoftCostPaymentStatusId,
						LastUpdatedBy,																
						LastUpdatedOn
						)
						SELECT
						@v_NEW_APPLICATION_ID,
						FP.PAMS_PIN,
						0 AS HardCostPaymentTypeId,
						HardCostReimbursementDate,
						0 AS HardCostPaymentStatusId,
						0 AS SoftCostPaymentTypeId,
						SoftCostReimbursementDate,
						0 AS SoftCostPaymentStatusId,
						'flood-admin' AS LastUpdatedBy,
						GetDate() AS LastUpdatedOn
						from [FloodMitigation].[floodmp].[tblFloodParcel] AS FP
						WHERE FP.[PAMS_PIN] IS NOT NULL AND FP.[ProjectAreaID] = @v_LEGACY_APPLICATION_ID;

					 Insert Into [Flood].[FloodParcelTracking]
					(
						ApplicationId,
						PamsPin,
						ClosingDate,
						DeedBook,
						DeedPage,
						DeedDate,
						DemolitionDate,
						SiteVisitConfirmDate,
						PublicPark,
						RainGarden,
						CommunityGarden,
						ActiveRecreation,
						NaturalHabitat,
						LastUpdatedBy,																
						LastUpdatedOn
						)
						SELECT
						@v_NEW_APPLICATION_ID,
						FP.PAMS_PIN,
						FP.ClosingDate,
						FP.DeedBook,
						FP.DeedPage,
						FP.DeedDate,
						FP.DemolitionDate,
						FP.SiteVisitConfirmDemo,
						FP.UsePublicPark,
						FP.UseRain,
						FP.UseGarden,
						FP.UseActive,
						FP.UseNatural,
						'flood-admin' AS LastUpdatedBy,
						GetDate() AS LastUpdatedOn
						FROM [FloodMitigation].[floodmp].[tblFloodParcel] AS FP
						WHERE FP.PAMS_PIN IS NOT NULL AND FP.[ProjectAreaID] = @v_LEGACY_APPLICATION_ID;


			--======= Application Parcel Tabs - End =======--
			
			SET @v_LEGACY_RECORD_INDEX = @v_LEGACY_RECORD_INDEX + 1;

		END;

		DROP TABLE IF EXISTS [#LegacyApplicationIds];

	--==============================================================================================================--
	--SELECT 1/0;
	COMMIT;
	print 'SUCCESS';
END TRY
BEGIN CATCH
    DECLARE @ErrorMessage NVARCHAR(4000); 

	SET   @ErrorMessage = ERROR_MESSAGE();
	ROLLBACK;

	SELECT @ErrorMessage;	
END CATCH
