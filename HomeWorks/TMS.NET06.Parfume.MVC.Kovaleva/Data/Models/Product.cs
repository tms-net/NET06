﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.NET06.Parfume.MVC.Kovaleva.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BrandId { get; set; }
    }
}