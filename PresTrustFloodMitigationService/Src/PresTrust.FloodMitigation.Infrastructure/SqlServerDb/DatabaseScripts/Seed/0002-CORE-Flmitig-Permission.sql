
SET IDENTITY_INSERT [Core].[Permission] ON

INSERT INTO [Core].[Permission]
	([Id],[Name],[Description],[ProgramTypeId],[IsActive]) 
	VALUES 
	(9, 'MANAGE_PROGRAM', 'Manage program sections like municipal finance, calendering, user management, parcels, email templates',	2,	1);
GO 

INSERT INTO [Core].[Permission]
	([Id],[Name],[Description],[ProgramTypeId],[IsActive]) 
	VALUES 
	(10, 'MANAGE_AGENCY', 'Manage agency sections like user management', 2,	1);
GO 

INSERT INTO [Core].[Permission]
	([Id],[Name],[Description],[ProgramTypeId],[IsActive]) 
	VALUES 
	(11, 'MANAGE_REPORTS', 'Generate or View reports', 2, 1);
GO 

INSERT INTO [Core].[Permission]
	([Id],[Name],[Description],[ProgramTypeId],[IsActive]) 
	VALUES 
	(12, 'FLOOD_MITIGATION', 'View and/or Edit Flood Mitigation Application', 2, 1);
GO 

SET IDENTITY_INSERT [Core].[Permission] OFF

 