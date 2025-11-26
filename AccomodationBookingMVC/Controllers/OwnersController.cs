using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AccomodationBookingMVC.Models;

namespace AccomodationBookingMVC.Controllers
{
    public class OwnersController : Controller
    {
        private readonly AccommodationContext _context;

        public OwnersController(AccommodationContext context)
        {
            _context = context;
        }

        // GET: Owners
        [ResponseCache(CacheProfileName = "DefaultCachedList")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Owners.ToListAsync());
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var owner = await _context.Owners
                .FirstOrDefaultAsync(m => m.OwnerId == id);

            if (owner == null) return NotFound();

            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OwnerId,FullName,Gender,BirthDate,PhoneNumber,PassportData")] Owner owner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(owner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(owner);
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var owner = await _context.Owners.FindAsync(id);
            if (owner == null) return NotFound();

            return View(owner);
        }

        // POST: Owners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OwnerId,FullName,Gender,BirthDate,PhoneNumber,PassportData")] Owner owner)
        {
            if (id != owner.OwnerId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(owner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Owners.Any(e => e.OwnerId == id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }
            return View(owner);
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var owner = await _context.Owners
                .FirstOrDefaultAsync(m => m.OwnerId == id);

            if (owner == null) return NotFound();

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner != null) _context.Owners.Remove(owner);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
