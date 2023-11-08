namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommandstails;

public class GetTechDetailsSqlCommand
{
    private readonly string _sqlCommand =
      @"  SELECT        [Id]                                                        
                        ,[ApplicationId]                                            
                        ,[PamsPin]                                                   
                        ,[FEMASevereRepetitiveLossList]                      
                        ,[FEMARepetitiveLossList]                            
                        ,[IsthepropertywithinthePassaicRiverBasin]           
                        ,[IsthepropertywithinFloodway]                       
                        ,[IsthepropertywithinFloodplain]                     
                        ,[Claim10Years]                                      
                        ,[TotalOfClaims]                                     
                        ,[BenefitCostRatio]                                  
                        ,[FEMACommunityId]                                   
                        ,[FirmEffectiveDate]                                 
                        ,[FirmPanel]                                         
                        ,[FirmPanelFinal]                                    
                        ,[FloodZoneDesignation]                              
                        ,[BaseFloodElevation]                                
                        ,[BaseFloodElevationFinal]                           
                        ,[RiverId]                                           
                        ,[RiverIdFinal]                                            
                        ,[FisEffectiveDate]                                  
                        ,[FloodProfile]                                      
                        ,[FloodProfileFinal]                                 
                        ,[FloodSource]                                       
                        ,[FirstFloodElevation]                               
                        ,[FirstFloodElevationFinal]                          
                        ,[StreambedElevation]                                
                        ,[StreambedElevationFinal]                           
                        ,[ElevationBeforeMitigation]                         
                        ,[ElevationBeforeMitigationFinal]                    
                        ,[FloodType]                                         
                        ,[TenPercent]                                        
                        ,[TwoPercent]                                        
                        ,[OnePercent]                                        
                        ,[PointOnePercent]                                         
                        ,[LastUpdatedBy]                                           
                        ,[LastUpdatedOn]                                           

            FROM [Flood].[FloodParcelTech]
            WHERE ApplicationId = @p_ApplicationId AND [PamsPin] = @p_PamsPin";


    public GetTechDetailsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
