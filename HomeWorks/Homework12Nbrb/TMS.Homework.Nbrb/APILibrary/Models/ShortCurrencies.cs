using System;
using System.Collections.Generic;
using System.Text;

namespace APILibrary.Models
{
    public class ShortCurrencies
    {
        /// <summary>
        /// Code currency (код валюты)
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Abbreviation currency (Абревиатура валюты)
        /// </summary>
        public string Abbreviation { get; set; }
    }
}
