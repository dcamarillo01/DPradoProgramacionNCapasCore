using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {

        private readonly DL.DpradoProgramacionNcapasContext _context;
        public Estado(DL.DpradoProgramacionNcapasContext context)
        {

            _context = context;

        }

        public ML.Result GetAll()
        {

            ML.Result result = new ML.Result();

            try
            {

                var query = _context.Estados.FromSqlRaw("EstadoGetAll").ToList();
                result.Objects = new List<object>();

                if (query.Count() > 0)
                {

                    foreach (var item in query)
                    {

                        ML.Estado estado = new ML.Estado();
                        estado.IdEstado = item.IdEstado;
                        estado.Nombre = item.Nombre;

                        result.Objects.Add(estado);
                    }

                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Ocurrio un error al obtener los estados";
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
