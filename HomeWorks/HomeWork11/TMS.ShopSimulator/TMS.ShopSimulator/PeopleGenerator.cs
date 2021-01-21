using System;
using System.Text;

namespace TMS.ShopSimulator
{
    internal class PeopleGenerator
    {
        private Random randomTime;
        private Random randomName;

        public PeopleGenerator()
        {
            randomTime = new Random();
            randomName = new Random();
        }

        internal Person GetPerson()
        {
            return new Person
            {
                TimeToProcess = randomTime.Next(10000),
                Name = GetRandomString(randomName.Next(5,15))
            };
        }

        private string GetRandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):   
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)randomName.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}