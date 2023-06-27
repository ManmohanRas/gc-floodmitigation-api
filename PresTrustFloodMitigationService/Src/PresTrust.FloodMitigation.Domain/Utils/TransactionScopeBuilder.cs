namespace PresTrust.FloodMitigation.Domain.Utils;

public static class TransactionScopeBuilder
{

    /// <summary>
    /// Creates a transactionscope with ReadCommitted Isolation and given TimeOut, the same level as sql server
    /// </summary>
    /// <returns>A transaction scope</returns>
    public static TransactionScope CreateReadCommitted(int transScopeTimeOut)
    {
        var options = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = new TimeSpan(0, transScopeTimeOut, 0)
        };

        return new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled);
    }
    

    /// <summary>
    /// Creates a transactionscope with Serializable Isolation and given TimeOut, the same level as sql server
    /// </summary>
    /// <returns>A transaction scope</returns>
    public static TransactionScope CreateSerializable(int transScopeTimeOut)
    {
        var options = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = new TimeSpan(0, transScopeTimeOut, 0)
        };

        return new TransactionScope(TransactionScopeOption.Required, options);
    }
}
