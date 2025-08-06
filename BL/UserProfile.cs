using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{

    public class UserProfile
    {

        private readonly DL.DpradoProgramacionNcapasContext _context;

        public UserProfile(DL.DpradoProgramacionNcapasContext context) {

            _context = context;
        }


        public ML.Result Add(ML.UserProfile userProfile) {

            ML.Result result = new();

            try {

                var filasAfectas = _context.Database.ExecuteSqlRaw($"UserProfileAdd {userProfile.Empleado.IdEmpleado}, '{userProfile.UserName}', '{userProfile.Email}', '{userProfile.PasswordHash}','{userProfile.Status}', '{userProfile.Rol.IdRol}'");

                if (filasAfectas > 0)
                {

                    result.Correct = true;
                }
                else { 
                    result.Correct = false;
                    result.ErrorMessage = "Error al crear cuenta";
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
