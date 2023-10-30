IF OBJECT_ID('[Flood].[FloodApplicationFinanceLineItems]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodApplicationFinanceLineItems] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationFinanceLineItems];

	ALTER TABLE [Flood].[FloodApplicationFinanceLineItems] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationFinanceLineItems];

END;
GO


-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationFinanceLineItems]
GO


-- Create Table
CREATE TABLE [Flood].[FloodApplicationFinanceLineItems](
[Id]	                                [integer]     IDENTITY(1,1)               NOT NULL,
[ApplicationId]                         [integer] 								  NOT NULL,
[PamsPin]					            [nvarchar](76)							  NOT NULL,
[ValueEstimate]				            [decimal](18,2)				              NULL    ,
[LastUpdatedBy]							[varchar](128)					          NULL	  ,
[LastUpdatedOn]							[dateTime]						          NULL	  ,
CONSTRAINT [PK_FloodApplicationFinanceLineItems_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodApplicationFinanceLineItems] ADD CONSTRAINT FK_ApplicationId_FloodApplicationFinanceLineItems  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO

ALTER TABLE [Flood].[FloodApplicationFinanceLineItems] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationFinanceLineItems]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO