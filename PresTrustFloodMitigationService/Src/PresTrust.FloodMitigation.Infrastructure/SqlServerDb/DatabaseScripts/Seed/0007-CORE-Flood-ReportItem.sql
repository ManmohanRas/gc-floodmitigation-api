DELETE FROM [Core].[ReportItem] WHERE [Id] IN (25,26,27,28,29,30,31,32,33,34,35);

SET IDENTITY_INSERT [Core].[ReportItem] ON

INSERT INTO [Core].[ReportItem] (Id, Title, ReportUrl, [Description], Icon, SortOrder, ProgramTypeId, IsActive) 
VALUES (25, 'Core Application Report', 'FloodCoreApplicationReportGeneral', 'Flood Mitigation Core Application Report', 'description', 1, 2, 1);
 
INSERT INTO [Core].[ReportItem] (Id, Title, ReportUrl, [Description], Icon, SortOrder, ProgramTypeId, IsActive) 
VALUES (26, 'Flood Core Review Report', 'FloodCoreReviewReportPreview', 'Flood Mitigation Core Review Report', 'description', 2, 2, 1);
 
INSERT INTO [Core].[ReportItem] (Id, Title, ReportUrl, [Description], Icon, SortOrder, ProgramTypeId, IsActive) 
VALUES (27, 'Flood Municipal Program Summary Report', 'FloodMunicipalProgramSummaryReport', 'Flood Mitigation Municipal Summary', 'description', 3, 2, 1);
 
INSERT INTO [Core].[ReportItem] (Id, Title, ReportUrl, [Description], Icon, SortOrder, ProgramTypeId, IsActive) 
VALUES (28, 'Flood Project Area Funds Extension Request', 'FloodProjectAreaFundsExtensionRequest', 'Flood Mitigation Project Area Funds Extension Request', 'description', 4, 2, 1);
 
INSERT INTO [Core].[ReportItem] ([Id], Title, ReportUrl, [Description], Icon, SortOrder, ProgramTypeId, IsActive) 
VALUES (29, 'Flood Expiration Report', 'FloodExpirationReport', 'Flood Mitigation Expiration Report', 'description', 5, 2, 1);
  
INSERT INTO [Core].[ReportItem] ([Id], Title, ReportUrl, [Description], Icon, SortOrder, ProgramTypeId, IsActive) 
VALUES (30, 'Flood Status Report', 'FloodStatusReport', 'Flood Mitigation Status Report', 'description', 6, 2, 1);
  
INSERT INTO [Core].[ReportItem] ([Id], Title, ReportUrl, [Description], Icon, SortOrder, ProgramTypeId, IsActive) 
VALUES (31, 'Flood Flap Summary', 'FloodFlapSummary', 'Flood Mitigation Flap Summary', 'description', 7, 2, 1);
  
INSERT INTO [Core].[ReportItem] ([Id], Title, ReportUrl, [Description], Icon, SortOrder, ProgramTypeId, IsActive) 
VALUES (32, 'Flood Annual Audit Report', 'FloodAnnualAuditReport', 'Flood Mitigation Annual Audit Report', 'description', 8, 2, 1);
  
INSERT INTO [Core].[ReportItem] ([Id], Title, ReportUrl, [Description], Icon, SortOrder, ProgramTypeId, IsActive) 
VALUES (33, 'Flood County Cost Report', 'FloodCountyCostReport', 'Flood Mitigation County Cost Report', 'description', 9, 2, 1);
  
INSERT INTO [Core].[ReportItem] ([Id], Title, ReportUrl, [Description], Icon, SortOrder, ProgramTypeId, IsActive) 
VALUES (34, 'Flood Monthly Funding Summary', 'FloodMonthlyFundingSummary', 'Flood Mitigation Monthly Funding Summary Report', 'description', 10, 2, 1);
  
INSERT INTO [Core].[ReportItem] ([Id], Title, ReportUrl, [Description], Icon, SortOrder, ProgramTypeId, IsActive) 
VALUES (35, 'Flood Program Summary Report', 'FloodProgramSummaryReport', 'Flood Mitigation Program Summary Report', 'description', 11, 2, 1);
  
SET IDENTITY_INSERT [Core].[ReportItem] OFF
