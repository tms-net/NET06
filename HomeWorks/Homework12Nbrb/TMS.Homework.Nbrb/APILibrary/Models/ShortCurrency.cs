using System;
using System.Collections.Generic;
using System.Text;

namespace APILibrary.Models
{
    public class ShortCurrency
    {
        /// <summary>
        /// Currency code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Abbreviation
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// Currency name
        /// </summary>
        public string Name { get; set; }
    }
}
