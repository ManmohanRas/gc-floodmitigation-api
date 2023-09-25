IF OBJECT_ID('[Flood].[FloodApplicationFundingAgency]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodApplicationFundingAgency] DROP CONSTRAINT IF EXISTS  FK_ApplicationId_FloodApplicationFundingAgency;

	
END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationFundingAgency]
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplicationFundingAgency](
	[Id]						[integer]	  IDENTITY(1,1)	    NOT NULL,
	[ApplicationId]				[integer]				        NOT NULL,
	[FundingAgencyName]         [varchar](256)					NOT NULL,
	[CurrentStatus]				[varchar](50)					NULL	,
	[DateOfApproval]			[datetime]					    NULL	,
CONSTRAINT [PK_FloodApplicationFundingAgency_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraint


ALTER TABLE [Flood].[FloodApplicationFundingAgency] ADD CONSTRAINT FK_ApplicationId_FloodApplicationFundingAgency  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO  
