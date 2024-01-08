using Microsoft.AspNetCore.Mvc;
using UTB.Eshop.Application.Abstraction;
using UTB.Eshop.Domain.Entities;
using UTB.Eshop.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using UTB.Eshop.Infrastructure.Identity.Enums;

namespace UTB.Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class ProductController : Controller
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            IList<Product> products = _productService.Select();
            return View(products);
        }

        [HttpGet] //vychozi atribut pro akcni metody

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _productService.Create(product);

            return RedirectToAction(nameof(ProductController.Index));
        }

        public IActionResult Delete(int id)
        {
            bool deleted = _productService.Delete(id);

            if (deleted)
                return RedirectToAction(nameof(ProductController.Index));
            else
                return NotFound();
        }

        public IActionResult Edit(int id)
        {
            // Načtěte produkt z databáze nebo jiného zdroje podle id
            Product product = _productService.SelectById(id);

            if (product == null)
            {
                return NotFound(); // Můžete také vrátit 404, pokud produkt s daným ID není nalezen.
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product updatedProduct)
        {
            // Pokud je model neplatný, zobrazte pohled s chybami
            if (!ModelState.IsValid)
            {
                return View(updatedProduct);
            }

            // Zavolejte metodu pro editaci produktu v ProductService
            bool edited = _productService.Edit(updatedProduct);

            if (edited)
            {
                return RedirectToAction(nameof(Index)); // Přesměrování na seznam produktů po úspěšné úpravě
            }
            else
            {
                return View(updatedProduct);
            }
        }


    }
}