using System.Linq;
using System.Threading.Tasks;
using Api.Models.Encryption;
using Api.Models.Mongo;
using Microsoft.AspNetCore.DataProtection;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Api.Models.Commands.Mongo
{
    public class MongoPasswordCommandExecutor : IPasswordCommandExecutor
    {
        private readonly IDataProtectionProvider _protector;
        private readonly IMongoPasswordMapper _mapper;

        public MongoPasswordCommandExecutor(IDataProtectionProvider protector, IMongoPasswordMapper mapper)
        {
            _protector = protector;
            _mapper = mapper;
        }

        public IPersistedDataProtector Protector => (IPersistedDataProtector) _protector.CreateProtector("Api.Jeppe");

        public async Task<AddPasswordResult> AddPasswordAsync(AddPasswordCommand command)
        {
            var collection = MongoUtility.Coll<Password>();
            var result = new Password()
            {
                Company = command.Company,
                EncryptedPassword = Protector.PersistentProtect(command.Password),
                User = command.User,
                Environment = command.Environment,
            };

            await collection.InsertOneAsync(result);

            return new AddPasswordResult()
            {
                Id = result.Id,
            };
        }

        public async Task<GetPasswordResult> GetPasswordsAsync(GetPasswordCommand command)
        {
            var queryResult = await MongoUtility.Coll<Password>()
                .AsQueryable()
                .OrderBy(x => x.Company)
                .ToListAsync();

            return new GetPasswordResult()
            {
                Passwords = queryResult.Select(x => _mapper.Map(x, Protector)).ToList()
            };
        }
    }
}