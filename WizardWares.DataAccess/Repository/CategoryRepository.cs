﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardWares.DataAccess.Data;
using WizardWares.DataAccess.Repository.IRepository;
using WizardWares.Models;

namespace WizardWares.DataAccess.Repositiory
{
    public class AdvertisementRepository : Repository<Advertisement>, IAdvertisementRepository
    {
        private ApplicationDbContext _db;
        public AdvertisementRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Advertisement obj)
        {
            _db.Advertisements.Update(obj);
        }

    }
}
