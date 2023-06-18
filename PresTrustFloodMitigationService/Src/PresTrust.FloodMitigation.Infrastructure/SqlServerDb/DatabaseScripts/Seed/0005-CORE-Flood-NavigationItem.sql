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
	20, 0, 'Applications','flood/applications', 'flaticon-squares-4', 2,0, 2, 1
);
GO

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly],  [ProgramTypeId],[IsActive]
) 
VALUES 
(
	21, 0, 'Program Manager','flood-manageprogram', 'flaticon2-layers-2', 3,0, 2, 1
);
GO

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly], [ProgramTypeId], [IsActive]
) 
VALUES 
(
	22, 0, 'Reports','reports/rptdashboard', 'flaticon2-document', 4,0, 2, 1
);
GO

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly], [ProgramTypeId], [IsActive]
) 
VALUES 
(
	23, 0, 'Admin','#', 'flaticon2-calendar-3', 5,0, 2, 1
);
GO

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly], [ProgramTypeId], [IsActive]
) 
VALUES 
(
	24, 0, 'How To','howto', 'flaticon-questions-circular-button', 6,0, 2, 1
);
GO

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly],  [ProgramTypeId],[IsActive]
) 
VALUES 
(
	25, 0, 'Manage Agency Users','flood-manageagency', 'flaticon2-layers-2', 3,0, 2, 1
);
GO

INSERT INTO [Core].[NavigationItem]
(
	[Id], [ParentId], [Title], [RouterLink], [Icon], [SortOrder], [IsViewOnly],  [ProgramTypeId],[IsActive]
) 
VALUES 
(
	26, 0, 'Create An Application','flood/createapplication', 'flaticon2-layers-2', 4,0, 2, 1
);
GO

SET IDENTITY_INSERT [Core].[NavigationItem] OFF
