using BL;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

            ML.Permiso permiso = new();
            permiso.Empleado = new();
            permiso.EmpleadoAutorizador = new();
            permiso.Permisos = new List<object>();

            ML.Result resultGetPermiso = _permiso.GetAll();

            if (resultGetPermiso.Correct) {

                permiso.Permisos = resultGetPermiso.Objects;
            }



            return View(permiso);
        }


        [HttpGet]
        public IActionResult PermisoForm() { 
            
            ML.Permiso permiso = new ML.Permiso();
            permiso.Empleado = new ML.Empleado();
            permiso.EmpleadoAutorizador = new ML.Empleado();

            ML.Result resultBoss = _permiso.GetBoss();
            if (resultBoss.Correct) {

                permiso.EmpleadoAutorizador.Empleados = resultBoss.Objects;
            }

            return View(permiso);
        }

        [HttpPost]
        public IActionResult PermisoForm(ML.Permiso permiso) {

            var userNameClaim = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            permiso.Empleado.IdEmpleado = Convert.ToInt32(userNameClaim);

            var FechaPermiso = permiso.FechaInicio.Split("to");
            permiso.FechaInicio = FechaPermiso[0];
            permiso.FechaFin = FechaPermiso[1];

            

            permiso.StatusPermiso = new ML.StatusPermiso();

            if (permiso.IdPermiso == 0) {

                permiso.StatusPermiso.IdStatusPermiso = 1;
            }

            ML.Result resultPermiso = _permiso.Add(permiso);

            if (resultPermiso.Correct)
            {

                return RedirectToAction("Index","Home");
            }


            return View(permiso);
        }

    }
}
