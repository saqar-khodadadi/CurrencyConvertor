using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ICurrencyConverter
    {
        Task ClearConfiguration();
        Task UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates);
        Task<double> Convert(string fromCurrency, string toCurrency, double amount);
    }
}
