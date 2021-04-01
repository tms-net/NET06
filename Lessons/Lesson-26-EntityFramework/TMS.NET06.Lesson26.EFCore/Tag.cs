using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TMSStudens.TMSContext;

namespace TMSStudens
{
    public class Tag
    {
        public string Id { get; set; }

		public string Name { get; set; }

		public ICollection<Homework> Homeworks { get; set; }
    }
}
