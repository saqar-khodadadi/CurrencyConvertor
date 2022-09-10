using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> Get(Guid id);
        Task Delete(T entity);
        Task Insert(T entity);
        Task Update(T entity);
    }
}
