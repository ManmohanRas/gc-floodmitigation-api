namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateTechDetailsSqlCommand
{
    private readonly string _sqlCommand =
     @"UPDATE [Flood].[FloodParcelTech]
						SET
							[ApplicationId] = @p_ApplicationId,
							[PamsPin] = @p_PamsPin,
							[FEMASevereRepetitiveLossList] = @p_FEMASevereRepetitiveLossList,
							[FEMARepetitiveLossList] = @p_FEMARepetitiveLossList,
							[IsthepropertywithinthePassaicRiverBasin] = @p_IsthepropertywithinthePassaicRiverBasin,
							[IsthepropertywithinFloodway] = @p_IsthepropertywithinFloodway,
							[IsthepropertywithinFloodplain] = @p_IsthepropertywithinFloodplain,
							[Claim10Years] = @p_Claim10Years,
							[TotalOfClaims] = @p_TotalOfClaims,
							[BenefitCostRatio] = @p_BenefitCostRatio,
							[FEMACommunityId] = @p_FEMACommunityId,
							[FirmEffectiveDate] = @p_FirmEffectiveDate,
							[FirmPanel] = @p_FirmPanel,
							[FirmPanelFinal] = @p_FirmPanelFinal,
							[FloodZoneDesignation] = @p_FloodZoneDesignation,
							[BaseFloodElevation] = @p_BaseFloodElevation,
							[BaseFloodElevationFinal] = @p_BaseFloodElevationFinal,
							[RiverId] = @p_RiverId,
							[RiverIdFinal] = @p_RiverIdFinal,
							[FisEffectiveDate] = @p_FisEffectiveDate,
							[FloodProfile] = @p_FloodProfile,
							[FloodProfileFinal] = @p_FloodProfileFinal,
							[FloodSource] = @p_FloodSource,
							[FirstFloodElevation] = @p_FirstFloodElevation,
							[FirstFloodElevationFinal] = @p_FirstFloodElevationFinal,
							[StreambedElevation] = @p_StreambedElevation,
							[StreambedElevationFinal] = @p_StreambedElevationFinal,
							[ElevationBeforeMitigation] = @p_ElevationBeforeMitigation,
							[ElevationBeforeMitigationFinal] = @p_ElevationBeforeMitigationFinal,
							[FloodType] = @p_FloodType,
							[TenPercent] = @p_TenPercent,
							[TwoPercent] = @p_TwoPercent,
							[OnePercent] = @p_OnePercent,
							[PointOnePercent] = @p_PointOnePercent,
							[LastUpdatedBy] = @p_LastUpdatedBy,
							[LastUpdatedOn] = @P_LastUpdatedOn
                            WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin";

    public UpdateTechDetailsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
