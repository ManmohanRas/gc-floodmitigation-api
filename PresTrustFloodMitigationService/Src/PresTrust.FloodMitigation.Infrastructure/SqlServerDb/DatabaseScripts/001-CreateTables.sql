use prestrusttemp
BEGIN TRY
	BEGIN TRANSACTION
	--==============================================================================================================--

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationStatus]

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationStatus](
			[Id]				[smallint] 			NOT NULL,
			[Name]				[varchar](128)		NOT NULL, 
		CONSTRAINT [PK_FloodApplicationStatus_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationType]

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationType](
			[Id]				[smallint] 			NOT NULL,
			[Title]				[varchar](128)		NOT NULL, 
		CONSTRAINT [PK_FloodApplicationType_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationSubType]

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationSubType](
			[Id]				[smallint] 			NOT NULL,
			[Title]				[varchar](128)		NOT NULL, 
		CONSTRAINT [PK_FloodApplicationSubType_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		-- Drop Constraints
		IF OBJECT_ID('[Flood].[FloodApplicationSection]') IS NOT NULL
		BEGIN
			ALTER TABLE [Flood].[FloodApplicationSection] DROP CONSTRAINT IF EXISTS  [DF_FloodApplicationSection_Description]
		END

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationSection];
 
		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationSection](
			[Id]				[smallint] 			NOT NULL,
			[Title]				[varchar](128)		NOT NULL,
			[Description]		[varchar](512)		NULL,		
		CONSTRAINT [PK_FloodApplicationSection_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]


		-- Create Constraint
		ALTER TABLE [Flood].[FloodApplicationSection] WITH NOCHECK ADD  CONSTRAINT [DF_FloodApplicationSection_Description]  DEFAULT ('') FOR [Description]

		-- Drop Constraints
		IF OBJECT_ID('[Flood].[FloodApplicationDocumentType]') IS NOT NULL
		BEGIN
			ALTER TABLE [Flood].[FloodApplicationDocumentType] DROP CONSTRAINT IF EXISTS  [FK_SectionId_FloodApplicationDocumentType]
		END

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationDocumentType];

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationDocumentType](
			[Id]				[smallint] 			NOT NULL,
			[Title]				[varchar](128)		NOT NULL,
			[Description]		[varchar](512)		NULL	,
			[SectionId]			[smallint]			NOT NULL
		CONSTRAINT [PK_FloodApplicationDocumentType_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		-- Create Constraints
		ALTER TABLE [Flood].[FloodApplicationDocumentType] ADD CONSTRAINT [FK_SectionId_FloodApplicationDocumentType]  FOREIGN KEY (SectionId) REFERENCES [Flood].FloodApplicationSection(Id);

		-- Drop Constraints
		IF OBJECT_ID('[Flood].[FloodApplicationCommentType]') IS NOT NULL
		BEGIN
			ALTER TABLE [Flood].[FloodApplicationCommentType] DROP CONSTRAINT IF EXISTS  [DF_FloodApplicationCommentType_Description];
	
			ALTER TABLE [Flood].[FloodApplicationCommentType] DROP CONSTRAINT IF EXISTS  [DF_FloodApplicationCommentType_IsActive];
		END;

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationCommentType];

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationCommentType](
			[Id]				[smallint] 			NOT NULL,
			[Title]				[varchar](128)		NOT NULL,
			[Description]		[varchar](512)		NULL,		
			[IsActive]			[bit]				NULL,
		CONSTRAINT [PK_FloodApplicationCommentType_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]


		-- Create Constraints
		ALTER TABLE [Flood].[FloodApplicationCommentType] WITH NOCHECK ADD  CONSTRAINT [DF_FloodApplicationCommentType_Description]  DEFAULT ('') FOR [Description]
 
		ALTER TABLE [Flood].[FloodApplicationCommentType] WITH NOCHECK ADD  CONSTRAINT [DF_FloodApplicationCommentType_IsActive]  DEFAULT (1) FOR [IsActive]

		IF OBJECT_ID('[Flood].[FloodApplicationFundingSourceType] ') IS NOT NULL

		BEGIN
			-- Drop Constraints	
			ALTER TABLE [Flood].[FloodApplicationFundingSourceType] DROP CONSTRAINT IF EXISTS  [DF_FloodApplicationFundingSourceType_IsActive];
		END;

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationFundingSourceType];


		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationFundingSourceType](
			[Id]                                    [integer]                              NOT NULL,
			[Title]                                 [varchar](216)			               NOT NULL,
			[SortOrder]                             [smallint]                             NOT NULL,
			[IsActive]								[bit]								   NULL
		CONSTRAINT [PK_FloodApplicationFundingSourceType_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		)ON [PRIMARY]


		-- Create Constraints
 
		ALTER TABLE [Flood].[FloodApplicationFundingSourceType] WITH NOCHECK ADD  CONSTRAINT [DF_FloodApplicationFundingSourceType_IsActive]  DEFAULT (1) FOR [IsActive]

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodParcelStatus]

		-- Create Table
		CREATE TABLE [Flood].[FloodParcelStatus](
			[Id]				[smallint] 			NOT NULL,
			[Name]				[varchar](128)		NOT NULL, 
		CONSTRAINT [PK_FloodParcelStatus_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]


		-- Drop Constraints
		IF OBJECT_ID('[Flood].[FloodParcelSection]') IS NOT NULL
		BEGIN
			ALTER TABLE [Flood].[FloodParcelSection] DROP CONSTRAINT IF EXISTS  [DF_FloodParcelSection_Description]
		END

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodParcelSection];
 
		-- Create Table
		CREATE TABLE [Flood].[FloodParcelSection](
			[Id]				[smallint] 			NOT NULL,
			[Title]				[varchar](128)		NOT NULL,
			[Description]		[varchar](512)		NULL,		
		CONSTRAINT [PK_FloodParcelSection_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]


		-- Create Constraint
		ALTER TABLE [Flood].[FloodParcelSection] WITH NOCHECK ADD  CONSTRAINT [DF_FloodParcelSection_Description]  DEFAULT ('') FOR [Description]

		-- Drop Constraints
		IF OBJECT_ID('[Flood].[FloodParcelDocumentType]') IS NOT NULL
		BEGIN
			ALTER TABLE [Flood].[FloodParcelDocumentType] DROP CONSTRAINT IF EXISTS  [FK_SectionId_FloodParcelDocumentType]
		END

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodParcelDocumentType];

		-- Create Table
		CREATE TABLE [Flood].[FloodParcelDocumentType](
			[Id]				[smallint] 			NOT NULL,
			[Title]				[varchar](128)		NOT NULL,
			[Description]		[varchar](512)		NULL	,
			[SectionId]			[smallint]			NOT NULL
		CONSTRAINT [PK_FloodParcelDocumentType_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]


		-- Create Constraints
		ALTER TABLE [Flood].[FloodParcelDocumentType] ADD CONSTRAINT [FK_SectionId_FloodParcelDocumentType]  FOREIGN KEY (SectionId) REFERENCES [Flood].FloodApplicationSection(Id);

		IF OBJECT_ID('[Flood].[FloodApplication]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodApplication] DROP CONSTRAINT IF EXISTS  [DF_CreatedByProgramAdmin_FloodApplication];

			ALTER TABLE [Flood].[FloodApplication] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplication];

			ALTER TABLE [Flood].[FloodApplication] DROP CONSTRAINT IF EXISTS  [DF_IsActive_FloodApplication];

		END;
 
 
		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplication]

		-- Create Table
		CREATE TABLE [Flood].[FloodApplication](
			[Id]						[integer] 		IDENTITY(1,1)	NOT NULL,
			[Title]						[varchar](256)					NOT NULL,
			[AgencyId]					[integer]						NOT NULL,
			[ApplicationTypeId]			[smallint]						NOT NULL,
			[ApplicationSubTypeId]		[smallint]						NOT NULL,
			[StatusId]					[smallint]						NOT NULL,
			[ExpirationDate]			[datetime]						NULL	,
			[CreatedByProgramAdmin]		[bit]							NOT NULL,
			[LastUpdatedBy]				[varchar](128)					NULL	,
			[LastUpdatedOn]				[datetime]						NOT NULL,
			[IsActive]					[bit]							NOT NULL,
	
		CONSTRAINT [PK_FloodApplication_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]


		-- Create Constraints
		ALTER TABLE [Flood].[FloodApplication] WITH NOCHECK ADD  CONSTRAINT [DF_CreatedByProgramAdmin_FloodApplication]  DEFAULT (0) FOR [CreatedByProgramAdmin]

		ALTER TABLE [Flood].[FloodApplication] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplication]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]

		ALTER TABLE [Flood].[FloodApplication] WITH NOCHECK ADD  CONSTRAINT [DF_IsActive_FloodApplication]  DEFAULT (1) FOR [IsActive]

 
		IF OBJECT_ID('[Flood].[FloodApplicationStatusLog]') IS NOT NULL
		BEGIN
			ALTER TABLE [Flood].[FloodApplicationStatusLog] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationStatusLog];

		END;
  
		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationStatusLog]

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationStatusLog](
			[ApplicationId]							[integer]						NOT NULL,
			[StatusId]								[integer]						NOT NULL,
			[StatusDate]							[datetime]						NULL,
			[Notes]									[varchar](max)					NOT NULL,
			[LastUpdatedBy]							[varchar](128)					NULL,
			[LastUpdatedOn]							[datetime]						NOT NULL)

		ALTER TABLE [Flood].[FloodApplicationStatusLog] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationStatusLog]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]

		IF OBJECT_ID('[Flood].[FloodApplicationComment]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodApplicationComment] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationComment];
	
			ALTER TABLE [Flood].[FloodApplicationComment] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationComment];

		END;
  
		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationComment]

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationComment](
			[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
			[ApplicationId]							[integer]						NOT NULL,
			[Comment]								[varchar](4000)					NOT NULL,
			[CommentTypeId]							[smallint]						NOT NULL,
			[LastUpdatedBy]							[varchar](128)					NULL	,
			[LastUpdatedOn]							[datetime]						NOT NULL,

		CONSTRAINT [PK_FloodApplicationComment_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]


		-- Create Constraints
		ALTER TABLE [Flood].[FloodApplicationComment] ADD CONSTRAINT [FK_ApplicationId_FloodApplicationComment]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);

		ALTER TABLE [Flood].[FloodApplicationComment] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationComment]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]

		IF OBJECT_ID('[Flood].[FloodApplicationFeedback]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodApplicationFeedback] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationFeedback];
	
			ALTER TABLE [Flood].[FloodApplicationFeedback] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationFeedback];

		END;
  
		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationFeedback]

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationFeedback](
			[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
			[ApplicationId]							[integer]						NOT NULL,
			[SectionId]								[smallint]						NOT NULL,
			[Feedback]								[varchar](4000)					NOT NULL,
			[RequestForCorrection]					[bit]							NOT NULL,
			[CorrectionStatus]						[varchar](100)					NOT NULL,
			[MarkRead]								[bit]							NOT NULL, 
			[LastUpdatedBy]							[varchar](128)					NULL	,
			[LastUpdatedOn]							[datetime]						NOT NULL,

		CONSTRAINT [PK_FloodApplicationFeedback_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]


		-- Create Constraints
		ALTER TABLE [Flood].[FloodApplicationFeedback] ADD CONSTRAINT [FK_ApplicationId_FloodApplicationFeedback]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);

		ALTER TABLE [Flood].[FloodApplicationFeedback] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationFeedback]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]

		IF OBJECT_ID('[Flood].[FloodApplicationUser]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodApplicationUser] DROP CONSTRAINT IF EXISTS  [DF_IsPrimaryContact_FloodApplicationUser];

			ALTER TABLE [Flood].[FloodApplicationUser] DROP CONSTRAINT IF EXISTS  [DF_IsAlternateContact_FloodApplicationUser];
			
			ALTER TABLE [Flood].[FloodApplicationUser] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationUser];

			ALTER TABLE [Flood].[FloodApplicationUser] DROP CONSTRAINT IF EXISTS   [DF_FirstName_FloodApplicationUser];  

			ALTER TABLE [Flood].[FloodApplicationUser] DROP CONSTRAINT IF EXISTS   [DF_LastName_FloodApplicationUser];  
		END;

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationUser]

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationUser](
			[Id]							[integer] 		IDENTITY(1,1)	NOT NULL,
			[ApplicationId]					[integer]						NOT NULL,
			[Email]							[varchar](128)					NULL	,
			[UserId]						[varchar](128)					NOT NULL,
			[UserName]						[varchar](128)					NOT NULL,
			[FirstName]						[varchar](128)					NULL,
			[LastName]						[varchar](128)					NULL,
			[Title]							[varchar](128)					NULL	,
			[PhoneNumber]					[varchar](15)					NULL	,
			[IsPrimaryContact]				[bit]							NOT NULL,
			[IsAlternateContact]			[bit]							NOT NULL,
			[LastUpdatedBy]					[varchar](128)					NULL	,
			[LastUpdatedOn]					[datetime]						NOT NULL,
		CONSTRAINT [PK_FloodRole_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]


		-- Create Constraint
		ALTER TABLE [Flood].[FloodApplicationUser] WITH NOCHECK ADD  CONSTRAINT [DF_IsPrimaryContact_FloodApplicationUser]  DEFAULT (0) FOR [IsPrimaryContact]

		ALTER TABLE [Flood].[FloodApplicationUser] WITH NOCHECK ADD  CONSTRAINT [DF_IsAlternateContact_FloodApplicationUser]  DEFAULT (0) FOR [IsAlternateContact]

		ALTER TABLE [Flood].[FloodApplicationUser] WITH NOCHECK ADD  CONSTRAINT [DF_FirstName_FloodApplicationUser]  DEFAULT ('') FOR [FirstName];

		ALTER TABLE [Flood].[FloodApplicationUser] WITH NOCHECK ADD  CONSTRAINT [DF_LastName_FloodApplicationUser]  DEFAULT ('') FOR [LastName];

		ALTER TABLE [Flood].[FloodApplicationUser] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationUser]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]

 
		IF OBJECT_ID('[Flood].[FloodApplicationOverview]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodApplicationOverview] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationOverview];
	
			ALTER TABLE [Flood].[FloodApplicationOverview] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationOverview];

		END;
		
  
		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationOverview]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationOverview](
			[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
			[ApplicationId]							[integer]						NOT NULL,

			[NoOfHomes]								[integer]						NULL,
			[NoOfContiguousHomes]					[integer]						NULL,

			[NatlDisaster]							[bit]							DEFAULT 0,
			[NatlDisasterId]						[varchar](256)							NULL	,	 
			[NatlDisasterName]						[varchar](256)					NULL	,	 
			[NatlDisasterYear]						[smallint] 						NULL	,
			[NatlDisasterMonth]						[smallint] 						NULL	,

			[LOI]									[bit]							DEFAULT 0,
			[LOIStatus]								[varchar](50)					NULL	,
			[LOIApprovedDate]						[datetime]						NULL	,

			[FEMA_OR_NJDEP_Applied]					[bit]							DEFAULT 0,

			[FEMAApplied]							[bit]							DEFAULT 0,
			[FEMAStatus]							[varchar](50)					NULL	,
			[FEMAApprovedDate]						[datetime]						NULL	,
			[FEMADenialReason]						[varchar](max)					NULL	,

			[GreenAcresApplied]						[bit]							DEFAULT 0,
			[GreenAcresStatus]						[varchar](50)					NULL	,
			[GreenAcresApprovedDate]				[datetime]						NULL	,

			[BlueAcresApplied]						[bit]							DEFAULT 0,
			[BlueAcresStatus]						[varchar](50)					NULL	,
			[BlueAcresApprovedDate]					[datetime]						NULL	,

			[FundingAgenciesApplied]				[bit]							DEFAULT 0,
 
			[LastUpdatedBy]							[varchar](128)					NULL	,
			[LastUpdatedOn]							[datetime]						NOT NULL,
	
	
		CONSTRAINT [PK_FloodApplicationOverview_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		-- Create Constraints
		ALTER TABLE [Flood].[FloodApplicationOverview] ADD CONSTRAINT [FK_ApplicationId_FloodApplicationOverview]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
		 

		ALTER TABLE [Flood].[FloodApplicationOverview] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationOverview]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		  

		IF OBJECT_ID('[Flood].[FloodApplicationFundingAgency]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodApplicationFundingAgency] DROP CONSTRAINT IF EXISTS  FK_ApplicationId_FloodApplicationFundingAgency;
		END;
		

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationFundingAgency]
		

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

		

		-- Create Constraint


		--ALTER TABLE [Flood].[FloodApplicationFundingAgency] ADD CONSTRAINT FK_ApplicationId_FloodApplicationFundingAgency  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
		  
		IF OBJECT_ID('[Flood].[FloodApplicationParcel]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodApplicationParcel] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationParcel];

			ALTER TABLE [Flood].[FloodApplicationParcel] DROP CONSTRAINT IF EXISTS  [DF_IsLocked_FloodApplicationParcel];
		END;
		

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationParcel]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationParcel](
	[ApplicationId]					[integer]						NOT NULL,
	[PamsPin]						[varchar](76)					NOT NULL,
	[StatusId]						[smallint]						NOT NULL,
	[IsLocked]						[bit]							NOT NULL,
	[IsSubmitted]                   [bit]                           DEFAULT 0,
	[IsApproved]                    [bit]                           DEFAULT 0,
	[WaitingApproved]               [bit]                           DEFAULT 0,
	[RejectedApproved]              [bit]                           DEFAULT 0

	)
		

		-- Create Constraint
		ALTER TABLE [Flood].[FloodApplicationParcel] ADD CONSTRAINT [FK_ApplicationId_FloodApplicationParcel]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].[FloodApplication](Id);
		 

		ALTER TABLE [Flood].[FloodApplicationParcel] WITH NOCHECK ADD  CONSTRAINT [DF_IsLocked_FloodApplicationParcel]  DEFAULT (0) FOR [IsLocked]
		
		IF OBJECT_ID('[Flood].[FloodApplicationFinance]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodApplicationFinance] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationFinance];
	
			ALTER TABLE [Flood].[FloodApplicationFinance] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationFinance];
		END;
		
  
		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationFinance]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationFinance](
			[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
			[ApplicationId]							[integer]						NOT NULL,

			[MatchPercent]							[decimal](18,2)					NOT NULL,
 
			[LastUpdatedBy]							[varchar](128)					NULL	,
			[LastUpdatedOn]							[datetime]						NOT NULL,
		
		CONSTRAINT [PK_FloodApplicationFinance_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		-- Create Constraints
		ALTER TABLE [Flood].[FloodApplicationFinance] ADD CONSTRAINT [FK_ApplicationId_FloodApplicationFinance]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
		 

		ALTER TABLE [Flood].[FloodApplicationFinance] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationFinance]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		  
 
		IF OBJECT_ID('[Flood].[FloodApplicationFinanceFund]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodApplicationFinanceFund] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationFinanceFund];
	
			ALTER TABLE [Flood].[FloodApplicationFinanceFund] DROP CONSTRAINT IF EXISTS  [FK_FundingSourceTypeId_FloodApplicationFinanceFund];

			ALTER TABLE [Flood].[FloodApplicationFinanceFund] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationFinanceFund];
		END;
		
  
		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationFinanceFund]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationFinanceFund](
			[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
			[ApplicationId]							[integer]						NOT NULL,

			[FundingSourceTypeId]                   [integer]                       NOT NULL,
			[Title]									[varchar](256)				    NULL    ,
			[Amount]								[decimal](18,2)					NOT NULL,
			[AwardDate]								[datetime]						NULL    ,

			[LastUpdatedBy]							[varchar](128)					NULL,
			[LastUpdatedOn]							[datetime]						NOT NULL,
		
		CONSTRAINT [PK_FloodApplicationFinanceFund_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		-- Create Constraints
		--ALTER TABLE [Flood].[FloodApplicationFinanceFund] ADD CONSTRAINT [FK_ApplicationId_FloodApplicationFinanceFund]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
		

		ALTER TABLE [Flood].[FloodApplicationFinanceFund] ADD CONSTRAINT [FK_FundingSourceTypeId_FloodApplicationFinanceFund]  FOREIGN KEY (FundingSourceTypeId) REFERENCES [Flood].FloodApplicationFundingSourceType(Id);
		

		ALTER TABLE [Flood].[FloodApplicationFinanceFund] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationFinanceFund]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		  

		IF OBJECT_ID('[Flood].[FloodApplicationSignatory]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodApplicationSignatory] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationSignatory];
		
			ALTER TABLE [Flood].[FloodApplicationSignatory] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationSignatory];
		END;
		

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationSignatory]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationSignatory](
			[Id]							[integer] 		IDENTITY(1,1)	NOT NULL,
			[ApplicationId]					[integer]						NOT NULL,
			[Designation]					[varchar](128)					NULL	,
			[Title]							[varchar](128)					NULL	, 
			[SignedOn]						[datetime]						NULL	, 
			[SignatoryType]					[varchar](128)					NOT NULL,
			[LastUpdatedBy]					[varchar](128)					NULL	, 
			[LastUpdatedOn]					[datetime]						NOT NULL, 
		CONSTRAINT [PK_FloodApplicationSignatory_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		-- Create Constraint
		--ALTER TABLE [Flood].[FloodApplicationSignatory] ADD CONSTRAINT [FK_ApplicationId_FloodApplicationSignatory]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
		 

		ALTER TABLE [Flood].[FloodApplicationSignatory] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationSignatory]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		  

		IF OBJECT_ID('[Flood].[FloodApplicationDocument]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodApplicationDocument] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationDocument];
		
			ALTER TABLE [Flood].[FloodApplicationDocument] DROP CONSTRAINT IF EXISTS  [FK_DocumentTypeId_FloodApplicationDocument];

			ALTER TABLE [Flood].[FloodApplicationDocument] DROP CONSTRAINT IF EXISTS  [DF_ShowCommittee_FloodApplicationDocument];
	
			ALTER TABLE [Flood].[FloodApplicationDocument] DROP CONSTRAINT IF EXISTS  [DF_UseInReport_FloodApplicationDocument];
	
			ALTER TABLE [Flood].[FloodApplicationDocument] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationDocument];
		END;
		

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationDocument]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationDocument](
			[Id]						[integer] 		IDENTITY(1,1)	NOT NULL,
			[ApplicationId]				[integer]						NOT NULL,
			[DocumentTypeId]			[smallint]						NOT NULL,
			[OtherFundingSourceId]		[integer]						NULL,
			[FileName]					[varchar](128)					NOT NULL,
			[Title]						[varchar](128)					NOT NULL,
			[Description]				[varchar](256)					NULL,
			[ShowCommittee]				[bit]							NOT NULL,
			[UseInReport]				[bit]							NOT NULL,
			[HardCopy]					[bit]							NOT NULL,
			[Approved]					[bit]							NOT NULL,
			[ReviewComment]				[varchar](2000)					NULL,
			[LastUpdatedBy]				[varchar](128)					NULL,
			[LastUpdatedOn]				[datetime]						NOT NULL,
		CONSTRAINT [PK_FloodApplicationDocument_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		-- Create Constraint
		ALTER TABLE [Flood].[FloodApplicationDocument] ADD CONSTRAINT FK_DocumentTypeId_FloodApplicationDocument  FOREIGN KEY (DocumentTypeId) REFERENCES [Flood].FloodApplicationDocumentType(Id);
		 

		ALTER TABLE [Flood].[FloodApplicationDocument] ADD CONSTRAINT FK_ApplicationId_FloodApplicationDocument  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
		  

		ALTER TABLE [Flood].[FloodApplicationDocument] WITH NOCHECK ADD  CONSTRAINT [DF_ShowCommittee_FloodApplicationDocument]  DEFAULT (0) FOR [ShowCommittee]
		  

		ALTER TABLE [Flood].[FloodApplicationDocument] WITH NOCHECK ADD  CONSTRAINT [DF_UseInReport_FloodApplicationDocument]  DEFAULT (0) FOR [UseInReport]
		  

		ALTER TABLE [Flood].[FloodApplicationDocument] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationDocument]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		  
  

		 IF OBJECT_ID('[Flood].[FloodApplicationBrokenRules]') IS NOT NULL
		BEGIN
			-- Drop Constraint
			ALTER TABLE [Flood].[FloodApplicationBrokenRules] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationBrokenRules];
			ALTER TABLE [Flood].[FloodApplicationBrokenRules] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationBrokenRules];
			DROP INDEX IF EXISTS [IX_FloodApplicationBrokenRules_ApplicationId_SectionId] ON [Flood].[FloodApplicationBrokenRules];
		END
		



		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationBrokenRules];
		

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationBrokenRules](
			[ApplicationId]		[integer]						NOT NULL,
			[SectionId]			[integer]						NOT NULL,
			[Message]			[varchar](1024)					NOT NULL,
			[IsApplicantFlow]	[bit]							NOT NULL,
			[LastUpdatedOn]		[datetime]						NOT NULL,
		) ON [PRIMARY]

		

		-- Create a clustered index  
		CREATE CLUSTERED INDEX IX_FloodApplicationBrokenRules_ApplicationId_SectionId ON [Flood].[FloodApplicationBrokenRules] (ApplicationId, SectionId); 
		
 
		-- Create Constraint
		ALTER TABLE [Flood].[FloodApplicationBrokenRules] ADD CONSTRAINT FK_ApplicationId_FloodApplicationBrokenRules  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
		  

		ALTER TABLE [Flood].[FloodApplicationBrokenRules] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationBrokenRules]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		  
		IF OBJECT_ID('[Flood].[FloodContacts]') IS NOT NULL
		BEGIN

			ALTER TABLE [Flood].[FloodContacts] DROP CONSTRAINT IF EXISTS [FK_ApplicationId_FloodContacts];

			ALTER TABLE [Flood].[FloodContacts] DROP CONSTRAINT IF EXISTS [DF_LastUpdatedOn_FloodContacts];

	
		END;
		

		DROP TABLE IF EXISTS [Flood].[FloodContacts]
		

		CREATE TABLE [Flood].[FloodContacts](
			[Id]					[integer]			IDENTITY(1,1)	NOT NULL,
			[ApplicationId]			[integer]							NOT NULL,
			[ContactName]			[varchar](76)						NULL,
			[Agency]				[varchar](128)						NULL,
			[Email]					[varchar](128)						NULL,
			[MainNumber]			[varchar](128)						NULL,
			[AlternateNumber]		[varchar](128)						NULL,
			[SelectContact]			[bit]								NULL,
			[LastUpdatedBy]			[varchar](128)						NULL	,
			[LastUpdatedOn]			[datetime]							NOT NULL,
		CONSTRAINT [PK_FloodContacts_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		ALTER TABLE [Flood].[FloodContacts] ADD CONSTRAINT [FK_ApplicationId_FloodContacts]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].[FloodApplication](Id);
		

		ALTER TABLE [Flood].[FloodContacts] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodContacts]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		
		IF OBJECT_ID('[Flood].[FloodApplicationPayment]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodApplicationPayment] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationPayment];

			ALTER TABLE [Flood].[FloodApplicationPayment] DROP CONSTRAINT IF EXISTS  [FK_CAFClosed_FloodApplicationPayment];

			ALTER TABLE [Flood].[FloodApplicationPayment] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationPayment];

		END;
		


		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationPayment]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationPayment](
		[Id]	                                [integer]     IDENTITY(1,1)               NOT NULL,
		[ApplicationId]                         [integer] 								  NOT NULL,
		[CAFNumber]                             [varchar](128)                            NOT NULL,
		[CAFClosed]                             [bit]                                     NOT NULL,
		[LastUpdatedBy]							[varchar](128)					          NULL,
		[LastUpdatedOn]							[datetime]						          NOT NULL,
		CONSTRAINT [PK_FloodApplicationPayment_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		)ON [PRIMARY]

		

		-- Create Constraints
		--ALTER TABLE [Flood].[FloodApplicationPayment] ADD CONSTRAINT FK_ApplicationId_FloodApplicationPayment  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
		

		ALTER TABLE [Flood].[FloodApplicationPayment] WITH NOCHECK ADD  CONSTRAINT [DF_CAFClosed_FloodApplicationPayment]  DEFAULT (0) FOR [CAFClosed]
		

		ALTER TABLE [Flood].[FloodApplicationPayment] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationPayment]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		
		IF OBJECT_ID('[Flood].[FloodApplicationAdminDetails]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodApplicationAdminDetails] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationAdminDetails];

			ALTER TABLE [Flood].[FloodApplicationAdminDetails] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationAdminDetails];

		END;
		


		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationAdminDetails]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationAdminDetails](
		[Id]	                                [integer]     IDENTITY(1,1)             NOT NULL,
		[ApplicationId]                         [integer] 								NOT NULL,
		[MunicipalResolutionDate]				[datetime]								NULL,
		[MunicipalResolutionNumber]             [varchar](128)  						NULL,
		[FMCPreliminaryApprovalDate]			[datetime]								NULL,
		[FMCPreliminaryNumber]                  [varchar](128)							NULL,
		[BCCPreliminaryApprovalDate]            [datetime]								NULL,
		[BCCPreliminaryNumber]                  [varchar](128)							NULL,
		[ProjectDescription]                    [varchar](4000)							NULL,
		[FundingExpirationDate]                 [datetime]								NULL,
		[FirstFundingExpirationDate]            [datetime]								NULL,
		[SecondFundingExpirationDate]           [datetime]								NULL,
		[CommissionerMeetingDate]               [datetime]								NULL,
		[FirstCommitteeMeetingDate]             [datetime]								NULL,
		[SecondCommitteeMeetingDate]            [datetime]								NULL,
		[LastUpdatedBy]							[varchar](128)					        NULL,
		[LastUpdatedOn]							[datetime]						        NOT NULL,
		CONSTRAINT [PK_FloodApplicationAdminDetails_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		)ON [PRIMARY]

		

		-- Create Constraints
		ALTER TABLE [Flood].[FloodApplicationAdminDetails] ADD CONSTRAINT FK_ApplicationId_FloodApplicationAdminDetails  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
		

		ALTER TABLE [Flood].[FloodApplicationAdminDetails] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationAdminDetails]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		

		IF OBJECT_ID('[Flood].[FloodParcel]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodParcel] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcel];

			ALTER TABLE [Flood].[FloodParcel] DROP CONSTRAINT IF EXISTS  [DF_IsValidPamsPin_FloodParcel];

			ALTER TABLE [Flood].[FloodParcel] DROP CONSTRAINT IF EXISTS  [DF_IsActive_FloodParcel];

		END;
		

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodParcel]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodParcel](
			[Id]						[integer] 			IDENTITY(1,1)		NOT NULL,
			[PamsPin]					[varchar](76)							NOT NULL,
			[AgencyID]					[varchar](8)							NULL,
			[Block]						[varchar](20)							NULL,
			[Lot]						[varchar](20)							NULL,
			[QualificationCode]			[varchar](22)							NULL,
			[Latitude]					[varchar](50)							NULL,
			[Longitude]					[varchar](50)							NULL,
			[StreetNo]					[varchar](50)							NULL,
			[StreetAddress]				[varchar](50)							NULL,
			[Acreage]					[decimal](18,4)							NULL,
			[OwnersName]				[varchar](70)							NULL,
			[OwnersAddress1]			[varchar](128)							NULL,
			[OwnersAddress2]			[varchar](128)							NULL,
			[OwnersCity]				[varchar](128)							NULL,
			[OwnersState]				[varchar](128)							NULL,
			[OwnersZipcode]				[varchar](20)							NULL,
			[SquareFootage]				[decimal](18,2)							NULL,
			[YearOfConstruction]		[smallint]								NULL,
			[TotalAssessedValue]		[decimal](18,2)							NULL,
			[LandValue]					[decimal](18,2)							NULL,
			[ImprovementValue]			[decimal](18,2)							NULL,
			[AnnualTaxes]				[decimal](18,2)							NULL,
			[TargetAreaId]				[integer]								NULL,
			[DateOfFLAP]				[datetime]								NULL,
			[IsValidPamsPin]			[bit]									NOT NULL,
			[LastUpdatedBy]				[varchar](128)							NULL,
			[LastUpdatedOn]				[datetime]								NOT NULL,
			[IsActive]					[bit]									NOT NULL,
	        [IsElevated]				[bit]									NULL,

		CONSTRAINT [PK_FloodParcel_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		ALTER TABLE [Flood].[FloodParcel] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcel]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		  

		ALTER TABLE [Flood].[FloodParcel] WITH NOCHECK ADD  CONSTRAINT [DF_IsValidPamsPin_FloodParcel]  DEFAULT (0) FOR [IsValidPamsPin]
		  

		ALTER TABLE [Flood].[FloodParcel] WITH NOCHECK ADD  CONSTRAINT [DF_IsActive_FloodParcel]  DEFAULT (1) FOR [IsActive]
		  

		IF OBJECT_ID('[Flood].[FloodLockedParcel]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodLockedParcel] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodLockedParcel];

			ALTER TABLE [Flood].[FloodLockedParcel] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodLockedParcel];

			ALTER TABLE [Flood].[FloodLockedParcel] DROP CONSTRAINT IF EXISTS  [DF_IsValidPamsPin_FloodLockedParcel];

			ALTER TABLE [Flood].[FloodLockedParcel] DROP CONSTRAINT IF EXISTS  [DF_IsActive_FloodLockedParcel];

		END;
		

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodLockedParcel]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodLockedParcel](
			[Id]						[integer] 			IDENTITY(1,1)			NOT NULL,
			[ApplicationId]				[integer]									NOT NULL,
			[PamsPin]					[varchar](76)								NOT NULL,
			[AgencyID]					[varchar](8)								NULL,
			[Block]						[varchar](20)								NULL,
			[Lot]						[varchar](20)								NULL,
			[QualificationCode]			[varchar](22)								NULL,
			[Latitude]					[varchar](50)								NULL,
			[Longitude]					[varchar](50)								NULL,
			[StreetNo]					[varchar](50)								NULL,
			[StreetAddress]				[varchar](50)								NULL,
			[Acreage]					[decimal](18,4)								NULL,
			[OwnersName]				[varchar](70)								NULL,
			[OwnersAddress1]			[varchar](128)								NULL,
			[OwnersAddress2]			[varchar](128)								NULL,
			[OwnersCity]				[varchar](128)								NULL,
			[OwnersState]				[varchar](128)								NULL,
			[OwnersZipcode]				[varchar](20)								NULL,
			[SquareFootage]				[decimal](18,2)								NULL,
			[YearOfConstruction]		[smallint]									NULL,
			[TotalAssessedValue]		[decimal](18,2)								NULL,
			[LandValue]					[decimal](18,2)								NULL,
			[ImprovementValue]			[decimal](18,2)								NULL,
			[AnnualTaxes]				[decimal](18,2)								NULL,
			[IsValidPamsPin]			[bit]										NOT NULL,
			[TargetAreaId]				[integer]									NULL,
			[DateOfFLAP]				[datetime]									NULL,
			[IsElevated]				[bit]										NULL,
			[IsActive]					[bit]										NOT NULL,
			[LastUpdatedBy]				[varchar](128)								NULL,
			[LastUpdatedOn]				[datetime]									NOT NULL,
	
		CONSTRAINT [PK_FloodLockedParcel_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]
		

		ALTER TABLE [Flood].[FloodLockedParcel] ADD CONSTRAINT [FK_ApplicationId_FloodLockedParcel]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
		 

		ALTER TABLE [Flood].[FloodLockedParcel] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodLockedParcel]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		  

		ALTER TABLE [Flood].[FloodLockedParcel] WITH NOCHECK ADD  CONSTRAINT [DF_IsValidPamsPin_FloodLockedParcel]  DEFAULT (0) FOR [IsValidPamsPin]
		  

		ALTER TABLE [Flood].[FloodLockedParcel] WITH NOCHECK ADD  CONSTRAINT [DF_IsActive_FloodLockedParcel]  DEFAULT (1) FOR [IsActive]
		  

		IF OBJECT_ID('[Flood].[FloodParcelStatusLog]') IS NOT NULL
		BEGIN
			ALTER TABLE [Flood].[FloodParcelStatusLog] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelStatusLog];

		END;
		
  
		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodParcelStatusLog]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodParcelStatusLog](
			[ApplicationId]							[integer]						NOT NULL,
			[PamsPin]								[varchar](76)					NOT NULL,
			[StatusId]								[integer]						NOT NULL,
			[StatusDate]							[datetime]						NULL,
			[Notes]									[varchar](max)					NOT NULL,
			[LastUpdatedBy]							[varchar](128)					NULL,
			[LastUpdatedOn]							[datetime]						NOT NULL)
		

		ALTER TABLE [Flood].[FloodParcelStatusLog] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelStatusLog]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		  

  

		 IF OBJECT_ID('[Flood].[FloodParcelComment]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodParcelComment] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelComment];
	
			ALTER TABLE [Flood].[FloodParcelComment] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelComment];

		END;
		
  
		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodParcelComment]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodParcelComment](
			[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
			[ApplicationId]							[integer]						NOT NULL,
			[PamsPin]								[varchar](76)					NOT NULL,
			[Comment]								[varchar](4000)					NOT NULL,
			[CommentTypeId]							[smallint]						NOT NULL,
			[LastUpdatedBy]							[varchar](128)					NULL	,
			[LastUpdatedOn]							[datetime]						NOT NULL,
	
	
		CONSTRAINT [PK_FloodParcelComment_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		-- Create Constraints
		ALTER TABLE [Flood].[FloodParcelComment] ADD CONSTRAINT [FK_ApplicationId_FloodParcelComment]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
		 

		ALTER TABLE [Flood].[FloodParcelComment] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelComment]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		  
		IF OBJECT_ID('[Flood].[FloodParcelFeedback]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodParcelFeedback] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelFeedback];
	
			ALTER TABLE [Flood].[FloodParcelFeedback] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelFeedback];

		END;
		
  
		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodParcelFeedback]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodParcelFeedback](
			[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
			[ApplicationId]							[integer]						NOT NULL,
			[PamsPin]								[varchar](76)					NOT NULL,
			[SectionId]								[smallint]						NOT NULL,
			[Feedback]								[varchar](4000)					NOT NULL,
			[RequestForCorrection]					[bit]							NOT NULL,
			[CorrectionStatus]						[varchar](100)					NOT NULL,
			[MarkRead]								[bit]							NOT NULL, 
			[LastUpdatedBy]							[varchar](128)					NULL	,
			[LastUpdatedOn]							[datetime]						NOT NULL,
	
	
		CONSTRAINT [PK_FloodParcelFeedback_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		-- Create Constraints
		ALTER TABLE [Flood].[FloodParcelFeedback] ADD CONSTRAINT [FK_ApplicationId_FloodParcelFeedback]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
		 

		ALTER TABLE [Flood].[FloodParcelFeedback] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelFeedback]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		  

		IF OBJECT_ID('[Flood].[FloodParcelProperty]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodParcelProperty] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelProperty];

		END;
		

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodParcelProperty]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodParcelProperty](
			[Id]						[integer] 			IDENTITY(1,1)		NOT NULL,
			[ApplicationId]				[integer]								NOT NULL,
			[PamsPin]					[varchar](76)							NOT NULL,
			[Priority]			        [integer]								NOT NULL,
			[ValueEstimate]				[decimal](18,2)				            NULL,
			[EstimatedPurchasePrice]	[decimal](18,2)				            NULL,
			[BRV]						[decimal](18,2)							NULL,
			[NfipPolicyNo]				[varchar](128)							NULL,
			[SourceOfValueEstimate]		[varchar](128)							NULL,
			[FirstFloorElevation]		[decimal](18,2)							NULL,
			[StructureType]			    [integer]								NULL,
			[FoundationType]			[integer]								NULL,
			[OccupancyClass]			[integer]								NULL,
			[PercentageOfDamage]		[decimal](18,2)							NULL,
			[HasContaminants]			[bit]									NULL,
			[IsLowIncomeHousing]		[bit]									NULL,
			[HasHistoricSignificance]	[bit]									NULL,
			[IsRentalProperty]			[bit]									NULL,
			[RentPerMonth]				[decimal](18,2)							NULL,
			[NeedSoftCost]				[bit]									NULL,
			[IsPreIrenePropertyOwner]	[bit]									NULL,
			[LastUpdatedBy]				[varchar](128)							NULL,
			[LastUpdatedOn]				[datetime]								NOT NULL
	
		CONSTRAINT [PK_FloodParcelProperty_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		ALTER TABLE [Flood].[FloodParcelProperty] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelProperty]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		


		IF OBJECT_ID('[Flood].[FloodApplicationFinanceLineItems]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodApplicationFinanceLineItems] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationFinanceLineItems];

			ALTER TABLE [Flood].[FloodApplicationFinanceLineItems] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationFinanceLineItems];

		END;
		


		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationFinanceLineItems]
		


		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationFinanceLineItems](
		[Id]	                                [integer]     IDENTITY(1,1)               NOT NULL,
		[ApplicationId]                         [integer] 								  NOT NULL,
		[PamsPin]					            [nvarchar](76)							  NOT NULL,
		[ValueEstimate]				            [decimal](18,2)				              NULL    ,
		[LastUpdatedBy]							[varchar](128)					          NULL	  ,
		[LastUpdatedOn]							[datetime]						          NOT NULL,
		CONSTRAINT [PK_FloodApplicationFinanceLineItems_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		)ON [PRIMARY]

		

		-- Create Constraints
		ALTER TABLE [Flood].[FloodApplicationFinanceLineItems] ADD CONSTRAINT FK_ApplicationId_FloodApplicationFinanceLineItems  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
		

		ALTER TABLE [Flood].[FloodApplicationFinanceLineItems] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationFinanceLineItems]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodParcelSoftCostType]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodParcelSoftCostType](
			[Id]						[integer] 			IDENTITY(1,1)		NOT NULL,
			[Title]						[varchar](128) 							NOT NULL,
	
		CONSTRAINT [PK_FloodParcelSoftCostType_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		IF OBJECT_ID('[Flood].[FloodParcelSoftCost]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodParcelSoftCost] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelSoftCost];

			ALTER TABLE [Flood].[FloodParcelSoftCost] DROP CONSTRAINT IF EXISTS  [FK_SoftCostTypeId_FloodParcelSoftCost];

			ALTER TABLE [Flood].[FloodParcelSoftCost] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelSoftCost];

		END;
		

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodParcelSoftCost]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodParcelSoftCost](
			[Id]						[integer] 			IDENTITY(1,1)		NOT NULL,
			[ApplicationId]				[integer]								NOT NULL,
			[PamsPin]					[varchar](76)							NOT NULL,
			[SoftCostTypeId]			[integer]								NOT NULL,
			[VendorName]				[varchar](256)							NOT NULL,
			[InvoiceAmount]				[decimal](18,2)							NOT NULL,
			[PaymentAmount]				[decimal](18,2)							NOT NULL,
			[LastUpdatedBy]				[varchar](128)							NULL	,
			[LastUpdatedOn]				[datetime]								NOT NULL,
	
		CONSTRAINT [PK_FloodParcelSoftCost_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		ALTER TABLE [Flood].[FloodParcelSoftCost] ADD CONSTRAINT [FK_ApplicationId_FloodParcelSoftCost]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].[FloodApplication](Id);
		

		ALTER TABLE [Flood].[FloodParcelSoftCost] ADD CONSTRAINT [FK_SoftCostTypeId_FloodParcelSoftCost]  FOREIGN KEY (SoftCostTypeId) REFERENCES [Flood].[FloodParcelSoftCostType](Id);
		

		ALTER TABLE [Flood].[FloodParcelSoftCost] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelSoftCost]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		

		IF OBJECT_ID('[Flood].[FloodParcelFinance]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodParcelFinance] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelFinance];

			ALTER TABLE [Flood].[FloodParcelFinance] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelFinance];

		END;
		


		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodParcelFinance]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodParcelFinance](
		[Id]	                                [integer]     IDENTITY(1,1)               NOT NULL,
		[ApplicationId]                         [integer] 								  NOT NULL,
		[PamsPin]					            [varchar](76)							  NOT NULL,
		[EstimatePurchasePrice]                 [decimal](18,2)					          NOT NULL,
		[AdditionalSoftCostEstimate]            [decimal](18,2)					          NOT NULL,
		[AppraisedValue]                        [decimal](18,2)					          NULL    ,
		[AMV]                                   [decimal](18,2)					          NULL    ,
		[TotalFEMABenifits]                     [decimal](18,2)					          NULL    ,
		[DOBAffidavitType]						[varchar](128)					          NULL    ,
		[DOBAffidavitAmt]						[decimal](18,2)					          NULL    ,
		[HardCostFMPAmt]						[decimal](18,2)					          NULL    ,
		[SoftCostFMPAmt]                        [decimal](18,2)					          NULL    ,
		[AppraisersFee]                         [decimal](18,2)					          NULL    ,
		[SurveyorsFee]                          [decimal](18,2)					          NULL    ,
		[LastUpdatedBy]							[varchar](128)					          NULL	  ,
		[LastUpdatedOn]							[datetime]						          NOT NULL,
		CONSTRAINT [PK_FloodParcelFinance_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		)ON [PRIMARY]

		

		-- Create Constraints
		ALTER TABLE [Flood].[FloodParcelFinance] ADD CONSTRAINT FK_ApplicationId_FloodParcelFinance  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
		

		ALTER TABLE [Flood].[FloodParcelFinance] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelFinance]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		
		IF OBJECT_ID('[Flood].[FloodParcelTech]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodParcelTech] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelTech];
		

			ALTER TABLE [Flood].[FloodParcelTech] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelTech];

		END;
		

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodParcelTech]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodParcelTech](
			[Id]												[integer] 			IDENTITY(1,1)			NOT NULL,
			[ApplicationId]										[integer]									NOT NULL,
			[PamsPin]											[varchar](76)								NOT NULL,
			[FEMASevereRepetitiveLossList]                      [bit]										NULL	,
			[FEMARepetitiveLossList]                            [bit]										NULL	,
			[IsthepropertywithinthePassaicRiverBasin]           [bit]										NULL	,
			[IsthepropertywithinFloodway]                       [bit]										NULL	,
			[IsthepropertywithinFloodplain]                     [bit]										NULL	,
			[Claim10Years]                                      [integer]									NULL	,
			[TotalOfClaims]                                     [decimal](18,2)								NULL	,
			[BenefitCostRatio]                                  [decimal](18,2)								NULL	,
			[FEMACommunityId]                                   [varchar](256)							    NULL	,
			[FirmEffectiveDate]                                 [datetime]									NULL	,
			[FirmPanel]                                         [varchar](256)								NULL	,
			[FirmPanelFinal]                                    [varchar](256)								NULL	,
			[FloodZoneDesignation]                              [varchar](256)								NULL	,
			[BaseFloodElevation]                                [varchar](256)								NULL	,
			[BaseFloodElevationFinal]                           [varchar](256)								NULL	,
			[RiverId]                                           [varchar](256)								NULL	,
			[RiverIdFinal]										[varchar](256)								NULL	,
			[FisEffectiveDate]                                  [datetime]									NULL	,
			[FloodProfile]                                      [varchar](256)								NULL	,
			[FloodProfileFinal]                                 [varchar](256)								NULL	,
			[FloodSource]                                       [varchar](256)								NULL	,
			[FirstFloodElevation]                               [varchar](256)								NULL	,
			[FirstFloodElevationFinal]                          [varchar](256)								NULL	,
			[StreambedElevation]                                [varchar](256)								NULL	,
			[StreambedElevationFinal]                           [varchar](256)								NULL	,
			[ElevationBeforeMitigation]                         [varchar](256)								NULL	,
			[ElevationBeforeMitigationFinal]                    [varchar](256)								NULL	,
			[FloodType]                                         [varchar](256)								NULL	,
			[TenPercent]                                        [integer]									NULL	,
			[TwoPercent]                                        [integer]									NULL	,
			[OnePercent]                                        [integer]									NULL	,
			[PointOnePercent]									[integer]									NULL	,
			[LastUpdatedBy]										[varchar](128)								NULL	,
			[LastUpdatedOn]										[datetime]									NOT NULL,
	
		CONSTRAINT [PK_FloodParcelTech_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		ALTER TABLE [Flood].[FloodParcelTech] ADD CONSTRAINT [FK_ApplicationId_FloodParcelTech]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].[FloodApplication](Id);
		

		ALTER TABLE [Flood].[FloodParcelTech] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelTech]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		

		IF OBJECT_ID('[Flood].[FloodParcelPayment]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodParcelPayment] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelPayment];

			ALTER TABLE [Flood].[FloodParcelPayment] DROP CONSTRAINT IF EXISTS  [FK_HardCostPaymentStatusId_FloodParcelPayment];

			ALTER TABLE [Flood].[FloodParcelPayment] DROP CONSTRAINT IF EXISTS  [FK_SoftCostPaymentStatusId_FloodParcelPayment];

			ALTER TABLE [Flood].[FloodParcelPayment] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelPayment];

		END;
		


		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodParcelPayment]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodParcelPayment](
		[Id]	                                [integer]     IDENTITY(1,1)               NOT NULL,
		[ApplicationId]                         [integer] 								  NOT NULL,
		[PamsPin]					            [varchar](76)							  NOT NULL,
		[HardCostPaymentTypeId]                 [integer]                                 NULL    ,
		[HardCostPaymentDate]                   [datetime]                                NULL    ,
		[HardCostPaymentStatusId]               [bit]                                     NOT NULL,
		[SoftCostPaymentTypeId]                 [integer]                                 NULL    ,
		[SoftCostPaymentDate]                   [datetime]                                NULL    ,
		[SoftCostPaymentStatusId]               [bit]                                     NOT NULL,
		[LastUpdatedBy]							[varchar](128)					          NULL	  ,
		[LastUpdatedOn]							[datetime]						          NOT NULL,
		CONSTRAINT [PK_FloodParcelPayment_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		)ON [PRIMARY]

		

		-- Create Constraints
		ALTER TABLE [Flood].[FloodParcelPayment] ADD CONSTRAINT FK_ApplicationId_FloodParcelPayment  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
		

		ALTER TABLE [Flood].[FloodParcelPayment] WITH NOCHECK ADD  CONSTRAINT [DF_HardCostPaymentStatusId_FloodParcelPayment]  DEFAULT (0) FOR [HardCostPaymentStatusId]
		

		ALTER TABLE [Flood].[FloodParcelPayment] WITH NOCHECK ADD  CONSTRAINT [DF_SoftCostPaymentStatusId_FloodParcelPayment]  DEFAULT (0) FOR [SoftCostPaymentStatusId]
		

		ALTER TABLE [Flood].[FloodParcelPayment] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelPayment]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		

		IF OBJECT_ID('[Flood].[FloodParcelDocument]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodParcelDocument] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelDocument];
		
			ALTER TABLE [Flood].[FloodParcelDocument] DROP CONSTRAINT IF EXISTS  [FK_DocumentTypeId_FloodParcelDocument];

			ALTER TABLE [Flood].[FloodParcelDocument] DROP CONSTRAINT IF EXISTS  [DF_ShowCommittee_FloodParcelDocument];
	
			ALTER TABLE [Flood].[FloodParcelDocument] DROP CONSTRAINT IF EXISTS  [DF_UseInReport_FloodParcelDocument];
	
			ALTER TABLE [Flood].[FloodParcelDocument] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelDocument];
		END;
		

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodParcelDocument]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodParcelDocument](
			[Id]				[integer] 		IDENTITY(1,1)	NOT NULL,
			[ApplicationId]		[integer]						NOT NULL,
			[PamsPin]			[varchar](76)					NOT NULL,
			[DocumentTypeId]	[smallint]						NOT NULL,
			[FileName]			[varchar](128)					NOT NULL,
			[Title]				[varchar](128)					NOT NULL,
			[Description]		[varchar](256)					NULL	,
			[ShowCommittee]		[bit]							NOT NULL,
			[UseInReport]		[bit]							NOT NULL,
			[HardCopy]			[bit]							NOT NULL,
			[Approved]			[bit]							NOT NULL,
			[ReviewComment]		[varchar](2000)					NULL	,
			[LastUpdatedBy]		[varchar](128)					NULL	,
			[LastUpdatedOn]		[datetime]						NOT NULL,
		CONSTRAINT [PK_FloodParcelDocument_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		-- Create Constraint
		ALTER TABLE [Flood].[FloodParcelDocument] ADD CONSTRAINT FK_DocumentTypeId_FloodParcelDocument  FOREIGN KEY (DocumentTypeId) REFERENCES [Flood].FloodParcelDocumentType(Id);
		 

		ALTER TABLE [Flood].[FloodParcelDocument] ADD CONSTRAINT FK_ApplicationId_FloodParcelDocument  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
		  

		ALTER TABLE [Flood].[FloodParcelDocument] WITH NOCHECK ADD  CONSTRAINT [DF_ShowCommittee_FloodParcelDocument]  DEFAULT (0) FOR [ShowCommittee]
		  

		ALTER TABLE [Flood].[FloodParcelDocument] WITH NOCHECK ADD  CONSTRAINT [DF_UseInReport_FloodParcelDocument]  DEFAULT (0) FOR [UseInReport]
		  

		ALTER TABLE [Flood].[FloodParcelDocument] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelDocument]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		  
  

		 IF OBJECT_ID('[Flood].[FloodParcelSurvey]') IS NOT NULL
		BEGIN
			ALTER TABLE [Flood].[FloodParcelSurvey] DROP CONSTRAINT IF EXISTS [FK_ApplicationId_FloodParcelSurvey];

			ALTER TABLE [Flood].[FloodParcelSurvey] DROP CONSTRAINT IF EXISTS [DF_LastUpdatedOn_FloodParcelSurvey];
		END;
		

		DROP TABLE IF EXISTS [Flood].[FloodParcelSurvey]
		

		CREATE TABLE [Flood].[FloodParcelSurvey](
			[Id]					[integer]			IDENTITY(1,1)	NOT NULL,
			[ApplicationId]			[integer]							NOT NULL,
			[PamsPin]				[varchar](76)						NOT NULL,
			[Surveyor]				[varchar](256)						NULL,
			[SurveyDate]			[datetime]							NULL,
			[LastRevision]			[datetime]							NULL,
			[DateCorrected]			[datetime]							NULL,
			[LastUpdatedBy]			[varchar](128)						NULL,
			[LastUpdatedOn]			[datetime]							NOT NULL

		CONSTRAINT [PK_FloodParcelSurvey_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		ALTER TABLE [Flood].[FloodParcelSurvey] ADD CONSTRAINT [FK_ApplicationId_FloodParcelSurvey]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].[FloodApplication](Id);
		

		ALTER TABLE [Flood].[FloodParcelSurvey] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelSurvey]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		

		IF OBJECT_ID('[Flood].[FloodParcelTracking]') IS NOT NULL
		BEGIN
			ALTER TABLE [Flood].[FloodParcelTracking] DROP CONSTRAINT IF EXISTS [FK_ApplicationId_FloodParcelTracking];
 
			ALTER TABLE [Flood].[FloodParcelTracking] DROP CONSTRAINT IF EXISTS [DF_LastUpdatedOn_FloodParcelTracking];
		END;
		
 
		DROP TABLE IF EXISTS [Flood].[FloodParcelTracking] 
		
 
		CREATE TABLE [Flood].[FloodParcelTracking](
			[Id]					[integer]			IDENTITY(1,1)	NOT NULL,
			[ApplicationId]			[integer]							NOT NULL,
			[PamsPin]				[varchar](76)						NOT NULL,
			[ClosingDate]			[datetime]							NULL,
			[DeedBook]				[varchar](128)						NULL,
			[DeedPage]				[varchar](128)						NULL,
			[DeedDate]				[datetime]							NULL,
			[DemolitionDate]		[datetime]							NULL,
			[SiteVisitConfirmDate]	[datetime]							NULL,
			[PublicPark]			[Bit]								NULL,
			[RainGarden]			[Bit]								NULL,
			[CommunityGarden]		[Bit]								NULL,
			[ActiveRecreation]		[Bit]								NULL,
			[NaturalHabitat]		[Bit]								NULL,
			[LastUpdatedBy]			[varchar](128)						NULL,
			[LastUpdatedOn]			[datetime]							NOT NULL
 
		CONSTRAINT [PK_FloodParcelTracking_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]
 
		
 
		ALTER TABLE [Flood].[FloodParcelTracking] ADD CONSTRAINT [FK_ApplicationId_FloodParcelTracking]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].[FloodApplication](Id);
		
 
		ALTER TABLE [Flood].[FloodParcelTracking] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelTracking]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		

		IF OBJECT_ID('[Flood].[FloodParcelAdminDetails]') IS NOT NULL
		BEGIN
			ALTER TABLE [Flood].[FloodParcelAdminDetails] DROP CONSTRAINT IF EXISTS [FK_ApplicationId_FloodParcelAdminDetails];

			ALTER TABLE [Flood].[FloodParcelAdminDetails] DROP CONSTRAINT IF EXISTS [DF_LastUpdatedOn_FloodParcelAdminDetails];
		END;
		

		DROP TABLE IF EXISTS [Flood].[FloodParcelAdminDetails]
		

		CREATE TABLE [Flood].[FloodParcelAdminDetails](
			[Id]									[integer]				IDENTITY(1,1)				NOT NULL,
			[ApplicationId]							[integer]											NOT NULL,
			[PamsPin]								[varchar](76)										NOT NULL,
			[DOBDocumentsMissingDate]               [datetime]											NULL,
			[FMCFinalApprovalDate]                  [datetime]											NULL,
			[FMCFinalNumber]                        [varchar](128)										NULL,
			[BCCFinalApprovalDate]                  [datetime]											NULL,
			[BCCFinalNumber]                        [varchar](128)										NULL,
			[MunicipalPurchaseDate]                 [datetime]											NULL,
			[MunicipalPurchaseNumber]               [varchar](128)										NULL,
			[GrantAgreementDate]                    [datetime]											NULL,
			[GrantAgreementExpirationDate]          [datetime]											NULL,
			[DueDiligenceDocumentsMissingDate]      [datetime]											NULL,
			[ScheduleClosingDate]                   [datetime]											NULL,
			[SoftCostReimbursementRequestDate]      [datetime]											NULL,
			[FMCSoftCostReimbApprovalDate]          [datetime]											NULL,
			[FMCSoftCostReimbApprovalNumber]        [varchar](128)										NULL,
			[BCCSoftCostReimbApprovalDate]          [datetime]											NULL,
			[BCCSoftCostReimbApprovalNumber]        [varchar](128)										NULL,
			[DoesHomeOwnerHaveNFIPInsurance]        [bit]												NULL,
			[IsDEPInvolved]                         [bit]												NULL,
			[IsPARRequestedbyFunder]                [bit]												NULL,
			[LastUpdatedBy]							[varchar](128)										NULL,
			[LastUpdatedOn]							[datetime]											NOT NULL,
		CONSTRAINT [PK_FloodParcelAdminDetails_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		ALTER TABLE [Flood].[FloodParcelAdminDetails] ADD CONSTRAINT [FK_ApplicationId_FloodParcelAdminDetails]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].[FloodApplication](Id);
		

		ALTER TABLE [Flood].[FloodParcelAdminDetails] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelAdminDetails]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		

		IF OBJECT_ID('[Flood].[FloodParcelBrokenRules]') IS NOT NULL
		BEGIN
			-- Drop Constraint
			ALTER TABLE [Flood].[FloodParcelBrokenRules] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelBrokenRules];
			ALTER TABLE [Flood].[FloodParcelBrokenRules] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelBrokenRules];
			DROP INDEX IF EXISTS [IX_FloodParcelBrokenRules_ApplicationId_SectionId] ON [Flood].[FloodParcelBrokenRules];
		END
		



		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodParcelBrokenRules];
		

		-- Create Table
		CREATE TABLE [Flood].[FloodParcelBrokenRules](
			[ApplicationId]		[integer]						NOT NULL,
			[PamsPin]			[varchar](76)					NOT NULL,
			[SectionId]			[integer]						NOT NULL,
			[Message]			[varchar](1024)					NOT NULL,
			[IsPropertyFlow]	[bit]							NOT NULL,
			[LastUpdatedOn]		[datetime]						NOT NULL,
		) ON [PRIMARY]

		

		-- Create a clustered index  
		CREATE CLUSTERED INDEX IX_FloodParcelBrokenRules_ApplicationId_SectionId ON [Flood].[FloodParcelBrokenRules] (ApplicationId, SectionId); 
		
 
		-- Create Constraint
		ALTER TABLE [Flood].[FloodParcelBrokenRules] ADD CONSTRAINT FK_ApplicationId_FloodParcelBrokenRules  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
		  

		ALTER TABLE [Flood].[FloodParcelBrokenRules] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelBrokenRules]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		  

		IF OBJECT_ID('[Flood].[FloodAgencyFlap]') IS NOT NULL
		BEGIN
			-- Drop Constraints
	
			ALTER TABLE [Flood].[FloodAgencyFlap] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodAgencyFlap];

		END;
		
  
		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodAgencyFlap]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodAgencyFlap](
			[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
			[AgencyId]							    [integer]						NOT NULL,
			[FlapApproved]							[bit]						    DEFAULT 0,
			[ApprovedDate]						    [datetime]					    NULL     ,
			[LastRevisedDate]						[datetime]					    NULL     ,
			[FlapMailToGrantee]						[datetime]					    NULL    ,
			[LastUpdatedBy]							[varchar](128)					NULL	,
			[LastUpdatedOn]							[datetime]						NOT NULL,

		CONSTRAINT [PK_FloodAgencyFlap_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		-- Create Constraints

		ALTER TABLE [Flood].[FloodAgencyFlap] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodAgencyFlap]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		  

		IF OBJECT_ID('[Flood].[FloodAgencyFlapComment]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodAgencyFlapComment] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodAgencyFlapComment];

		END;
		
  
		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodAgencyFlapComment]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodAgencyFlapComment](
			[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
			[AgencyId]							    [integer]						NOT NULL,
			[Comment]								[varchar](4000)					NOT NULL,
			[LastUpdatedBy]							[varchar](128)					NULL	,
			[LastUpdatedOn]							[datetime]						NOT NULL,

		CONSTRAINT [PK_FloodAgencyFlapComment_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		-- Create Constraints

		ALTER TABLE [Flood].[FloodAgencyFlapComment] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodAgencyFlapComment]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		  

		IF OBJECT_ID('[Flood].[FloodFlapTargetArea]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodFlapTargetArea] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodFlapTargetArea];

		END;
		
  
		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodFlapTargetArea]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodFlapTargetArea](
			[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
			[AgencyId]							    [integer]						NOT NULL,
			[TargetArea]							[varchar](128)					NOT NULL,
			[CreatedDate]                           [datetime]						NOT NULL,
			[LastUpdatedBy]							[varchar](128)					NULL	,
			[LastUpdatedOn]							[datetime]						NOT NULL,

		CONSTRAINT [PK_FloodFlapTargetArea_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		-- Create Constraints

		ALTER TABLE [Flood].[FloodFlapTargetArea] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodFlapTargetArea]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		  

		IF OBJECT_ID('[Flood].[FloodFlapDocument]') IS NOT NULL
		BEGIN
			-- Drop Constraints
	
			ALTER TABLE [Flood].[FloodFlapDocument] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodFlapDocument];
		END;
		

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodFlapDocument]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodFlapDocument](
			[Id]						[integer] 		IDENTITY(1,1)	NOT NULL,
			[AgencyId]				    [integer]						NOT NULL,
			[DocumentTypeId]			[smallint]						NOT NULL,
			[FileName]					[varchar](128)					NOT NULL,
			[Title]						[varchar](128)					NOT NULL,
			[LastUpdatedBy]				[varchar](128)					NULL,
			[LastUpdatedOn]				[datetime]						NOT NULL,
		CONSTRAINT [PK_FloodFlapDocument_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		-- Create Constraint

		ALTER TABLE [Flood].[FloodFlapDocument] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodFlapDocument]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		  

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodMunicipalFinance]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodMunicipalFinance](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[AgencyId] [int] NOT NULL,
			[FiscalYear] [int] NOT NULL,
			[TaxRate] [decimal](10, 3) NULL,
			[AnticipatedHistCollection] [decimal](18, 2) NULL,
			[AnnualTaxLevy] [decimal](18, 2) NULL,
			[Reimbursements] [decimal](18, 2) NULL,
			[CashReceipts] [decimal](18, 2) NULL,
			[Interest] [decimal](18, 2) NULL,
			[OtherRevenues] [decimal](18, 2) NULL,
			[OtherRevenuesExplained] [varchar](256) NULL,
			[Disbursements] [decimal](18, 2) NULL,
			[DebtPayments] [decimal](18, 2) NULL,
			[OtherExpenses] [decimal](18, 2) NULL,
			[OtherExpensesExplained] [varchar](256) NULL,
			[LastUpdatedBy]							[varchar](128)					NULL	,
			[LastUpdatedOn]							[datetime]						NOT NULL,
	
		CONSTRAINT [PK_FloodMunicipalFinance_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		IF OBJECT_ID('[Flood].[FloodMunicipalComment]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodMunicipalComment] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodMunicipalComment];
		END;
		
  
		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodMunicipalComment]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodMunicipalComment](
			[Id]					[integer] 			IDENTITY(1,1)	        NOT NULL,
			[AgencyId]				[integer]									NOT NULL,
			[Comment]				[varchar](4000)				                NULL,												
			[LastUpdatedBy]			[varchar](128)								NOT NULL,
			[LastUpdatedOn]			[datetime]									NOT NULL,
		CONSTRAINT [PK_FloodMunicipalComment_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		-- Create Constraints
		ALTER TABLE [Flood].[FloodMunicipalComment] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodMunicipalComment]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodMunicipalTrustFundPermittedUses]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodMunicipalTrustFundPermittedUses](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[AgencyId] [int] NOT NULL,
			[YearOfInception] [char](4) NULL,
			[AcquisitionOfLands] [bit] NULL,
			[AcquisitionOfFarmLands] [bit] NULL,
			[DevelopmentOfLands] [bit] NULL,
			[MaintenanceOfLands] [bit] NULL,
			[SalariesAndBenefits] [bit] NULL,
			[BondDownPayments] [bit] NULL,
			[HistoricPreservation] [bit] NULL,
			[OpenspaceMasterPlan] [bit] NULL,
			[OpenspaceMasterPlanDate] [datetime] NULL,
			[GreenAcresGrant] [bit] NULL,
			[TrustFundComments] [varchar](2000) NULL,
			[Other] [varchar](2000) NULL,
			[LastUpdatedBy]							[varchar](128)					NULL	,
			[LastUpdatedOn]							[datetime]						NOT NULL,
		CONSTRAINT [PK_FloodMunicipalTrustFundPermittedUses_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodAnnualFunding]
		

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

		

		IF OBJECT_ID('[Flood].[FloodEmailTemplate]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodEmailTemplate] DROP CONSTRAINT IF EXISTS  [DF_IsActive_FloodEmailTemplate];

			ALTER TABLE [Flood].[FloodEmailTemplate] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodEmailTemplate];

		END
			

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodEmailTemplate]
		

		CREATE TABLE [Flood].[FloodEmailTemplate](
		[Id] [smallint]  NOT NULL,
		[TemplateCode] [varchar](128) NOT NULL,
		[Title] [varchar](256) NOT NULL,
		[Subject] [varchar](512) NULL,
		[Description] [varchar](max) NOT NULL,
		[IsActive] [bit] NULL,
		[LastUpdatedBy] [varchar](128) NULL,
		[LastUpdatedOn] [datetime] NULL,

		CONSTRAINT [PK_FloodEmailTemplate_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		-- Create Constraints
		ALTER TABLE [Flood].[FloodEmailTemplate] WITH NOCHECK ADD  CONSTRAINT [DF_IsActive_FloodEmailTemplate]  DEFAULT (1) FOR [IsActive]
		

		ALTER TABLE [Flood].[FloodEmailTemplate] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodEmailTemplate]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		

		IF OBJECT_ID('[Flood].[FloodParcelHistory]') IS NOT NULL
		BEGIN
			-- Drop Constraints
			ALTER TABLE [Flood].[FloodParcelHistory] DROP CONSTRAINT IF EXISTS  [FK_ParcelId_FloodParcelHistory];

			ALTER TABLE [Flood].[FloodParcelHistory] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelHistory];

			ALTER TABLE [Flood].[FloodParcelHistory] DROP CONSTRAINT IF EXISTS  [DF_IsActive_FloodParcelHistory];

		END
		

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodParcelHistory]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodParcelHistory](
			[Id]								[integer] 		IDENTITY(1,1)	NOT NULL,
			[ParcelId]							[integer] 						NOT NULL,
			[CurrentPamsPin]					[varchar](76)					NOT NULL,
			[PreviousPamsPin]					[varchar](76)					NOT NULL,
			[Section]							[varchar](128)					NULL,
			[Acres]								[decimal](18,4)					NULL,
			[AcresToBeAcquired]					[decimal](18,4)					NULL,
			[Partial]							[bit]							NULL,
			[InterestType]						[varchar](100)					NULL,
			[IsThisAnExclusionArea]				[bit]							NULL,
			[Notes]								[varchar](4000)					NULL,
			[EasementId]						[varchar](100)					NULL,
			[IsActive]							[bit]							NOT NULL,
			[ChangeType]                        [varchar](100)                  NULL,
			[ChangeDate]						[datetime]                      NULL,
			[ReasonForChange]					[varchar](4000)					NULL,
			[LastUpdatedBy]						[varchar](128)					NOT NULL,
			[LastUpdatedOn]						[datetime]						NOT NULL,
	
		CONSTRAINT [PK_FloodParcelHistory_Id] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		

		-- Create Constraints  

		ALTER TABLE [Flood].[FloodParcelHistory] ADD CONSTRAINT [FK_ParcelId_FloodParcelHistory] FOREIGN KEY (ParcelId) REFERENCES [Flood].[FloodParcel](Id);
		

		ALTER TABLE [Flood].[FloodParcelHistory] WITH NOCHECK ADD  CONSTRAINT [DF_IsActive_FloodParcelHistory]  DEFAULT (0) FOR [IsActive]
		 

		ALTER TABLE [Flood].[FloodParcelHistory] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelHistory]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
		

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodProgramExpenses]
		

		-- Create Table
		CREATE TABLE [Flood].[FloodProgramExpenses](
			[Id]						[int]					IDENTITY(1,1)			NOT NULL,
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
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

	--==============================================================================================================--
	--SELECT 1/0;
	COMMIT;
	print 'SUCCESS';
END TRY
BEGIN CATCH
    DECLARE @ErrorMessage NVARCHAR(4000); 

	SET   @ErrorMessage = ERROR_MESSAGE();
	ROLLBACK;

	SELECT @ErrorMessage;	
END CATCH
