﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public string Name { get; set; }
        public bool IsPermanent { get; set; }
        public bool IsChecked { get; set; }
        public int Count { get; set; }
    }
}
