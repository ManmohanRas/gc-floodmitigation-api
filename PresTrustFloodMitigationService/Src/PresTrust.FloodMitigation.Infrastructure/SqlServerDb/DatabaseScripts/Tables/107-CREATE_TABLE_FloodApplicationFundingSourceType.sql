IF OBJECT_ID('[Flood].[FloodApplicationFundingSourceType] ') IS NOT NULL

BEGIN
	-- Drop Constraints	
	ALTER TABLE [Flood].[FloodApplicationFundingSourceType] DROP CONSTRAINT IF EXISTS  [DF_FloodApplicationFundingSourceType_IsActive];
END;
GO


-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationFundingSourceType];
GO

-- Create Table
Create Table [Flood].[FloodApplicationFundingSourceType](
[Id]                                    [integer]                              NOT NULL,
[Title]                                 [varchar](216)			               NOT NULL,
[SortOrder]                             [smallint]                             NOT NULL,
[IsActive]								[bit]								   NULL
CONSTRAINT [PK_FloodApplicationFundingSourceType_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)ON [PRIMARY]

GO

-- Create Constraints
 
ALTER TABLE [Flood].[FloodApplicationFundingSourceType] WITH NOCHECK ADD  CONSTRAINT [DF_FloodApplicationFundingSourceType_IsActive]  DEFAULT (1) FOR [IsActive]
GO