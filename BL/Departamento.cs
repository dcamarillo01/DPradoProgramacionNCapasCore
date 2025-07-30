using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Departamento
    {

        private readonly DL.DpradoProgramacionNcapasContext _context;

        public Departamento(DL.DpradoProgramacionNcapasContext context) { 
            
            _context = context;
        }

        public ML.Result GetAll() { 
            
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            try
            {

                var query = _context.Departamentos.FromSqlRaw("DepartamentoGetAll").ToList();

                if (query.Count > 0) {

                    foreach (var departamentoScaff in query) { 
                        
                        ML.Departamento departamento = new ML.Departamento();

                        departamento.IdDepartamento = departamentoScaff.IdDepartamento;
                        departamento.Descripcion = departamentoScaff.Descripcion;

                        result.Objects.Add(departamento);

                    }

                    result.Correct = true;
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
