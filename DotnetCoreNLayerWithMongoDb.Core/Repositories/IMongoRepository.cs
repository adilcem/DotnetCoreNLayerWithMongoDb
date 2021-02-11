using DotnetCoreNLayerWithMongoDb.Core.MongoDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoreNLayerWithMongoDb.Core.Repositories
{
    public interface IMongoRepository<TEntity> where TEntity : MongoBaseClass
    {
        Task<TEntity> GetByIdAsync(string id);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity document);
        Task AddRangeAsync(IEnumerable<TEntity> documents);
        void Remove(TEntity document);
        void Remove(string id);
        void RemoveRange(Expression<Func<TEntity, bool>> expression);
        TEntity Update(TEntity document);
    }
}
