using Microsoft.AspNetCore.Mvc;

namespace PL_MVC.Controllers
{
    public class UserProfileController : Controller
    {

        private readonly BL.UserProfile _userProfile;
        private readonly BL.Empleado _empleado;
        private readonly BL.Rol _rol;

        public UserProfileController(BL.UserProfile userProfile, BL.Empleado empleado, BL.Rol rol) { 
            
            _userProfile = userProfile;
            _empleado = empleado;
            _rol = rol;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult GetAll() { 
            
            return View();
        }

        [HttpPost]
        public IActionResult GetAll(ML.UserProfile userProfile) { 
            
            return View();
        }

        [HttpGet]
        public IActionResult FormUserProfile(int IdEmpleado) {

            ML.UserProfile userProfile = new();
            userProfile.Empleado = new();
            userProfile.Rol = new();

            //Get Empleado By Id

            ML.Result resultGetEmpleadyById = _empleado.GetById(IdEmpleado);
            if (resultGetEmpleadyById.Correct) {

                ML.Empleado empleado = new();
                empleado.Departamento = new();
                empleado = (ML.Empleado) resultGetEmpleadyById.Object;

                userProfile.Empleado.IdEmpleado = IdEmpleado;
                userProfile.UserName = empleado.Nombre + empleado.ApellidoPaterno + empleado.IdEmpleado;
                userProfile.Email = empleado.Nombre.First() + empleado.ApellidoPaterno+empleado.IdEmpleado + "@" + empleado.Departamento.Descripcion + ".com";
            }

            //Llenar Roles 
            ML.Result resultRol = _rol.GetAll();
            if (resultRol.Correct) {

                ML.Rol rol = new ML.Rol();

                rol.Roles = resultRol.Objects;

                userProfile.Rol.Roles = rol.Roles;
            }

            return View(userProfile);
        }

        [HttpPost]
        public IActionResult FormUserProfile(ML.UserProfile userProfile) {

            ML.Result result = _userProfile.Add(userProfile);

            if (result.Correct) { 
                return RedirectToAction("EmpleadoGetAll","Empleado");

            }

            return View(userProfile);
        }

    }
}
