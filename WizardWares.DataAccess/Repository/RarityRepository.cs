using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardWares.DataAccess.Data;
using WizardWares.DataAccess.Repositiory.IRepository;
using WizardWares.Models;

namespace WizardWares.DataAccess.Repositiory
{
    public class RarityRepository : Repository<Rarity>, IRarityRepository
    {
        private ApplicationDbContext _db;
        public RarityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Rarity obj)
        {
            _db.Rarities.Update(obj);
        }

    }
}
