using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Domains;

namespace Application.Services
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private readonly ICurrencyRepository _currencyRepository;
        public CurrencyConverter(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
        public async Task ClearConfiguration()
        {
            await _currencyRepository.ClearCurrencies();
        }

        public async Task UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates)
        {
            foreach (var conversionRate in conversionRates)
            {
                var isExist =await _currencyRepository.GetBySourceAndTarget(conversionRate.Item1, conversionRate.Item2);
                if (isExist != null)
                {
                    isExist.Rate = conversionRate.Item3;
                    await _currencyRepository.Update(isExist);
                }
                else
                {
                    var newCurrency = new Currency()
                    {
                        Id =Guid.NewGuid(),
                        SourceCurrency = conversionRate.Item1,
                        TargetCurrency = conversionRate.Item2,
                        Rate = conversionRate.Item3
                    };
                    await _currencyRepository.Insert(newCurrency);
                }
            }
        }

        public async Task<double> Convert(string fromCurrency, string toCurrency, double amount)
        {
            double result=-1;
            var currencies =(await _currencyRepository.GetAll()).ToList();
            //var isExist = currencies.ToList()
            //    .FirstOrDefault(x => x.SourceCurrency == fromCurrency && x.TargetCurrency == toCurrency);
            //var isExistReverse = currencies.ToList()
            //    .FirstOrDefault(x => x.SourceCurrency == toCurrency && x.TargetCurrency == fromCurrency);
            //if (isExist != null)
            //{
            //    result = isExist.Rate * amount;
            //}
            //if (isExistReverse != null)
            //{
            //    result = (1 / isExistReverse.Rate) * amount;
            //}

            //else
            //{
                var list = new List<Tuple<string, double, int>>();
                list.Add(Tuple.Create(fromCurrency.ToUpper(), amount, 0));
                int count = 0;
                var tr = true;
                while (tr)
                {
                    if (list.Count()<= count)
                    {
                        break;
                    }
                    var source = list[count];
                    var edges = new List<Currency>();
                    var normal = currencies.Where(x => x.SourceCurrency == source.Item1).ToArray().ToList();
                    var reverse = currencies.Where(x => x.TargetCurrency == source.Item1).Select(a => new Currency()
                    {
                        SourceCurrency = a.TargetCurrency,
                        TargetCurrency = a.SourceCurrency,
                        Rate = 1 / a.Rate
                    });
                    edges.AddRange(normal);
                    edges.AddRange(reverse);
                    foreach (var edge in edges)
                    {
                        if (edge.TargetCurrency == toCurrency.ToUpper())
                        {
                            //list.Add(Tuple.Create(edge.SourceCurrency, amount * edge.Rate, list[count].Item3+1, true));
                            result = source.Item2 * edge.Rate;
                            tr = false;
                        }
                        if (currencies.Any(x=>x.SourceCurrency== edge.TargetCurrency || x.TargetCurrency == edge.TargetCurrency))
                        {
                            list.Add(Tuple.Create(edge.TargetCurrency, source.Item2 * edge.Rate, source.Item3 + 1));
                        }
                    }
                    count++;
                }
            //}

            return result;
        }
    }

}
