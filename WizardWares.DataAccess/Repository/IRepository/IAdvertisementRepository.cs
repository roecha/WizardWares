using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardWares.DataAccess.Repositiory.IRepository;
using WizardWares.Models;

namespace WizardWares.DataAccess.Repository.IRepository
{
    public interface IAdvertisementRepository : IRepository<Advertisement>
    {
        void Update(Advertisement obj);
    }
}
