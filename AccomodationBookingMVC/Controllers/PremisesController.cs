using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AccomodationBookingMVC.Models;

namespace AccomodationBookingMVC.Controllers
{
    public class PremisesController : Controller
    {
        private readonly AccommodationContext _context;

        public PremisesController(AccommodationContext context)
        {
            _context = context;
        }

        // GET: Premises
        [ResponseCache(CacheProfileName = "DefaultCachedList")]
        public async Task<IActionResult> Index()
        {
            var premises = _context.Premises
                .Include(p => p.Owner)
                .Include(p => p.PremiseType);
            return View(await premises.ToListAsync());
        }


        // GET: Premises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var premise = await _context.Premises
                .Include(p => p.Owner)
                .Include(p => p.PremiseType)
                .FirstOrDefaultAsync(m => m.PremiseId == id);

            if (premise == null) return NotFound();

            return View(premise);
        }

        // GET: Premises/Create
        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId");
            ViewData["PremiseTypeId"] = new SelectList(_context.PremiseTypes, "PremiseTypeId", "PremiseTypeId");
            return View();
        }

        // POST: Premises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PremiseId,OwnerId,PremiseTypeId,PremiseName,Address,RoomCount,Area,HasRestroom")] Premise premise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(premise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId", premise.OwnerId);
            ViewData["PremiseTypeId"] = new SelectList(_context.PremiseTypes, "PremiseTypeId", "PremiseTypeId", premise.PremiseTypeId);

            return View(premise);
        }

        // GET: Premises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var premise = await _context.Premises.FindAsync(id);
            if (premise == null) return NotFound();

            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId", premise.OwnerId);
            ViewData["PremiseTypeId"] = new SelectList(_context.PremiseTypes, "PremiseTypeId", "PremiseTypeId", premise.PremiseTypeId);

            return View(premise);
        }

        // POST: Premises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PremiseId,OwnerId,PremiseTypeId,PremiseName,Address,RoomCount,Area,HasRestroom")] Premise premise)
        {
            if (id != premise.PremiseId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(premise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Premises.Any(e => e.PremiseId == id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId", premise.OwnerId);
            ViewData["PremiseTypeId"] = new SelectList(_context.PremiseTypes, "PremiseTypeId", "PremiseTypeId", premise.PremiseTypeId);

            return View(premise);
        }

        // GET: Premises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var premise = await _context.Premises
                .Include(p => p.Owner)
                .Include(p => p.PremiseType)
                .FirstOrDefaultAsync(m => m.PremiseId == id);

            if (premise == null) return NotFound();

            return View(premise);
        }

        // POST: Premises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var premise = await _context.Premises.FindAsync(id);

            if (premise != null)
                _context.Premises.Remove(premise);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
