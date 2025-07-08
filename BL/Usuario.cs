using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {

        private readonly DL.DpradoProgramacionNcapasContext _context;


        public Usuario(DL.DpradoProgramacionNcapasContext context) { 
        
            _context = context;

        }


        public  ML.Result Add(ML.Usuario Usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                var img = new SqlParameter("@Imagen", System.Data.SqlDbType.VarBinary);
                if (Usuario.Imagen != null)
                {
                    img.Value = Usuario.Imagen;
                }
                else
                {
                    img.Value = DBNull.Value;
                }


                var filasAfectadas = _context.Database.ExecuteSqlRaw($"UsuarioAdd '{Usuario.Nombre}', '{Usuario.ApellidoPaterno}', '{Usuario.ApellidoMaterno}', '{Usuario.Email}', '{Usuario.UserName}', '{Usuario.Password}', '{Usuario.Sexo}', '{Usuario.Telefono}', '{Usuario.Celular}', '{Usuario.FechaNacimiento}', '{Usuario.Curp}', '{Usuario.Rol.IdRol}','{Usuario.Direccion.Calle}', '{Usuario.Direccion.NumeroInterior}', '{Usuario.Direccion.NumeroExterior}', '{Usuario.Direccion.Colonia.IdColonia}',@Imagen",img);
               
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al agregar usuario";
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


        // DELETE Usuario con Entity Framework y Store Procedure

        public static ML.Result DeleteEFSP(int UsuarioID)
        {

            ML.Result result = new ML.Result();

            try
            {

                using (DL_EF.DPradoProgramacionNCapasEntities context = new DL_EF.DPradoProgramacionNCapasEntities())
                {

                    int filasAfectadas = context.UsuarioDelete(UsuarioID);

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un problema al eliminar al usuario";
                    }
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


        //UPDATE Usuario con EntityFramework y StoreProcedure

        public static ML.Result UpdateEFSP(int UsuarioId, ML.Usuario Usuario)
        {

            ML.Result result = new ML.Result();


            try
            {

                using (DL_EF.DPradoProgramacionNCapasEntities context = new DL_EF.DPradoProgramacionNCapasEntities())
                {
                    int filasAfectadas = context.UsuarioUpdate(UsuarioId, Usuario.Nombre, Usuario.ApellidoPaterno, Usuario.ApellidoMaterno, Usuario.Email, Usuario.UserName, Usuario.Password, Usuario.Sexo, Usuario.Telefono, Usuario.Celular, Usuario.FechaNacimiento, Usuario.Curp, Usuario.Rol.IdRol, Usuario.Imagen, Usuario.Direccion.Calle, Usuario.Direccion.NumeroInterior, Usuario.Direccion.NumeroExterior, Usuario.Direccion.Colonia.IdColonia);

                    if (filasAfectadas > 0)
                    {

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un problema al actualizar los datos del usuario";
                    }

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

        // GetAll EntityFramework Stored Procedure 

        public static ML.Result GetAllEFSP(ML.Usuario Usuario)
        {

            ML.Result result = new ML.Result();

            try
            {

                using (DL_EF.DPradoProgramacionNCapasEntities context = new DL_EF.DPradoProgramacionNCapasEntities())
                {

                    result.Objects = new List<object>();

                    var query = context.UsuarioGetAll(Usuario.Nombre, Usuario.ApellidoPaterno, Usuario.ApellidoMaterno, Usuario.Rol.IdRol).ToList();

                    if (query.Count > 0)
                    {
                        foreach (var row in query)
                        {

                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();
                            usuario.Direccion = new ML.Direccion();


                            usuario.IdUsuario = row.IdUsuario;
                            usuario.Nombre = row.Nombre;
                            usuario.ApellidoPaterno = row.ApellidoPaterno;
                            usuario.ApellidoMaterno = row.ApellidoMaterno;
                            usuario.Email = row.Email;
                            usuario.UserName = row.UserName;
                            usuario.Password = row.Password;
                            usuario.Sexo = row.Sexo;
                            usuario.Telefono = row.Telefono;
                            usuario.Celular = row.Celular;
                            usuario.FechaNacimiento = row.FechaNacimiento;
                            usuario.Curp = row.CURP;
                            usuario.Rol.Nombre = row.Rol;
                            usuario.Imagen = row.Imagen;
                            if (usuario.Imagen == null)
                            {
                                usuario.ImagenBase64 = "";
                            }
                            else
                            {
                                usuario.ImagenBase64 = Convert.ToBase64String(usuario.Imagen);
                            }
                            usuario.Direccion.Calle = row.Calle;
                            usuario.Direccion.NumeroInterior = row.NumeroInterior;
                            usuario.Direccion.NumeroExterior = row.NumeroExterior;

                            result.Objects.Add(usuario);

                        }

                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un problema al traer la informacion de usuarios";
                    }


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

        //GetAllByID EntityFramework StoredProcedure

        public static ML.Result GetByIdEFSP(int IdUsuario)
        {

            ML.Result result = new ML.Result();

            try
            {

                using (DL_EF.DPradoProgramacionNCapasEntities context = new DL_EF.DPradoProgramacionNCapasEntities())
                {

                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Rol = new ML.Rol();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                    var row = context.UsuarioGetById(IdUsuario).SingleOrDefault();

                    usuario.IdUsuario = row.IdUsuario;
                    usuario.Nombre = row.Nombre;
                    usuario.ApellidoPaterno = row.ApellidoPaterno;
                    usuario.ApellidoMaterno = row.ApellidoMaterno;
                    usuario.Email = row.Email;
                    usuario.UserName = row.UserName;
                    usuario.Password = row.Password;
                    usuario.Sexo = row.Sexo;
                    usuario.Telefono = row.Telefono;
                    usuario.Celular = row.Celular;
                    usuario.FechaNacimiento = row.FechaNacimiento.ToString();
                    usuario.Curp = row.CURP;
                    usuario.Rol.IdRol = row.IdRol.Value;
                    usuario.Imagen = row.Imagen;
                    usuario.Direccion.IdDireccion = (row.IdDireccion == null) ? 0 : row.IdDireccion.Value;
                    usuario.Direccion.Calle = row.Calle ?? " ";
                    usuario.Direccion.NumeroInterior = row.NumeroInterior ?? " ";
                    usuario.Direccion.NumeroExterior = row.NumeroExterior ?? " ";
                    usuario.Direccion.Colonia.IdColonia = (row.IdDireccion == null) ? 0 : row.IdColonia;
                    usuario.Direccion.Colonia.Municipio.IdMunicipio = (row.IdMunicipio == null) ? 0 : row.IdMunicipio.Value;
                    usuario.Direccion.Colonia.Municipio.Estado.IdEstado = (row.IdEstado == null) ? 0 : row.IdEstado.Value;
                    result.Object = usuario;

                    if (result.Object != null)
                    {

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un problema al traer los datos del usuario";
                    }

                }

            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        //Set Status 

        public static ML.Result SetStatus(int IdUsuario, bool statusCheck)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DPradoProgramacionNCapasEntities context = new DL_EF.DPradoProgramacionNCapasEntities())
                {
                    var query = context.UsuarioStatusSet(IdUsuario, statusCheck);

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un problema al actualizar status";
                    }
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


        public static ML.Result GetAllView()
        {

            ML.Result result = new ML.Result();

            try
            {

                using (DL_EF.DPradoProgramacionNCapasEntities context = new DL_EF.DPradoProgramacionNCapasEntities())
                {

                    var query = context.vwUsuarios.ToList();
                    result.Objects = new List<object>();

                    if (query.Count > 0)
                    {

                        foreach (var item in query)
                        {

                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();
                            usuario.Direccion = new ML.Direccion();
                            usuario.IdUsuario = item.IdUsuario;
                            usuario.Nombre = item.Nombre;
                            usuario.ApellidoPaterno = item.ApellidoPaterno;
                            usuario.ApellidoMaterno = item.ApellidoMaterno;
                            usuario.Email = item.Email;
                            usuario.UserName = item.UserName;
                            usuario.Password = item.Password;
                            usuario.Telefono = item.Telefono;
                            usuario.Celular = item.Celular;
                            usuario.FechaNacimiento = item.FechaNacimiento.ToString();
                            usuario.Curp = item.CURP;
                            usuario.Rol.IdRol = item.IdRol;
                            usuario.Rol.Nombre = item.Rol;
                            usuario.Direccion.Calle = item.Calle;
                            usuario.Direccion.NumeroExterior = item.NumeroExterior;
                            usuario.Direccion.NumeroInterior = item.NumeroInterior;
                            usuario.Imagen = item.Imagen;

                            result.Objects.Add(usuario);
                        }

                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al obtener los datos";
                    }

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


        //public static ML.Result InsertUser(string path)
        //{
        //    ML.Result result = new ML.Result();

        //    try
        //    {

        //        using (DL_EF.DPradoProgramacionNCapasEntities context = new DL_EF.DPradoProgramacionNCapasEntities())
        //        {
        //            ML.CargaMasiva cargaMasiva = new ML.CargaMasiva();
        //            cargaMasiva.Errores = new List<string>();
        //            cargaMasiva.Validados = new List<string>();

        //            ML.Result resultCarga = BL.CargaMasiva.LeerArchivoExcel(path, cargaMasiva.Errores, cargaMasiva.Validados);

        //            foreach (ML.Usuario usuarioCarga in resultCarga.Objects)
        //            {
        //                ML.Usuario usuario = new ML.Usuario();
        //                usuario.Rol = new ML.Rol();



        //                int filasAfectadas = context.UsuarioInsert(usuario.Nombre = usuarioCarga.Nombre, usuario.ApellidoMaterno = usuarioCarga.ApellidoPaterno, usuario.ApellidoMaterno = usuarioCarga.ApellidoMaterno, usuario.Email = usuarioCarga.Email, usuario.UserName = usuarioCarga.UserName, usuario.Password = usuarioCarga.Password, usuario.Sexo = usuarioCarga.Sexo, usuario.Telefono = usuarioCarga.Telefono, usuario.Celular = usuarioCarga.Celular, usuario.FechaNacimiento = usuarioCarga.FechaNacimiento, usuario.Curp = usuarioCarga.Curp, usuario.Rol.IdRol = usuarioCarga.Rol.IdRol);


        //                if (filasAfectadas > 0)
        //                {
        //                    result.Correct = true;
        //                }
        //                else
        //                {
        //                    result.Correct = false;
        //                    result.ErrorMessage = "Ocurrio un problema al insertar datos";
        //                }
        //            }






        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;
        //}



    }
}
