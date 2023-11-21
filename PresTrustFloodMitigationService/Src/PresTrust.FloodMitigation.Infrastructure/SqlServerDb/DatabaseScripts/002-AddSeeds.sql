use PresTrust_DEV;
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

DELETE FROM [Core].[NavigationItem] WHERE [Id] IN (19,20,21,22,23,24,25,26);

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
	26, 0, 'Create An Application','flood/chooseapp', 'flaticon2-add-1', 2,0, 2, 1
);
GO


INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly],  [ProgramTypeId],[IsActive]
) 
VALUES 
(
	20, 0, 'Manage Project Areas','flood/applications', 'flaticon-squares-4', 3,0, 2, 1
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
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly],  [ProgramTypeId],[IsActive]
) 
VALUES 
(
	21, 0, 'Program Manager','flood-manageprogram', 'flaticon2-layers-2', 5,0, 2, 1
);
GO

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly], [ProgramTypeId], [IsActive]
) 
VALUES 
(
	22, 0, 'Reports','reports/rptdashboard', 'flaticon2-document', 6,0, 2, 1
);
GO

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly], [ProgramTypeId], [IsActive]
) 
VALUES 
(
	23, 0, 'Admin','#', 'flaticon2-calendar-3', 7,0, 2, 1
);
GO

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly], [ProgramTypeId], [IsActive]
) 
VALUES 
(
	24, 0, 'How To','howto', 'flaticon-questions-circular-button', 8,0, 2, 1
);
GO

SET IDENTITY_INSERT [Core].[NavigationItem] OFF

-- delete previous records
DELETE FROM [Core].[NavigationItemUserRole] WHERE NavigationItemId IN (19,20,21,22,23,24,25,26);

-- dashboard
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 1);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 2);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 3);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 4);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 5);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 6);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 7);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 8);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 9);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (19, 10);

--applications
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (20, 1);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (20, 2);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (20, 3);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (20, 4);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (20, 5);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (20, 6);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (20, 7);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (20, 8);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (20, 9);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (20, 10);

--manage program
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (21, 1);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (21, 3);

--reports
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (22, 1);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (22, 3);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (22, 4);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (22, 6);

--admin
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (23, 1);

-- howto
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (24, 1);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (24, 2);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (24, 3);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (24, 4);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (24, 5);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (24, 6);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (24, 7);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (24, 8);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (24, 9);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (24, 10);

--manage agency
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (25, 7);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (25, 8);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (25, 9);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (25, 10);


--create application
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (26, 7);
INSERT INTO [Core].[NavigationItemUserRole]([NavigationItemId], [UserRoleId]) VALUES (26, 3);


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

INSERT INTO [Flood].[FloodApplicationSection] ([Id], [Title], [Description]) VALUES (6, 'SIGNATORY', 'Signatory');
GO

INSERT INTO [Flood].[FloodApplicationSection] ([Id], [Title], [Description]) VALUES (7, 'OTHER_DOCUMENTS', 'Other Documents');
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

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (6, 'APPLICATION_CHECKLIST', 'Application checklist', 7);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (7, 'PUBLIC_HEARING_CERTIFICATE', 'Public hearing certificate', 7);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (8, 'MINUTES_FROM_PUBLIC_HEARING', 'Minutes from public hearing', 7);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (9, 'MUNICIPAL_RESOLUTION_OF_SUPPORT', 'Municipal Resolution of support', 7);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (10, 'NON_COUNTY_AGENCY_RESOLUTION', 'Non county agency resolution', 7);
GO

INSERT INTO [Flood].[FloodApplicationDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (11, 'OTHER_DOCUMENTS', 'Other Documents', 7);
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
			0 AS IsFLAP,
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
	IsFLAP,
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
	IsFLAP,
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

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (6, 'HOME_OWNER_AFFIDAVIT', 'Homeowner Affidavit document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (7, 'CONTRACTOR_AFFIDAVIT', 'Contractor Affidavit document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (8, 'ENGINEER_AFFIDAVIT', 'Engineer Affidavit document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (9, 'SURVEY_LEGAL_DESCRIPTION', 'Survey Legal Description document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (10, 'TITLE_SEARCH_REPORT', 'Title Search Report document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (11, 'HOME_OWNERSURVEY', 'Homeowner Survey document', 2);
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

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (17, 'EXECUTED', 'Executed document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (18, 'TITLE_INSURANCE_POLICY', 'Title Insurance Policy document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (19, 'REIMBURSEMENT_FORM', 'Reimbursement Form document', 3);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (20, 'REIMBURSEMENT_PROOF', 'Reimbursement Proof document', 3);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (21, 'OTHER_DOCUMENTS', 'Other Documents ', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (22, 'FMC_FINAL_APPROVAL', 'Fmc Final Approval document', 2);
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

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (30, 'VOLUNTARY_PARTICIPATIONS_FORM', 'Voluntary Participations Form document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (31, 'STEP_TO_CLOSING', 'Step To Closing document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (32, 'BENEFITS_PROCESS', 'Benefits Process document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (33, 'SURVEY_REVIEW_LETTER', 'Survey Review Letter document', 2);
GO

INSERT INTO [Flood].[FloodParcelDocumentType]([Id], [Title], [Description], [SectionId]) VALUES (34, 'POST_ACQUISITION', 'Post Acquisition Picture document', 10);
GO