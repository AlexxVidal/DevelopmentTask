using FullStackDeveloperTask.Api.Models;
using FullStackDeveloperTask.Api.Models.Extensions;
using FullStackDeveloperTask.Core.Business;

namespace FullStackDeveloperTask.Api.Services
{
    public class ForkliftService(IForkliftBusiness forkliftBusiness): IForkliftService
    {
        public CommandExecuteResponse ExecuteCommand(string command)
        {
            var commandResult = forkliftBusiness.ExecuteCommand(command);

            var commandExecuteReponse = commandResult.ToCommandExecuteReponse();

            return commandExecuteReponse;
        }

        public async Task<IEnumerable<GetForkliftResponse>> GetForklifts(int? skip, int? take, CancellationToken cancellationToken = default)
        {
            var forkliftModels = await forkliftBusiness.GetForkliftsAsync(skip, take, cancellationToken);

            var forkliftsReponse = forkliftModels.Select(f => f.ToGetForkliftReponse());

            return forkliftsReponse;
        }

        public async Task ImportForklifts(IFormFile file, string fileExtension, CancellationToken cancellationToken = default)
        {
            using var stream = file.OpenReadStream();
            await forkliftBusiness.ImportAsync(stream, fileExtension, cancellationToken);
        }
    }
}
