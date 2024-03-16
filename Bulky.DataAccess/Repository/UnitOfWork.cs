using System;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IDataService;
using Bulky.Models;

namespace Bulky.DataAccess.Repository {
    public class UnitOfWork : IUnitOfWork {

        public ICategoryDataService category { get; private set; }

        public IProductDataService product { get; private set; }

        ApplicationDbContext dbContext;

        public UnitOfWork(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
            category = new CategoryDataService(dbContext);
            product = new ProductDataService(dbContext);
        }

        public void Save() {
            dbContext.SaveChanges();
        }
    }
}

