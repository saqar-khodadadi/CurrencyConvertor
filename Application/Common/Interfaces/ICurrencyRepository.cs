using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Domains;

namespace Application.Common.Interfaces
{
    public interface ICurrencyRepository:IBaseRepository<Currency>
    {
        Task ClearCurrencies();
        Task<Currency?> GetBySourceAndTarget(string source, string target);
    }
}
