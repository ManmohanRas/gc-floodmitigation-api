-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodProgramExpenses]
GO

-- Create Table
CREATE TABLE [Flood].[FloodProgramExpenses](
	[Id]						[int]		IDENTITY(1,1)						NOT NULL,
	[ExpenseYear]		       	[char](4)										NOT NULL,
	[ExpenseAmount]			    [decimal](18, 2)								NULL,
	[ExpenseDate]				[datetime]    								    NULL,
	[CategoryId]		        [smallint]				     				    NULL,
	[Comment]					[varchar](4000)									NULL,
	[LastUpdatedBy]				[varchar](128)									NULL	,
	[LastUpdatedOn]				[datetime]										NOT NULL,
CONSTRAINT [PK_FloodProgramExpenses_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) 

GO