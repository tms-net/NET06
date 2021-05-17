using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TMS.NET06.TwitterListener.Manager.React.Data.Models;

namespace TMS.NET06.TwitterListener.Manager.React.Data
{
    public class ListenerManagerContext : DbContext
    {
        private readonly string _connectionString;

        public ListenerManagerContext(DbContextOptions<ListenerManagerContext> options)
            : base(options)
        {
        }

        internal ListenerManagerContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListenerTask>(taskBuilder =>
            {
                taskBuilder.HasKey(t => t.TaskId);
                taskBuilder.OwnsOne(t => t.TaskOptions);
                taskBuilder.Property(t => t.ListenerOptions)
                    .HasConversion(
                        lo => JsonSerializer.Serialize(lo, null),
                        lo => JsonSerializer.Deserialize<ListenerOptions>(lo, null));
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ListenerTask> ListenerTasks { get; set; }
    }
}
