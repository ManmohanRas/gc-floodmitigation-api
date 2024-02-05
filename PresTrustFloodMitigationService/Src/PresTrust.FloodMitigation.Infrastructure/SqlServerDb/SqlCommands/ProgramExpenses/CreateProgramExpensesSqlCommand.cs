namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateProgramExpensesSqlCommand
{

    /*
       DECLARE		@p_ExpenseId							AS  INT
                  ,@p_ExpenseYear				            AS	STRING(4)
                  ,@p_ExpenseAmount					        AS	INT
                  ,@p_ExpenseDate					        AS	VARCHAR(100)
                  ,@p_Category 			                    AS	VARCHAR(100)
                  ,@p_Comment				                AS	VARCHAR(100)
                  ,@p_LastUpdatedBy				            AS	VARCHAR(100);

       SET			@p_ExpenseId							=   5;
       SET			@p_ExpenseYear				            =   2024;
       SET			@p_ExpenseAmount				    	=   100;
       SET			@p_ExpenseDate					        =   '01/01/2024';
       SET			@p_Category			                    =   'food';
       SET			@p_Comment				                =  'blah blah';;
       SET			@p_LastUpdatedBy				        =   'Aparna';
   */


    private readonly string _sqlCommand =
       @"INSERT INTO [Flood].[FloodProgramExpenses]
                   (
                   [ExpenseId]
                   ,[ExpenseYear]
                   ,[ExpenseAmount]
                   ,[ExpenseDate]
                   ,[Category]
                   ,[Comment]
                   ,[LastUpdatedBy]
                   ,[LastUpdatedOn])
             VALUES
                   (
                   @p_ExpenseId
                   ,@p_ExpenseYear
                   ,@p_ExpenseAmount
                   ,@p_ExpenseDate
                   ,@p_Category
                   ,@p_Comment
                   ,@p_LastUpdatedBy
                   ,GETDATE());

           SELECT CAST( SCOPE_IDENTITY() AS INT);";
    public CreateProgramExpensesSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }

}
