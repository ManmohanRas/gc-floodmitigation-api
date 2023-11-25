using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IParcelPropertyRepository
{
/// <summary>
/// Get ParceFinance.
/// </summary>
/// <param name="applicationId"></param>
/// <param name="pamsPin"></param>
/// <returns></returns>
Task<FloodParcelPropertyEntity> GetAsync(int applicationId, string pamsPin);

/// <summary>
/// Save ParceFinance.
/// </summary>
/// <param name="parcelFinance"></param>
/// <returns></returns>
Task<FloodParcelPropertyEntity> SaveParcelPropertyAsync(FloodParcelPropertyEntity parcelFinance);
}

