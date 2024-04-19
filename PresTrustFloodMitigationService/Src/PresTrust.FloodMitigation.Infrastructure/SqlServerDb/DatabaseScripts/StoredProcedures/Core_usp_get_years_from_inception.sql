IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[core].[usp_get_years_from_inception]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE IF EXISTS [core].[usp_get_years_from_inception]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [core].[usp_get_years_from_inception]
(
	@p_InceptionYear INT
)
AS

--<flowerbox>
/***********************************************************************************
-- <summary>
-- This sproc populates list of years based on inception year
-- </summary>
--
-- <remarks>
- </remarks>
--
-- <syntax>
	
	EXEC	[core].[usp_get_years_from_inception]
												
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

	WITH CTE AS (
		SELECT @p_InceptionYear AS [YEAR]
		UNION ALL
		SELECT [YEAR] + 1 FROM CTE WHERE [YEAR] < YEAR(GETDATE())
	)
	SELECT [YEAR] FROM CTE;

	-------------------------------------------------
	--** Dispose 
	-------------------------------------------------
	DISPOSE:
 
END

GO
