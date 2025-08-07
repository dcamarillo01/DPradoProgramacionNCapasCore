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

    }
}
