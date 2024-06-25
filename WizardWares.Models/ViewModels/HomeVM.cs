using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace WizardWares.Models.ViewModels
{
    public class HomeVM
    {
        [ValidateNever]
        public IEnumerable<Product> ProductList { get; set; }
        [ValidateNever]
        public IEnumerable<Advertisement> AdList { get; set; }

        public ShoppingCart ShoppingCart { get; set; }
    }
}
