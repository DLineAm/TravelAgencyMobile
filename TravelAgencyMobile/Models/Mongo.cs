using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TravelAgencyMobile;

namespace Mongich
{
    public class Mongo
    {
        private readonly IMongoDatabase _database;

        public Mongo(string dbName)
        {
            //var client = new MongoClient(App.Settings);
           // _database = client.GetDatabase(dbName);

        }

        public async void InsertRecord<T>(string tableName, T record)
        {
            IMongoCollection<T> collection = _database.GetCollection<T>(tableName);
            await collection.InsertOneAsync(record);
        }

        public async Task<List<T>> FindAsync<T>(string tableName, BsonDocument filter = null)
        {
            IMongoCollection<T> collection = _database.GetCollection<T>(tableName);

            var cursor = await collection.FindAsync(filter ?? new BsonDocument());

            while (await cursor.MoveNextAsync())
            {
                var list = cursor.Current;
                return list.ToList();
            }

            return null;
        }

        public List<T> Find<T>(string tableName, BsonDocument filter = null)
        {
            IMongoCollection<T> collection = _database.GetCollection<T>(tableName);

            return collection.Find(filter ?? new BsonDocument()).ToList();
        }

        public T FindById<T>(string table, Guid id)
        {
            var collection = _database.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).FirstOrDefault();
        }

        public void UpsertRecord<T>(string table, Guid id, T record)
        {
            var collection = _database.GetCollection<T>(table);

            var result = collection.ReplaceOne(
                new BsonDocument("_id", id),
                record,
                new ReplaceOptions{IsUpsert = true});
        }

        public void DeleteRecord<T>(string table, Guid id)
        {
            var collection = _database.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            var result = collection.DeleteOne(filter);
        }
    }
}