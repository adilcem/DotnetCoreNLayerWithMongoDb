using DotnetCoreNLayerWithMongoDb.Core.MongoDb.Models;
using DotnetCoreNLayerWithMongoDb.Core.Repositories;
using DotnetCoreNLayerWithMongoDb.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreBestPractices.Service.Services
{
    public class MongoService<TEntity> : IMongoService<TEntity> where TEntity : MongoBaseClass
    {
        private readonly IMongoRepository<TEntity> _mongoRepository;

        public MongoService(IMongoRepository<TEntity> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<TEntity> AddAsync(TEntity document)
        {
            await _mongoRepository.AddAsync(document);

            return document;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> documents)
        {
            await _mongoRepository.AddRangeAsync(documents);

            return documents;
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await _mongoRepository.GetAllAsync();
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await _mongoRepository.GetByIdAsync(id);
        }

        public void Remove(string id)
        {
            _mongoRepository.Remove(id);
        }

        public TEntity Update(TEntity document)
        {
            _mongoRepository.Update(document);

            return document;
        }
    }
}
