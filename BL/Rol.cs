using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        private readonly DL.DpradoProgramacionNcapasContext _context;


        public Rol(DL.DpradoProgramacionNcapasContext context)
        {

            _context = context;

        }

        public ML.Result GetAll()
        {

            ML.Result result = new ML.Result();
            try
            {

                result.Objects = new List<object>();
                var query = _context.Rols.FromSqlRaw("RolGetAll").ToList();

                if (query.Count > 0)
                {
                    foreach (var row in query)
                    {

                        ML.Rol rol = new ML.Rol();

                        rol.IdRol = row.IdRol;
                        rol.Nombre = row.Nombre;

                        result.Objects.Add(rol);
                    }

                    result.Correct = true;

                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Ocurrio un problema al obtener la informacion de rol";
                }

            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
                result.Ex = Ex;
            }

            return result;
        }


    }
}
