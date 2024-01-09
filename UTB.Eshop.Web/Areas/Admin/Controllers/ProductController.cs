using Microsoft.AspNetCore.Mvc;
using UTB.Eshop.Application.Abstraction;
using UTB.Eshop.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Collections.Generic;
using UTB.Eshop.Infrastructure.Identity.Enums;
using UTB.Eshop.Infrastructure.Identity;

namespace UTB.Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager) + ", " + nameof(Roles.Customer))]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductController(IProductService productService, UserManager<User> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _productService = productService;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            User user = _userManager.GetUserAsync(User).Result;
            IList<Product> products = (user.Id == 1 || user.Id == 2) ? _productService.Select() : _productService.SelectByUser(user.Id);

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product, IFormFile image)
        {
            User user = _userManager.GetUserAsync(User).Result;
            product.UserId = user.Id;

            if (image != null && image.Length > 0)
            {
                // Generování unikátního názvu pro nový soubor
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;

                // Vytvoření cesty k novému souboru v složce wwwroot/img/products
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "img", "products", uniqueFileName);

                // Kopírování souboru
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                // Uložení cesty do databáze
                product.ImageSrc = "/img/products/" + uniqueFileName;
            }

            _productService.Create(product);

            return RedirectToAction(nameof(Index));
        }



        public IActionResult Delete(int id)
        {
            bool deleted = _productService.Delete(id);

            return deleted ? RedirectToAction(nameof(Index)) : NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product product = _productService.SelectById(id);

            return product != null ? View(product) : NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Product updatedProduct, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedProduct);
            }

            if (image != null && image.Length > 0)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img", "products");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                updatedProduct.ImageSrc = "/img/products/" + uniqueFileName;
            }

            bool edited = _productService.Edit(updatedProduct);

            return edited ? RedirectToAction(nameof(Index)) : View(updatedProduct);
        }
    }
}
