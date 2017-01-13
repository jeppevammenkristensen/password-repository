using Api.Models.Encryption;
using Api.Models.Mongo;
using Microsoft.AspNetCore.DataProtection;

namespace Api.Models.Commands.Mongo
{
    public class MongoPasswordMapper : IMongoPasswordMapper
    {
        public PasswordView Map(Password password, IPersistedDataProtector protector)
        {
            bool requiresMigration = true;
            bool wasRevoked;

            return new PasswordView()
            {
                Id = password.Id,
                Customer = password.Company,
                UserName = password.User,
                Password = protector.PersistentUnprotect(password.EncryptedPassword,out requiresMigration,out wasRevoked)
            };
        }
    }
}