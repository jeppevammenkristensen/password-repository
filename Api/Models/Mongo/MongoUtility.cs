using MongoDB.Driver;

namespace Api.Models.Mongo
{
    public class MongoUtility
    {
        public static IMongoCollection<T> Coll<T>()
                 {
                     var client = new MongoClient();

                     return client.GetDatabase("passwords").GetCollection<T>(typeof(T).Name);
                 }
    }


}