using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        private readonly DL.DpradoProgramacionNcapasContext _context;

        public Empleado(DL.DpradoProgramacionNcapasContext context) { 
            
            _context = context;
        }



        // ============ ADD EMPLEADO ============== \\
        public ML.Result Add(ML.Empleado Empleado)
        {
            ML.Result result = new ML.Result();
            try {

                int filasAfectadas = _context.Database.ExecuteSqlRaw($"EmpleadoAdd '{Empleado.Nombre}', '{Empleado.ApellidoPaterno}','{Empleado.ApellidoMaterno}','{Empleado.FechaNacimiento}','{Empleado.RFC}','{Empleado.NSS}','{Empleado.CURP}','{Empleado.FechaIngreso}','{Empleado.Departamento.IdDepartamento}','{Empleado.SalarioBase}','{Empleado.NoFaltas}'");

                if (filasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else { 
                    result.Correct = false;
                    result.ErrorMessage = "Ocurrio un error al agregar al Empleado";
                }

            }
            catch(Exception ex) 
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        // ============ UPDATE EMPLEADO ============== \\
        public ML.Result Update(ML.Empleado Empleado) {

            ML.Result result = new ML.Result();

            try { }
            catch (Exception ex) 
            {
                result.Correct = false;
                result.ErrorMessage= ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        // =============== DELETE EMPLEADO ================= \\

        public ML.Result Delete(ML.Empleado Empleaado) {

            ML.Result result = new ML.Result();

            try { }
            catch (Exception ex) 
            {
                result.Correct = false;
                result.ErrorMessage= ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        // =============== GET ALL EMPLEADO =============== \\

        public ML.Result GetAll() {

            ML.Result result = new ML.Result();

            try { }
            catch (Exception ex) 
            {
                result.Correct= false;
                result.ErrorMessage= ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        // =============== GET BY ID EMPLEADO ================= \\
        public ML.Result GetById(int IdEmpleado) { 
            
            ML.Result result = new ML.Result();

            try { }
            catch (Exception ex) 
            {
                result.Correct= false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

    }
}
