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