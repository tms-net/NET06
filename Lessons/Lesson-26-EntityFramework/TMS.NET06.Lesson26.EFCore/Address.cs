using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMSStudens
{
	[NotMapped]
	//[Owned]
	public class Address
	{
		public string City { get; set; }
		public string Street { get; set; }
	}
}
