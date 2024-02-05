IF OBJECT_ID('[Flood].[FloodApplicationPayment]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodApplicationPayment] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationPayment];

	ALTER TABLE [Flood].[FloodApplicationPayment] DROP CONSTRAINT IF EXISTS  [FK_CAFClosed_FloodApplicationPayment];

	ALTER TABLE [Flood].[FloodApplicationPayment] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationPayment];

END;
GO


-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationPayment]
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplicationPayment](
[Id]	                                [integer]     IDENTITY(1,1)               NOT NULL,
[ApplicationId]                         [integer] 								  NOT NULL,
[CAFNumber]                             [varchar](128)                            NULL,
[CAFClosed]                             [bit]                                     NOT NULL,
[LastUpdatedBy]							[varchar](128)					          NULL,
[LastUpdatedOn]							[datetime]						          NOT NULL,
CONSTRAINT [PK_FloodApplicationPayment_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodApplicationPayment] ADD CONSTRAINT FK_ApplicationId_FloodApplicationPayment  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO

ALTER TABLE [Flood].[FloodApplicationPayment] WITH NOCHECK ADD  CONSTRAINT [DF_CAFClosed_FloodApplicationPayment]  DEFAULT (0) FOR [CAFClosed]
GO

ALTER TABLE [Flood].[FloodApplicationPayment] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationPayment]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO
