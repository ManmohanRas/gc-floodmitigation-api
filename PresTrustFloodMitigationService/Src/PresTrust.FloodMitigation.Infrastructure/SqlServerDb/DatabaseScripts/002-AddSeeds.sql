UPDATE [CORE].[ProgramType]
SET IsActive = 1
WHERE Id = 2;

DELETE FROM [Core].[Permission] WHERE [Id] IN (9, 10, 11, 12);

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

 -- delete previous records
DELETE FROM [Core].[PermissionUserRole] WHERE PermissionId IN (9, 10, 11, 12);


-- manage_program
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (9, 1);
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (9, 3);

-- manage_agency
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (10, 1);
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (10, 7);

-- manage_reports
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (11, 1);
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (11, 2);
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (11, 3);
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (11, 4);
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (11, 6);
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (11, 7);
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (11, 8);

-- flood mitigation view edit
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (12, 1);
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (12, 3);
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (12, 4);
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (12, 5);
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (12, 6);
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (12, 7);
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (12, 8);
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (12, 9);
INSERT INTO [Core].[PermissionUserRole]([PermissionId], [UserRoleId]) VALUES (12, 10);

DELETE FROM [Core].[NavigationItem] WHERE [Id] IN (19,20,21,22,23,24,25,26,27);

SET IDENTITY_INSERT [Core].[NavigationItem] ON

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly], [ProgramTypeId], [IsActive]
) 
VALUES 
(
	19, 0, 'Dashboard','dashboard', 'flaticon2-protection', 1,0, 2, 1
);
GO

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly],  [ProgramTypeId],[IsActive]
) 
VALUES 
(
	20, 0, 'Create An Application','flood/chooseapp', 'flaticon2-add-1', 2,0, 2, 1
);
GO

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly],  [ProgramTypeId],[IsActive]
) 
VALUES 
(
	21, 0, 'Manage Project Areas','flood/applications', 'flaticon-squares-4', 3,0, 2, 1
);
GO

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly],  [ProgramTypeId],[IsActive]
) 
VALUES 
(
	22, 0, 'Manage Properties','flood/properties', 'flaticon-map-location', 4,0, 2, 1
);
GO

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly],  [ProgramTypeId],[IsActive]
) 
VALUES 
(
	23, 0, 'Program Manager','flood-manageprogram', 'flaticon2-layers-2', 5,0, 2, 1
);
GO

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly], [ProgramTypeId], [IsActive]
) 
VALUES 
(
	24, 0, 'Reports','reports/rptdashboard', 'flaticon2-document', 6,0, 2, 1
);
GO

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly],  [ProgramTypeId],[IsActive]
) 
VALUES 
(
	25, 0, 'Manage Agency Users','flood-manageagency', 'flaticon2-layers-2', 4,0, 2, 1
);
GO

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly], [ProgramTypeId], [IsActive]
) 
VALUES 
(
	26, 0, 'Admin','#', 'flaticon2-calendar-3', 7,0, 2, 1
);
GO

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly], [ProgramTypeId], [IsActive]
) 
VALUES 
(
	27, 0, 'How To','howto', 'flaticon-questions-circular-button', 8,0, 2, 1
);
GO

SET IDENTITY_INSERT [Core].[NavigationItem] OFF

-- delete previous records
DELETE FROM [Core].[NavigationItemUserRole] WHERE NavigationItemId IN (19,20,21,22,23,24,25,26,27);

-- Dashboard
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 1);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 3);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 4);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 5);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 6);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 7);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 8);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 9);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 10);

-- Create An Application
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (20, 3);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (20, 4);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (20, 7);

-- Manage Project Areas
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (21, 1);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (21, 3);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (21, 4);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (21, 5);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (21, 6);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (21, 7);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (21, 8);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (21, 9);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (21, 10);

-- Manage Properties
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (22, 1);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (22, 3);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (22, 4);

-- Program Manager
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (23, 1);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (23, 3);

-- Reports
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (24, 1);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (24, 3);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (24, 4);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (24, 6);

-- Manage Agency Users
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (25, 7);

-- Admin
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (26, 1);

-- howto
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (27, 1);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (27, 3);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (27, 4);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (27, 5);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (27, 6);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (27, 7);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (27, 8);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (27, 9);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (27, 10);

GO

DELETE FROM [Flood].[FloodApplicationStatus];

INSERT INTO [Flood].[FloodApplicationStatus] ([Id],[Name]) VALUES (1,'DECLARATION_OF_INTENT_DRAFT');
GO

INSERT INTO [Flood].[FloodApplicationStatus] ([Id],[Name]) VALUES (2,'DECLARATION_OF_INTENT_SUBMITTED');
GO

INSERT INTO [Flood].[FloodApplicationStatus] ([Id],[Name]) VALUES (3,'DRAFT');
GO

INSERT INTO [Flood].[FloodApplicationStatus] ([Id],[Name]) VALUES (4,'SUBMITTED');
GO

INSERT INTO [Flood].[FloodApplicationStatus] ([Id],[Name]) VALUES (5,'IN_REVIEW');
GO

INSERT INTO [Flood].[FloodApplicationStatus] ([Id],[Name]) VALUES (6,'ACTIVE');
GO

INSERT INTO [Flood].[FloodApplicationStatus] ([Id],[Name]) VALUES (7,'CLOSED');
GO

INSERT INTO [Flood].[FloodApplicationStatus] ([Id],[Name]) VALUES (8,'REJECTED');
GO

INSERT INTO [Flood].[FloodApplicationStatus] ([Id],[Name]) VALUES (9,'WITHDRAWN');
GO

DELETE FROM [Flood].[FloodApplicationType];

INSERT INTO [Flood].[FloodApplicationType] ([Id], [Title])  VALUES (1, 'CORE');
GO
INSERT INTO [Flood].[FloodApplicationType] ([Id], [Title])  VALUES (2, 'MATCH');
GO

DELETE FROM [Flood].[FloodApplicationSubType];

INSERT INTO [Flood].[FloodApplicationSubType] ([Id], [Title])  VALUES (1, 'DISASTER');
GO
INSERT INTO [Flood].[FloodApplicationSubType] ([Id], [Title])  VALUES (2, 'FASTTRACK');
GO
INSERT INTO [Flood].[FloodApplicationSubType] ([Id], [Title])  VALUES (3, 'MATCH');
GO
INSERT INTO [Flood].[FloodApplicationSubType] ([Id], [Title])  VALUES (4, 'ONGOING_FLOOD');
GO


DELETE FROM [Flood].[FloodApplicationSection];

INSERT INTO [Flood].[FloodApplicationSection] ([Id], [Title], [Description]) VALUES (0, 'NONE', '');
GO

INSERT INTO [Flood].[FloodApplicationSection] ([Id], [Title], [Description]) VALUES (1, 'DECLARATION_OF_INTENT', 'Declaration of Intent');
GO

INSERT INTO [Flood].[FloodApplicationSection] ([Id], [Title], [Description]) VALUES (2, 'ROLES', 'Roles');
GO

INSERT INTO [Flood].[FloodApplicationSection] ([Id], [Title], [Description]) VALUES (3, 'OVERVIEW', 'Overview');
GO

INSERT INTO [Flood].[FloodApplicationSection] ([Id], [Title], [Description]) VALUES (4, 'PROJECT_AREA', 'Project Area');
GO

INSERT INTO [Flood].[FloodApplicationSection] ([Id], [Title], [Description]) VALUES (5, 'FINANCE', 'Finance');
GO

INSERT INTO [Flood].[FloodApplicationSection] ([Id], [Title], [Description]) VALUES (6, 'OTHER_DOCUMENTS', 'Other Documents');
GO

INSERT INTO [Flood].[FloodApplicationSection] ([Id], [Title], [Description]) VALUES (7, 'SIGNATORY', 'Signatory');
GO

INSERT INTO [Flood].[FloodApplicationSection] ([Id], [Title], [Description]) VALUES (8, 'ADMIN_DOCUMENT_CHECKLIST', 'Admin Document Checklist');
GO

INSERT INTO [Flood].[FloodApplicationSection] ([Id], [Title], [Description]) VALUES (9, 'ADMIN_DETAILS', 'Admin Details');
GO

INSERT INTO [Flood].[FloodApplicationSection] ([Id], [Title], [Description]) VALUES (10, 'ADMIN_CONTACTS', 'Admin Contacts');
GO

INSERT INTO [Flood].[FloodApplicationSection] ([Id], [Title], [Description]) VALUES (11, 'ADMIN_RELEASE_OF_FUNDS', 'Admin Release of Funds');
GO

DELETE FROM  [Flood].[FloodApplicationDocumentType];

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (1, 'LOE', 'LOE document', 3);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (2, 'FEMA', 'FEMA document', 3);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (3, 'GREEN_ACRES', 'Green acres document', 3);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (4, 'BLUE_ACRES', 'Blue acres document', 3);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (5, 'OTHER_FUNDING_AGENCY', 'Other funding agency document', 3);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (6, 'APPLICATION_CHECKLIST', 'Application checklist', 6);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (7, 'PUBLIC_HEARING_CERTIFICATE', 'Public hearing certificate', 6);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (8, 'MINUTES_FROM_PUBLIC_HEARING', 'Minutes from public hearing', 6);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (9, 'MUNICIPAL_RESOLUTION_OF_SUPPORT', 'Municipal Resolution of support', 6);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (10, 'NON_COUNTY_AGENCY_RESOLUTION', 'Non county agency resolution', 6);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (11, 'OTHER_DOCUMENTS', 'Other Documents', 6);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (12, 'FMC_PRELIMINARY_APPROVAL_RESOLUTION', 'FMC preliminary approval resolution', 9);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (13, 'BCC_PRELIMINARY_APPROVAL_RESOLUTION', 'BCC preliminary approval resolution', 9);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (14, 'CONGRATULATIONS_LETTER_TO_HOME_OWNER', 'Congratulations letter to home owner', 9);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (15, 'NOTIFICATION_OF_APPROVAL_AND_PROCEDURES_LETTER', 'Notification of approval and procedures letter', 9);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (16, 'PROJECT_AREA_FUNDS_EXPIRATION_REQUEST', 'Project area funds expiration request', 9);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (17, 'COMMISSIONER_RESOLUTION', 'Commissiorner resolution request', 9);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (18, 'FIRST_COMMISSIONER_RESOLUTION', 'First Commissioner resolution request', 9);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (19, 'SECOND_COMMISSIONER_RESOLUTION', 'Second Commissioner resolution request', 9);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (20, 'CORE_APPLICATION_REPORT', 'Core Application Report request', 9);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (21, 'CORE_REVIEW_REPORT', 'Core Review Report request', 9);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (22, 'PROJECT_AREA_APPLICATION_MAP', 'Project area application map request', 9);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (23, 'CAF_CLOSE_OUT_SUMMARY', 'CAF Close out summary application map request', 9);
GO

DELETE FROM  [Flood].[FloodApplicationCommentType];

INSERT INTO [Flood].[FloodApplicationCommentType]([Id], [Title]) VALUES (1, 'General Comment');
GO

INSERT INTO [Flood].[FloodApplicationCommentType]([Id], [Title]) VALUES (2, 'Staff Comment');
GO

INSERT INTO [Flood].[FloodApplicationCommentType]([Id], [Title]) VALUES (3, 'Application Comment');
GO
DELETE FROM [Flood].[FloodApplicationFundingSourceType];

INSERT INTO [Flood].[FloodApplicationFundingSourceType] ([Id],[Title], [SortOrder], [IsActive]) VALUES (1,'FEMA', 1, 1);
GO

INSERT INTO [Flood].[FloodApplicationFundingSourceType] ([Id],[Title], [SortOrder], [IsActive]) VALUES (2,'NJ_DEP_GREEN_ACRES', 2, 1);
GO

INSERT INTO [Flood].[FloodApplicationFundingSourceType] ([Id],[Title], [SortOrder], [IsActive]) VALUES (3,'NJ_DEP_BLUE_ACRES', 3, 1);
GO

INSERT INTO [Flood].[FloodApplicationFundingSourceType] ([Id],[Title], [SortOrder], [IsActive]) VALUES (4,'MUNICIPAL_OSTF', 4, 1);
GO

INSERT INTO [Flood].[FloodApplicationFundingSourceType] ([Id],[Title], [SortOrder], [IsActive]) VALUES (5,'MUNICIPAL_FUNDS', 5, 1);
GO

INSERT INTO [Flood].[FloodApplicationFundingSourceType] ([Id],[Title], [SortOrder], [IsActive]) VALUES (6,'LAND_OWNER_DONATION', 6, 1);
GO

INSERT INTO [Flood].[FloodApplicationFundingSourceType] ([Id],[Title], [SortOrder], [IsActive]) VALUES (7,'OTHER_FUNDING_SOURCE', 7, 1);
GO

TRUNCATE TABLE [Flood].[FloodParcel];

WITH CoreParcelCTE AS
(
	SELECT
		*
	FROM
	(
		SELECT
			ROW_NUMBER() OVER(ORDER BY PAMS_PIN ASC, OBJECTID ASC) AS RowNo,
			[PAMS_PIN] AS PamsPin,
			MunicipalID AS AgencyID,
			Block AS Block,
			Lot AS Lot,
			QualificationCode AS QualificationCode,
			ISNULL(PropertyLocation, '') AS StreetNoStreetAddress,
			CalculatedAcreage AS Acreage,
			OwnersName AS OwnersName,
			NULL AS OwnersAddress1,
			NULL AS OwnersAddress2,
			BuildingSquareFeet AS SquareFootage,
			YearConstructed AS YearOfConstruction,
			NULL AS TargetAreaId,
			NULL AS DateOfFLAP,
			1 AS IsValidPamsPin,
			'flood-admin' AS LastUpdatedBy,
			GetDate() AS LastUpdatedOn,
			1 AS IsActive
		FROM		[Core].[Parcels]
	) CoreParcels
)
INSERT INTO [Flood].[FloodParcel]
(
	PamsPin,
	AgencyID,
	Block,
	Lot,
	QualificationCode,
	Latitude,
	Longitude,
	StreetNo,
	StreetAddress,
	Acreage,
	OwnersName,
	OwnersAddress1,
	OwnersAddress2,
	OwnersCity,
	OwnersState,
	OwnersZipcode,
	SquareFootage,
	YearOfConstruction,
	TargetAreaId,
	DateOfFLAP,
	IsValidPamsPin,
	LastUpdatedBy,
	LastUpdatedOn,
	IsActive
)
SELECT
	PamsPin,
	AgencyID,
	Block,
	Lot,
	QualificationCode,
	NULL,
	NULL,
	TRIM(StreetNo),
	TRIM(StreetAddress),
	Acreage,
	OwnersName,
	OwnersAddress1,
	OwnersAddress2,
	NULL,
	NULL,
	NULL,
	SquareFootage,
	YearOfConstruction,
	TargetAreaId,
	DateOfFLAP,
	IsValidPamsPin,
	LastUpdatedBy,
	LastUpdatedOn,
	IsActive
FROM
(
	SELECT
		ROW_NUMBER() OVER(PARTITION BY RowNo ORDER BY RowNo ASC) AS StreetNoRow,
		value AS StreetNo,
		SUBSTRING(StreetNoStreetAddress, (LEN(value) + 1), LEN(StreetNoStreetAddress)) AS StreetAddress,
		*
	FROM CoreParcelCTE
	CROSS APPLY STRING_SPLIT(StreetNoStreetAddress, ' ')
) Parcel
WHERE Parcel.StreetNoRow = 1;

DELETE FROM [Flood].[FloodParcelStatus];

INSERT INTO [Flood].[FloodParcelStatus] ([Id],[Name]) VALUES (1,'SUBMITTED');
GO

INSERT INTO [Flood].[FloodParcelStatus] ([Id],[Name]) VALUES (2,'IN_REVIEW');
GO

INSERT INTO [Flood].[FloodParcelStatus] ([Id],[Name]) VALUES (3,'PENDING');
GO

INSERT INTO [Flood].[FloodParcelStatus] ([Id],[Name]) VALUES (4,'APPROVED');
GO

INSERT INTO [Flood].[FloodParcelStatus] ([Id],[Name]) VALUES (5,'PRESERVED');
GO

INSERT INTO [Flood].[FloodParcelStatus] ([Id],[Name]) VALUES (6,'GRANT_EXPIRED');
GO

INSERT INTO [Flood].[FloodParcelStatus] ([Id],[Name]) VALUES (7,'REJECTED');
GO

INSERT INTO [Flood].[FloodParcelStatus] ([Id],[Name]) VALUES (8,'WITHDRAWN');
GO

INSERT INTO [Flood].[FloodParcelStatus] ([Id],[Name]) VALUES (9,'PROJECT_AREA_EXPIRED');
GO

INSERT INTO [Flood].[FloodParcelStatus] ([Id],[Name]) VALUES (10,'TRANSFERRED');
GO

DELETE FROM [Flood].[FloodParcelSection];

INSERT INTO [Flood].[FloodParcelSection] ([Id], [Title], [Description]) VALUES (0, 'NONE', 'None');
GO

INSERT INTO [Flood].[FloodParcelSection] ([Id], [Title], [Description]) VALUES (1, 'PROPERTY', 'Property');
GO

INSERT INTO [Flood].[FloodParcelSection] ([Id], [Title], [Description]) VALUES (2, 'OTHER_DOCUMENTS', 'Other Documents');
GO

INSERT INTO [Flood].[FloodParcelSection] ([Id], [Title], [Description]) VALUES (3, 'SOFT_COSTS', 'Soft Costs');
GO

INSERT INTO [Flood].[FloodParcelSection] ([Id], [Title], [Description]) VALUES (4, 'TECH', 'Tech');
GO

INSERT INTO [Flood].[FloodParcelSection] ([Id], [Title], [Description]) VALUES (5, 'FINANCE', 'Finance');
GO

INSERT INTO [Flood].[FloodParcelSection] ([Id], [Title], [Description]) VALUES (6, 'ADMIN_DOCUMENT_CHECKLIST', 'Admin Document Checklist');
GO

INSERT INTO [Flood].[FloodParcelSection] ([Id], [Title], [Description]) VALUES (7, 'ADMIN_SURVEY', 'Admin Survey');
GO

INSERT INTO [Flood].[FloodParcelSection] ([Id], [Title], [Description]) VALUES (8, 'ADMIN_DETAILS', 'Admin Details');
GO

INSERT INTO [Flood].[FloodParcelSection] ([Id], [Title], [Description]) VALUES (9, 'ADMIN_RELEASE_OF_FUNDS', 'Admin Release of Funds');
GO

INSERT INTO [Flood].[FloodParcelSection] ([Id], [Title], [Description]) VALUES (10, 'ADMIN_TRACKING', 'Admin Tracking');
GO

DELETE FROM  [Flood].[FloodParcelSoftCostType];

SET IDENTITY_INSERT [Flood].[FloodParcelSoftCostType] ON

INSERT INTO [Flood].[FloodParcelSoftCostType]([Id], [Title]) VALUES (1, 'APPRAISALS');
GO

INSERT INTO [Flood].[FloodParcelSoftCostType]([Id], [Title]) VALUES (2, 'ENVIRONMENTAL_ANALYSIS');
GO

INSERT INTO [Flood].[FloodParcelSoftCostType]([Id], [Title]) VALUES (3, 'TITLE_SEARCH_INSURANCE');
GO

INSERT INTO [Flood].[FloodParcelSoftCostType]([Id], [Title]) VALUES (4, 'SURVEY');
GO

INSERT INTO [Flood].[FloodParcelSoftCostType]([Id], [Title]) VALUES (5, 'DEMOLITION');
GO

INSERT INTO [Flood].[FloodParcelSoftCostType]([Id], [Title]) VALUES (6, 'OTHER_SOFT_COSTS');
GO

SET IDENTITY_INSERT [Flood].[FloodParcelSoftCostType] OFF

DELETE FROM  [Flood].[FloodParcelDocumentType];

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (1, 'APPRISAL', 'Appraisal document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (2, 'COUNTY_APPRAISAL_REPORT', 'County Appraisal Report document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (3, 'VOLUNTARY_PARTICIPATION_FORM', 'Voluntary Participation Form Report document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (4, 'DUPLICATION_BENEFITS_DOCUMENTS', 'Duplication Benefits Documents document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (5, 'SETTLEMENT_SHEET', 'Settlementsheet document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (6, 'HOMEOWNER_AFFIDAVIT', 'Homeowner Affidavit document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (7, 'CONTRACTOR_AFFIDAVIT', 'Contractor Affidavit document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (8, 'ENGINEER_AFFIDAVIT', 'Engineer Affidavit document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (9, 'SURVEY_LEGAL_DESCRIPTION', 'Survey Legal Description document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (10, 'TITLE_SEARCH_REPORT', 'Title Search Report document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (11, 'HOMEOWNER_SURVEY', 'Homeowner Survey document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (12, 'PRELIMINARY_ASSESSMENT_REPORT', 'Preliminary Assessment Report document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (13, 'PRELIMINARY_ASSESSMENT_REPORT_REVIEWIETTER', 'Preliminary Assessment ReportreviewIetter document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (14, 'FINAL_MITIGATION_OFFER', 'Final Mitigation Offer document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (15, 'MUNICIPAL_ORDINANCE_PURCHASE', 'Municipal Ordinance Purchase document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (16, 'RECORDED_DEED', 'Recorded Deed document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (17, 'EXECUTED_HUD1', 'Executed document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (18, 'TITLE_INSURANCE_POLICY', 'Title Insurance Policy document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (19, 'REIMBURSEMENT_FORM', 'Reimbursement Form document', 3);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (20, 'REIMBURSEMENT_PROOF', 'Reimbursement Proof document', 3);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (21, 'OTHER_DOCUMENTS', 'Other Documents ', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (22, 'FMC_FINAL_APPROVAL', 'Fmc Final Approval document', 8);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (23, 'CONGRATULATION_LETTER_HOMEOWNER', 'Congratuation letter to home owner', 8);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (24, 'BCC_FINAL_APPROVAL', 'Bcc Final Approval document', 8);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (25, 'GRANT_AGREEMENT', 'Grant Agreement document', 8);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (26, 'FMC_SOFTCOST', 'Fmc Soft Cost document', 8);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (27, 'BCC_SOFTCOST', 'Bcc Soft Cost document', 8);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (28, 'DOB_FAQ', 'Dob Faq document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (29, 'BANK_VERIFICATION_FORM', 'Bank Verification Form document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (30, 'STEP_TO_CLOSING', 'Step To Closing document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (31, 'BENEFITS_PROCESS', 'Benefits Process document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (32, 'SURVEY_REVIEW_LETTER', 'Survey Review Letter document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (33, 'POST_ACQUISITION', 'Post Acquisition Picture document', 10);
GO

DELETE FROM [Flood].[FloodEmailTemplate];

--Project area flow
INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    1, 
    'CHANGE_STATUS_FROM_DOI_DRAFT_TO_DOI_SUBMITTED', 
    'Change status from DOI Draft to DOI Submitted', 
    'Morris County Flood Mitigation Program – Declaration Of Intent Submitted', 
    '<p>Dear {{PrimaryContactName}},</p>
<p>This email is to inform you that the Declaration of Intent for {{ApplicationName}} has been submitted and is under review by County staff. Some additional documents or steps may be necessary to complete your submission. If so, you&rsquo;ll be notified by email.</p>
<p>As Morris County staff reviews the Declaration of Intent, staff will prepare feedback comments for any section(s) that require further clarification or attention. Once approved, an email will be sent. From there, you&rsquo;ll be able to log back into the Flood Mitigation Program Program Portal and complete the application.</p>
<p>Please contact me if you have any questions or concerns.<br>
 <br>
Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood </p>', 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    2, 
    'CHANGE_STATUS_FROM_DOI_SUBMITTED_TO_DOI_APPROVED', 
    'Change status from DOI Submitted to Draft Application', 
    'Morris County Flood Mitigation Program – Declaration Of Intent Approved', 
    '<p>Dear {{PrimaryContactName}},</p>
<p>This email is to inform you that the Declaration of Intent for {{ApplicationName}} has been approved by County staff.</p>
<p>The draft application is now open. Please begin to fill out mandatory fields and upload any required documentation. Morris County staff will be working in conjunction with you to complete the application. If clarification or more information is required for any section(s) of the application, you will receive an email to review the feedback and attend to these requests.</p>
<p>Please contact me if you have any questions or concerns.<br>
 <br>
Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood </p>', 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    3, 
    'CHANGE_STATUS_FROM_DOI_APPROVED_TO_SUBMITTED', 
    'Change status from Draft Application to Application Submitted', 
    'Morris County Flood Mitigation Program – Application Submitted', 
    '<p>Dear {{PrimaryContactName}},</p>
<p>This email is to inform you that the {{ApplicationName}} application has been submitted and is under review by County staff. Some additional steps may be necessary to complete your submission. If so, you&rsquo;ll be notified by email.</p>
<p>As Morris County staff reviews the application, staff will prepare feedback comments for any section(s) of the application that require further clarification or attention. Once completed, an email will be sent. From there, you&rsquo;ll be able to log back into the Flood Mitigation Program Portal, review the feedback and attend to any additional requirements.</p>
<p>Please contact me if you have any questions or concerns.<br>
 <br>
Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood</p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    4, 
    'CHANGE_STATUS_FROM_SUBMITTED_TO_IN_REVIEW', 
    'Change status from Application Submitted to In Review', 
    'Morris County Flood Mitigation Program – Application In Review', 
    '<p>Dear {{PrimaryContactName}},</p>
	<p>This email is to inform you that the {{ApplicationName}} application is complete and will be under review by the Flood Mitigation Committee. Upon a determination from the committee, your application will be reviewed by the Morris County Board of County Commissioners. You will be notified via email of both determinations.</p>
<p>Please contact me if you have any questions or concerns.</p>
<p>Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood</p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    5, 
    'CHANGE_STATUS_FROM_IN_REVIEW_TO_REJECTED', 
    'Change status from In Review to Reject', 
    'Morris County Flood Mitigation Program – Application Rejected', 
    '<p>Dear {{PrimaryContactName}},</p>
<p>We are sorry to inform you that the Morris County Flood Mitigation Committee have denied the {{ApplicationName}} application for grant funding at this time.</p>
<p>If you wish to apply for funding from the Morris County Flood Mitigation Program for this area in the future, a new application that meets all current application requirements will need to be completed.</p>
<p>Please contact me if you have any questions or concerns.</p>
<p>Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    6, 
    'CHANGE_STATUS_FROM_IN_REVIEW_TO_ACTIVE', 
    'Change status from In Review to Active', 
    'Morris County Flood Mitigation Program – Application Approved', 
    '<p>Dear {{PrimaryContactName}},</p>
<p>We are pleased to inform you that the Morris County Flood Mitigation Committee and Board of County Commissioners has approved the {{ApplicationName}} application in the amount of {{TotalEncumbered}}.</p>
<p>Please begin the process of obtaining the property appraisals and all required documentation. You can log into the Flood Mitigation Program portal and upload each document as it is completed. Attached you will find copies of the resolutions, the Flood Mitigation Program&rsquo;s rules and regulations, and additional information to assist you in the remainder of this process.</p>
<p>Please contact me if you have any questions or concerns.</p>
<p>Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    7, 
    'CHANGE_STATUS_FROM_ACTIVE_TO_WITHDRAWN', 
    'Project Area Withdrawn', 
    'Morris County Flood Mitigation Program - Project Area Withdrawn', 
    '<p>Dear {{PrimaryContactName}},</p>
<p>As per our discussion, the {{ApplicationName}} Project Area has been withdrawn. Funds for the project area will be held until {{CurrentExpirationDate}}. After this date, the project area will be closed and funds cannot be released.</p>
<p>Please contact me if you have any questions or concerns.</p>
<p>Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    8, 
    'CHANGE_STATUS_FROM_ACTIVE_TO_CLOSED', 
    'Project Area Closed', 
    'Morris County Flood Mitigation Program - Project Area Closed', 
    '<p>Dear {{PrimaryContactName}},</p>
<p>This email is to inform you the {{ApplicationName}} Project Area has been closed. Funds for this project area can no longer be released.</p>
<p>Please contact me if you have any questions or concerns.</p>
<p>Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

--Project area flow


--Feedback
INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    9, 
    'FEEDBACK_EMAIL', 
    'Feedback Sent', 
    'Morris County Flood Mitigation Program - Feedback', 
    '<p>Dear {{PrimaryContactName}},</p>
<p>After careful review of the {{ApplicationName}} Project Area, there are items that require your attention prior to deeming the application complete. Please login to Morris County Flood Mitigation program portal and select the project area to view provided feedback.</p>
<p>Instructions:</p>
<p>The items in Feedback indicate the section(s) of the application that require your attention. Navigate to that section, and you&rsquo;ll find that the application has been re-enabled for your completion. Please make the necessary modifications, then Save Changes. Once you&rsquo;ve completed all the feedback requirements, click the RESUBMIT button. You&rsquo;ll be able to provide your own feedback or questions to us in the Resubmit dialog box that pops up.</p>
<p>Please contact me if you have any questions or concerns.</p>
<p>Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    10, 
    'FEEDBACK_RESPONSE_EMAIL', 
    'Feedback Complete', 
    'Morris County Flood Mitigation Program - Feedback Complete', 
    '<p>{{ContactName}} has completed the required feedback item(s) for the {{ApplicationName}} Project Area. Review the project area to ensure feedback was adequately completed.</p>
	<p>Sincerely,</p>
     <p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO


--Feedback


--Property
INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    11, 
    'CHANGE_PROPERTY_STATUS_FROM_PENDING_TO_APPROVED', 
    'Property Receives Final Approval', 
    'Morris County Flood Mitigation Program - Final Approval', 
    '<p>Dear {{PrimaryContactName}},</p>
    <p>We are pleased to inform you that the Morris County Flood Mitigation Committee and Board of County Commissioners have granted final approval for {{PropertyName}} in the {{ApplicationName}} Project Area in the amount of {{MCHardCostShare}}.</p>
    <p>Attached is the Grant Agreement for {{PropertyName}}. Please sign and seal two copies of the signature page and return to our office prior to the Board of County Commissioner&rsquo;s meeting on {{BCCDate}}.</p>
    <p>Morris County staff will review the property&rsquo;s documents to ensure all due diligence documents (environmental analysis, title search, survey and legal description, etc.) have been received and approved. If any documents are outstanding, you will receive an email requesting them be uploaded. Once all documents have been approved, you will receive an email informing you that you can schedule a closing date with the homeowner.</p>
    <p>Please contact me if you have any questions or concerns.</p>
	<p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    12, 
    'CHANGE_PROPERTY_STATUS_FROM_APPROVED_TO_PRESERVED', 
    'Change Property Status from Approved to Preserved', 
    'Morris County Flood Mitigation Program - Property Preserved', 
    '<p>Dear {{PrimaryContactName}},</p>
	<p>{{PropertyName}} in the {{ApplicationName}} Project Area has been preserved. When a demolition date has been determined, please provide us the date.</p>
	<p>Please contact me if you have any questions or concerns.</p>
    <p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    13, 
    'PROPERTY_SCHEDULE_CLOSING', 
    'Municipality Can Schedule Closing', 
    'Morris County Flood Mitigation Program - Closing Date Confirmation', 
    '<p>Dear {{PrimaryContactName}},</p>
	 <p>We have received all required documentation in order for our program to release funds for {{PropertyName}} in the {{ApplicationName}} Project Area. You may work to schedule a closing date with the homeowner. Please provide at least 7 days notice for the closing to ensure funds are transferred to the muncipality&rsquo;s account in time.</p>
	<p>Please contact me if you have any questions or concerns.</p>
    <p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO
--Property

--Tab wise
INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    14, 
    'SUBMIT_SOFTCOST', 
    'Soft Cost Reimbursement Confirmation', 
    'Morris County Flood Mitigation Program - Soft Cost Reimbursement Confirmation', 
    '<p>Dear {{PrimaryContactName}},</p>
	<p>This email is to inform you the soft cost reimbursement request for {{PropertyName}} in the {{ApplicationName}} Project Area has been submitted. Morris County staff will review and you will receive more information regarding the reimbursement via email.</p>
	<p>Please contact me if you have any questions or concerns.</p>
    <p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    15, 
    'APPROVE_SOFTCOST', 
    'Soft Cost Reimbursement Approval', 
    'Morris County Flood Mitigation Program - Soft Cost Reimbursement Approved', 
    '<p>Dear {{PrimaryContactName}},</p>
	<p>This email is to inform you the soft cost reimbursement request for {{PropertyName}} in the {{ApplicationName}} Project Area has been approved in the total of {{FMPSoftCostReimbursed}}. Funds will be released to your municipality&rsquo;s account within the next week.</p>
	<p>Please contact me if you have any questions or concerns.</p>
    <p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    16, 
    'REMINDER_ABOUT_EXPIRATION_OF_FIRST_GRANT_EXTENSION', 
    'First Project Area Extension Approval', 
    'Morris County Flood Mitigation Program - First Project Area Extension Approval', 
    '<p>Dear {{PrimaryContactName}},</p>
	<p>This email is to inform you that your request for an extension for the {{ApplicationName}} Project Area was approved. It will now expire on {{FirstProjectAreaExtensionDate}}. As a reminder, you have an additional six-month extension remaining. Please reach out within the next three months if you wish to request this second extension.</p>
    <p>Please contact me if you have any questions or concerns.</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    17, 
    'REMINDER_ABOUT_EXPIRATION_OF_SECOND_GRANT_EXTENSION', 
    'Second Project Area Extension Approval', 
    'Morris County Flood Mitigation Program - Second Project Area Extension Approval', 
    '<p>Dear {{PrimaryContactName}},</p>
	<p>This email is to inform you that your request for an extension for the {{ApplicationName}} Project Area was approved. It will now expire on {{SecondProjectAreaExtensionDate}}. After this date, the project area will not be eligible for another extension.</p>
	<p>As a reminder, you will be unable to receive reimbursement for any properties that were not yet preserved at the time of expiration. You will need to reapply with a new project area for these properties if you wish to pursue them again. If there are any outstanding soft cost reimbursement requests at the time of expiration, those can still be released for preserved properties.</p>
	<p>Please contact me if you have any questions or concerns.<br></p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    18, 
    'DUE_DILIGIENCE_DOCUMENTS', 
    'Due Diligience Documents Missing', 
    'Morris County Flood Mitigation Program - Due Diligience Documentation Requested', 
    '<p>Dear {{PrimaryContactName}},</p>
    <p>We are missing one of the following documents for {{PropertyName}} in the {{ApplicationName}} Project Area:</p>
	<p>1. Environmental Analysis<br>
    2. Title Search<br>
    3. Survey and Legal Description</p>
	<p>These documents must be uploaded before funds will be released for the closing. Please log into the Flood Mitigation Program and upload these documents. Once these are uploaded and Morris County staff reviews and approves them, you can then schedule a closing date for the property. Please provide at least 7 days notice for the closing to ensure funds are transferred to the muncipality&rsquo;s account in time.</p>
	<p>Please contact me if you have any questions or concerns.</p>
	<p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
     19, 
    'SOFTCOST_REIMBURSEMENT_INFORMATION', 
	'Soft Cost Reimbursement Information',
    'Morris County Flood Mitigation Program - Soft Cost Reimbursement Request', 
    '<p>Dear {{PrimaryContactName}},</p>
	 <p>This is a reminder that you had requested for soft cost reimbursement for {{PropertyName}} in the {{ApplicationName}} Project Area. Morris County staff has confirmed the demolition of the property, and you are now able to request for soft cost reimbursement. Please log into the Flood Mitigation Program portal and fill out the Soft Cost Reimbursement tab for this property. The following documents are required for each of the reimbursable activities:</p>
	<p>1. Purchase Order<br>
	2. Invoice<br>
	3. Copy of check(s)</p>
	<p>Please contact me if you have any questions or concerns.<br>
	</p>
	<p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO


INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
     20, 
    'FLAP_UPDATE', 
    'FLAP Update', 
    'Morris County Flood Mitigation Program - FLAP Update', 
    '<p>Dear {{PrimaryContactName}},</p>
	 <p>This email is to inform you that your municipality&rsquo;s FLAP has been updated. Please contact me if you have any questions or concerns.</p>
	<p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
     21, 
    'PROJECT_AREA_EXPIRATION_REMINDER', 
    'Project Area Expiration Reminder', 
    'Morris County Flood Mitigation Program - Project Area Extension', 
    '<p>Dear {{PrimaryContactName}},</p>
     <p>This email is to remind you that the {{ApplicationName}} Project Area will expire on {{ProjectAreaExpirationDate}}. After expiration, you will be unable to receive reimbursement for any properties that were not yet preserved at the time of expiration. You will need to reapply with a new project area for these properties if you wish to pursue them again. If there are any outstanding soft cost reimbursement requests at the time of expiration, those can still be released for preserved properties.</p>
	<p>You may request for two separate six-month extensions. The second extension can only be requested once the first extension is reviewed and approved by the Flood Mitigation Committee. Please reach out with any questions or with your extension request.</p>
	<p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
     22, 
    'GRANT_EXPIRATION_REMINDER', 
    'Grant Expiration Reminder', 
    'Morris County Flood Mitigation Program - Grant Expiration Reminder', 
    '<p>Dear {{PrimaryContactName}},</p>
	 <p>This is a reminder that funds for {{PropertyName}} in the {{ApplicationName}} Project Area have not been released. The grant will expire in 3 months on {{GrantExpirationDate}}. Please work to schedule a closing with the homeowner, and provide at least 7 days notice for the closing to ensure funds are transferred to the municipality&rsquo;s account in time. If a closing will not occur, let us know and the property will be withdrawn.</p>
	 <p>Please contact me if you have any questions or concerns.</p>
	<p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
     23, 
    'UPLOAD_CLOSING_DOCUMENTS_REMINDER', 
    'Upload Closing Documents', 
    'Morris County Flood Mitigation Program - Closing Documents Requested', 
    '<p>Dear {{PrimaryContactName}},</p>
	 <p>This is a reminder to upload the following closing documents for {{PropertyName}} in the {{ApplicationName}} Project Area:</p>
	<p>1. Recorded Deed<br>
	   2. HUD-1 Form<br>
	   3. Title Insurance Policy</p>
	<p>If you requested soft cost reimbursement once demolition of the home occurs, these documents must be uploaded before funds will be released.</p>
	<p>Please contact me if you have any questions or concerns.<br></p>
	<p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
     24, 
    'DEMOLITION_REMINDER', 
    'Demolition Reminder', 
    'Morris County Flood Mitigation Program - Demolition Reminder', 
    '<p>Dear {{PrimaryContactName}},</p>
	 <p>This is a reminder that a demolition date for {{PropertyName}} in the {{ApplicationName}} Project Area has not been scheduled. If the home has already been demolished, provide us the date of demoltion and proof of demolition. If it has not, provide us the anticipated date of demolition.</p>
	<p>Please contact me if you have any questions or concerns.</p>
	<p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO


--Tab wise
