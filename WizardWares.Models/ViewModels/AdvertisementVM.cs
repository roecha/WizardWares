using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace WizardWares.Models.ViewModels
{
    public class AdvertisementVM
    {
        public Advertisement Advertisement { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ProductList { get; set; }

    }
}
