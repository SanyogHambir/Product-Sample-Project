using Dapper;
using Microsoft.AspNetCore.Mvc;
using Product.Infrastructure;
using Product.Models;
using System.Data;

namespace Product.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _product;

        public ProductController(IProductRepository product)
        {
            _product = product;
        }
        public IActionResult Index()
        {
            var products = _product.GetAll().ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Products product)
        {
            if (ModelState.IsValid)
            {
                _product.Add(product);
                TempData["Inserted_Success"] = "Data Has Been Inserted...";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        public IActionResult Details(int id)
        {
            var product = _product.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product); 
        }

        public IActionResult Edit(int id)
        {
            var product = _product.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Products product)
        {
            if (id != product.SN)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _product.Update(product);
                TempData["Updeted_Success"] = "Data Has Been Updeted...";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _product.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _product.Delete(id);
            TempData["Deleted_Success"] = "Data Has Been Deleted...";
            return RedirectToAction(nameof(Index));
        }
    }
}

