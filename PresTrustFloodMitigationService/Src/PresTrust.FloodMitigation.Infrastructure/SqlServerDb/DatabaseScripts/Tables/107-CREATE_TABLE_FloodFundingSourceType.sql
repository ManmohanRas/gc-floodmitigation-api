IF OBJECT_ID('[Flood].[FloodFundingSourceType] ') IS NOT NULL

BEGIN
	-- Drop Constraints	
	ALTER TABLE [Flood].[FloodFundingSourceType] DROP CONSTRAINT IF EXISTS  [DF_FloodFundingSourceType_IsActive];
END;
GO


-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodFundingSourceType];
GO

-- Create Table
Create Table [Flood].[FloodFundingSourceType](
[Id]                                    [integer]                              NOT NULL,
[Title]                                 [varchar](216)			               NOT NULL,
[SortOrder]                             [smallint]                             NOT NULL,
[IsActive]								[bit]								   NULL
CONSTRAINT [PK_FloodFundingSourceType_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)ON [PRIMARY]

GO

-- Create Constraints
 
ALTER TABLE [Flood].[FloodFundingSourceType] WITH NOCHECK ADD  CONSTRAINT [DF_FloodFundingSourceType_IsActive]  DEFAULT (1) FOR [IsActive]
GO