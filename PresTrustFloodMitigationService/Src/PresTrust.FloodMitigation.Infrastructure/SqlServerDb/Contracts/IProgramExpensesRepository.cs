namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IProgramExpensesRepository
{
    /// <summary>
    /// Procedure to fetch all programexpenses by ExpenseYear
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    Task<List<FloodProgramExpensesEntity>> GetAllProgramExpensesAsync();

    /// <summary>
    /// Procedure to save all programexpenses by expenses
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    Task<FloodProgramExpensesEntity> SaveExpensesAsync(FloodProgramExpensesEntity expenses);

    /// <summary>
    /// Procedure to delete all programexpenses by expenses
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    Task DeleteProgramExpensesAsync(FloodProgramExpensesEntity expenses);
}
