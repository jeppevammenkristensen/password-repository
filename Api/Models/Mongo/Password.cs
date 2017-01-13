using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Api.Models.Mongo
{
    public class Password
    {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid Id { get; set; }

        public string Company { get; set; }

        public string EncryptedPassword { get; set; }

        [BsonIgnore]
        public string UnencryptedPassword { get; set; }

        public string Environment { get; set; }
        public string User { get; set; }
    }


}