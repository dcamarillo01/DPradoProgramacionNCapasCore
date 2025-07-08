using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Direccion
    {

        private readonly DL.DpradoProgramacionNcapasContext _context;
        public Direccion(DL.DpradoProgramacionNcapasContext context)
        {

            _context = context;

        }

        //public static ML.Result Add(ML.Usuario Usuario)
        //{

        //    ML.Result result = new ML.Result();

        //    try
        //    {
        //        using (DL_EF.DPradoProgramacionNCapasEntities context = new DL_EF.DPradoProgramacionNCapasEntities())
        //        {

        //            int filasAfectadas = context.DireccionAdd(Usuario.Direccion.Calle, Usuario.Direccion.NumeroInterior, Usuario.Direccion.NumeroExterior, Usuario.Direccion.Colonia.IdColonia);

        //            if (filasAfectadas > 0)
        //            {
        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "Ocurrio un problema al guardar la direccion";
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;
        //}


    }
}
