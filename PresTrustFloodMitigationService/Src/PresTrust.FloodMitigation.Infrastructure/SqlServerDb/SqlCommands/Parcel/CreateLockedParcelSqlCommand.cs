namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateLockedParcelSqlCommand
{
    private readonly string _sqlCommand =
            @"  DELETE FROM [Flood].[FloodLockedParcel] WHERE [ApplicationId] = @p_ApplicationId AND [PamsPin] = @p_PamsPin;
				INSERT INTO [Flood].[FloodLockedParcel]
				SELECT TOP 1
					@p_ApplicationId AS [ApplicationId],
					FP.[PamsPin],
					FP.[AgencyID],
					FP.[Block],
					FP.[Lot],
					FP.[QualificationCode],
					FP.[Latitude],
					FP.[Longitude],
					FP.[StreetNo],
					FP.[StreetAddress],
					FP.[Acreage],
					FP.[OwnersName],
					FP.[OwnersAddress1],
					FP.[OwnersAddress2],
					FP.[OwnersCity],
					FP.[OwnersState],
					FP.[OwnersZipcode],
					FP.[SquareFootage],
					FP.[YearOfConstruction],
					FP.[TotalAssessedValue],
					FP.[LandValue],
					FP.[ImprovementValue],
					FP.[AnnualTaxes],
					FP.[IsValidPamsPin],
					FP.[TargetAreaId],
					FP.[DateOfFLAP],
					FP.[IsElevated],
					FP.[IsActive],
					@p_LastUpdatedBy AS [LastUpdatedBy],
					GETDATE() AS [LastUpdatedOn]
				FROM [Flood].[FloodParcel] FP where FP.[PamsPin] = @p_PamsPin;";

    public CreateLockedParcelSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
