using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDeveloperTask.Data.Models
{
    public interface IRoboticsContext
    {
        DbSet<Forklift> Forklifts { get; }

        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
