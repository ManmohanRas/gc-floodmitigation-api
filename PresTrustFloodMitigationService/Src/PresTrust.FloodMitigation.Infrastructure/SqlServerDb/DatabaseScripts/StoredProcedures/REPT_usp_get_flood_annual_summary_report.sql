IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[rept].[usp_get_flood_annual_summary_report]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE IF EXISTS [rept].[usp_get_flood_annual_summary_report]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [rept].[usp_get_flood_annual_summary_report]

AS

--<flowerbox>
/***********************************************************************************
-- <summary>
-- This sproc populates updates flood dashboard level 3 details
-- </summary>
--
-- <remarks>
- </remarks>
--
-- <syntax>
	
	EXEC	[rept].[usp_get_flood_annual_summary_report]
												
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

	SELECT
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
		[ExpiringWithInAYear]
	FROM [rept].[FLOODAnnualSummaryReport]
	ORDER BY [FundingYear];

	-------------------------------------------------
	--** Dispose 
	-------------------------------------------------
	DISPOSE:
 
END

GO
