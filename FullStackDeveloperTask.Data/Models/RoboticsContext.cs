using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDeveloperTask.Data.Models
{
    public class RoboticsContext(DbContextOptions<RoboticsContext> options) : DbContext(options), IRoboticsContext
    {
        public virtual DbSet<Forklift> Forklifts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Forklift>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.ModelNumber).HasMaxLength(50);
            });
        }
    }
}
