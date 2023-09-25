namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateApplicationSignatorySqlCommand
{
    private readonly string _sqlCommand =
                 @"INSERT INTO [Flood].[FloodApplicationSignatory]
						(
							 ApplicationId
							,Designation
							,Title	
							,SignedOn
							,SignatoryType
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
							,GETDATE()	
						);

				  SELECT CAST( SCOPE_IDENTITY() AS INT);";


    public CreateApplicationSignatorySqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
