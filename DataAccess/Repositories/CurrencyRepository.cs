using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Domains;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.Repositories
{
    public class CurrencyRepository:BaseRepository<Currency>,ICurrencyRepository
    {
        private readonly IMemoryCache _cache;
        public CurrencyRepository(IMemoryCache cache):base(cache)
        {
            _cache = cache;
        }

        public async Task ClearCurrencies()
        {
            _cache.Remove(typeof(Currency));
        }

        public async Task<Currency?> GetBySourceAndTarget(string source, string target)
        {

                return _cache.Get<IEnumerable<Currency>>(typeof(Currency))?.FirstOrDefault(x => x.SourceCurrency == source && x.TargetCurrency == target);
        }
    }
}
