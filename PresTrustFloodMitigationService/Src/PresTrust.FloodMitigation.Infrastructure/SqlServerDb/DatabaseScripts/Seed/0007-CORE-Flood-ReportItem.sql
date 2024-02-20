DELETE FROM [Core].[ReportItem] WHERE [Id] IN (25,26,27,28,29);

SET IDENTITY_INSERT [Core].[ReportItem] ON

INSERT INTO [Core].[ReportItem] (Id, Title, ReportUrl, [Description], Icon, SortOrder, ProgramTypeId, IsActive) 
VALUES (25, 'Core Appliacation Report', 'FloodCoreApplicationReport', 'Flood Mitigation Core Application Report', 'description', 1, 2, 1);
 
INSERT INTO [Core].[ReportItem] (Id, Title, ReportUrl, [Description], Icon, SortOrder, ProgramTypeId, IsActive) 
VALUES (26, 'Flood Core Review Report', 'FloodCoreReviewReport', 'Flood Mitigation Core Review Report', 'description', 2, 2, 1);
 
INSERT INTO [Core].[ReportItem] (Id, Title, ReportUrl, [Description], Icon, SortOrder, ProgramTypeId, IsActive) 
VALUES (27, 'Flood Municipal Program Summary Report', 'FloodMunicipalProgramSummaryReport', 'Flood Mitigation Municipal Summary', 'description', 3, 2, 1);
 
INSERT INTO [Core].[ReportItem] (Id, Title, ReportUrl, [Description], Icon, SortOrder, ProgramTypeId, IsActive) 
VALUES (28, 'Flood Muni Congratulation Prelim Funding', 'FloodMuniCongratulationPrelimFunding', 'Flood Mitigation Muni Congratulation Prelim Funding', 'description', 4, 2, 1);
 
INSERT INTO [Core].[ReportItem] ([Id], Title, ReportUrl, [Description], Icon, SortOrder, ProgramTypeId, IsActive) 
VALUES (29, 'Flood Project Area Funds Extension Request', 'FloodProjectAreaFundsExtensionRequest', 'Flood Mitigation Project Area Funds Extension Request', 'description',5, 2, 1);
  
SET IDENTITY_INSERT [Core].[ReportItem] OFF
