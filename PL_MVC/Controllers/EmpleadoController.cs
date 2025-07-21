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
            
            ML.Result resultGetAll = _empleado.GetAll();

            ML.Empleado empleado = new ML.Empleado();
            empleado.Empleados = new List<object>();

            empleado.Empleados = resultGetAll.Objects;


            return View(empleado);
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
            

            return View(empleado);
        }

        [HttpPost]
        public IActionResult EmpleadoForm(ML.Empleado Empleado) {

            if (Empleado.IdEmpleado > 0)
            {

                ML.Result resultUpdate = _empleado.Update(Empleado,Empleado.IdEmpleado);

                if (resultUpdate.Correct) {

                    return RedirectToAction("EmpleadoGetAll","Empleado");
                }

            }
            else {

                ML.Result resultAdd = _empleado.Add(Empleado);

                if (resultAdd.Correct) {

                    return RedirectToAction("EmpleadoGetAll","Empleado");
                }
            
            }


                return View();
        }

    }
}
