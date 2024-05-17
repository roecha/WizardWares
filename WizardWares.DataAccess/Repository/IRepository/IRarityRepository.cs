using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardWares.Models;

namespace WizardWares.DataAccess.Repositiory.IRepository
{
    public interface IRarityRepository : IRepository<Rarity>
    {
        void Update(Rarity obj);
    }
}
