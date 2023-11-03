-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodParcelStatus]
GO

-- Create Table
CREATE TABLE [Flood].[FloodParcelStatus](
	[Id]				[smallint] 			NOT NULL,
	[Name]				[varchar](128)		NOT NULL, 
CONSTRAINT [PK_FloodParcelStatus_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
