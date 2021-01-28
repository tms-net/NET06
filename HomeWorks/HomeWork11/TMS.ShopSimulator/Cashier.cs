using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.ShopSimulator
{
    internal class Cashier
    {
        private static Random rnd = new Random();

        public int TimeToProcess { get; }
        public string Name { get; }

        public Cashier(int num)
        {
            this.TimeToProcess = rnd.Next(3000);
            this.Name = $"#{num}";
        }

    }
}
