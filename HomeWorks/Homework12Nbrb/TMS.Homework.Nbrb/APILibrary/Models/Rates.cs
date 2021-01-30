using System;
using System.Collections.Generic;
using System.Text;

namespace APILibrary.Models
{
    public class Rates
    {
        public int CurID { get; set; } //– внутренний код

        public DateTime Date { get; set; } //– дата, на которую запрашивается курс

        public string Cur_Abbreviation { get; set; } //– буквенный код

        public int Cur_Scale { get; set; } //– количество единиц иностранной валюты

        public string Cur_Name { get; set; } //– наименование валюты на русском языке во множественном, либо в единственном числе, в зависимости от количества единиц

        public double? Cur_OfficialRate { get; set; } //– курс*
    }
}
