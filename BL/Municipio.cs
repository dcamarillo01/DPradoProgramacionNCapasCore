using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {

        private readonly DL.DpradoProgramacionNcapasContext _context;
        public Municipio(DL.DpradoProgramacionNcapasContext context)
        {

            _context = context;

        }


        public ML.Result GetMunicipioByIdEstado(int IdEstado)
        {

            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            try
            {

                var query = _context.Municipios.FromSqlRaw($"MunicipioGetByIdEstado {IdEstado}").ToList();

                if (query != null)
                {

                    foreach (var item in query)
                    {

                        ML.Municipio municipio = new ML.Municipio();
                        municipio.IdMunicipio = item.IdMunicipio;
                        municipio.Nombre = item.Nombre;
                        municipio.IdEstado = (int)item.IdEstado;
                        result.Objects.Add(municipio);

                    }

                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Ocurrio un problema al obtener municipios";
                }

            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

    }
}
