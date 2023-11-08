namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands
{
    public class UpdateDocumentCheckListItemsSqlCommand
	{
		private readonly string _sqlCommand =
          @" UPDATE [Flood].[FloodApplicationDocument]
				SET  [Title] = @p_Title
					,[Description] = @p_Description
					,[UseInReport] = @p_UseInReport
					,[HardCopy] = @p_HardCopy
					,[Approved] = @p_Approved
					,[ReviewComment] = @p_ReviewComment
					,[LastUpdatedOn] = GETDATE()
					,[LastUpdatedBy] = @p_LastUpdatedBy
					,[OtherFundingSourceId] = @p_OtherFundingSourceId
				WHERE Id = @p_Id;";

		public UpdateDocumentCheckListItemsSqlCommand() { }

		public override string ToString()
		{
			return _sqlCommand;
		}
	}
}
