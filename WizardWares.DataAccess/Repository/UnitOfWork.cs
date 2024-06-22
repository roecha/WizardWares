using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardWares.DataAccess.Data;
using WizardWares.DataAccess.Repositiory.IRepository;
using WizardWares.DataAccess.Repository;
using WizardWares.DataAccess.Repository.IRepository;

namespace WizardWares.DataAccess.Repositiory
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public IRarityRepository Rarity { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IAdvertisementRepository Advertisement { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Rarity = new RarityRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            Advertisement = new AdvertisementRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();

        }
    }
}
