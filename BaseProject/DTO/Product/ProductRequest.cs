﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.DTO.Product
{
    public class ProductRequest
    {
        public string ProductName { get; set; }
        public string Category { get; set; }
        public float Price { get; set; }
    }
}
