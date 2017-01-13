using System.Threading.Tasks;
using Api.Models.Commands.Mongo;

namespace Api.Models.Commands
{
    public interface IPasswordCommandExecutor
    {
        Task<AddPasswordResult> AddPasswordAsync(AddPasswordCommand command);
        Task<GetPasswordResult> GetPasswordsAsync(GetPasswordCommand command);
    }
}