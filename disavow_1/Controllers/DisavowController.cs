using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using disavow_1.Models;

namespace disavow_1.Controllers
{
    public class DisavowController : Controller
    {
        private readonly disavow_1Context _context;

        public DisavowController(disavow_1Context context)
        {
            _context = context;
        }

        // GET: Disavow
        public async Task<IActionResult> Index()
        {
            return View(await _context.Disavow.ToListAsync());
        }

        // GET: Disavow/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disavow = await _context.Disavow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disavow == null)
            {
                return NotFound();
            }

            return View(disavow);
        }

        // GET: Disavow/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Disavow/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Url,CheckedDate,NoFollowNoLink")] Disavow disavow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disavow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disavow);
        }

        // GET: Disavow/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disavow = await _context.Disavow.FindAsync(id);
            if (disavow == null)
            {
                return NotFound();
            }
            return View(disavow);
        }

        // POST: Disavow/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Url,CheckedDate,NoFollowNoLink")] Disavow disavow)
        {
            if (id != disavow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disavow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisavowExists(disavow.Id))
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
            return View(disavow);
        }

        // GET: Disavow/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disavow = await _context.Disavow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disavow == null)
            {
                return NotFound();
            }

            return View(disavow);
        }

        // POST: Disavow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disavow = await _context.Disavow.FindAsync(id);
            _context.Disavow.Remove(disavow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisavowExists(int id)
        {
            return _context.Disavow.Any(e => e.Id == id);
        }
    }
}
