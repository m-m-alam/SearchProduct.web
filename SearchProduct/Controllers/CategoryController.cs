﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchProduct.Data;
using SearchProduct.Models;

namespace SearchProduct.Controllers
{
    public class CategoryController : Controller
    {
        public readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //var category = _context.Categories.ToList();
            return View();
        }
        [HttpGet]
        public IActionResult GetData()
        {
            var category = _context.Categories.ToList();
            return Json(category);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {            
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Json(new { result = "success" });           
        }
       
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return Json(new { success = true });
        }

        
        public JsonResult Delete(int id)
        {
            var product = _context.Categories.FirstOrDefault(x=>x.Id==id);
            if (product != null)
            {
                _context.Categories.Remove(product);
                _context.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        
    }

}