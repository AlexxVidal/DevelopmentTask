using FullStackDeveloperTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDeveloperTask.Core.Readers
{
    public  interface IForkliftFileReader
    {
        Task<IEnumerable<Forklift>> ReadForkliftDataAsync(Stream stream, CancellationToken cancellationToken = default);
    }
}
