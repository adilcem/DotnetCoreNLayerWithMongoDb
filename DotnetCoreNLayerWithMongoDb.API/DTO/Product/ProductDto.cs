﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreNLayer.API.DTO.Product
{
    public class ProductDto
    {
        public virtual long Id { get; set; }
        [Required(ErrorMessage = "{0} field is required")]
        public string Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage ="{0} field must be greater than 1")]
        public int Stock { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "{0} field must be greater than 1")]
        public decimal Price { get; set; }
        public long CategoryId { get; set; }       
    }
}
