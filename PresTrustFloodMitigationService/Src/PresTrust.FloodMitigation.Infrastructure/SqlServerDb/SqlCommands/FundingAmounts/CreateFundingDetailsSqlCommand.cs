

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateFundingDetailsSqlCommand
{
    /*
      DECLARE		 @p_Id                      AS INT
                    ,@p_AllocationYear          AS STRING(4)
                    ,@p_AllocationAmount	    AS  INT
                    ,@p_Interest				AS	INT
                    ,@p_AddedOrOmittedAmount	AS	INT
                    ,@p_Total					AS INT
			        ,@p_Comment                AS	VARCHAR(100)
                    ,@p_LastUpdatedBy			AS	VARCHAR(100);

      SET           @P_Id                   =    1
      SET			@p_AllocationYear       =   2012     
      SET           @p_AllocationAmount		=   5;
      SET			@p_Interest				=   1;
      SET			@p_AddedOrOmittedAmount	=   1;
      SET			@p_Total				=   7;
      SET			@p_Comment		  	    =   'blah blah';
      SET			@p_LastUpdatedBy		=   'Aparna';
  */

    private readonly string _sqlCommand =
       @"INSERT INTO [Flood].[FloodAnnualFunding]
                   (
                   [AllocationYear]
                   ,[AllocationAmount]
                   ,[Interest]
                   ,[AddedOrOmittedAmount]
                   ,[Comment]
                   ,[LastUpdatedBy]
                   ,[LastUpdatedOn])
             VALUES
                   (
                   @p_AllocationYear
                   ,@p_AllocationAmount
                   ,@p_Interest
                   ,@p_AddedOrOmittedAmount
                   ,@p_Comment
                   ,@p_LastUpdatedBy
                   ,GETDATE());

           SELECT CAST( SCOPE_IDENTITY() AS INT);";
    public CreateFundingDetailsSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
