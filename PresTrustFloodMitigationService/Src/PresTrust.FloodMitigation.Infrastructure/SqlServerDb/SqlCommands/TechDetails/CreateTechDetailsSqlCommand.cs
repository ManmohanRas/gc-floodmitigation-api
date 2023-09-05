namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateTechDetailsSqlCommand
{
    private readonly string _sqlCommand =
                 @"INSERT INTO [Flood].[FloodParcelTech]
						(
							[ApplicationId],
							[PamsPin],
							[FEMASevereRepetitiveLossList],
							[FEMARepetitiveLossList],
							[IsthepropertywithinthePassaicRiverBasin],
							[IsthepropertywithinFloodway],
							[IsthepropertywithinFloodplain],
							[Claim10Years],
							[TotalOfClaims],
							[BenefitCostRatio],
							[FEMACommunityId],
							[FirmEffectiveDate],
							[FirmPanel],
							[FirmPanelFinal],
							[FloodZoneDesignation],
							[BaseFloodElevation],
							[BaseFloodElevationFinal],
							[RiverId],
							[RiverIdFinal],
							[FisEffectiveDate],
							[FloodProfile],
							[FloodProfileFinal],
							[FloodSource],
							[FirstFloodElevation],
							[FirstFloodElevationFinal],
							[StreambedElevation],
							[StreambedElevationFinal],
							[ElevationBeforeMitigation],
							[ElevationBeforeMitigationFinal],
							[FloodType],
							[TenPercent],
							[TwoPercent],
							[OnePercent],
							[PointOnePercent],
							[LastUpdatedBy],
							[LastUpdatedOn]
						)

						VALUES
						(
							@p_ApplicationId,
							@p_PamsPin,
							@p_FEMASevereRepetitiveLossList,
							@p_FEMARepetitiveLossList,
							@p_IsthepropertywithinthePassaicRiverBasin,
							@p_IsthepropertywithinFloodway,
							@p_IsthepropertywithinFloodplain,
							@p_Claim10Years,
							@p_TotalOfClaims,
							@p_BenefitCostRatio,
							@p_FEMACommunityId,
							@p_FirmEffectiveDate,
							@p_FirmPanel,
							@p_FirmPanelFinal,
							@p_FloodZoneDesignation,
							@p_BaseFloodElevation,
							@p_BaseFloodElevationFinal,
							@p_RiverId,
							@p_RiverIdFinal,
							@p_FisEffectiveDate,
							@p_FloodProfile,
							@p_FloodProfileFinal,
							@p_FloodSource,
							@p_FirstFloodElevation,
							@p_FirstFloodElevationFinal,
							@p_StreambedElevation,
							@p_StreambedElevationFinal,
							@p_ElevationBeforeMitigation,
							@p_ElevationBeforeMitigationFinal,
							@p_FloodType,
							@p_TenPercent,
							@p_TwoPercent,
							@p_OnePercent,
							@p_PointOnePercent,
							@p_LastUpdatedBy,
							GETDATE()
						);

				  SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateTechDetailsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
