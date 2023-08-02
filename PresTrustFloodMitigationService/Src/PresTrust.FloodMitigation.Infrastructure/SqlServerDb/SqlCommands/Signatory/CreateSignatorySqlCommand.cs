namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateSignatorySqlCommand
{
    private readonly string _sqlCommand =
                 @"INSERT INTO [Flood].[FloodSignature]
						(
							 ApplicationId
							,Designation
							,Title	
							,SignedOn
							,SignatureType
							,LastUpdatedBy  
							,LastUpdatedOn	
						)

						VALUES
						(
							 @p_ApplicationId
							,@p_Designation
							,@p_Title	
							,@p_SignedOn
							,1
							,@p_LastUpdatedBy  
							,@p_LastUpdatedOn	
						);

				  SELECT CAST( SCOPE_IDENTITY() AS INT);";


    public CreateSignatorySqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
