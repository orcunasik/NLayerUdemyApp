using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Web.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;

        public ProductsController(CategoryApiService categoryApiService, ProductApiService productApiService)
        {
            _categoryApiService = categoryApiService;
            _productApiService = productApiService;
        }

        public async Task<IActionResult> Index()
        {
            var customResponse = await _productApiService.GetProductsWithCategoryAsync();
            return View(customResponse);
        }
        [HttpGet]
        public async Task<IActionResult> Save()
        {
            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.SaveAsync(productDto);
                return RedirectToAction(nameof(Index));
            }
            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet]
        public  async Task<IActionResult> Update(int id)
        {
            var products = await _productApiService.GetByIdAsync(id);
            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name",products.CategoryId);
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.UpdateAsync(productDto);
                return RedirectToAction(nameof(Index));
            }
            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);
            return View(productDto);
        }
        public async Task<IActionResult> Remove(int id)
        {
            await _productApiService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
