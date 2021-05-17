using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.NET06.TwitterListener.Manager.React.Data;

namespace TMS.NET06.Parfume.Manager.MVC.Data
{
    public class ListenerManagerContextFactory : IDesignTimeDbContextFactory<ListenerManagerContext>
    {
        public ListenerManagerContext CreateDbContext(string[] args)
        {
            var connectionString = args[1];
            return new ListenerManagerContext(connectionString);
        }
    }
}
