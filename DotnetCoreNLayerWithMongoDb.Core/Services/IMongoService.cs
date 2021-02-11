using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoreNLayerWithMongoDb.Core.Services
{
    public interface IMongoService<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(string id);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<TEntity> AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(string id);
        TEntity Update(TEntity entity);
    }
}
