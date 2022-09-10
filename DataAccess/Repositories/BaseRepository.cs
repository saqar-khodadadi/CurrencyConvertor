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
    public class BaseRepository<T>:IBaseRepository<T> where T : BaseEntity
    {
        private readonly IMemoryCache _cache;

        public BaseRepository(IMemoryCache cache)
        {
            _cache = cache;
            if (_cache!=null && _cache?.Get(typeof(T)) == null)
            {
                _cache.Set(typeof(T), new List<T>());
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return _cache.Get<IEnumerable<T>>(typeof(T));
        }

        public async Task<T?> Get(Guid id)
        {
            return _cache.Get<IEnumerable<T>>(typeof(T)).ToList().Find(x => x.Id == id);

        }
        public async Task Delete(T entity)
        {
            var newValue = _cache.Get<IEnumerable<T>>(typeof(T)).ToList();
            newValue.Remove(entity);
            _cache.Set(typeof(T), newValue);
        }

        public async Task Insert(T entity)
        {
            var newValue = _cache.Get<IEnumerable<T>>(typeof(T)).ToList();
            newValue.Add(entity);
            _cache.Set(typeof(T), newValue);
        }

        public async Task Update(T entity)
        {
            var newValue = _cache.Get<IEnumerable<T>>(typeof(T));
            var baseEntities = newValue.ToList();
            baseEntities.Where(x => x.Id == entity.Id).ToList().ForEach(x => x = entity);
            _cache.Set(typeof(T), baseEntities);
        }
    }
}
