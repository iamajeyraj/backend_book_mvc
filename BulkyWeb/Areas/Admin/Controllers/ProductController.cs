using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bulky.DataAccess.Repository.IDataService;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BulkyWeb.Areas.Admin.Controllers {
    [Area("Admin")]
    public class ProductController : Controller {
        // GET: /<controller>/
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index() {
            var products = unitOfWork.product.GetAll();
            return View(products);
        }

        public IActionResult Create() {
            ProductViewModel productVM = new ProductViewModel() {
                Product = new Product(),
                CategoryList = unitOfWork.category.GetAll().Select(x => new SelectListItem {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                })
            };

            return View(productVM);
        }

        public IActionResult Upsert(int? id) {
            ProductViewModel productVM = new ProductViewModel() {
                CategoryList = unitOfWork.category.GetAll().Select(x => new SelectListItem {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                })
            };

            if(id == 0 || id == null) {
                return View(productVM);
            } else {
                productVM.Product = unitOfWork.product.Get(x=>x.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel productVM, IFormFile formFile) {

            ModelState.Remove("Product.Id");
            if(ModelState.IsValid) {
            string webRoot = webHostEnvironment.WebRootPath;

                if(formFile != null) {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                    string productPath = Path.Combine(webRoot, @"Images/Product");

                    using(FileStream fileStream = new FileStream(Path.Combine(productPath,fileName), FileMode.Create)) {
                        formFile.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"/Images/Product/" + fileName;
                }

                unitOfWork.product.Add(productVM.Product);
                unitOfWork.Save();
                TempData["success"] = "Created product successfuly";
                return RedirectToAction("Index");
            }

            productVM.CategoryList = unitOfWork.category.GetAll().Select(x => new SelectListItem {
                Text = x.CategoryName,
                Value = x.Id.ToString()
            });
            return View(productVM);
        }

        public IActionResult Delete(int? id) {
            if(id == null || id == 0) {
                return NotFound();
            }

            var product = unitOfWork.product.Get(x => x.Id == id);

            if(product == null) {
                return NotFound();
            };
            return View(product);
        }

        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteProduct(Product item) {
            var product = unitOfWork.product.Get(x => x.Id == item.Id);
            if(product != null) {
                TempData["success"] = "Product deleted successfuly";
                unitOfWork.product.Remove(product);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
