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
				NULL AS NatlDisasterId,
				NULL AS NatlDisasterName,
				NULL AS NatlDisasterYear,
				NULL AS NatlDisasterMonth,
				FP.[LOI],
				FP.[LOIApproved],
				FP.[LOIDate],
				FP.[FEMAapplication],
				FP.[FEMAApprovedDate],
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
				FP.[ProgramMatchPercent],
				'flood-admin'  AS LastUpdatedBy,
				GETDATE() AS LastUpdatedOn
			FROM [FloodMitigation].[floodmp].tblFloodParcel AS FP 
			LEFT JOIN [FloodMitigation].[floodmp].tblProjectArea AS PA ON (FP.ProjectAreaID = PA.ProjectAreaID)
			WHERE PA.ProjectAreaID = @v_LEGACY_APPLICATION_ID;

			INSERT INTO [Flood].[FloodApplicationFinanceFund]
			(
				[ApplicationId],
				[FundingSourceTypeId],
				[Title],
				[Amount],
				[AwardDate],
				[LastUpdatedBy],
				[LastUpdatedOn]
			)
			SELECT TOP 1
				@v_NEW_APPLICATION_ID,
				NULL AS FundingSourceTypeId,
				NULL AS Title,
				PA.[FEMAAwardDate],
				PA.[FEMAAwardAmt],
				'flood-admin'  AS LastUpdatedBy,
				GETDATE() AS LastUpdatedOn
			FROM  [FloodMitigation].[floodmp].tblProjectArea AS PA 
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
				'1900-01-01' AS SignedOn,
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
				FP.MuniResoSupportDate,
				FP.MuniResoSupport#,
				FP.FMCPrelimDate,
				FP.FMCPrelim#,
				FP.BCFPrelimDate,
				FP.BCFPrelim#,
				PA.ProjectDescription,
				PA.FundingExpirationDate,
				PA.FundingExtension_6Mo,
				PA.FundingExtension_12Mo,
				NULL As CommissionerMeetingDate,
				NULL As FirstCommitteeMeetingDate,
				NULL As SecondCommitteeMeetingDate,
				1 AS CreatedByProgramAdmin,
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
			SELECT 
				@v_NEW_APPLICATION_ID,
				M.ContactPerson,
				NULL AS Agency,
				M.Email,
				M.CellPhone,
				M.AltCellPhone,
				NULL AS SelectContact,
				1 AS CreatedByProgramAdmin,
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
				CAFNumber,
				CAFClosedYN,
				1 AS CreatedByProgramAdmin,
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
