using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.NET06.TwitterListener.Manager.React.Data.Models
{
    public class ListenerTask
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public ListenerTaskStatus Status { get; set; }

        public ListenerOptions ListenerOptions { get; set; }
        public TaskOptions TaskOptions { get; set; }
    }
}
