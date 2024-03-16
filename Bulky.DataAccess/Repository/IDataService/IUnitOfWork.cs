using System;
using Bulky.Models;

namespace Bulky.DataAccess.Repository.IDataService {

	public interface IUnitOfWork {
		ICategoryDataService category { get; }
        IProductDataService product { get; }
        void Save();
	}
}

