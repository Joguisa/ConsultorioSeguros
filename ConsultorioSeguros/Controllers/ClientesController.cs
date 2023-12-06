using ConsultorioSeguros.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioSeguros.Controllers
{
    public class ClientesController : Controller
    {
        private readonly DBSegurosContext _context;

        public ClientesController(DBSegurosContext context)
        {
            _context = context;
        }

        // GET: ClientesController
        public async Task<IActionResult> Index()
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'DBSegurosContext.Clientes' is null.");
            }

            var clientes = await _context.Clientes.ToListAsync();
            return View(clientes);
        }


        // GET: ClientesController/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);

            return clientes != null ? View(clientes) : NotFound();

        }

        // GET: ClientesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cedula,NombreCliente,Telefono,Edad")] Cliente clientes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientes);
        }



        // GET: ClientesController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var clientes = await _context.Clientes.FindAsync(id);

            return clientes == null ? NotFound() : View(clientes);
        }

        // POST: ClientesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cedula,NombreCliente,Telefono,Edad")] Cliente clientes)
        {
            if (id != clientes.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(clientes);
            }

            try
            {
                _context.Update(clientes);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(clientes.Id))
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

        private bool ClienteExists(int id)
        {
            return (_context.Clientes?.Any(x => x.Id == id)).GetValueOrDefault();
        }

        // GET: ClientesController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);

            return clientes == null ? NotFound() : View(clientes);
        }


        // POST: ClientesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);

            if (clientes == null)
            {
                return Problem("El registro no fue encontrado.");
            }

            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
