using FullStackDeveloperTask.Api.Models;

namespace FullStackDeveloperTask.Api.Services
{
    public interface IForkliftService
    {
        Task ImportForklifts(IFormFile forklifts, string fileExtension, CancellationToken cancellationToken = default);
        Task<IEnumerable<GetForkliftResponse>> GetForklifts(int? skip, int? take, CancellationToken cancellationToken = default);
        CommandExecuteResponse ExecuteCommand(string command);
    }
}
