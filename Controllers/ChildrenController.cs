using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChildrenWebApp.Data;
using ChildrenWebApp.Models;
using System.Text;

namespace ChildrenWebApp.Controllers
{
    public class ChildrenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChildrenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Children
        public async Task<IActionResult> Index()
        {
            return View(await _context.Child.ToListAsync());
        }

        // GET: Children/Create
        public IActionResult Create()
        {
            var child = new Child();
            return View(child);
        }

        // POST: Children/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Gender,FavoriteAnimals,Email")] Child child)
        {
            if (ModelState.IsValid)
            {                

                child.IsExported = false; // Set IsExported to false on new entry
                _context.Add(child);
                await _context.SaveChangesAsync();
                // Check for "Bugs Bunny"
                if (child.Name == "Bugs Bunny" && child.FavoriteAnimals.Contains("Bunny"))
                {
                    TempData["FunnySuccessMessage"] = "You are funny!";
                }
                else
                {
                    TempData["CreateSuccessMessage"] = "The child is created successfully.";
                }
            }
            return View(child);
        }

        // GET: Children/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Child.FindAsync(id);
            if (child == null)
            {
                return NotFound();
            }
            return View(child);
        }

        // POST: Children/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender,FavoriteAnimals,Email")] Child child)
        {
            if (id != child.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    child.IsExported = false; // Set IsExported to false on edited entry
                    _context.Update(child);
                    await _context.SaveChangesAsync();
                    // Check for "Bugs Bunny"
                    if (child.Name == "Bugs Bunny" && child.FavoriteAnimals.Contains("Bunny"))
                    {
                        TempData["FunnySuccessMessage"] = "You are funny!";
                    } else
                    {
                        TempData["EditSuccessMessage"] = "The child is updated successfully.";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChildExists(child.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(child);
        }

        // GET: Children/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Child
                .FirstOrDefaultAsync(m => m.Id == id);
            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }

        // POST: Children/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var child = await _context.Child.FindAsync(id);
            if (child != null)
            {
                _context.Child.Remove(child);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChildExists(int id)
        {
            return _context.Child.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ExportToCsv(bool onlyChanged = false)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Name, Gender, FavoriteAnimals, Email");
            var children = _context.Child;
            var exportList = onlyChanged ? children.Where(c => !c.IsExported) : children;

            if(exportList != null && exportList.Any())
            {
                foreach (var child in exportList)
                {
                    var animals = string.Join(", ", child.FavoriteAnimals);
                    csv.AppendLine($"{child.Name}, {child.Gender}, {animals}, {child.Email}");
                    child.IsExported = true; // Set IsExported to true on export
                }
                _context.UpdateRange(exportList); // Update all children in exportList
                await _context.SaveChangesAsync();

                var bytes = Encoding.UTF8.GetBytes(csv.ToString());
                return File(bytes, "text/csv", "children.csv");
            }
            // No records to export
            return RedirectToAction(nameof(Index));
        }
    }
}
