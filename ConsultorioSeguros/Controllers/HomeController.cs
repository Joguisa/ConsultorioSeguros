using ConsultorioSeguros.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConsultorioSeguros.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBSegurosContext _context;

        public HomeController(ILogger<HomeController> logger, DBSegurosContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult BuscarPorCedula(string cedula)
        {
            var resultados = _context.SegurosClientes
                .Where(x => x.Cliente.Cedula == cedula)
                .Select(x => new
                {
                    Cedula = x.Cliente.Cedula,
                    NombreCliente = x.Cliente.NombreCliente,
                    NombreSeguro = x.Seguro.NombreSeguro
                })
                .ToList();

            if (resultados.Any())
            {
                ViewBag.ResultadosPorCedula = resultados;
                return View("Index");
            }
            else
            {
                ViewBag.CedulaNoEncontrada = cedula;
                return View("NoResults");

            }
        }


        [HttpGet]
        public IActionResult BuscarPorCodigoSeguro(string codigoSeguro)
        {
            var resultados = _context.SegurosClientes
                .Where(x => x.Seguro.CodigoSeguro == codigoSeguro)
                .Select(x => new
                {
                    Cedula = x.Cliente.Cedula,
                    NombreCliente = x.Cliente.NombreCliente,
                    NombreSeguro = x.Seguro.NombreSeguro
                })
                .ToList();            

            if (resultados.Any())
            {
                ViewBag.ResultadosPorCodigoSeguro = resultados;
                return View("Index");
            }
            else
            {
                ViewBag.CodigoSeguroNoEncontrado = codigoSeguro;
                return View("NoResults");
            }
        }


    }
}
