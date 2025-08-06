using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Login
    {

        private readonly DpradoProgramacionNcapasContext _context;

        public Login(DpradoProgramacionNcapasContext context) { 
            
            _context = context;
        }


        public ML.Result LoginUser(ML.Login Login) 
        {
            ML.Result result = new();

            try {

                var query = _context.LoginInfo.FromSqlRaw($"GetByEmailPassword '{Login.Email}','{Login.Passsword}'").AsEnumerable().SingleOrDefault();

                if (query != null)
                {

                    ML.UserProfile userProfile= new()
                    {
                        Rol = new()
                    };

                    userProfile.UserName = query.UserName;
                    userProfile.Email = query.Email;
                    userProfile.Rol.Nombre = query.RolNombre;

                    result.Object = userProfile;

                    result.Correct = true;

                }
                else { 
                    
                    result.Correct = false;
                    result.ErrorMessage = "Correo o contraseña incorrectos";
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
