using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class HistorialPermiso
    {

        private readonly DL.DpradoProgramacionNcapasContext _context;

        public HistorialPermiso(DL.DpradoProgramacionNcapasContext context) { 
            
            _context = context;
        }


        public ML.Result AprobarRechazarSolicitud(ML.HistorialPermiso historial)
        {

            ML.Result result = new();

            try
            {

                var filasAfectadas = _context.Database.ExecuteSqlRaw($"StatusPermisoUpdate {historial.Permiso.IdPermiso}, {historial.StatusPermiso.IdStatusPermiso}, {historial.AprovoRechazo.IdEmpleado},'{historial.Observaciones}'");

                if (filasAfectadas > 0)
                {

                    result.Correct = true;
                }
                else { 
                    result.Correct = false;
                    result.ErrorMessage = "Error al aprovar/rechazar solicitud";
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


        public ML.Result GetEmailByIdPermiso(int IdPermiso) {

            ML.Result result = new();

            try {

                var query = _context.GetEmailByIdPermisos.FromSqlRaw($"GetEmailForRequestStatus {IdPermiso}").AsEnumerable().SingleOrDefault();

                if (query != null)
                {

                    var Email = query.Email;
                    result.Object = Email;

                    result.Correct = true;
                    return result;
                }
                else {

                    result.Correct = false;
                    result.ErrorMessage = "Correo no encontrado";
                }

            }
            catch (Exception ex) {

                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;

        }


        public ML.Result GetAll(ML.HistorialPermiso historial) {

            ML.Result result = new();
            result.Objects = new List<object>();

            try {

                var query = _context.VwHistorialPermisos.FromSqlRaw($"HistorialGetAll '{historial.StatusPermiso.Descripcion}', '{historial.AprovoRechazo.Nombre}'").ToList();

                if (query != null)
                {

                    foreach (var historialTemp in query)
                    {

                        ML.HistorialPermiso historialPermiso = new ML.HistorialPermiso();
                        historialPermiso.StatusPermiso = new();
                        historialPermiso.AprovoRechazo = new();
                        historialPermiso.Permiso = new();

                        historialPermiso.IdHistorialPermiso = historialTemp.IdHistorialPermiso;
                        historialPermiso.Permiso.IdPermiso = historialTemp.IdPermiso.Value;
                        historialPermiso.FechaRevision = historialTemp.FechaRevision;
                        historialPermiso.StatusPermiso.Descripcion = historialTemp.Descripcion;
                        historialPermiso.Observaciones = historialTemp.Observaciones;
                        historialPermiso.AprovoRechazo.IdEmpleado = historialTemp.IdEmpleado;
                        historialPermiso.AprovoRechazo.Nombre = historialTemp.Autorizo;
                        
                        result.Objects.Add(historialPermiso);

                    }

                    result.Correct = true;
                }
                else {
                    result.Correct = false;
                    result.ErrorMessage = "No se encontro historial de este permiso";
                }

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
