using System;

namespace APILibrary.Models
{
    public class Currency
    {
        /// <summary>
        /// Internal code (внутренний код)
        /// </summary>
        public int Cur_ID { get; set; }

        /// <summary>
        /// Parent ID code currency (этот код используется для связи, при изменениях наименования)
        /// </summary>
        public int Cur_ParentID { get; set; }

        /// <summary>
        /// Digital code (цифровой код)
        /// </summary>
        public int Cur_Code { get; set; }

        /// <summary>
        /// Letter code (буквенный код)
        /// </summary>
        public string Cur_Abbreviation { get; set; }

        /// <summary>
        /// Name of currency in Russian (наименование валюты на русском языке)
        /// </summary>
        public string Cur_Name { get; set; }

        /// <summary>
        /// Name of currency in Belorussian (наименование валюты на белорусском языке)
        /// </summary>
        public string Cur_Name_Bel { get; set; }

        /// <summary>
        /// Name of currency in English (наименование валюты на английском языке)
        /// </summary>
        public string Cur_Name_Eng { get; set; }

        /// <summary>
        /// The name of the currency in Russian, containing the number of units (наименование валюты на русском языке, содержащее количество единиц)
        /// </summary>
        public string Cur_QuotName { get; set; }

        /// <summary>
        /// The name of the currency in Belorussian, containing the number of units (наименование валюты на белорусском языке, содержащее количество единиц)
        /// </summary>
        public string Cur_QuotName_Bel { get; set; }

        /// <summary>
        /// The name of the currency in English, containing the number of units (наименование валюты на английском языке, содержащее количество единиц)
        /// </summary>
        public string Cur_QuotName_Eng { get; set; }

        /// <summary>
        /// The name of the currency in Russian in the plural (наименование валюты на русском языке во множественном числе)
        /// </summary>
        public string Cur_NameMulti { get; set; }

        /// <summary>
        /// The name of the currency in Belorussian in the plural (наименование валюты на белорусском языке во множественном числе)
        /// </summary>
        public string Cur_Name_BelMulti { get; set; }

        /// <summary>
        /// The name of the currency in English in the plural (наименование валюты на английском языке во множественном числе)
        /// </summary>
        public string Cur_Name_EngMulti { get; set; }

        /// <summary>
        /// Number of units of foreign currency (количество единиц иностранной валюты)
        /// </summary>
        public decimal Cur_Scale { get; set; }

        /// <summary>
        /// The frequency of setting currency (периодичность установления курса)
        /// </summary>
        public int Cur_Periodicity { get; set; }

        /// <summary>
        /// Date of inclusion of a currency in the list of currencies (дата включения валюты в перечень валют)
        /// </summary>
        public DateTime? Cur_DateStart { get; set; }

        /// <summary>
        /// Date of exclusion of a currency from the list of currencies (дата исключения валюты из перечня валют)
        /// </summary>
        public DateTime? Cur_DateEnd { get; set; }
    }
}
