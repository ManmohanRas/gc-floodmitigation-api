IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[rept].[usp_update_flood_annual_summary_report]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE IF EXISTS [rept].[usp_update_flood_annual_summary_report]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [rept].[usp_update_flood_annual_summary_report]

AS

--<flowerbox>
/***********************************************************************************
-- <summary>
-- This sproc populates updates flood dashboard level 1 details
-- </summary>
--
-- <remarks>
- </remarks>
--
-- <syntax>
	
	EXEC	[rept].[usp_update_flood_annual_summary_report]
												
-- </syntax>
--
-- <author> 
-- <name>
-- SAI CHARAN KODATI
-- </name>
-- </author>
-- 
-- <template version="1.0" name="PresTrustSprocTemplate.sql" />
************************************************************************************
-- <floodorylog>
--  <log build="" revision="1.0" date="2023/01/20" GitWorkItem="">
--      Created 
--  </log>
--  <log build="" revision="1.0" date="" GitWorkItem="" DefectID="" email="">
--		 
--  </log>
-- </floodorylog>
***********************************************************************************/
--</flowerbox>

SET NOCOUNT ON;
BEGIN

	-------------------------------------------------
	--** DECLARE Variables
	-------------------------------------------------
	DECLARATION:
	
	-------------------------------------------------
	--** Initialize Standard Variables
	-------------------------------------------------
	INIT_ROUTINE:
		 
	-------------------------------------------------
	--** Main Procedure Logic
	-------------------------------------------------
	MAIN_ROUTINE:
	
	----------------------------------------------------------------------------------------------
	--** Populate BatchJobDetail for the given Job_Code.
	----------------------------------------------------------------------------------------------

	DROP TABLE IF EXISTS #temp_FLOODAnnualSummaryReport;
	CREATE TABLE #temp_FLOODAnnualSummaryReport
	(
			[AgencyId]								[int]					NOT NULL,
			[FundingYear]							[int]					NOT NULL,
			[FundsEncumbered]		  			    [decimal](18, 2)		NULL,
			[FundsReimbursed]						[decimal](18, 2)		NULL,
			[PA_Submitted]						    [int]         	    	NULL,
			[PA_Active]					 	        [int]      				NULL,
			[PA_Rejected]							[int]					NULL,
			[PA_Withdrawn]							[int]					NULL,
			[PA_Closed]								[int]					NULL,
			[P_OfHomes]								[int]					NULL,
			[P_Pending]								[int]					NULL,
			[P_Preserved]							[int]					NULL,
			[P_Withdrawn]							[int]					NULL,
			[P_Rejected]							[int]					NULL,
			[P_PropertyExpired]						[int]					NULL,
			[P_PropertyGrantExpired]				[int]					NULL,
			[HardCosts]						        [decimal](18, 2)		NULL,
			[SoftCosts]						        [decimal](18, 2)		NULL,
			[CountyExpenses]						[decimal](18, 2)		NULL,
			[TotalAllocated]						[decimal](18, 2)		NULL,
			[FundsSpent]							[decimal](18, 2)		NULL,
			[ExpiringWithInAYear]					[int]					NULL
	)

	DECLARE             @v_AGENCY_INDEX		AS INTEGER, 
						@v_AGENCY_COUNT		AS INTEGER,
						@v_AGENCY_ID		AS INTEGER,
						@v_YearBegin		AS INTEGER,
						@v_YearEnd			AS INTEGER;

	SET @v_AGENCY_INDEX = 1;
	SET @v_AGENCY_COUNT = 0;
	SET    @v_AGENCY_ID = 0;

	DROP TABLE IF EXISTS #temp_AgencyIds
	CREATE TABLE #temp_AgencyIds
	(
		[RowId]                                     [integer]            IDENTITY(1,1) NOT NULL,
		[AgencyId]									[integer]            NOT NULL,
		[AgencyName]								nvarchar(200)		 NOT NULL
	)
	INSERT INTO #temp_AgencyIds (AgencyId, AgencyName)	
	SELECT DISTINCT AgencyId, AgencyName  FROM [Core].[View_AgencyEntities_FLOOD];

	SET @v_AGENCY_COUNT = @@ROWCOUNT;

	WHILE(@v_AGENCY_INDEX <= @v_AGENCY_COUNT)
	BEGIN
			SET @v_YearBegin = 2012; -- Inception Year
			SET @v_YearEnd = YEAR(GETDATE()); -- Current Year

			SELECT        @v_AGENCY_ID = AgencyId
			FROM          #temp_AgencyIds
			WHERE         [RowId] = @v_AGENCY_INDEX

			DROP TABLE IF EXISTS #temp_AgencyApplications
			CREATE TABLE #temp_AgencyApplications
			(
	   			[ApplicationId]                             [integer]            NOT NULL,
	   			[AgencyId]									[integer]            NOT NULL,
				[ApplicationTypeId]							[integer]			 NOT NULL,
				[ApplicationSubTypeId]						[integer]			 NOT NULL,
	   			[StatusId]									[integer]            NOT NULL
			);
			INSERT INTO #temp_AgencyApplications
			SELECT
				[Id],
				[AgencyId],
				[ApplicationTypeId],
				[ApplicationSubTypeId],
				[StatusId]
			FROM Flood.FloodApplication 
			WHERE IsActive = 1 AND AgencyId = @v_AGENCY_ID AND StatusId IN (4,5,6,7,8,9);

			WHILE(@v_YearBegin <= @v_YearEnd)
			BEGIN
				DECLARE @v_EncumberedApplicationIds NVARCHAR(MAX),
						@v_ReimbursedHardCostApplicationIds NVARCHAR(MAX),
						@v_ReimbursedSoftCostApplicationIds NVARCHAR(MAX),
						@v_SubmittedApplicationIds NVARCHAR(MAX);
				SELECT
					@v_EncumberedApplicationIds = STRING_AGG(A.ApplicationId, ',')
				FROM #temp_AgencyApplications A
				JOIN [Flood].[FloodApplicationStatusLog] ASL ON A.[ApplicationId] = ASL.[ApplicationId]
				WHERE A.[StatusId] IN (6,7,8,9) AND ASL.[StatusId] = 6 AND YEAR(ASL.[StatusDate]) = @v_YearBegin;
				SELECT
					@v_ReimbursedHardCostApplicationIds = STRING_AGG(A.ApplicationId, ',')
				FROM #temp_AgencyApplications A
				JOIN [Flood].[FloodParcelPayment] FP ON A.[ApplicationId] = FP.[ApplicationId]
				WHERE A.[StatusId] IN (6,7,8,9) AND FP.HardCostPaymentStatusId = 1 AND YEAR(FP.HardCostPaymentDate) = @v_YearBegin;
				SELECT
					@v_ReimbursedSoftCostApplicationIds = STRING_AGG(A.ApplicationId, ',')
				FROM #temp_AgencyApplications A
				JOIN [Flood].[FloodParcelPayment] FP ON A.[ApplicationId] = FP.[ApplicationId]
				WHERE A.[StatusId] IN (6,7,8,9) AND FP.SoftCostPaymentStatusId = 1 AND YEAR(FP.SoftCostPaymentDate) = @v_YearBegin;
				SELECT
					@v_SubmittedApplicationIds = STRING_AGG(A.ApplicationId, ',')
				FROM #temp_AgencyApplications A
				JOIN [Flood].[FloodApplicationStatusLog] ASL ON A.[ApplicationId] = ASL.[ApplicationId] AND A.[StatusId] = ASL.[StatusId]
				WHERE A.[StatusId] = 4 AND YEAR(ASL.[StatusDate]) = @v_YearBegin;
			
				DECLARE @v_FundsEncumbered [decimal](18,2),
						@v_FundsReimbursed [decimal](18,2),
						@v_PA_Submitted [integer],
						@v_PA_Active [integer],
						@v_PA_Rejected [integer],
						@v_PA_Withdrawn [integer],
						@v_PA_Closed [integer],
						@v_ExpiringWithInAYear [integer],
						@v_P_OfHomes [integer],
						@v_P_Pending [integer],
						@v_P_Preserved [integer],
						@v_P_Withdrawn [integer],
						@v_P_Rejected [integer],
						@v_P_PropertyExpired [integer],
						@v_P_PropertyGrantExpired [integer],
						@v_HardCosts [decimal](18,2),
						@v_SoftCosts [decimal](18,2),
						@v_CountyExpenses [decimal](18,2),
						@v_TotalAllocated [decimal](18,2),
						@v_FundsSpent [decimal](18,2);
				SELECT
					@v_FundsEncumbered = (SUM(PF.EstimatePurchasePrice * AF.MatchPercent / 100) + (SUM(PF.EstimatePurchasePrice * AF.MatchPercent / 100)) / 4 + SUM(PF.AdditionalSoftCostEstimate))
				FROM 
					Flood.FloodParcelFinance PF
				JOIN 
					Flood.FloodApplicationFinance AF ON PF.ApplicationId = AF.ApplicationId
				WHERE PF.ApplicationId IN (SELECT DISTINCT VALUE FROM STRING_SPLIT(@v_EncumberedApplicationIds, ','));
				SELECT
					@v_HardCosts = SUM(HardCostFMPAmt) FROM Flood.FloodParcelFinance
				WHERE ApplicationId IN (SELECT DISTINCT VALUE FROM STRING_SPLIT(@v_ReimbursedHardCostApplicationIds, ','));
				SELECT
					@v_SoftCosts = SUM(SoftCostFMPAmt) FROM Flood.FloodParcelFinance
				WHERE ApplicationId IN (SELECT DISTINCT VALUE FROM STRING_SPLIT(@v_ReimbursedSoftCostApplicationIds, ','));
				SELECT
					@v_FundsReimbursed = @v_HardCosts + @v_SoftCosts;
				SELECT
					@v_CountyExpenses = SUM(ExpenseAmount)
				FROM Flood.FloodProgramExpenses
				WHERE ExpenseYear = @v_YearBegin;
				SELECT
					@v_PA_Submitted = COUNT(DISTINCT VALUE)
				FROM STRING_SPLIT(@v_SubmittedApplicationIds, ',');
				SELECT
					@v_PA_Active = COUNT(DISTINCT A.ApplicationId)
				FROM #temp_AgencyApplications A
				JOIN [Flood].[FloodApplicationStatusLog] ASL ON A.[ApplicationId] = ASL.[ApplicationId] AND A.[StatusId] = ASL.[StatusId]
				WHERE A.[StatusId] = 6 AND YEAR(ASL.[StatusDate]) = @v_YearBegin;
				SELECT
					@v_PA_Rejected = COUNT(DISTINCT A.ApplicationId)
				FROM #temp_AgencyApplications A
				JOIN [Flood].[FloodApplicationStatusLog] ASL ON A.[ApplicationId] = ASL.[ApplicationId] AND A.[StatusId] = ASL.[StatusId]
				WHERE A.[StatusId] = 8 AND YEAR(ASL.[StatusDate]) = @v_YearBegin;
				SELECT
					@v_PA_Withdrawn = COUNT(DISTINCT A.ApplicationId)
				FROM #temp_AgencyApplications A
				JOIN [Flood].[FloodApplicationStatusLog] ASL ON A.[ApplicationId] = ASL.[ApplicationId] AND A.[StatusId] = ASL.[StatusId]
				WHERE A.[StatusId] = 9 AND YEAR(ASL.[StatusDate]) = @v_YearBegin;
				SELECT
					@v_PA_Closed = COUNT(DISTINCT A.ApplicationId)
				FROM #temp_AgencyApplications A
				JOIN [Flood].[FloodApplicationStatusLog] ASL ON A.[ApplicationId] = ASL.[ApplicationId] AND A.[StatusId] = ASL.[StatusId]
				WHERE A.[StatusId] = 7 AND YEAR(ASL.[StatusDate]) = @v_YearBegin;
				SELECT DISTINCT
					@v_P_OfHomes = COUNT(DISTINCT AP.PamsPin)
				FROM #temp_AgencyApplications A
				JOIN [Flood].[FloodApplicationParcel] AP ON A.[ApplicationId] = AP.[ApplicationId]
				JOIN [Flood].[FloodParcelStatusLog] PSL ON AP.[ApplicationId] = PSL.[ApplicationId] AND AP.[PamsPin] = PSL.[PamsPin]
				WHERE PSL.[StatusId] = 1 AND YEAR(PSL.[StatusDate]) = @v_YearBegin;
				SELECT DISTINCT
					@v_P_Pending = COUNT(DISTINCT AP.PamsPin)
				FROM #temp_AgencyApplications A
				JOIN [Flood].[FloodApplicationParcel] AP ON A.[ApplicationId] = AP.[ApplicationId]
				JOIN [Flood].[FloodParcelStatusLog] PSL ON AP.[ApplicationId] = PSL.[ApplicationId] AND AP.[PamsPin] = PSL.[PamsPin]
				WHERE PSL.[StatusId] = 3 AND YEAR(PSL.[StatusDate]) = @v_YearBegin;
				SELECT DISTINCT
					@v_P_Preserved = COUNT(DISTINCT AP.PamsPin)
				FROM #temp_AgencyApplications A
				JOIN [Flood].[FloodApplicationParcel] AP ON A.[ApplicationId] = AP.[ApplicationId]
				JOIN [Flood].[FloodParcelStatusLog] PSL ON AP.[ApplicationId] = PSL.[ApplicationId] AND AP.[PamsPin] = PSL.[PamsPin]
				WHERE PSL.[StatusId] = 5 AND YEAR(PSL.[StatusDate]) = @v_YearBegin;
				SELECT DISTINCT
					@v_P_Withdrawn = COUNT(DISTINCT AP.PamsPin)
				FROM #temp_AgencyApplications A
				JOIN [Flood].[FloodApplicationParcel] AP ON A.[ApplicationId] = AP.[ApplicationId]
				JOIN [Flood].[FloodParcelStatusLog] PSL ON AP.[ApplicationId] = PSL.[ApplicationId] AND AP.[PamsPin] = PSL.[PamsPin]
				WHERE PSL.[StatusId] = 8 AND YEAR(PSL.[StatusDate]) = @v_YearBegin;
				SELECT DISTINCT
					@v_P_Rejected = COUNT(DISTINCT AP.PamsPin)
				FROM #temp_AgencyApplications A
				JOIN [Flood].[FloodApplicationParcel] AP ON A.[ApplicationId] = AP.[ApplicationId]
				JOIN [Flood].[FloodParcelStatusLog] PSL ON AP.[ApplicationId] = PSL.[ApplicationId] AND AP.[PamsPin] = PSL.[PamsPin]
				WHERE PSL.[StatusId] = 7 AND YEAR(PSL.[StatusDate]) = @v_YearBegin;
				SELECT DISTINCT
					@v_P_PropertyExpired = COUNT(DISTINCT AP.PamsPin)
				FROM #temp_AgencyApplications A
				JOIN [Flood].[FloodApplicationParcel] AP ON A.[ApplicationId] = AP.[ApplicationId]
				JOIN [Flood].[FloodParcelStatusLog] PSL ON AP.[ApplicationId] = PSL.[ApplicationId] AND AP.[PamsPin] = PSL.[PamsPin]
				WHERE PSL.[StatusId] = 9 AND YEAR(PSL.[StatusDate]) = @v_YearBegin;
				SELECT DISTINCT
					@v_P_PropertyGrantExpired = COUNT(DISTINCT AP.PamsPin)
				FROM #temp_AgencyApplications A
				JOIN [Flood].[FloodApplicationParcel] AP ON A.[ApplicationId] = AP.[ApplicationId]
				JOIN [Flood].[FloodParcelStatusLog] PSL ON AP.[ApplicationId] = PSL.[ApplicationId] AND AP.[PamsPin] = PSL.[PamsPin]
				WHERE PSL.[StatusId] = 6 AND YEAR(PSL.[StatusDate]) = @v_YearBegin;
				SELECT
					@v_TotalAllocated = SUM(AllocationAmount)
				FROM [Flood].[FloodAnnualFunding] WHERE AllocationYear <= @v_YearBegin;
				SELECT
					@v_FundsSpent = SUM(ExpenseAmount)
				FROM [Flood].[FloodProgramExpenses] WHERE ExpenseYear <= @v_YearBegin;
				SELECT
					@v_ExpiringWithInAYear = COUNT(DISTINCT A.ApplicationId)
				FROM #temp_AgencyApplications A
				JOIN (
					SELECT
						ApplicationId,
						CASE
							WHEN SecondFundingExpirationDate IS NOT NULL
							THEN SecondFundingExpirationDate
							WHEN FirstFundingExpirationDate IS NOT NULL
							THEN FirstFundingExpirationDate
							WHEN FundingExpirationDate IS NOT NULL
							THEN FundingExpirationDate
							ELSE NULL
						END AS ExpirationDate
					FROM [Flood].[FloodApplicationAdminDetails]
				) AD ON A.[ApplicationId] = AD.[ApplicationId]
				WHERE CAST(AD.ExpirationDate AS DATE) >= CAST(GETDATE() AS DATE)
				AND CAST(AD.ExpirationDate AS DATE) < CAST(DATEADD(YEAR, 1, GETDATE()) AS DATE);

				INSERT INTO #temp_FLOODAnnualSummaryReport
				(
					[AgencyId],
					[FundingYear],
					[FundsEncumbered],
					[FundsReimbursed],
					[PA_Submitted],
					[PA_Active],
					[PA_Rejected],
					[PA_Withdrawn],
					[PA_Closed],
					[ExpiringWithInAYear],
					[P_OfHomes],
					[P_Pending],
					[P_Preserved],
					[P_Withdrawn],
					[P_Rejected],
					[P_PropertyExpired],
					[P_PropertyGrantExpired],
					[HardCosts],
					[SoftCosts],
					[CountyExpenses],
					[TotalAllocated],
					[FundsSpent]
				)
				VALUES
				(
					@v_AGENCY_ID,
					@v_YearBegin,
					@v_FundsEncumbered,
					@v_FundsReimbursed,
					@v_PA_Submitted,
					@v_PA_Active,
					@v_PA_Rejected,
					@v_PA_Withdrawn,
					@v_PA_Closed,
					@v_ExpiringWithInAYear,
					@v_P_OfHomes,
					@v_P_Pending,
					@v_P_Preserved,
					@v_P_Withdrawn,
					@v_P_Rejected,
					@v_P_PropertyExpired,
					@v_P_PropertyGrantExpired,
					@v_HardCosts,
					@v_SoftCosts,
					@v_CountyExpenses,
					@v_TotalAllocated,
					@v_FundsSpent
				);
		  
				SET @v_YearBegin = @v_YearBegin + 1;
			END;
		
			SET    @v_AGENCY_INDEX = @v_AGENCY_INDEX + 1;
	END;

	SELECT * FROM #temp_FLOODAnnualSummaryReport; 

	DELETE FROM [rept].[FLOODAnnualSummaryReport];

	INSERT INTO [rept].[FLOODAnnualSummaryReport]
	(	
		[AgencyId],
		[FundingYear],
		[FundsEncumbered],
		[FundsReimbursed],
		[PA_Submitted],
		[PA_Active],
		[PA_Rejected],
		[PA_Withdrawn],
		[PA_Closed],
		[P_OfHomes],
		[P_Pending],
		[P_Preserved],
		[P_Withdrawn],
		[P_Rejected],
		[P_PropertyExpired],
		[P_PropertyGrantExpired],
		[HardCosts],
		[SoftCosts],
		[CountyExpenses],
		[TotalAllocated],
		[FundsSpent],
		[ExpiringWithInAYear],
		[LastUpdatedBy],
		[LastUpdatedOn]
	)
	SELECT
		tASR.[AgencyId],
		tASR.[FundingYear],
		ISNULL(tASR.[FundsEncumbered], 0),
		ISNULL(tASR.[FundsReimbursed], 0),
		ISNULL(tASR.[PA_Submitted], 0),
		ISNULL(tASR.[PA_Active], 0),
		ISNULL(tASR.[P_Rejected], 0),
		ISNULL(tASR.[PA_Withdrawn], 0),
		ISNULL(tASR.[PA_Closed], 0),
		ISNULL(tASR.[P_OfHomes], 0),
		ISNULL(tASR.[P_Pending], 0),
		ISNULL(tASR.[P_Preserved], 0),
		ISNULL(tASR.[P_Withdrawn], 0),
		ISNULL(tASR.[P_Rejected], 0),
		ISNULL(tASR.[P_PropertyExpired], 0),
		ISNULL(tASR.[P_PropertyGrantExpired], 0),
		ISNULL(tASR.[HardCosts], 0),
		ISNULL(tASR.[SoftCosts], 0),
		ISNULL(tASR.[CountyExpenses], 0),
		ISNULL(tASR.[TotalAllocated], 0),
		ISNULL(tASR.[FundsSpent], 0),
		ISNULL(tASR.[ExpiringWithInAYear], 0),
		'SQL JOB' AS [LastUpdatedBy],
		SYSDATETIME() AS [LastUpdatedOn]
	FROM #temp_FLOODAnnualSummaryReport tASR

	DROP TABLE IF EXISTS #temp_AgencyIds;
	DROP TABLE IF EXISTS #temp_AgencyApplications;
	DROP TABLE IF EXISTS #temp_FLOODAnnualSummaryReport;

 
	-------------------------------------------------
	--** Dispose 
	-------------------------------------------------
	DISPOSE:
 
END

GO
