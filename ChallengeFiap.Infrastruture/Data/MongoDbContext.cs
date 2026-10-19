using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeFiap.Infrastruture.Data
{
    public class MongoDbContext
    {
        private readonly IMongoClient _client;

        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoClient client, string databaseName)
        {
            _client = client;
            _database = _client.GetDatabase(databaseName);


            try
            {
                _client.GetDatabase("admin")
                    .RunCommand<BsonDocument>( new BsonDocument("ping", 1) );

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while connecting to MongoDB", ex);
            }
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
