using System;

namespace TMS.NET06.TwitterListener.Manager.React.Data.Models
{
    public class ListenerOptions
    {
        public string[] FilterRules { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}