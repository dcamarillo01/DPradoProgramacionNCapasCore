using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BL
{
    public class CargaMasiva
    {
        private readonly DL.DpradoProgramacionNcapasContext _context;
        public CargaMasiva(DL.DpradoProgramacionNcapasContext context)
        {

            _context = context;

        }

        //LeerArchivos
        public static ML.Result LeerArchivo(string filePath, List<string> Errores, List<string> Validados)
        {
            ML.Result result = new ML.Result();
            string file = filePath;

            try
            {
                if (File.Exists(file))
                {
                    string[] str = File.ReadAllLines(file).Skip(1).ToArray();
                    result.Objects = new List<object>();

                    ML.Usuario Usuario = new ML.Usuario();
                    foreach (var textLine in str)
                    {
                        string[] newItem = textLine.Split('|');

                        ValidarDatos(newItem, Errores, Validados);

                        //ValidarDatos(usuario, Errores, Validados);

                        if (Errores.Count == 0)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();
                            usuario.Direccion = new ML.Direccion();

                            usuario.IdUsuario = (newItem[0] == " ") ? 0 : Convert.ToInt32(newItem[0]);
                            usuario.Nombre = newItem[1] ?? " ";
                            usuario.ApellidoPaterno = newItem[2] ?? " ";
                            usuario.ApellidoMaterno = newItem[3] ?? " ";
                            usuario.Email = newItem[4] ?? " ";
                            usuario.UserName = newItem[5] ?? " ";
                            usuario.Password = newItem[6] ?? " ";
                            usuario.Sexo = newItem[7] ?? " ";
                            usuario.Telefono = newItem[8] ?? " ";
                            usuario.Celular = newItem[9] ?? " ";
                            usuario.FechaNacimiento = newItem[10];
                            usuario.Curp = newItem[10] ?? " ";
                            usuario.Rol.IdRol = (newItem[11] == " ") ? 0 : Convert.ToInt32(newItem[12]);

                            result.Objects.Add(usuario);

                        }

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

        // ============= VALIDAR DATOS =============== \\
        public static void ValidarDatos(string[] Usuario, List<string> ErroresLista, List<string> ValidadosLista)
        {

            ML.Result result = new ML.Result();
            List<string> strings = new List<string>();

            try
            {

                ML.Usuario usuario = new ML.Usuario();
                usuario.Rol = new ML.Rol();
               //ML.Result resultGetAll = BL.Usuario.GetAll(usuario);
                result.Correct = true;
                //Validar Id
                //if (Usuario[0] == 0 || Usuario.IdUsuario < 0)
                //{
                //    strings.Add($"IdUsuario: {Usuario.IdUsuario} es invalido |");
                //}
                if (!Regex.IsMatch(Usuario[0], "^[0-9]+$") && int.TryParse(Usuario[0], out int IdUsuario) && IdUsuario < 0)
                {
                    strings.Add($"IdUsuario: {Usuario[0]} es invalido |");

                }
                //Validar Nombre
                if (!Regex.IsMatch(Usuario[1], @"^[a-z A-Z]+$"))
                {
                    strings.Add($"Nombre: {Usuario[1]} es invalido |");
                }
                //Validar ApellidoPaterno
                if (!Regex.IsMatch(Usuario[2], @"^[a-z A-Z]+$"))
                {
                    strings.Add($"ApellidoPaterno: {Usuario[2]} es invalido |");

                }
                //Validar ApellidoMaterno
                if (!Regex.IsMatch(Usuario[3], @"^[a-z A-Z]+$"))
                {
                    strings.Add($"ApellidoMaterno: {Usuario[3]} es invalido |");

                }
                //Validar Email 
                bool isUniqueEmail = true;
                //REvisar por que no agrega el mensaje
                //foreach (ML.Usuario usuarioDB in resultGetAll.Objects)
                //{
                //    if (usuarioDB.Email == Usuario[4])
                //    {
                //        isUniqueEmail = false;
                //    }
                //}
                if (!Regex.IsMatch(Usuario[4], @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$") || !isUniqueEmail)
                {
                    strings.Add($"Email: {Usuario[4]} es invalido |");

                }
                //Validar UserName


                bool isUniqueUserName = true;

                //foreach (ML.Usuario usuarioDB in resultGetAll.Objects)
                //{
                //    if (usuarioDB.UserName == Usuario[5])
                //    {
                //        isUniqueUserName = false;
                //    }
                //}
                if (!Regex.IsMatch(Usuario[5], @"^[a-z A-Z]+$") || !isUniqueUserName)
                {
                    strings.Add($"UserName: {Usuario[5]} es invalido |");

                }
                //Validar Password
                if (!Regex.IsMatch(Usuario[6], "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"))
                {
                    strings.Add($"Contrasena : {Usuario[6]} es invalido |");

                }
                //Validar Sexo
                if (!Regex.IsMatch(Usuario[7], @"^[a-z A-Z]+$"))
                {
                    strings.Add($"Sexo: {Usuario[7]} es invalido |");

                }
                //Validar Telefono 
                if (!Regex.IsMatch(Usuario[8], "^\\+?[1-9][0-9]{7,14}$"))
                {
                    strings.Add($"Telefono: {Usuario[8]} es invalido |");

                }
                //Validar Celular 
                if (!Regex.IsMatch(Usuario[9], "^\\+?[1-9][0-9]{7,14}$"))
                {
                    strings.Add($"Celular: {Usuario[9]} es invalido |");

                }
                //ValidarFechaNacimiento 
                if (!Regex.IsMatch(Usuario[10], "^[0-9]{1,2}\\/[0-9]{1,2}\\/[0-9]{4}$") && DateTime.TryParse(Usuario[10], out DateTime Fecha))
                {
                    strings.Add($"Fecha: {Usuario[10]} es formato incorrecto o invalido |");

                }
                //Validar CURP
                if (!Regex.IsMatch(Usuario[11], @"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$"))
                {
                    strings.Add($"CURP: {Usuario[11]} es invalido |");

                }
                //Validar IdRol
                //if (Usuario.Rol.IdRol == 0 || Usuario.Rol.IdRol < 0)
                //{
                //    strings.Add($"IdRol: {Usuario.Rol.IdRol} es invalido |");

                //}
                if (!Regex.IsMatch(Usuario[12], "^[0-9]+$") && int.TryParse(Usuario[12], out int IdRol) && IdRol < 0)
                {
                    strings.Add($"IdUsuario: {Usuario[12]} es invalido |");

                }

                if (strings.Count > 0)
                {
                    ErroresLista.Add(string.Join("", strings));
                }
                else if (strings.Count == 0)
                {
                    ValidadosLista.Add($"Todos los campos son correctos: IdUser: {Usuario[0]} {Usuario[1]} {Usuario[2]} ");
                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

        }


        //LeerArchivoExcel

        public static ML.Result LeerArchivoExcel(string path, List<string> Errores, List<string> Validados)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            OleDbConnection objConn;
            try
            {
                using (OleDbConnection conn = new OleDbConnection())
                {
                    DataTable dt = new DataTable();
                    conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path
                    + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0'";

                    ///Codigo para obtener sheetname
                    ///// Get the data table containg the schema guid.
                    objConn = new OleDbConnection(conn.ConnectionString);
                    // Open connection with the database.
                    objConn.Open();
                    // Get the data table containg the schema guid.
                    dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    if (dt == null)
                    {
                        return null;
                    }
                    String[] excelSheets = new String[dt.Rows.Count];
                    int i = 0;
                    // Add the sheet name to the string array.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[i] = row["TABLE_NAME"].ToString();
                        i++;
                    }
                    objConn.Close();
                    using (OleDbCommand comm = new OleDbCommand())
                    {
                        DataTable mytable = new DataTable();
                        comm.CommandText = "Select * from [" + excelSheets[0] + "]";
                        comm.Connection = conn;
                        using (OleDbDataAdapter da = new OleDbDataAdapter())
                        {
                            da.SelectCommand = comm;
                            da.Fill(mytable);
                            result.Object = mytable;
                            //Validar 
                            //Pasar datos de dataset a string array 
                            List<string> listaString = new List<string>();

                            foreach (DataRow row in mytable.Rows)
                            {
                                DataRow newRow = row;
                                //Crear string apartir de ese row y despues agregar a nuestro string[]
                                string myString = $"{row[0]}|{row[1]}|{row[2]}|{row[3]}|{row[4]}|{row[5]}|{row[6]}|{row[7]}|{row[8]}|{row[9]}|{row[10]}|{row[11]}|{row[12]}|";
                                listaString.Add(myString);
                            }

                            string[] listaExcel = listaString.ToArray();

                            foreach (string str in listaExcel)
                            {

                                string[] newItem = str.Split('|');


                                ValidarDatos(newItem, Errores, Validados);

                                if (Errores.Count == 0)
                                {
                                    ML.Usuario usuario = new ML.Usuario();
                                    usuario.Rol = new ML.Rol();
                                    usuario.Direccion = new ML.Direccion();

                                    usuario.IdUsuario = (newItem[0] == " ") ? 0 : Convert.ToInt32(newItem[0]);
                                    usuario.Nombre = newItem[1] ?? " ";
                                    usuario.ApellidoPaterno = newItem[2] ?? " ";
                                    usuario.ApellidoMaterno = newItem[3] ?? " ";
                                    usuario.Email = newItem[4] ?? " ";
                                    usuario.UserName = newItem[5] ?? " ";
                                    usuario.Password = newItem[6] ?? " ";
                                    usuario.Sexo = newItem[7] ?? " ";
                                    usuario.Telefono = newItem[8] ?? " ";
                                    usuario.Celular = newItem[9] ?? " ";
                                    usuario.FechaNacimiento = newItem[10];
                                    usuario.Curp = newItem[10] ?? " ";
                                    usuario.Rol.IdRol = (newItem[11] == " ") ? 0 : Convert.ToInt32(newItem[12]);

                                    result.Objects.Add(usuario);

                                }

                            }

                            return result;
                        }
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


    }
}
