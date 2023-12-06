using ConsultorioSeguros.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioSeguros.Controllers
{
    public class SegurosController : Controller
    {
        private readonly DBSegurosContext _context;

        public SegurosController(DBSegurosContext context)
        {
            _context = context;
        }

        // GET: SegurosController
        public async Task<IActionResult> Index()
        {
            if (_context.Seguros == null)
            {
                return Problem("Entity set 'DBSeguros.Seguros' is null.");
            }

            var seguros = await _context.Seguros.ToListAsync();
            return View(seguros);
        }

        // GET: SegurosController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seguros = await _context.Seguros.FirstOrDefaultAsync(x => x.Id == id);

            return seguros != null ? View(seguros) : NotFound();
        }

        // GET: SegurosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SegurosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreSeguro,CodigoSeguro,SumaAsegurada,Prima")] Seguro seguros)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seguros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seguros);
        }

        // GET: SegurosController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var seguros = await _context.Seguros.FindAsync(id);

            return seguros == null ? NotFound() : View(seguros);
        }

        // POST: SegurosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreSeguro,CodigoSeguro,SumaAsegurada,Prima")] Seguro seguros)
        {
            if (id != seguros.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(seguros);
            }

            try
            {
                _context.Update(seguros);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeguroExists(seguros.Id))
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

        private bool SeguroExists(int id)
        {
            return (_context.Seguros?.Any(x => x.Id == id)).GetValueOrDefault();
        }

        // GET: SegurosController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seguros = await _context.Seguros.FirstOrDefaultAsync(x => x.Id == id);

            return seguros == null ? NotFound() : View(seguros);
        }

        // POST: SegurosController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seguros = await _context.Seguros.FindAsync(id);

            if (seguros == null)
            {
                return Problem("El registro no fue encontrado.");
            }

            _context.Seguros.Remove(seguros);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
