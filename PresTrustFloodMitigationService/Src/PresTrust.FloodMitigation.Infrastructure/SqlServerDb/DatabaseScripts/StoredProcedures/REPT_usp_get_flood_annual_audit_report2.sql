USE [PresTrustTemp]
GO

/****** Object:  StoredProcedure [rept].[usp_get_flood_annual_audit_report2]    Script Date: 13-12-2024 12:38:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [rept].[usp_get_flood_annual_audit_report2]
(@p_SelectedYear INT)
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
	
	EXEC	[rept].[usp_get_flood_annual_audit_report2]
												
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


--DECLARE @p_SelectedYear AS INT;
                --SET       @p_SelectedYear = 2024;
 
                DECLARE @v_InceptionYear AS INT;    
                SET       @v_InceptionYear = 2012;
                
                 
                 
 
                -- List of previous project-areas holding ACTIVE, WITHDRAWN & Closed status from the year of inception to the current/selected  year 
                DROP TABLE IF EXISTS #ProjAreaEstimatesAndPayments;
                CREATE TABLE #ProjAreaEstimatesAndPayments
                (
                                ApplicationId                            INT,
                                EstimatePurchasePrice                    DECIMAL(18,2),  
                                AdditionalSoftCostEstimate               DECIMAL(18,2),  
                                HardCostFMPAmt                           DECIMAL(18,2),  
                                SoftCostFMPAmt                           DECIMAL(18,2),  
                                HardCostPaymentYear                      INT,
                                SoftCostPaymentYear                      INT,
								[Priority]                               INT
                );
                INSERT INTO #ProjAreaEstimatesAndPayments(ApplicationId, EstimatePurchasePrice, AdditionalSoftCostEstimate, 
				HardCostFMPAmt, SoftCostFMPAmt, HardCostPaymentYear, SoftCostPaymentYear, [Priority])  
                SELECT
                                                                                a.Id                                                                                                                                                       AS ApplicationId
                                                                                ,fpf.EstimatePurchasePrice                                                                 AS EstimatePurchasePrice
                                                                                ,fpf.AdditionalSoftCostEstimate                                                      AS AdditionalSoftCostEstimate
                                                                                ,fpf.HardCostFMPAmt                                                                                                             AS HardCostFMPAmt
                                                                                ,fpf.SoftCostFMPAmt                                                                                                               AS SoftCostFMPAmt
                                                                                ,YEAR(fpp1.HardCostPaymentDate)                                                             AS HardCostPaymentDate
                                                                                ,YEAR(fpp2.SoftCostPaymentDate)                                                               AS SoftCostPaymentYear
																				,fppr.[Priority]
                FROM                                  Flood.FloodApplication a
                INNER JOIN   Flood.FloodApplicationAdminDetails ad
                       ON (a.StatusId IN (6,7,9) AND ad.ApplicationId = a.Id)
                INNER JOIN   Flood.FloodApplicationParcel fap
                       ON (fap.ApplicationId = a.Id)
                INNER JOIN   Flood.FloodParcelFinance fpf
                       ON (fpf.ApplicationId = fap.ApplicationId AND fpf.PamsPin = fap.PamsPin)
                LEFT JOIN    Flood.FloodParcelPayment fpp1
                       ON (fpp1.ApplicationId = fpf.ApplicationId AND fpp1.PamsPin = fpf.PamsPin AND ISNULL(fpp1.HardCostPaymentStatusId,0) = 1)
                LEFT JOIN    Flood.FloodParcelPayment fpp2
                       ON (fpp2.ApplicationId = fpf.ApplicationId AND fpp2.PamsPin = fpf.PamsPin AND ISNULL(fpp2.SoftCostPaymentStatusId,0) = 1)
		        INNER JOIN Flood.FloodParcelProperty fppr 
					    ON (fppr.ApplicationId = fpf.ApplicationId AND fppr.PamsPin = fpf.PamsPin)
                WHERE  (YEAR(ad.BCCPreliminaryApprovalDate) BETWEEN @v_InceptionYear AND @p_SelectedYear and fppr.Priority=1);
 
                                
                DROP TABLE IF EXISTS #PrevProjectAreas;
                CREATE TABLE #PrevProjectAreas
                (
                                ApplicationId                                  INT,
                                BCCPreliminaryApprovalYear                     INT,
                                Title                                          VARCHAR(128),
                                NoOfHomes                                      INT,
                                MatchPercent                                   DECIMAL(18,2),  
                                CAFNumber                                      VARCHAR(128),
                                FundsEncumbered                                DECIMAL(18,2),  
                                HardCostFMPAmtCurrentYear                      DECIMAL(18,2),  
                                SoftCostFMPAmtCurrentYear                      DECIMAL(18,2),  
                                FundsReleasedCurrentYear                       DECIMAL(18,2),  
                                HardCostFMPAmtInceptionYearToPrevYear          DECIMAL(18,2),  
                                SoftCostFMPAmtInceptionYearToPrevYear          DECIMAL(18,2),  
                                FundsReleasedInceptionYearToPrevYear           DECIMAL(18,2), 
                                HardCostFMPAmtInceptionYearToCurrentYear       DECIMAL(18,2),  
                                SoftCostFMPAmtInceptionYearToCurrentYear       DECIMAL(18,2),  
                                FundsReleasedInceptionYearToCurrentYear        DECIMAL(18,2), 
 
                                ClosingsCurrentYear                            INT,
                                ClosingsInceptionYearToPrevYear                INT,
                );
 
                ;WITH cte_EstimatesAndCostAmounts AS 
                 ( 
                                SELECT
                                         ApplicationId                      AS ApplicationId
                                        ,SUM(EstimatePurchasePrice)         AS EstimatePurchasePrice
                                        ,SUM(AdditionalSoftCostEstimate)    AS AdditionalSoftCostEstimate
                                FROM       #ProjAreaEstimatesAndPayments
                               GROUP BY    ApplicationId                 
                )
                INSERT INTO #PrevProjectAreas(ApplicationId, BCCPreliminaryApprovalYear, Title, NoOfHomes, MatchPercent, CAFNumber, FundsEncumbered)  
                SELECT      a.Id
                           ,YEAR(ad.BCCPreliminaryApprovalDate)  
                           ,a.Title
                           ,fao.NoOfHomes    
                           ,faf.MatchPercent
                          ,CASE WHEN fap.CAFClosed = 1 THEN CONCAT('*', fap.CAFNumber) ELSE fap.CAFNumber END  
                         ,(((cte.[EstimatePurchasePrice] * faf.[MatchPercent]) / 100)  + ((((cte.[EstimatePurchasePrice] * faf.[MatchPercent]) / 100) * 25) / 100)  + cte.[AdditionalSoftCostEstimate])   
                FROM                  Flood.FloodApplication a
                INNER JOIN     Flood.FloodApplicationAdminDetails ad
                                                                    ON (ad.ApplicationId = a.Id)   
                INNER JOIN     Flood.FloodApplicationOverview fao
                                                                    ON (fao.ApplicationId = a.Id)
                INNER JOIN     Flood.FloodApplicationFinance faf
                                                                    ON (faf.ApplicationId = a.Id)
                INNER JOIN     Flood.FloodApplicationPayment fap
                                                                     ON (fap.ApplicationId = a.Id)
                INNER JOIN     cte_EstimatesAndCostAmounts cte
                                                                     ON (cte.ApplicationId = a.Id);

 
                -- populate HardCostFMPAmtCurrentYear
                ;WITH cte_HardCostFMPAmtCurrentYear AS 
                ( 
                                SELECT   ApplicationId, SUM(ISNULL(HardCostFMPAmt,0)) As HardCostFMPAmt
                                FROM      #ProjAreaEstimatesAndPayments
                                WHERE                              ISNULL(HardCostPaymentYear,0) = @p_SelectedYear
                                GROUP BY       ApplicationId                 
                )
                UPDATE     prevprojareas
                SET       HardCostFMPAmtCurrentYear = cte.HardCostFMPAmt
                FROM      #PrevProjectAreas prevprojareas
                INNER JOIN     cte_HardCostFMPAmtCurrentYear cte
                                                                ON (cte.ApplicationId = prevprojareas.ApplicationId);
 
                -- populate SoftCostFMPAmtCurrentYear
                ;WITH cte_SoftCostFMPAmtCurrentYear AS 
                ( 
                                SELECT   ApplicationId, SUM(ISNULL(SoftCostFMPAmt,0)) As SoftCostFMPAmt
                                FROM     #ProjAreaEstimatesAndPayments
                                WHERE    ISNULL(SoftCostPaymentYear,0) = @p_SelectedYear
                                GROUP BY  ApplicationId                 
                )
                UPDATE   prevprojareas
                SET    SoftCostFMPAmtCurrentYear = cte.SoftCostFMPAmt
                FROM   #PrevProjectAreas prevprojareas
                INNER JOIN     cte_SoftCostFMPAmtCurrentYear cte
                    ON (cte.ApplicationId = prevprojareas.ApplicationId);
 
                -- populate HardCostFMPAmtInceptionYearToPrevYear
                ;WITH cte_HardCostFMPAmtInceptionYearToPrevYear AS 
                ( 
                                SELECT     ApplicationId, SUM(ISNULL(HardCostFMPAmt,0)) As HardCostFMPAmt
                                FROM        #ProjAreaEstimatesAndPayments
                                WHERE     (ISNULL(HardCostPaymentYear,0) BETWEEN @v_InceptionYear AND @p_SelectedYear - 1)
                                GROUP BY    ApplicationId                 
                )
                UPDATE  prevprojareas
                SET     HardCostFMPAmtInceptionYearToPrevYear = cte.HardCostFMPAmt
                FROM    #PrevProjectAreas prevprojareas
                INNER JOIN     cte_HardCostFMPAmtInceptionYearToPrevYear cte
                                                                ON (cte.ApplicationId = prevprojareas.ApplicationId);
 
                -- populate SoftCostFMPAmtInceptionYearToPrevYear
                ;WITH cte_SoftCostFMPAmtInceptionYearToPrevYear AS 
                ( 
                                SELECT   ApplicationId, SUM(ISNULL(SoftCostFMPAmt,0)) As SoftCostFMPAmt
                                FROM    #ProjAreaEstimatesAndPayments
                                WHERE   (ISNULL(SoftCostPaymentYear,0) BETWEEN @v_InceptionYear AND @p_SelectedYear - 1)
                                GROUP BY  ApplicationId                 
                )
                UPDATE   prevprojareas
                SET    SoftCostFMPAmtInceptionYearToPrevYear = cte.SoftCostFMPAmt
                FROM   #PrevProjectAreas prevprojareas
                INNER JOIN     cte_SoftCostFMPAmtInceptionYearToPrevYear cte
                                                                ON (cte.ApplicationId = prevprojareas.ApplicationId);

			   ;WITH cte_EstimatesClosing AS
			   (
					select  fap.ApplicationId as applicationId,
					count(fap.Pamspin) as Closings
					from flood.floodapplicationparcel fap 
					inner join flood.floodparcelstatuslog fpsl ON fpsl.ApplicationId = fap.ApplicationId and fpsl.PamsPin = fap.PamsPin
					where fpsl.statusId=5 and YEAR(fpsl.statusDate) = @p_SelectedYear
					group by fap.ApplicationId
			   )
			   UPDATE #PrevProjectAreas
			   SET ClosingsCurrentYear = ISNULL(cte.Closings,0)
			    FROM #PrevProjectAreas prevprojareas
				INNER JOIN cte_EstimatesClosing cte 
												ON (cte.ApplicationId = prevprojareas.ApplicationId);


				---closingsInceptionYearToPrevYear
				 ;WITH cte_EstimatesClosing AS
			   (
					select  fap.ApplicationId as applicationId,
					count(fap.Pamspin) as PrevClosings
					from flood.floodapplicationparcel fap 
					inner join flood.floodparcelstatuslog fpsl ON fpsl.ApplicationId = fap.ApplicationId and fpsl.PamsPin = fap.PamsPin
					where fpsl.statusId=5 and YEAR(fpsl.statusDate) BETWEEN @v_InceptionYear AND (@p_SelectedYear-1)
					group by fap.ApplicationId
			   )
			   UPDATE #PrevProjectAreas
			   SET ClosingsInceptionYearToPrevYear = ISNULL(cte.PrevClosings,0)
			    FROM #PrevProjectAreas prevprojareas
				INNER JOIN cte_EstimatesClosing cte 
												ON (cte.ApplicationId = prevprojareas.ApplicationId);
                -- populate HardCostFMPAmtInceptionYearToCurrentYear
                ;WITH cte_HardCostFMPAmtInceptionYearToCurrentYear AS 
                ( 
                                SELECT  ApplicationId, SUM(ISNULL(HardCostFMPAmt,0)) As HardCostFMPAmt
                                FROM   #ProjAreaEstimatesAndPayments
                                WHERE  (ISNULL(HardCostPaymentYear,0) BETWEEN @v_InceptionYear AND @p_SelectedYear)
                                GROUP BY       ApplicationId                 
                )
                UPDATE  prevprojareas
                SET  HardCostFMPAmtInceptionYearToCurrentYear = cte.HardCostFMPAmt
                FROM #PrevProjectAreas prevprojareas
                INNER JOIN     cte_HardCostFMPAmtInceptionYearToCurrentYear cte
                                                                ON (cte.ApplicationId = prevprojareas.ApplicationId);
 
                -- populate SoftCostFMPAmtInceptionYearToCurrentYear
                ;WITH cte_SoftCostFMPAmtInceptionYearToCurrentYear AS 
                ( 
                                SELECT  ApplicationId, SUM(ISNULL(SoftCostFMPAmt,0)) As SoftCostFMPAmt,YEAR(GETDATE()) as "current Year",YEAR(GETDATE())-1 as "Previous year"
                                FROM     #ProjAreaEstimatesAndPayments
                                WHERE   (ISNULL(SoftCostPaymentYear,0) BETWEEN @v_InceptionYear AND @p_SelectedYear)
                                GROUP BY    ApplicationId                 
                )
                UPDATE  prevprojareas
                SET   SoftCostFMPAmtInceptionYearToCurrentYear = cte.SoftCostFMPAmt
                FROM  #PrevProjectAreas prevprojareas
                INNER JOIN     cte_SoftCostFMPAmtInceptionYearToCurrentYear cte
                       ON (cte.ApplicationId = prevprojareas.ApplicationId);
                                                                                
                UPDATE             #PrevProjectAreas
                SET   FundsReleasedCurrentYear = ISNULL(HardCostFMPAmtCurrentYear,0) + ISNULL(SoftCostFMPAmtCurrentYear,0)
                     ,FundsReleasedInceptionYearToPrevYear = ISNULL(HardCostFMPAmtInceptionYearToPrevYear,0) + ISNULL(SoftCostFMPAmtInceptionYearToPrevYear,0)
                     ,FundsReleasedInceptionYearToCurrentYear = ISNULL(HardCostFMPAmtInceptionYearToCurrentYear,0) + ISNULL(SoftCostFMPAmtInceptionYearToCurrentYear,0)
                                                 
                --SELECT * FROM #ProjAreaEstimatesAndPayments;
                SELECT * FROM #PrevProjectAreas;
	-------------------------------------------------
	--** Dispose 
	-------------------------------------------------
	DISPOSE:
 
END

GO


