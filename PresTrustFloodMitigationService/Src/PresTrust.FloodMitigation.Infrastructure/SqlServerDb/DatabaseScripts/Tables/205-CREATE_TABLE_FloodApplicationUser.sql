IF OBJECT_ID('[Flood].[FloodApplicationUser]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodApplicationUser] DROP CONSTRAINT IF EXISTS  [DF_IsPrimaryContact_FloodApplicationUser];

	ALTER TABLE [Flood].[FloodApplicationUser] DROP CONSTRAINT IF EXISTS  [DF_IsAlternateContact_FloodApplicationUser];
			
	ALTER TABLE [Flood].[FloodApplicationUser] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationUser];

    ALTER TABLE [Flood].[FloodApplicationUser] DROP CONSTRAINT IF EXISTS   [DF_FirstName_FloodApplicationUser];  

	ALTER TABLE [Flood].[FloodApplicationUser] DROP CONSTRAINT IF EXISTS   [DF_LastName_FloodApplicationUser];  
END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationUser]
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplicationUser](
	[Id]							[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]					[integer]						NOT NULL,
	[Email]							[varchar](128)					NULL	,
	[UserId]						[varchar](128)					NOT NULL,
	[UserName]						[varchar](128)					NOT NULL,
	[FirstName]						[varchar](128)					NULL,
	[LastName]						[varchar](128)					NULL,
	[Title]							[varchar](128)					NULL	,
	[PhoneNumber]					[varchar](15)					NULL	,
	[IsPrimaryContact]				[bit]							NOT NULL,
	[IsAlternateContact]			[bit]							NOT NULL,
	[LastUpdatedBy]					[varchar](128)					NULL	,
	[LastUpdatedOn]					[datetime]						NOT NULL,
CONSTRAINT [PK_FloodRole_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraint
ALTER TABLE [Flood].[FloodApplicationUser] WITH NOCHECK ADD  CONSTRAINT [DF_IsPrimaryContact_FloodApplicationUser]  DEFAULT (0) FOR [IsPrimaryContact]
GO  

ALTER TABLE [Flood].[FloodApplicationUser] WITH NOCHECK ADD  CONSTRAINT [DF_IsAlternateContact_FloodApplicationUser]  DEFAULT (0) FOR [IsAlternateContact]
GO  

ALTER TABLE [Flood].[FloodApplicationUser] WITH NOCHECK ADD  CONSTRAINT [DF_FirstName_FloodApplicationUser]  DEFAULT ('') FOR [FirstName];
GO

ALTER TABLE [Flood].[FloodApplicationUser] WITH NOCHECK ADD  CONSTRAINT [DF_LastName_FloodApplicationUser]  DEFAULT ('') FOR [LastName];
GO

ALTER TABLE [Flood].[FloodApplicationUser] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationUser]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO
