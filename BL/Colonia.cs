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

    }
}
