using Microsoft.AspNetCore.Mvc;

namespace PL_MVC.Controllers
{
    public class EmpleadoController : Controller
    {

        private readonly BL.Empleado _empleado;

        public EmpleadoController(BL.Empleado empleado) { 
            
            _empleado = empleado;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
