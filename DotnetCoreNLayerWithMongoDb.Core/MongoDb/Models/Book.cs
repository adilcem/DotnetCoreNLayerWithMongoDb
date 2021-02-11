using DotnetCoreNLayerWithMongoDb.Core.MongoDb.Settings;

namespace DotnetCoreNLayerWithMongoDb.Core.MongoDb.Models
{
    [BsonCollection("Books")]
    public class Book : MongoBaseClass
    {
        public string BookName { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }
    }
}
