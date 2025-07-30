using Microsoft.AspNetCore.Mvc;

namespace PL_MVC.Controllers
{
    // Se CREO BRANCH EMPLEADOCRUD PARA IMPLEMENTAR EMPLEADO CRUD.
    public class EmpleadoController : Controller
    {

        private readonly BL.Empleado _empleado;
        private readonly BL.Departamento _departamento;

        public EmpleadoController(BL.Empleado empleado, BL.Departamento departamento)
        {

            _empleado = empleado;
            _departamento = departamento;
        }

        public IActionResult Index()
        {
            return View();
        }


        // =================== GET ALL ======================= \\

        [HttpGet]
        public IActionResult EmpleadoGetAll() {


            ML.Empleado empleado = new ML.Empleado();
            empleado.Empleados = new List<object>();
            empleado.Departamento = new ML.Departamento();
            empleado.Departamento.Departamentos = new List<object>();

            empleado.Nombre = empleado.Nombre ?? "";
            empleado.ApellidoPaterno = empleado.ApellidoPaterno ?? "";
            empleado.ApellidoMaterno = empleado.ApellidoMaterno ?? "";

            ML.Result resultGetAll = _empleado.GetAll(empleado);

            empleado.Empleados = resultGetAll.Objects;


            // ======== LLenar Departamentos =========== \\

            ML.Result resultDepartamento = _departamento.GetAll();

            empleado.Departamento.Departamentos = resultDepartamento.Objects;
            empleado.Empleados = resultGetAll.Objects;


            return View(empleado);
        }

        [HttpPost]
        public IActionResult EmpleadoGetAll(ML.Empleado Empleado) {

            Empleado.Nombre = Empleado.Nombre ?? "";
            Empleado.ApellidoPaterno = Empleado.ApellidoPaterno ?? "";
            Empleado.ApellidoMaterno = Empleado.ApellidoMaterno ?? "";


            // ======== LLenar Departamentos =========== \\

            ML.Result resultDepartamento = _departamento.GetAll();

            Empleado.Departamento.Departamentos = resultDepartamento.Objects;

            ML.Result resultGetAll = _empleado.GetAll(Empleado);

            if (resultGetAll.Correct) {
                Empleado.Empleados = resultGetAll.Objects;
            }

            return View(Empleado);
        }


        [HttpGet]
        public IActionResult EmpleadoJS()
        {


            ML.Empleado empleado = new ML.Empleado();
            empleado.Empleados = new List<object>();
            empleado.Departamento = new ML.Departamento();
            empleado.Departamento.Departamentos = new List<object>();

            empleado.Nombre = empleado.Nombre ?? "";
            empleado.ApellidoPaterno = empleado.ApellidoPaterno ?? "";
            empleado.ApellidoMaterno = empleado.ApellidoMaterno ?? "";

            ML.Result resultGetAll = _empleado.GetAll(empleado);

            empleado.Empleados = resultGetAll.Objects;


            // ======== LLenar Departamentos =========== \\

            ML.Result resultDepartamento = _departamento.GetAll();

            empleado.Departamento.Departamentos = resultDepartamento.Objects;
            empleado.Empleados = resultGetAll.Objects;


            return View(empleado);
        }

        [HttpPost]
        public IActionResult EmpleadoJS(ML.Empleado Empleado)
        {

            Empleado.Nombre = Empleado.Nombre ?? "";
            Empleado.ApellidoPaterno = Empleado.ApellidoPaterno ?? "";
            Empleado.ApellidoMaterno = Empleado.ApellidoMaterno ?? "";


            // ======== LLenar Departamentos =========== \\

            ML.Result resultDepartamento = _departamento.GetAll();

            Empleado.Departamento.Departamentos = resultDepartamento.Objects;

            ML.Result resultGetAll = _empleado.GetAll(Empleado);

            if (resultGetAll.Correct)
            {
                Empleado.Empleados = resultGetAll.Objects;
            }

            return View(Empleado);
        }



        // ===================== FORM ADD EMPLOYEE ======================== \\

        [HttpGet]
        public IActionResult EmpleadoForm(int? IdEmpleado) {

            ML.Empleado empleado = new ML.Empleado();
            empleado.Departamento = new ML.Departamento();
            empleado.Departamento.Departamentos = new List<object>();


            // Llenar Departamentos 

            ML.Result resultDepartamento = _departamento.GetAll();
            empleado.Departamento.Departamentos = resultDepartamento.Objects;

            if (IdEmpleado > 0) {

                ML.Result resultGetById = _empleado.GetById(IdEmpleado.Value);

                if (resultGetById.Correct) {

                    empleado = (ML.Empleado)resultGetById.Object;
                    empleado.Departamento.Departamentos = resultDepartamento.Objects;
                }

            }


            return View(empleado);
        }

        [HttpPost]
        public IActionResult EmpleadoForm(ML.Empleado Empleado) {

            if (ModelState.IsValid)
            {
                if (Empleado.IdEmpleado > 0)
                {

                    ML.Result resultUpdate = _empleado.Update(Empleado, Empleado.IdEmpleado);

                    if (resultUpdate.Correct)
                    {

                        return RedirectToAction("EmpleadoGetAll", "Empleado");
                    }

                }
                else
                {

                    ML.Result resultAdd = _empleado.Add(Empleado);

                    if (resultAdd.Correct)
                    {

                        return RedirectToAction("EmpleadoGetAll", "Empleado");
                    }

                }
            }
            else {

                ML.Result resultDepartamento = _departamento.GetAll();
                Empleado.Departamento.Departamentos = resultDepartamento.Objects;


                return View(Empleado);
            }




                return View();
        }



        [HttpGet]
        public IActionResult Delete(int IdEmpleado) {

            ML.Result resultDelete = _empleado.Delete(IdEmpleado);

            return RedirectToAction("EmpleadoGetAll");
        }



        // =================== METODOS PARA CONSUMO CON JS AJAX ======================== \\4

        [HttpGet]
        public JsonResult GetAllJson(ML.Empleado Empleado) {


            Empleado.Nombre = Empleado.Nombre ?? "";
            Empleado.ApellidoPaterno = Empleado.ApellidoPaterno ?? "";
            Empleado.ApellidoMaterno = Empleado.ApellidoMaterno ?? "";

            Empleado.Departamento = new ML.Departamento();


            ML.Result result = _empleado.GetAll(Empleado);

            return Json(result);
        }
        [HttpPost]
        public JsonResult GetAllOpenJson(ML.Empleado Empleado) {


            Empleado.Nombre = Empleado.Nombre ?? "";
            Empleado.ApellidoPaterno = Empleado.ApellidoPaterno ?? "";
            Empleado.ApellidoMaterno = Empleado.ApellidoMaterno ?? "";



            ML.Result result = _empleado.GetAll(Empleado);

            return Json(result);
        }

        [HttpGet]

        public JsonResult GetDepartamentos() {

            ML.Result result = _departamento.GetAll();

            return Json(result);
        }

        [HttpPost]
        public JsonResult AddJS(ML.Empleado empleado) {

            ML.Result result = _empleado.Add(empleado);


            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateJS(ML.Empleado empleado) {

            ML.Result result = _empleado.Update(empleado, empleado.IdEmpleado);

            return Json(result);
        }

        [HttpGet]
        public JsonResult DeleteJS(int IdEmpleado) {

            ML.Result result = _empleado.Delete(IdEmpleado);

            return Json(result);
        }


        // ========= GET BY ID JSON =========\\

        [HttpGet]
        public JsonResult GetByIdJson(int EmpleadoID)
        {

            ML.Result result = _empleado.GetById(EmpleadoID);

            return Json(result);
        }




    }
}
