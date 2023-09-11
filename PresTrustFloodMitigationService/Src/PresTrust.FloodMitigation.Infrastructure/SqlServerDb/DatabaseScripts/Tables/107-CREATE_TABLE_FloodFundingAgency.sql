IF OBJECT_ID('[Flood].[FloodFundingAgency]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodFundingAgency] DROP CONSTRAINT IF EXISTS  FK_ApplicationId_FloodFundingAgency;

	
END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodFundingAgency]
GO

-- Create Table
CREATE TABLE [Flood].[FloodFundingAgency](
	[Id]						[integer]	  IDENTITY(1,1)	    NOT NULL,
	[ApplicationId]				[integer]				        NOT NULL,
	[FundingAgencyName]         [varchar](256)					NOT NULL,
	[CurrentStatus]				[varchar](50)					NULL	,
	[DateOfApproval]			[datetime]					    NULL	,
CONSTRAINT [PK_FloodFundingAgency_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraint


ALTER TABLE [Flood].[FloodFundingAgency] ADD CONSTRAINT FK_ApplicationId_FloodFundingAgency  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO  
