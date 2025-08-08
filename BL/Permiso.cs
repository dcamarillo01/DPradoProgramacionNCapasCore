using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Permiso
    {
        private readonly DL.DpradoProgramacionNcapasContext _context;

        public Permiso(DL.DpradoProgramacionNcapasContext context) { 
            
            _context = context;
        }


        public ML.Result Add(ML.Permiso permiso) {

            ML.Result result = new();

            try {

                var filasAfectadas = _context.Database.ExecuteSqlRaw($"PermisoAdd {permiso.Empleado.IdEmpleado}, '{permiso.FechaInicio}', '{permiso.FechaFin}', '{permiso.HoraInicio}', '{permiso.HoraFin}', '{permiso.Motivo}', {permiso.StatusPermiso.IdStatusPermiso}, {permiso.EmpleadoAutorizador.IdEmpleado}");

                if (filasAfectadas > 0)
                {

                    result.Correct = true;
                }
                else { 
                    result.Correct = false;
                    result.ErrorMessage = "Ocurrio un error al agendar permiso";
                }
            }
            catch (Exception ex) 
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public ML.Result GetAll() {

            ML.Result result = new();
            result.Objects = new List<object>();

            try {

                var query = _context.Permisos.FromSqlRaw("PermisoGetAll").ToList();

                if (query.Count > 0)
                {

                    foreach (var permisoBD in query)
                    {

                        ML.Permiso permiso = new();
                        permiso.Empleado = new();
                        permiso.StatusPermiso = new();
                        permiso.EmpleadoAutorizador = new();
                        permiso.IdPermiso = permisoBD.IdPermiso;
                        permiso.Empleado.IdEmpleado = permisoBD.IdEmpleado;
                        permiso.FechaSolicitud = Convert.ToString(permisoBD.FechaSolicitud);
                        permiso.FechaInicio = Convert.ToString(permisoBD.FechaInicio);
                        permiso.FechaFin = Convert.ToString(permisoBD.FechaFin);
                        permiso.HoraInicio = Convert.ToString(permisoBD.HoraInicio);
                        permiso.HoraFin = Convert.ToString(permisoBD.HoraFin);
                        permiso.Motivo = permisoBD.Motivo;
                        permiso.StatusPermiso.IdStatusPermiso = permisoBD.IdStatusPermiso;
                        permiso.EmpleadoAutorizador.IdEmpleado = permisoBD.IdAutorizador;

                        result.Objects.Add(permiso);

                    }

                    result.Correct = true;
                }
                else {

                    result.Correct = false;
                    result.ErrorMessage = "No hay solicitudes pendientes";
                }

            }
            catch (Exception ex) {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public ML.Result GetBoss() {

            ML.Result result = new();
            result.Objects = new List<object>();

            try {

                var query = _context.GetBosses.FromSqlRaw("GetBoss").ToList();

                if (query.Count > 0)
                {

                    foreach (var boss in query)
                    {
                        ML.Empleado empleado = new ML.Empleado();

                        empleado.IdEmpleado = boss.IdEmpleado;
                        empleado.Nombre = boss.Nombre;

                        result.Objects.Add(empleado);
                    }

                    result.Correct = true;
                }
                else {
                    result.Correct = false;
                    result.ErrorMessage = "Error al obtener registros";
                }
            
            }
            catch (Exception ex) 
            {
            
            }

            return result;
        }

        public ML.Result AprobarRechazarSolicitud() {

            ML.Result result = new();

            try { 
                

            }
            catch (Exception ex) {

                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

    }
}
