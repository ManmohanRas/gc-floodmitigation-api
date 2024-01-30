
namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

    public  class UpdateFundingAmountsSqlCommand
    {

        /*
          DECLARE		@p_AllocationAmount			AS  INT
                        ,@p_Interest				AS	INT
                        ,@p_Added					AS	INT
                        ,@p_Total					AS INT
                        ,@p_Comment                AS	VARCHAR(100)
                        ,@p_LastUpdatedBy			AS	VARCHAR(100);

          SET			@p_AllocationAmount		=   5;
          SET			@p_Interest				=   1;
          SET			@p_Added				=   1;
          SET			@p_Total				=   7;
          SET			@p_Comment		  	    =   'blah blah';
          SET			@p_LastUpdatedBy		=   'Aparna';
      */

        private readonly string _sqlCommand =
           @"UPDATE		       [Flood].[FloodAnnualFunding]
                     SET			   [AllocationAmount] = @p_AllocationAmount
			                          ,[Interest] = @p_Interest
                                      ,[AddedOrOmittedAmount] = @p_AddedOrOmittedAmount
                                      ,[Comment] = @p_Comment
			                          ,[LastUpdatedBy] = @p_LastUpdatedBy
			                          ,[LastUpdatedOn] = GETDATE()
                     WHERE		       [Id] = @p_Id and [AllocationYear] = @p_AllocationYear;";

            public UpdateFundingAmountsSqlCommand() { }

            public override string ToString()
            {
                return _sqlCommand;
            }
}
