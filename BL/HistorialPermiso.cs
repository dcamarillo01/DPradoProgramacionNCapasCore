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

    }
}
