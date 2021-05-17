using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.NET06.Parfume.Manager.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly string _connString;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        internal ApplicationDbContext(string connString)
        {
            _connString = connString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_connString))
            {
                optionsBuilder.UseSqlServer(_connString);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
