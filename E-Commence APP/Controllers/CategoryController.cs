﻿using E_Commence_APP.Data;
using Microsoft.AspNetCore.Mvc;
using E_Commence_APP.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_Commence_APP.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext db)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _context.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The display Order cannot match the name");
            }
            if (obj.Name!= null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value");
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("index", "Category");
            }
            return View();
            
        }

        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            Category category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            
            if (ModelState.IsValid)
            {
                _context.Categories.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("index", "Category");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category obj = _context.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(obj);
            _context.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("index");

        }
    }
}
