using System;
using System.Collections.Generic;
using System.Text;

namespace APILibrary.Models
{
    public class Currency
    { public int Cur_ID { get; set; }// внутренний код
        public int Cur_ParentID { get; set; }//– этот код используется для связи, при изменениях наименования, количества единиц к которому устанавливается курс белорусского рубля, буквенного, цифрового кодов и т.д.фактически одной и той же валюты*.
        public int Cur_Code { get; set; }//– цифровой код
        public string Cur_Abbreviation { get; set; } //– буквенный код
        public string Cur_Name { get; set; } //– наименование валюты на русском языке
        public string Cur_Name_Bel { get; set; } //– наименование на белорусском языке
        public string Cur_Name_Eng { get; set; } //– наименование на английском языке
        public string Cur_QuotName { get; set; }//наименование валюты на русском языке, содержащее количество единиц
        public string Cur_QuotName_Bel { get; set; } //– наименование на белорусском языке, содержащее количество единиц
        public string Cur_QuotName_Eng { get; set; }// – наименование на английском языке, содержащее количество единиц
        public string Cur_NameMulti { get; set; } //– наименование валюты на русском языке во множественном числе
        public string Cur_Name_BelMulti { get; set; } //– наименование валюты на белорусском языке во множественном числе*
        public string Cur_Name_EngMulti { get; set; } //– наименование на английском языке во множественном числе*
        public decimal Cur_Scale { get; set; } //– количество единиц иностранной валюты
        public int Cur_Periodicity { get; set; } //– периодичность установления курса(0 – ежедневно, 1 – ежемесячно)
        public DateTime? Cur_DateStart { get; set; } // – дата включения валюты в перечень валют, к которым устанавливается официальный курс бел.рубля
        public DateTime? Cur_DateEnd { get; set; } //– дата исключения валюты из перечня валют, к которым устанавливается официальный курс бел.рубля
    }
}
