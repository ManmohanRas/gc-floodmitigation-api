namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateProgramExpensesSqlCommand
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
       @"UPDATE		       [Flood].[FloodProgramExpenses]
                     SET			   [Id] = @p_Id
			                          ,[ExpenseAmount] = @p_ExpenseAmount
                                      ,[ExpenseDate] = @p_ExpenseDate
                                      ,[CategoryId] = @p_CategoryId
                                      ,[Comment]= @p_Comment
			                          ,[LastUpdatedBy] = @p_LastUpdatedBy
			                          ,[LastUpdatedOn] = GETDATE()
                     WHERE		      [Id] = @p_Id and [ExpenseYear] = @p_ExpenseYear;";

    public UpdateProgramExpensesSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }

}
