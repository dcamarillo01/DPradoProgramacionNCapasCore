using Microsoft.AspNetCore.Mvc;

namespace PL_MVC.Controllers
{
    // Se CREO BRANCH EMPLEADOCRUD PARA IMPLEMENTAR EMPLEADO CRUD.
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
