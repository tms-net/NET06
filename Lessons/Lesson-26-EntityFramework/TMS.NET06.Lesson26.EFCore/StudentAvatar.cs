using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSStudens
{
    public class StudentAvatar
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string Caption { get; set; }

        public /*virtual*/ Student Student { get; set; }
	}
}
