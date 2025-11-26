using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AccomodationBookingMVC.Models;

namespace AccomodationBookingMVC.Controllers
{
    public class TenantsController : Controller
    {
        private readonly AccommodationContext _context;

        public TenantsController(AccommodationContext context)
        {
            _context = context;
        }

        // GET: Tenants
        [ResponseCache(CacheProfileName = "DefaultCachedList")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tenants.ToListAsync());
        }


        // GET: Tenants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tenant = await _context.Tenants
                .FirstOrDefaultAsync(m => m.TenantId == id);

            if (tenant == null) return NotFound();

            return View(tenant);
        }

        // GET: Tenants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tenants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantId,FullName,Gender,BirthDate,PhoneNumber,PassportData,MaxPrice,AdditionalWishes")] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tenant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(tenant);
        }

        // GET: Tenants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tenant = await _context.Tenants.FindAsync(id);

            if (tenant == null) return NotFound();

            return View(tenant);
        }

        // POST: Tenants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TenantId,FullName,Gender,BirthDate,PhoneNumber,PassportData,MaxPrice,AdditionalWishes")] Tenant tenant)
        {
            if (id != tenant.TenantId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tenants.Any(e => e.TenantId == id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(tenant);
        }

        // GET: Tenants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tenant = await _context.Tenants
                .FirstOrDefaultAsync(m => m.TenantId == id);

            if (tenant == null) return NotFound();

            return View(tenant);
        }

        // POST: Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);

            if (tenant != null)
                _context.Tenants.Remove(tenant);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
