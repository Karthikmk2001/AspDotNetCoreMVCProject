using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectActivity.Models;

namespace ProjectActivity.Controllers
{
    public class BrandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Brands
        public async Task<IActionResult> Index()
        {
            ViewBag.isLogged = HttpContext.Session.GetString("islogged");
            ViewBag.Role = HttpContext.Session.GetString("UserRole");
            return _context.Brands != null ? 
                          View(await _context.Brands.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Brands'  is null.");
        }

        // GET: Brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.isLogged = HttpContext.Session.GetString("islogged");
            ViewBag.Role = HttpContext.Session.GetString("UserRole");
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Brands/Create
        public IActionResult Create()
        {
            ViewBag.isLogged = HttpContext.Session.GetString("islogged");
            ViewBag.Role = HttpContext.Session.GetString("UserRole");
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandId,BrandName,BrandDescription,BrandAddedDate")] Brand brand)
        {
           
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.isLogged = HttpContext.Session.GetString("islogged");
            ViewBag.Role = HttpContext.Session.GetString("UserRole");
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrandId,BrandName,BrandDescription,BrandAddedDate")] Brand brand)
        {
            if (id != brand.BrandId)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.BrandId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.isLogged = HttpContext.Session.GetString("islogged");
            ViewBag.Role = HttpContext.Session.GetString("UserRole");
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Brands == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Brands'  is null.");
            }
            var brand = await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
        {
          return (_context.Brands?.Any(e => e.BrandId == id)).GetValueOrDefault();
        }
    }
}
