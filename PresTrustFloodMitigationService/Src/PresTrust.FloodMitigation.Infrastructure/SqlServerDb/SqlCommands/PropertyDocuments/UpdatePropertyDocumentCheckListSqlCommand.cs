namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb
{
        public class UpdatePropertyDocumentChecklistSqlCommand
        {
            private readonly string _sqlCommand =
              @" UPDATE [Flood].[FloodParcelDocument]
				SET  [Title] = @p_Title
                    ,[PamsPin] = @p_PamsPin
					,[Description] = @p_Description
					,[UseInReport] = @p_UseInReport
					,[HardCopy] = @p_HardCopy
					,[Approved] = @p_Approved
					,[ReviewComment] = @p_ReviewComment
					,[LastUpdatedOn] = GETDATE()
					,[LastUpdatedBy] = @p_LastUpdatedBy
				WHERE Id = @p_Id;";

            public UpdatePropertyDocumentChecklistSqlCommand() { }

            public override string ToString()
            {
                return _sqlCommand;
            }
        }
}
