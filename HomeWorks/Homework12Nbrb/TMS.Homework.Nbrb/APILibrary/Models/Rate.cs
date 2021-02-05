using System;
using System.Collections.Generic;
using System.Text;

namespace APILibrary.Models
{
    public class Rate
    {
        /// <summary>
        /// Internal code (внутренний код)
        /// </summary>
        public int CurID { get; set; }

        /// <summary>
        /// Вate on which the course is requested (дата, на которую запрашивается курс)
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Abbreviation (буквенный код)
        /// </summary>
        public string Cur_Abbreviation { get; set; }

        /// <summary>
        /// Number of units of foreign currency (количество единиц иностранной валюты)
        /// </summary>
        public int Cur_Scale { get; set; }

        /// <summary>
        /// Currency name (наименование валюты)
        /// </summary>
        public string Cur_Name { get; set; }

        /// <summary>
        /// Rate (курс)
        /// </summary>
        public double? Cur_OfficialRate { get; set; }
    }
}
