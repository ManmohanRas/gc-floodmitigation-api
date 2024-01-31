-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodAnnualFunding]
GO

-- Create Table
CREATE TABLE [Flood].[FloodAnnualFunding](
	[Id]						[int]					IDENTITY(1,1)			NOT NULL,
	[AllocationYear]			[char](4)										NOT NULL,
	[AllocationAmount]			[decimal](18, 2)								NULL,
	[Interest]					[decimal](18, 2)								NULL,
	[AddedOrOmittedAmount]		[decimal](18, 2)								NULL,
	[Comment]					[varchar](4000)									NULL,
	[LastUpdatedBy]				[varchar](128)									NULL	,
	[LastUpdatedOn]				[datetime]										NOT NULL,

CONSTRAINT [PK_FloodAnnualFunding_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
