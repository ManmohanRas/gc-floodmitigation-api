BEGIN TRY
	BEGIN TRANSACTION
	--==============================================================================================================--

		ALTER TABLE [Flood].[FloodParcelStatusLog] DROP CONSTRAINT IF EXISTS  [PK_FloodParcelStatusLog_Id]
		ALTER TABLE [Flood].[FloodApplicationStatusLog] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationStatusLog_Id]
		ALTER TABLE [Flood].[FloodParcelTracking] DROP CONSTRAINT IF EXISTS  [PK_FloodParcelTracking_Id]
		ALTER TABLE [Flood].[FloodParcelTracking] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelTracking]
		ALTER TABLE [Flood].[FloodParcelTech] DROP CONSTRAINT IF EXISTS  [PK_FloodParcelTech_Id]
		ALTER TABLE [Flood].[FloodParcelTech] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelTech]
		ALTER TABLE [Flood].[FloodParcelSurvey] DROP CONSTRAINT IF EXISTS  [PK_FloodParcelSurvey_Id]
		ALTER TABLE [Flood].[FloodParcelSurvey] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelSurvey]
		ALTER TABLE [Flood].[FloodParcelStatus] DROP CONSTRAINT IF EXISTS  [PK_FloodParcelStatus_Id]
		ALTER TABLE [Flood].[FloodParcelSoftCost] DROP CONSTRAINT IF EXISTS  [PK_FloodParcelSoftCost_Id]
		ALTER TABLE [Flood].[FloodParcelSoftCost] DROP CONSTRAINT IF EXISTS  [FK_SoftCostTypeId_FloodParcelSoftCost]
		ALTER TABLE [Flood].[FloodParcelSoftCost] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelSoftCost]
		ALTER TABLE [Flood].[FloodParcelSoftCostType] DROP CONSTRAINT IF EXISTS  [PK_FloodParcelSoftCostType_Id]
		ALTER TABLE [Flood].[FloodParcelSection] DROP CONSTRAINT IF EXISTS  [PK_FloodParcelSection_Id]
		ALTER TABLE [Flood].[FloodParcelProperty] DROP CONSTRAINT IF EXISTS  [PK_FloodParcelProperty_Id]
		ALTER TABLE [Flood].[FloodParcelPayment] DROP CONSTRAINT IF EXISTS  [PK_FloodParcelPayment_Id]
		ALTER TABLE [Flood].[FloodParcelPayment] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelPayment]
		ALTER TABLE [Flood].[FloodParcelFinance] DROP CONSTRAINT IF EXISTS  [PK_FloodParcelFinance_Id]
		ALTER TABLE [Flood].[FloodParcelFinance] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelFinance]
		ALTER TABLE [Flood].[FloodParcelFeedback] DROP CONSTRAINT IF EXISTS  [PK_FloodParcelFeedback_Id]
		ALTER TABLE [Flood].[FloodParcelFeedback] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelFeedback]
		ALTER TABLE [Flood].[FloodParcelDocument] DROP CONSTRAINT IF EXISTS  [PK_FloodParcelDocument_Id]
		ALTER TABLE [Flood].[FloodParcelDocument] DROP CONSTRAINT IF EXISTS  [FK_DocumentTypeId_FloodParcelDocument]
		ALTER TABLE [Flood].[FloodParcelDocument] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelDocument]
		ALTER TABLE [Flood].[FloodParcelDocumentType] DROP CONSTRAINT IF EXISTS  [PK_FloodParcelDocumentType_Id]
		ALTER TABLE [Flood].[FloodParcelDocumentType] DROP CONSTRAINT IF EXISTS  [FK_SectionId_FloodParcelDocumentType]
		ALTER TABLE [Flood].[FloodParcelComment] DROP CONSTRAINT IF EXISTS  [PK_FloodParcelComment_Id]
		ALTER TABLE [Flood].[FloodParcelComment] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelComment]
		ALTER TABLE [Flood].[FloodParcelBrokenRules] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelBrokenRules]
		ALTER TABLE [Flood].[FloodLockedParcel] DROP CONSTRAINT IF EXISTS  [PK_FloodLockedParcel_Id]
		ALTER TABLE [Flood].[FloodLockedParcel] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodLockedParcel]
		ALTER TABLE [Flood].[FloodParcelAdminDetails] DROP CONSTRAINT IF EXISTS  [PK_FloodParcelAdminDetails_Id]
		ALTER TABLE [Flood].[FloodParcelAdminDetails] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelAdminDetails]
		ALTER TABLE [Flood].[FloodParcelHistory] DROP CONSTRAINT IF EXISTS  [FK_ParcelId_FloodParcelHistory]
		ALTER TABLE [Flood].[FloodParcel] DROP CONSTRAINT IF EXISTS  [PK_FloodParcel_Id]
		ALTER TABLE [Flood].[FloodContacts] DROP CONSTRAINT IF EXISTS  [PK_FloodContacts_Id]
		ALTER TABLE [Flood].[FloodContacts] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodContacts]
		ALTER TABLE [Flood].[FloodApplicationUser] DROP CONSTRAINT IF EXISTS  [PK_FloodRole_Id]
		ALTER TABLE [Flood].[FloodApplicationType] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationType_Id]
		ALTER TABLE [Flood].[FloodApplicationSubType] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationSubType_Id]
		ALTER TABLE [Flood].[FloodApplicationStatus] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationStatus_Id]
		ALTER TABLE [Flood].[FloodApplicationSignatory] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationSignatory_Id]
		ALTER TABLE [Flood].[FloodApplicationSignatory] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationSignatory]
		ALTER TABLE [Flood].[FloodApplicationPayment] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationPayment_Id]
		ALTER TABLE [Flood].[FloodApplicationPayment] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationPayment]
		ALTER TABLE [Flood].[FloodApplicationParcel] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationParcel]
		ALTER TABLE [Flood].[FloodApplicationOverview] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationOverview_Id]
		ALTER TABLE [Flood].[FloodApplicationOverview] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationOverview]
		ALTER TABLE [Flood].[FloodApplicationFundingAgency] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationFundingAgency_Id]
		ALTER TABLE [Flood].[FloodApplicationFundingAgency] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationFundingAgency]
		ALTER TABLE [Flood].[FloodApplicationFinanceLineItems] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationFinanceLineItems_Id]
		ALTER TABLE [Flood].[FloodApplicationFinanceLineItems] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationFinanceLineItems]
		ALTER TABLE [Flood].[FloodApplicationFinanceFund] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationFinanceFund_Id]
		ALTER TABLE [Flood].[FloodApplicationFinanceFund] DROP CONSTRAINT IF EXISTS  [FK_FundingSourceTypeId_FloodApplicationFinanceFund]
		ALTER TABLE [Flood].[FloodApplicationFinanceFund] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationFinanceFund]
		ALTER TABLE [Flood].[FloodApplicationFundingSourceType] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationFundingSourceType_Id]
		ALTER TABLE [Flood].[FloodApplicationFinance] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationFinance_Id]
		ALTER TABLE [Flood].[FloodApplicationFinance] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationFinance]
		ALTER TABLE [Flood].[FloodApplicationFeedback] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationFeedback_Id]
		ALTER TABLE [Flood].[FloodApplicationFeedback] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationFeedback]
		ALTER TABLE [Flood].[FloodApplicationDocument] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationDocument_Id]
		ALTER TABLE [Flood].[FloodApplicationDocument] DROP CONSTRAINT IF EXISTS  [FK_DocumentTypeId_FloodApplicationDocument]
		ALTER TABLE [Flood].[FloodApplicationDocument] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationDocument]
		ALTER TABLE [Flood].[FloodApplicationDocumentType] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationDocumentType_Id]
		ALTER TABLE [Flood].[FloodApplicationDocumentType] DROP CONSTRAINT IF EXISTS  [FK_SectionId_FloodApplicationDocumentType]
		ALTER TABLE [Flood].[FloodApplicationCommentType] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationCommentType_Id]
		ALTER TABLE [Flood].[FloodApplicationSection] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationSection_Id]
		ALTER TABLE [Flood].[FloodApplicationComment] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationComment_Id]
		ALTER TABLE [Flood].[FloodApplicationComment] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationComment]
		ALTER TABLE [Flood].[FloodApplicationBrokenRules] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationBrokenRules]
		ALTER TABLE [Flood].[FloodApplicationAdminDetails] DROP CONSTRAINT IF EXISTS  [PK_FloodApplicationAdminDetails_Id]
		ALTER TABLE [Flood].[FloodApplicationAdminDetails] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationAdminDetails]
		ALTER TABLE [Flood].[FloodApplication] DROP CONSTRAINT IF EXISTS  [PK_FloodApplication_Id]

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
