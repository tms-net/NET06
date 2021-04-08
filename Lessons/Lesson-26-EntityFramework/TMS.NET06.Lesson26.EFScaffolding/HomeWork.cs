using System;
using System.Collections.Generic;

#nullable disable

namespace TMS.NET06.Lesson26.EFScaffolding
{
    public partial class HomeWork
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PullRequestUrl { get; set; }
        public string Mark { get; set; }
        public bool IsComplete { get; set; }

        public virtual Student User { get; set; }
    }
}
