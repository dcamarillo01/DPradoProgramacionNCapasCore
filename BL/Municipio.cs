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


    }
}
