using System;
using System.Linq.Expressions;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IDataService;
using Bulky.Models;

namespace Bulky.DataAccess.Repository {
    public class ProductDataService : DataService<Product>, IProductDataService {

        public ProductDataService(ApplicationDbContext dbContext) : base(dbContext) {
            this.dbContext = dbContext;
        }

        public void Update(Product product) {
            dbContext.products.Update(product);
        }
    }
}

