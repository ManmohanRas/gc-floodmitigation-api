

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

internal class CreateFundingDetailsSqlCommand
{
    /*
      DECLARE		@p_AllocationAmount			AS  INT
                    ,@p_Interest				AS	INT
                    ,@p_Added					AS	INT
                    ,@p_Total					AS INT
			        ,@p_Comments                AS	VARCHAR(100)
                    ,@p_LastUpdatedBy			AS	VARCHAR(100);

      SET			@p_AllocationAmount		=   5;
      SET			@p_Interest				=   1;
      SET			@p_Added				=   1;
      SET			@p_Total				=   7;
      SET			@p_Comments		  	    =   'blah blah';
      SET			@p_LastUpdatedBy		=   'Aparna';
  */

    private readonly string _sqlCommand =
       @"INSERT INTO [Flood].[FloodFundingAmounts]
                   ([AllocationAmount]
                   ,[Interest]
                   ,[Added]
                   ,[Total]
                   ,[Comments]
                   ,[LastUpdatedBy]
                   ,[LastUpdatedOn])
             VALUES
                   (@p_AllocationAmount
                   ,@p_Interest
                   ,@p_Added
                   ,@p_Total
                   ,@p_Comments
                   ,@p_LastUpdatedBy
                   ,GETDATE());

           SELECT CAST( SCOPE_IDENTITY() AS INT);";
    public CreateFundingDetailsSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
