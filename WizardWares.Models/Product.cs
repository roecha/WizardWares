﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardWares.Models
{
    internal class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public double PriceMoney { get; set; }
        public string PriceObject { get; set; }
        //public int CategoryId { get; set; }
        //[ForeignKey("CategoryId")]
        //[ValidateNever]
        //public Category Category { get; set; }
        //[ValidateNever]
        public string ImageUrl { get; set; }
    }}
