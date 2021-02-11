using DotnetCoreNLayerWithMongoDb.Core.MongoDb;
using DotnetCoreNLayerWithMongoDb.Core.MongoDb.Models;
using DotnetCoreNLayerWithMongoDb.Core.MongoDb.Settings;
using DotnetCoreNLayerWithMongoDb.Core.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoreNLayerWithMongoDb.Data.Repositories
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : MongoBaseClass
    {
        private readonly IMongoCollection<TEntity> _collection;
        public MongoRepository(IMongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault())?.CollectionName;
        }

        public async Task AddAsync(TEntity document)
        {
            await _collection.InsertOneAsync(document);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await Task.Run(() => _collection.AsQueryable());
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, objectId);
                return _collection.FindAsync(filter).Result.SingleOrDefaultAsync();
            });
        }

        public void Remove(TEntity document)
        {
            _collection.DeleteOne(d => d.Id == document.Id);
        }
        public void Remove(string id)
        {
            var objectId = new ObjectId(id);
            _collection.DeleteOne(d => d.Id == objectId);
        }
        public void RemoveRange(Expression<Func<TEntity, bool>> expression)
        {
            _collection.DeleteMany(expression);
        }

        public TEntity Update(TEntity document)
        {
            _collection.ReplaceOne(r => r.Id == document.Id, document);
            return document;
        }
    }

}
