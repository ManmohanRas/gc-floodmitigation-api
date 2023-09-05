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
