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

    }
}
