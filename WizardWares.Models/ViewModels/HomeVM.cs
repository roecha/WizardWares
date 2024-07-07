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
        public string SortOrder { get; set; }
        public string[] FilterOptions { get; set; }

        [ValidateNever]
        public IEnumerable<Product> ProductList { get; set; }
        [ValidateNever]
        public IEnumerable<Advertisement> AdList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
        public IEnumerable<Rarity> RarityList { get; set; }

        public ShoppingCart ShoppingCart { get; set; }
    }
}
