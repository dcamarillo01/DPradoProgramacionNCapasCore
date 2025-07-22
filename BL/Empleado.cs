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
        public ML.Result Update(ML.Empleado Empleado, int IdEmpleado) {

            ML.Result result = new ML.Result();

            try
            {

                int filasAfectadas = _context.Database.ExecuteSqlRaw($"EmpleadoUpdate {IdEmpleado}, '{Empleado.Nombre}','{Empleado.ApellidoPaterno}','{Empleado.ApellidoMaterno}','{Empleado.FechaNacimiento}','{Empleado.RFC}','{Empleado.NSS}','{Empleado.CURP}','{Empleado.FechaIngreso}','{Empleado.Departamento.IdDepartamento}','{Empleado.SalarioBase}','{Empleado.NoFaltas}'");

                if (filasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else { 
                    result.Correct = false;
                    result.ErrorMessage = "Ocurrio un problema al actualizar Empleado";
                }

            }
            catch (Exception ex) 
            {
                result.Correct = false;
                result.ErrorMessage= ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        // =============== DELETE EMPLEADO ================= \\

        public ML.Result Delete(int IdEmpleado) {

            ML.Result result = new ML.Result();

            try {

                int filasAfectadas = _context.Database.ExecuteSqlRaw($"EmpleadoDelete {IdEmpleado}");

                if (filasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else {
                    result.Correct = false;
                    result.ErrorMessage = "Ocurrio un problema al eliminar usuario";
                }

            }
            catch (Exception ex) 
            {
                result.Correct = false;
                result.ErrorMessage= ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        // =============== GET ALL EMPLEADO =============== \\

        public ML.Result GetAll(ML.Empleado Empleado) {

            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            try
            {

                var query = _context.VwEmpleados.FromSqlRaw($"EmpleadoGetAll '{Empleado.Nombre}', '{Empleado.ApellidoPaterno}', '{Empleado.ApellidoMaterno}', '{Empleado.Departamento.IdDepartamento}'" ).ToList();

                if (query.Count > 0){

                    foreach (var empleadoScaffold in query) {
                        
                        ML.Empleado empleado = new ML.Empleado();
                        empleado.Departamento = new ML.Departamento();

                        empleado.IdEmpleado = (int)empleadoScaffold.IdEmpleado;
                        empleado.Nombre = empleadoScaffold.Nombre;
                        empleado.ApellidoPaterno = empleadoScaffold.ApellidoPaterno;
                        empleado.ApellidoMaterno = empleadoScaffold.ApellidoMaterno;
                        empleado.FechaNacimiento = empleadoScaffold.FechaNacimiento;
                        empleado.RFC = empleadoScaffold.Rfc;
                        empleado.NSS = empleadoScaffold.Nss;
                        empleado.CURP = empleadoScaffold.Curp;
                        empleado.FechaIngreso = empleadoScaffold.FechaIngreso;
                        empleado.Departamento.IdDepartamento = empleadoScaffold.IdDepartamento;
                        empleado.Departamento.Descripcion = empleadoScaffold.Descripcion;
                        empleado.SalarioBase = (int?)empleadoScaffold.SalarioBase;
                        empleado.NoFaltas = empleadoScaffold.NoFaltas;

                        result.Objects.Add(empleado);
                    
                    }

                    result.Correct = true;
                }
            
            }
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

            try {

                var query = _context.VwEmpleados.FromSqlRaw($"EmpleadoGetById {IdEmpleado}").ToList().SingleOrDefault();

                if (query != null) { 
                    
                    ML.Empleado empleado = new ML.Empleado();
                    empleado.Departamento = new ML.Departamento();

                    empleado.IdEmpleado = (int)query.IdEmpleado;
                    empleado.Nombre = query.Nombre;
                    empleado.ApellidoPaterno = query.ApellidoPaterno;
                    empleado.ApellidoMaterno = query.ApellidoMaterno;
                    empleado.FechaNacimiento = query.FechaNacimiento;
                    empleado.RFC = query.Rfc;
                    empleado.NSS = query.Nss;
                    empleado.CURP = query.Curp;
                    empleado.FechaIngreso = query.FechaIngreso;
                    empleado.Departamento.IdDepartamento = query.IdDepartamento;
                    empleado.SalarioBase = (int?)query.SalarioBase;
                    empleado.NoFaltas = query.NoFaltas;

                    result.Object = empleado;

                }

                result.Correct = true;
            
            }
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
