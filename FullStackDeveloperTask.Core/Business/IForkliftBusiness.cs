using FullStackDeveloperTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDeveloperTask.Core.Business
{
    public interface IForkliftBusiness
    {
        Task ImportAsync(Stream fileStream, string extension, CancellationToken cancellationToken = default);
        Task<Forklift[]> GetForkliftsAsync(int? take, int? skip, CancellationToken cancellationToken = default);
        Command ExecuteCommand(string command);
    }
}
