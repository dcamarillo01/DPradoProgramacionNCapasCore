using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        private readonly DL.DpradoProgramacionNcapasContext _context;
        public Colonia(DL.DpradoProgramacionNcapasContext context)
        {

            _context = context;

        }

        public ML.Result GetColoniaByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _context.Colonia.FromSqlRaw($"ColoniaGetByIdMunicipio {IdMunicipio}").ToList();
                result.Objects = new List<object>();

                if (query != null)
                {
                    foreach (var item in query)
                    {
                        ML.Colonia colonia = new ML.Colonia();
                        colonia.IdColonia = item.IdColonia;
                        colonia.Nombre = item.Nombre;

                        result.Objects.Add(colonia);
                    }

                    result.Correct = true;

                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Ocurrio un error al obtener colonias";
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
