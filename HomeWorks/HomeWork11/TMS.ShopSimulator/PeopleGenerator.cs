using System;
using System.Text;

namespace TMS.ShopSimulator
{
    internal class PeopleGenerator
    {
	    private Random random;
	    private int personNumber = 1;

        public PeopleGenerator()
        {
	        random = new Random();
        }

        internal Person GetPerson()
        {
            return new Person
            {
                TimeToProcess = random.Next(3000),
                Name = $"Person {personNumber++}"
            };
        }
    }
}