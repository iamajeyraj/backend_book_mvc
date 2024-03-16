using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bulky.DataAccess.Repository.IDataService;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BulkyWeb.Areas.Admin.Controllers {
    [Area("Admin")]
    public class ProductController : Controller {
        // GET: /<controller>/
        private readonly IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index() {
            var products = unitOfWork.product.GetAll();
            return View(products);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product) {
            if(ModelState.IsValid) {
                unitOfWork.product.Add(product);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id) {
            if(id == null || id == 0) {
                return NotFound();
            }

            var product = unitOfWork.product.Get(x => x.Id == id);

            if(product == null) {
                return NotFound();
            };
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product) {
            if(ModelState.IsValid) {
                unitOfWork.product.Update(product);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
