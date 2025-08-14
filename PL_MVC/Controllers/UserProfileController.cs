using Microsoft.AspNetCore.Mvc;

namespace PL_MVC.Controllers
{
    public class UserProfileController : Controller
    {

        private readonly BL.UserProfile _userProfile;
        private readonly BL.Empleado _empleado;
        private readonly BL.Rol _rol;

        public UserProfileController(BL.UserProfile userProfile, BL.Empleado empleado, BL.Rol rol)
        {

            _userProfile = userProfile;
            _empleado = empleado;
            _rol = rol;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult UserProfileGetAll()
        {

            ML.UserProfile userProfile = new();
            userProfile.UserProfiles = new List<object>();


            ML.Result resultGetAll = _userProfile.GetAll();
            if (resultGetAll.Correct)
            {

                userProfile.UserProfiles = resultGetAll.Objects;

            }

            return View(userProfile);
        }

        [HttpPost]
        public IActionResult GetAll(ML.UserProfile userProfile)
        {

            return View();
        }

        [HttpGet]
        public IActionResult FormUserProfile(int? IdEmpleado)
        {

            ML.UserProfile userProfile = new();
            userProfile.Empleado = new();
            userProfile.Rol = new();


            ML.Result resultGetProfileById = _userProfile.GetById(IdEmpleado.Value);
            if (resultGetProfileById.Correct)
            {

                userProfile = (ML.UserProfile)resultGetProfileById.Object;
            }
            else {

                ML.Result resultGetEmpleadyById = _empleado.GetById(IdEmpleado.Value);
                if (resultGetEmpleadyById.Correct)
                {

                    ML.Empleado empleado = new();
                    empleado.Departamento = new();
                    empleado = (ML.Empleado)resultGetEmpleadyById.Object;

                    userProfile.Empleado.IdEmpleado = IdEmpleado;
                    userProfile.UserName = empleado.Nombre + empleado.ApellidoPaterno + empleado.IdEmpleado;
                    userProfile.Email = empleado.Nombre.First() + empleado.ApellidoPaterno + empleado.IdEmpleado + "@" + empleado.Departamento.Descripcion + ".com";
                }

            }

            //Llenar Roles 
            ML.Result resultRol = _rol.GetAll();
            if (resultRol.Correct)
            {

                ML.Rol rol = new ML.Rol();

                rol.Roles = resultRol.Objects;

                userProfile.Rol.Roles = rol.Roles;
            }

            return View(userProfile);
        }

        [HttpPost]
        public IActionResult FormUserProfile(ML.UserProfile userProfile)
        {


            if (userProfile.IdUserProfile > 0)
            {
                ML.Result result = _userProfile.Update(userProfile.IdUserProfile, userProfile);

                if (result.Correct) { 
                    return RedirectToAction("EmpleadoGetAll", "Empleado");
                }

            }
            else
            {

                ML.Result result = _userProfile.Add(userProfile);

                if (result.Correct)
                {
                    return RedirectToAction("EmpleadoGetAll", "Empleado");

                }
            }

            //TraerRoles 
            ML.Result resultRol = _rol.GetAll();
            if (resultRol.Correct)
            {

                ML.Rol rol = new ML.Rol();

                rol.Roles = resultRol.Objects;

                userProfile.Rol.Roles = rol.Roles;
            }

            return View(userProfile);
        }


        [HttpPost]
        public JsonResult Delete(int IdUserProfile) {

            ML.Result result = _userProfile.Delete(IdUserProfile);

            return Json(result);

        }

        [HttpPost]
        public JsonResult ChangeStatus(int IdUserProfile, bool Status) {

            ML.Result result = _userProfile.ChangeStatus(IdUserProfile, Status);

            return Json(result);
        }

    }
}
