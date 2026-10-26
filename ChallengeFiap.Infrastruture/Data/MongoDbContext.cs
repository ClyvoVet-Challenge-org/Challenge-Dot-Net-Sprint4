using challengeFiap.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeFiap.Infrastruture.Data
{
    public class MongoDbContext
    {
        private static readonly object ClassMapLock = new();
        private readonly IMongoClient _client;

        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoClient client, string databaseName)
        {
            _client = client;
            _database = _client.GetDatabase(databaseName);

            RegisterMongoEntityClassMaps();

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

        private static void RegisterMongoEntityClassMaps()
        {
            RegisterIgnoreExtraElements<Animal>();
            RegisterIgnoreExtraElements<CarteiraVacinal>();
            RegisterIgnoreExtraElements<Clinica>();
            RegisterIgnoreExtraElements<Consulta>();
            RegisterIgnoreExtraElements<EnderecoAnimal>();
            RegisterIgnoreExtraElements<EnderecoClinica>();
            RegisterIgnoreExtraElements<EnderecoTutor>();
            RegisterIgnoreExtraElements<Medicamento>();
            RegisterIgnoreExtraElements<Prescricao>();
            RegisterIgnoreExtraElements<Tutor>();
            RegisterIgnoreExtraElements<VetClinica>();
            RegisterIgnoreExtraElements<Veterinario>();
        }

        private static void RegisterIgnoreExtraElements<T>() where T : class
        {
            lock (ClassMapLock)
            {
                if (!BsonClassMap.IsClassMapRegistered(typeof(T)))
                {
                    BsonClassMap.RegisterClassMap<T>(classMap =>
                    {
                        classMap.AutoMap();
                        classMap.SetIgnoreExtraElements(true);
                    });
                }
            }
        }

    }
}
