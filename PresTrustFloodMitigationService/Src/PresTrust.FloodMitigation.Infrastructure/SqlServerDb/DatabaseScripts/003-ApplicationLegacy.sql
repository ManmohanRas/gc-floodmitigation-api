BEGIN TRY
	BEGIN TRANSACTION
	--==============================================================================================================--

		-- Drop Table
		DROP TABLE IF EXISTS [Flood].[FloodApplicationLegacy]

		-- Create Table
		CREATE TABLE [Flood].[FloodApplicationLegacy](
			[LegacyApplicationId]			[integer]						NOT NULL,
			[LegacyApplicationType]			[varchar](256)					NULL,
			[LegacyApplicationSubType]		[varchar](256)					NULL,
			[LegacyApplicationStatus]		[varchar](256)					NOT NULL,
			[LegacyAgencyId]				[integer]						NOT NULL,
			[FloodApplicationId]			[integer]						NULL,
		CONSTRAINT [PK_FloodApplicationLegacy_Id] PRIMARY KEY CLUSTERED 
		(
			[LegacyApplicationId] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		-- Insert From Legacy Table
		INSERT INTO [Flood].[FloodApplicationLegacy]
		(
			LegacyApplicationId,
			LegacyApplicationType,
			LegacyApplicationSubType,
			LegacyApplicationStatus,
			LegacyAgencyId,
			FloodApplicationId
		)
		SELECT
			[ProjectAreaID],
			[ProgramType],
			[SubProgramType],
			[ProjectAreaStatus],
			ISNULL([MunicipalID], 0) AS [MunicipalID],
			NULL AS [FloodApplicationId]
		FROM [FloodMitigation].[floodmp].[tblProjectArea];

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
