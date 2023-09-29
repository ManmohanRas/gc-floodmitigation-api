-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodParcelSoftCostType]
GO

-- Create Table
CREATE TABLE [Flood].[FloodParcelSoftCostType](
	[Id]						[integer] 			IDENTITY(1,1)		NOT NULL,
	[Title]						[varchar](128) 							NOT NULL,
	
CONSTRAINT [PK_FloodParcelSoftCostType_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
