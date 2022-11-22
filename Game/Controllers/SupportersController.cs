using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Game.Context;
using Game.Models;

namespace Game.Controllers
{
    public class SupportersController : Controller
    {
        private readonly EFContext _context;

        public SupportersController(EFContext context)
        {
            _context = context;
        }

        // GET: Supporters
        public async Task<IActionResult> Index()
        {
            var eFContext = _context.Supporters.Include(s => s.ReferencedPlayer);
            return View(await eFContext.ToListAsync());
        }

        // GET: Supporters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Supporters == null)
            {
                return NotFound();
            }

            var supporter = await _context.Supporters
                .Include(s => s.ReferencedPlayer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (supporter == null)
            {
                return NotFound();
            }

            return View(supporter);
        }

        // GET: Supporters/Create
        public IActionResult Create()
        {
            ViewData["HeroID"] = new SelectList(_context.Players, "ID", "Name");
            return View();
        }

        // POST: Supporters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,HeroID,Name")] Supporter supporter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supporter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HeroID"] = new SelectList(_context.Players, "ID", "Name", supporter.HeroID);
            return View(supporter);
        }

        // GET: Supporters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Supporters == null)
            {
                return NotFound();
            }

            var supporter = await _context.Supporters.FindAsync(id);
            if (supporter == null)
            {
                return NotFound();
            }
            ViewData["HeroID"] = new SelectList(_context.Players, "ID", "Name", supporter.HeroID);
            return View(supporter);
        }

        // POST: Supporters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,HeroID,Name")] Supporter supporter)
        {
            if (id != supporter.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supporter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupporterExists(supporter.ID))
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
            ViewData["HeroID"] = new SelectList(_context.Players, "ID", "Name", supporter.HeroID);
            return View(supporter);
        }

        // GET: Supporters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Supporters == null)
            {
                return NotFound();
            }

            var supporter = await _context.Supporters
                .Include(s => s.ReferencedPlayer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (supporter == null)
            {
                return NotFound();
            }

            return View(supporter);
        }

        // POST: Supporters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Supporters == null)
            {
                return Problem("Entity set 'EFContext.Supporters'  is null.");
            }
            var supporter = await _context.Supporters.FindAsync(id);
            if (supporter != null)
            {
                _context.Supporters.Remove(supporter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupporterExists(int id)
        {
          return _context.Supporters.Any(e => e.ID == id);
        }
    }
}
