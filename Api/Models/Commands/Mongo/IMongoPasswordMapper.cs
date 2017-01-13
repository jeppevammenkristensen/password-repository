using Api.Models.Mongo;
using Microsoft.AspNetCore.DataProtection;
using MongoDB.Bson;

namespace Api.Models.Commands.Mongo
{
    public interface IMongoPasswordMapper
    {
        PasswordView Map(Password password, IPersistedDataProtector decryptedPassword);
    }
}