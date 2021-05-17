using System;

namespace TMS.NET06.TwitterListener.Manager.React.Data.Models
{
    public class TaskOptions
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CronSchedule { get; set; }
    }
}