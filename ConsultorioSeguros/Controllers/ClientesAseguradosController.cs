using ConsultorioSeguros.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioSeguros.Controllers
{
    public class ClientesAseguradosController : Controller
    {
        private readonly DBSegurosContext _context;

        public ClientesAseguradosController(DBSegurosContext context)
        {
            _context = context;
        }

        // GET: ClientesAseguradosController
        public async Task<IActionResult> Index()
        {
            var dbseguroscContext = _context.SegurosClientes.Include(x => x.Cliente).Include(x => x.Seguro);
            return View(await dbseguroscContext.ToListAsync());
        }

        // GET: ClientesAseguradosController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SegurosClientes == null)
            {
                return NotFound();
            }

            var clientesAsegurados = await _context.SegurosClientes
                .Include(a => a.Cliente)
                .Include(a => a.Seguro)
                .FirstOrDefaultAsync(x => x.Id == id);

            return clientesAsegurados == null ? NotFound() : View(clientesAsegurados);
        }


        // GET: ClientesAseguradosController/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = GetSelectListForAsegurados();
            ViewData["SeguroId"] = GetSelectListForSeguros();
            return View();
        }

        private SelectList GetSelectListForAsegurados()
        {
            return new SelectList(_context.Clientes, "Id", "NombreCliente");
        }

        private SelectList GetSelectListForSeguros()
        {
            return new SelectList(_context.Seguros, "Id", "NombreSeguro");
        }


        // POST: ClientesAseguradosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,SeguroId")] SegurosCliente clientesAsegurados)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("Seguro");

            if (ModelState.IsValid)
            {
                try
                {
                    // Verificar si la combinación ya existe
                    if (_context.SegurosClientes.Any(sc => sc.ClienteId == clientesAsegurados.ClienteId && sc.SeguroId == clientesAsegurados.SeguroId))
                    {
                        ModelState.AddModelError("ClienteId", "Ya existe una combinación única de ClienteId y SeguroId.");
                        ModelState.AddModelError("SeguroId", "Ya existe una combinación única de ClienteId y SeguroId.");

                        ViewData["ClienteId"] = GetSelectListForAsegurados(clientesAsegurados.ClienteId);
                        ViewData["SeguroId"] = GetSelectListForSeguros(clientesAsegurados.SeguroId);

                        return View(clientesAsegurados);
                    }

                    _context.Add(clientesAsegurados);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
                {
                    HandleUniqueConstraintViolation(ex, clientesAsegurados);
                    return View(clientesAsegurados);
                }
            }
            ConfigureViewDataForEdit(clientesAsegurados);
            return View(clientesAsegurados);
        }

        // GET: ClientesAseguradosController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SegurosClientes == null)
            {
                return NotFound();
            }

            var clientesAsegurados = await _context.SegurosClientes
                .Include(x => x.Cliente)
                .Include(x => x.Seguro)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (clientesAsegurados == null)
            {
                return NotFound();
            }
            ConfigureViewDataForEdit(clientesAsegurados);
            return View(clientesAsegurados);
        }

        // POST: ClientesAseguradosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,SeguroId")] SegurosCliente clientesAsegurados)
        {
            if (id != clientesAsegurados.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Cliente");
            ModelState.Remove("Seguro");

            if (ModelState.IsValid)
            {
                try
                {
                    // Verificar si la combinación ya existe
                    if (_context.SegurosClientes.Any(sc => sc.ClienteId == clientesAsegurados.ClienteId && sc.SeguroId == clientesAsegurados.SeguroId))
                    {
                        ModelState.AddModelError("ClienteId", "Ya existe una combinación única de ClienteId y SeguroId.");
                        ModelState.AddModelError("SeguroId", "Ya existe una combinación única de ClienteId y SeguroId.");

                        ViewData["ClienteId"] = GetSelectListForAsegurados(clientesAsegurados.ClienteId);
                        ViewData["SeguroId"] = GetSelectListForSeguros(clientesAsegurados.SeguroId);

                        return View(clientesAsegurados);
                    }

                    _context.Add(clientesAsegurados);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
                {
                    HandleUniqueConstraintViolation(ex, clientesAsegurados);
                    return View(clientesAsegurados);
                }
            }
            ConfigureViewDataForEdit(clientesAsegurados);
            return View(clientesAsegurados);


        }

        private SelectList GetSelectListForAsegurados(int? selectedValue = null)
        {
            return new SelectList(_context.Clientes, "Id", "NombreCliente", selectedValue);
        }

        private SelectList GetSelectListForSeguros(int? selectedValue = null)
        {
            return new SelectList(_context.Seguros, "Id", "NombreSeguro", selectedValue);
        }

        private void ConfigureViewDataForEdit(SegurosCliente clientesAsegurados)
        {
            ViewData["ClienteId"] = GetSelectListForAsegurados(clientesAsegurados.ClienteId);
            ViewData["SeguroId"] = GetSelectListForSeguros(clientesAsegurados.SeguroId);
        }

        private bool IsUniqueConstraintViolation(DbUpdateException ex)
        {
            return ex?.InnerException is SqlException sqlException &&
                   sqlException.Number == 2601;
        }

        private void HandleUniqueConstraintViolation(DbUpdateException ex, SegurosCliente clientesAsegurados)
        {
            // Obtener los nombres de las columnas involucradas en la restricción única
            var uniqueColumns = new[] { "ClienteId", "SeguroId" };

            // Agregar un error personalizado al ModelState
            foreach (var columnName in uniqueColumns)
            {
                ModelState.AddModelError(columnName, "Ya existe una combinación única de ClienteId y SeguroId.");
            }

            // Volver a agregar las opciones del SelectList para AseguradoId y SeguroId
            ConfigureViewDataForEdit(clientesAsegurados);
        }

        // GET: ClientesAseguradosController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SegurosClientes == null)
            {
                return NotFound();
            }

            var clientesAsegurados = await _context.SegurosClientes
                .Include(x => x.Cliente)
                .Include(x => x.Seguro)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (clientesAsegurados == null)
            {
                return NotFound();
            }

            return View(clientesAsegurados);
        }

        // POST: ClientesAseguradosController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SegurosClientes == null)
            {
                return Problem("Entity set 'DBSeguros.ClientesAsegurados' is null.");
            }
            var clientesAsegurado = await _context.SegurosClientes.FindAsync(id);

            if (clientesAsegurado != null)
            {
                _context.SegurosClientes.Remove(clientesAsegurado);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
