using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SearchProduct.Data;
using SearchProduct.Models;

namespace SearchProduct.Controllers
{
    public class ProductController : Controller
    {
        public readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Products.Include(x=>x.Category).ToList();
            ViewBag.CategoryList = new SelectList(_context.Categories, "Name", "Name");
            return View(data);
        }

        [HttpPost]
        public IActionResult Index(string category)
        {
            var data = _context.Products.Include(x => x.Category).Where(x=>x.Category.Name == category).ToList();
            ViewBag.CategoryList = new SelectList(_context.Categories, "Name", "Name");
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var exitProduct = _context.Products.FirstOrDefault(c => c.Name == product.Name);
                if (exitProduct != null)
                {
                    ViewBag.message = "This product is already exist";
                   
                    return View(product);
                }                               
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryList = new SelectList(_context.Categories, "Id", "Name");
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, Product product)
        {
            if (ModelState.IsValid)
            {
                if (product == null || id != product.Id)
                {
                    return NotFound();
                }
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }            
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id, Product product)
        {
           
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
            
        }
    }
}
