using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class DTOs
    {

        public class LoginInfo {

            public string? UserName { get; set; }
            public string? Email { get; set; }
            public string? RolNombre { get; set; }

            public int? IdEmpleado { get; set; }

        }

        public class GetBoss {

            public int IdEmpleado { get; set; }
            public string? Nombre { get; set; }
        }

        public class GetEmailByIdPermiso { 
            public string? Email { get; set; }

        }

    }
}
