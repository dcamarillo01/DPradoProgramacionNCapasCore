using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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


        public ML.Result Update(int IdUserProfile, ML.UserProfile userProfile) {

            ML.Result result = new();

            try {

                var filasAfectas = _context.Database.ExecuteSqlRaw($"UserProfileUpdate {IdUserProfile},'{userProfile.UserName}', '{userProfile.Email}', '{userProfile.PasswordHash}' , '{userProfile.Status}', '{userProfile.Rol.IdRol}' ");

                if (filasAfectas > 0)
                {
                    result.Correct = true;
                }
                else {
                    result.Correct = false;
                    result.ErrorMessage = "Error al actualizar UserProfile";
                }
            }
            catch(Exception ex) 
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public ML.Result Delete(int IdUserProfile) {

            ML.Result result = new();

            try {

                var filasAfectadas = _context.Database.ExecuteSqlRaw($"UserProfileDelete {IdUserProfile}");

                if (filasAfectadas > 0)
                {

                    result.Correct = true;
                }
                else { 
                    result.Correct = false;
                    result.ErrorMessage = "Error al eliminar UserProfile";
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

        public ML.Result GetAll() 
        {
            ML.Result result = new();
            result.Objects = new List<object>();
            try {

                var query = _context.VwUserProfiles.FromSqlRaw("UserProfileGetAll").ToList();

                if (query != null) {

                    foreach (var profile in query) { 
                        
                        ML.UserProfile profileUser = new ML.UserProfile();
                        profileUser.Empleado = new();
                        profileUser.Rol = new ML.Rol();

                        profileUser.IdUserProfile = profile.IdUserProfile;
                        profileUser.Empleado.IdEmpleado = profile.IdEmpleado;
                        profileUser.UserName = profile.UserName;
                        profileUser.Email = profile.Email;
                        profileUser.PasswordHash = profile.PasswordHash;
                        profileUser.Status = profile.Status;
                        profileUser.Rol.Nombre = profile.RolType;

                        result.Objects.Add(profileUser);

                    }

                    result.Correct = true;
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

        public ML.Result GetById(int IdUserProfile) {

            ML.Result result = new();

            try {

                var query = _context.VwUserProfiles.FromSqlRaw($"UserProfileGetById {IdUserProfile}").AsEnumerable().SingleOrDefault();

                if (query != null)
                {

                    ML.UserProfile userProfile = new ML.UserProfile();
                    userProfile.Empleado = new();
                    userProfile.Rol = new ML.Rol();

                    userProfile.IdUserProfile = query.IdUserProfile;
                    userProfile.Empleado.IdEmpleado = query.IdEmpleado;
                    userProfile.UserName = query.UserName;
                    userProfile.Email = query.Email;
                    userProfile.PasswordHash = query.PasswordHash;
                    userProfile.Status = query.Status;
                    userProfile.Rol.IdRol = query.IdRol;
                    userProfile.Rol.Nombre = query.RolType;

                    result.Object = userProfile;

                    result.Correct = true;
                }
                else { 
                    result.Correct = false;
                    result.ErrorMessage = "UserProfile no encontrado";
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage= ex.Message;
                result.Ex = ex;
            }

            return result;
        }

    }
}
