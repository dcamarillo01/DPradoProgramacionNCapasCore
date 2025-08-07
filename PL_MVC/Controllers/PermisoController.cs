using Microsoft.AspNetCore.Mvc;

namespace PL_MVC.Controllers
{
    public class PermisoController : Controller
    {

        private readonly BL.Permiso _permiso;

        public PermisoController(BL.Permiso permiso) { 
            
            _permiso = permiso;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PermisoGetAll() { 
            
            return View();
        }


        [HttpGet]
        public IActionResult PermisoForm() { 
            
            ML.Permiso permiso = new ML.Permiso();

            return View(permiso);
        }

    }
}
