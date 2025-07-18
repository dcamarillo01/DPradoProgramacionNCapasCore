using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using ML;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly BL.Usuario _usuario;
        private readonly BL.Rol _rol;
        private readonly BL.Colonia _colonia;
        private readonly BL.Estado _estado;
        private readonly BL.Municipio _municipio;
        private readonly IConfiguration _configuration;
        public UsuarioController(BL.Usuario usuario, BL.Rol rol, BL.Colonia colonia, BL.Estado estado, BL.Municipio municipio, IConfiguration configuration)
        {

            _usuario = usuario;
            _rol = rol;
            _colonia = colonia;
            _estado = estado;
            _municipio = municipio;
            _configuration = configuration;
        }



        public IActionResult Index()
        {
            return View();
        }


        // ======================== GET ALL ================================ \\

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario
            {
                Rol = new ML.Rol(),
                Direccion = new ML.Direccion()
            };




            /// ================ Utilizando API REST ==================== \\\


            /// ============ Utilizando WebServices ===========
            // Implementaion de Web Services Automaticamente s
            //UsuarioReference.UsuarioClient usuarioSoap = new UsuarioReference.UsuarioClient();
            //var respuesta = usuarioSoap.GetAll(usuario);
            //if (respuesta.Correct)
            //{

            //    usuario.Usuarios = new List<object>();
            //    usuario.Usuarios = respuesta.Objects.ToList();
            //}

            // ============= Funcionalidad desde BL ======================
            //ML.Result resultGetAll = _usuario.GetAll(usuario);
            //if (resultGetAll.Correct)
            //{
            //    usuario.Usuarios = new List<object>();
            //    usuario.Usuarios = resultGetAll.Objects;
            //}

            //===================== GetAll y deserialización Manual. ===========================//
            //var usuarioTemp = GetAllSoap();
            //if (usuarioTemp != null)
            //{
            //    usuario = usuarioTemp;
            //}
            //============ Web Service API REST ===============\\\
            ML.Result resultGetAll = GetAllByAPI(usuario);

            if (resultGetAll.Correct)
            {
                usuario.Usuarios = new List<object>();
                usuario.Usuarios = resultGetAll.Objects;
            }


            //Traer roles 
            ML.Result resultRol = _rol.GetAll();
            if (resultRol.Correct)
            {
                usuario.Rol.Roles = resultRol.Objects;
            }

            //ViewBag.Errores = TempData["Errores"];

            return View(usuario);
        }

        [HttpPost]
        //, HttpPostedFileBase archivo
        public ActionResult GetAll(ML.Usuario Usuario, string file)
        {
            //Traer roles 
            Usuario.Rol = new ML.Rol();

            //Busqueda Filtrada

            Usuario.Nombre = Usuario.Nombre ?? "";
            Usuario.ApellidoPaterno = Usuario.ApellidoPaterno ?? "";
            Usuario.ApellidoMaterno = Usuario.ApellidoMaterno ?? "";

            Usuario.Rol = new ML.Rol();

            //Implementacion de uso de Web Service
            //UsuarioReference.UsuarioClient usuarioSoap = new UsuarioReference.UsuarioClient();
            //var respuesta = usuarioSoap.GetAll(Usuario);
            //if (respuesta.Correct)
            //{

            //    Usuario.Usuarios = new List<object>();
            //    Usuario.Usuarios = respuesta.Objects.ToList();
            //}

            //BL ======== BUSQUEDA ABIERTA ===============
            //ML.Result result = _usuario.GetAll(Usuario);

            //if (result.Correct)
            //{
            //    Usuario.Usuarios = result.Objects;
            //}

            ML.Result result = GetAllByAPI(Usuario);
            if (result.Correct)
            {
                Usuario.Usuarios = result.Objects;
            }

            ML.Result resultRol = _rol.GetAll();
            if (resultRol.Correct)
            {
                Usuario.Rol.Roles = resultRol.Objects;
            }

            // ======================== Carga Masiva ============================ 
            //if (file != null)
            //{

            //    //Fetch the File.
            //    IFormFile postedFile = Request.Files["ImportFile2"];

            //    if (file == "txt")
            //    {

            //        //Create the Directory.
            //        string path = Server.MapPath("~/Content/txt/");
            //        if (!Directory.Exists(path))
            //        {
            //            Directory.CreateDirectory(path);
            //            //Session[Correctos] = ruta
            //        }

            //        ////Fetch the File Name.
            //        string fileName = Path.GetFileNameWithoutExtension(postedFile.FileName);

            //        string fullPath = path + fileName + DateTime.Now.ToString("ddMMyyhhmmss") + ".txt";
            //        //Session["RutaFile"] = fullPath;
            //        //Save the File.
            //        postedFile.SaveAs(fullPath);


            //        //Mandar como viewBag MOdelo Carga masiva 
            //        ML.CargaMasiva cargaMasiva = new ML.CargaMasiva();
            //        cargaMasiva.Errores = new List<string>();
            //        cargaMasiva.Validados = new List<string>();

            //        BL.CargaMasiva.LeerArchivo(fullPath, cargaMasiva.Errores, cargaMasiva.Validados);

            //        ViewBag.CargaMasiva = cargaMasiva;

            //        if (cargaMasiva.Errores.Count > 1)
            //        {
            //            //SEND PATH FILE 
            //            System.IO.File.Delete(fullPath);


            //            //Crear carpeteta errores en txt
            //            string pathErrores = Server.MapPath("~/Content/txt/Errores/");
            //            if (!Directory.Exists(pathErrores))
            //            {
            //                Directory.CreateDirectory(pathErrores);
            //            }
            //            string fullPathErrores = pathErrores + fileName + "Errors" + DateTime.Now.ToString("ddMMyyhhmmss") + ".txt";
            //            Session["ErroresFile"] = fullPathErrores;
            //            //Crear nuevo archivo con errores
            //            using (StreamWriter outputFile = new StreamWriter(Path.Combine(fullPathErrores)))
            //            {
            //                foreach (string line in cargaMasiva.Errores)
            //                    outputFile.WriteLine(line);
            //            }

            //            //AHORA VE COMO HACER PARA QUE EL USUARIO PUEDA DESCARGAR UN ARCHIVO
            //            TempData["downloadPath"] = fullPathErrores;
            //        }
            //        else
            //        {
            //            Session["noErrorFile"] = fullPath;
            //            Session["ErroresFile"] = null;
            //        }

            //    }
            //    else if (file == "xlsx")
            //    {
            //        //Create the Directory.
            //        string pathExcel = Server.MapPath("~/Content/xlsx/");
            //        if (!Directory.Exists(pathExcel))
            //        {
            //            Directory.CreateDirectory(pathExcel);
            //            //Session[Correctos] = ruta
            //        }

            //        ////Fetch the File Name.
            //        string fileName = Path.GetFileNameWithoutExtension(postedFile.FileName);

            //        string fullPath = pathExcel + fileName + DateTime.Now.ToString("ddMMyyhhmmss") + ".xlsx";
            //        //Session["RutaFile"] = fullPath;
            //        //Save the File.
            //        postedFile.SaveAs(fullPath);

            //        // Llamar a metodo para leer archivo excel
            //        ML.CargaMasiva cargaMasiva = new ML.CargaMasiva();
            //        cargaMasiva.Errores = new List<string>();
            //        cargaMasiva.Validados = new List<string>();
            //        ML.Result resultExcel = BL.CargaMasiva.LeerArchivoExcel(fullPath, cargaMasiva.Errores, cargaMasiva.Validados);

            //        ViewBag.CargaMasiva = cargaMasiva;

            //        ViewBag.Tabla = resultExcel.Object;
            //        if (cargaMasiva.Errores.Count > 1)
            //        {
            //            //SEND PATH FILE 
            //            System.IO.File.Delete(fullPath);


            //            //Crear carpeteta errores en txt
            //            string pathErrores = Server.MapPath("~/Content/xlsx/Errores/");
            //            if (!Directory.Exists(pathErrores))
            //            {
            //                Directory.CreateDirectory(pathErrores);
            //            }
            //            string fullPathErrores = pathErrores + fileName + "Errors" + DateTime.Now.ToString("ddMMyyhhmmss") + ".xlsx";
            //            Session["ErroresFile"] = fullPathErrores;
            //            //Crear nuevo archivo con errores
            //            using (StreamWriter outputFile = new StreamWriter(Path.Combine(fullPathErrores)))
            //            {
            //                foreach (string line in cargaMasiva.Errores)
            //                    outputFile.WriteLine(line);
            //            }

            //            //AHORA VE COMO HACER PARA QUE EL USUARIO PUEDA DESCARGAR UN ARCHIVO
            //            TempData["downloadPath"] = fullPathErrores;
            //        }
            //        else
            //        { 
            //            Session["noErrorFile"] = fullPath;
            //            Session["ErroresFile"] = null;
            //        }
            //    }
            //}

            return View(Usuario);
        }


        // ========================= FORMULARIO ================================ \\
        //GET : UsuarioFormulario
        [HttpGet]
        public ActionResult Formulario(int? IdUsuario)
        {

            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            //ML.Result result = BL.Usuario.AddEFSP(usuario);

            ML.Result resultEstado = _estado.GetAll();
            if (resultEstado.Correct)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
            }

            //Traer datos si usuario existe
            if (IdUsuario > 0)
            {
                // =================== IMPLEMENTACION DESDE BL ================ \\

                ML.Result result = _usuario.GetById(IdUsuario.Value);
                usuario = (ML.Usuario)result.Object;

                /// Implementacion de uso de Web Service
                //UsuarioReference.UsuarioClient usuarioSoap = new UsuarioReference.UsuarioClient();
                //var respuesta = usuarioSoap.GetById(IdUsuario.Value);
                //usuario = (ML.Usuario)respuesta.Object;

                /// GetById Serializacion Manual 
                /// 
                //ML.Result usuarioById = GetByIdSoap(IdUsuario.Value);
                //usuario = (ML.Usuario)usuarioById.Object;



                // GetById Utilizando API REST
                //ML.Result resultAPIGetId = GeyByIdAPI(IdUsuario.Value);
                //usuario = (ML.Usuario)resultAPIGetId.Object;


                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;


                ML.Result resultMunicipio = _municipio.GetMunicipioByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                if (resultMunicipio.Correct)
                {

                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                }

                ML.Result resultColonia = _colonia.GetColoniaByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                if (resultColonia.Correct)
                {
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                }

            }
             
            //Traer roles 
            ML.Result resultRol = _rol.GetAll();
            if (resultRol.Correct)
            {
                usuario.Rol.Roles = resultRol.Objects;
            }

            return View(usuario);
        }

        // POST : UsuarioPost
        [HttpPost]
        public ActionResult Formulario(ML.Usuario usuario, IFormFile? ImagenUser)
        {

            //ML.Usuario usuario = new ML.Usuario();
            //usuario.Rol = new ML.Rol();

            //HttpPostedFileBase imgInput = Request.Files["imagenInputUser"];

            if (ImagenUser != null)
            {

                using (Stream inputStream = ImagenUser.OpenReadStream())
                {
                    if (!(inputStream is MemoryStream memoryStream))
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    usuario.Imagen = memoryStream.ToArray();
                }
            }

            ML.Result resultRol = _rol.GetAll();
            if (resultRol.Correct)
            {
                usuario.Rol.Roles = resultRol.Objects;
            }

            ML.Result resultEstado = _estado.GetAll();
            if (resultEstado.Correct)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
            }

            ML.Result resultMunicipio = _municipio.GetMunicipioByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
            if (resultMunicipio.Correct)
            {

                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
            }

            ML.Result resultColonia = _colonia.GetColoniaByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
            if (resultColonia.Correct)
            {
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
            }


            if (ModelState.IsValid)
            {
                if (usuario.IdUsuario > 0)
                {

                    // ==================== Implementacion desde BL ================= \\
                    ML.Result resultUpdate = _usuario.Update(usuario.IdUsuario, usuario);

                    //Implementacion de Web Service
                    //UsuarioReference.UsuarioClient usuarioSoap = new UsuarioReference.UsuarioClient();
                    //usuarioSoap.Update(usuario.IdUsuario, usuario);

                    //Serializacion manual 
                    //Llamar a metodo para serializar , este regresara modelo
                    //var usuarioUpdate = AddUpdateSoap(usuario);
                    //if (usuarioUpdate.Correct) {
                    //    BL.Usuario.UpdateEFSP(usuario.IdUsuario, usuario);
                    //}

                    //AddUpdateSoap(usuario);
                    /// WEB SERVICE API REST
                    //UpdateByAPI(usuario);
                    if (resultUpdate.Correct) {
                        return RedirectToAction("GetAll", "Usuario");
                    }

                }
                else
                {
                    // =========== Implementacion con BL ========== \\
                    ML.Result resultAdd = _usuario.Add(usuario);
                    //Implementacion de WebService
                    //UsuarioReference.UsuarioClient usuarioSoap = new UsuarioReference.UsuarioClient();
                    //usuarioSoap.Add(usuario);

                    //WebService Serializacion Manual 
                    //var usuarioAdd = AddUpdateSoap(usuario);

                    //WebSercice API REST
                    //ML.Result resultadd = GetAllByAPI();
                    //AddByAPI(usuario);
                    if (resultAdd.Correct) { 
                        return RedirectToAction("GetAll", "Usuario");
                    }

                }
            }
            else
            {

                return View(usuario);
                //Mostrar los erroes en form
            }

            return View(usuario);

            //return View(usuario);
        }

        //[NonAction]
        //public FileResult Download()
        //{
        //    string data = TempData["downloadPath"].ToString();
        //    // Session de errores
        //    Session["noErrorFile"] = null;


        //    return File(data, " text / plain", Path.GetFileNameWithoutExtension(data));
        //}

        //Guardar insertar datos
        //[HttpPost]
        //public ActionResult InsertarDatos()
        //{

        //    BL.Usuario.InsertUser(Session["noErrorFile"].ToString());
        //    Session["noErrorFile"] = null;

        //    return RedirectToAction("GetAll", "Usuario");
        //}

        // DELETE : UsuarioDelete
        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            // Delete desde BL
            _usuario.Delete(IdUsuario);

            //Implementar web service 
            //UsuarioReference.UsuarioClient usuarioSoap = new UsuarioReference.UsuarioClient();
            //usuarioSoap.Delete(IdUsuario);

            //Serializar manualmente result delete Result
            //ML.Result usuarioDelete = DeleteSoap(IdUsuario);

            //Delete Usando API REST 

            //DeleteByAPI(IdUsuario);


            return RedirectToAction("GetAll", "Usuario");
        }


        ///Json para obtener municipios
        [HttpGet]
        public JsonResult GetMunicipioByIdEstado(int IdEstado)
        {

            ML.Result resultMunicipio = _municipio.GetMunicipioByIdEstado(IdEstado);


            return Json(resultMunicipio);
        }

        ///Json para obtener colonias
        [HttpGet]
        public JsonResult GetColoniaByIdMunicipio(int IdMunicipio)
        {

            ML.Result resultColonia = _colonia.GetColoniaByIdMunicipio(IdMunicipio);

            return Json(resultColonia);
        }

        ///Json para obtener Status bit 
        [HttpGet]
        public JsonResult SetUserStatus(int IdUsuario, bool statusCheck)
        {

            ML.Result resultStatus = _usuario.SetStatus(IdUsuario, statusCheck);

            return Json(resultStatus);
        }






        // ======================== CONSUMIR SOAP =========================\\

        // ========== Metodo para pasar de string a xml

        // ===================== Serializacion ========================== ]]

        [NonAction]
        private ML.Result AddResult(string xml)
        {
            ML.Result result = new ML.Result();


            var xdoc = XDocument.Parse(xml);
            // Acceder a GetUsuarioByIdResult usando el namespace correcto
            var usuarioElement = xdoc.Descendants().FirstOrDefault(e =>
                e.Name.LocalName == "AddResult" &&
                e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");


            result.Correct = bool.TryParse(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/SL_WCF}Correct")?.Value, out bool theresult);
            result.Correct = Convert.ToBoolean(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/SL_WCF}Correct")?.Value);
            result.ErrorMessage = (string)(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}ErrorMessage")?.Value ?? string.Empty);
            var newEx = (string)(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Ex")?.Value ?? string.Empty);
            result.Ex = new Exception(newEx);


            return result;
        }
        [NonAction]
        private ML.Result UpdateResult(string xml)
        {
            ML.Result result = new ML.Result();


            var xdoc = XDocument.Parse(xml);
            // Acceder a GetUsuarioByIdResult usando el namespace correcto
            var usuarioElement = xdoc.Descendants().FirstOrDefault(e =>
                e.Name.LocalName == "Update" &&
                e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");


            result.Correct = bool.TryParse(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/SL_WCF}Correct")?.Value, out bool theresult);
            result.Correct = Convert.ToBoolean(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/SL_WCF}Correct")?.Value);
            result.ErrorMessage = (string)(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}ErrorMessage")?.Value ?? string.Empty);
            var newEx = (string)(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Ex")?.Value ?? string.Empty);
            result.Ex = new Exception(newEx);


            return result;
        }

        [NonAction]
        private ML.Result DeleteResult(string xml)
        {
            ML.Result result = new ML.Result();


            var xdoc = XDocument.Parse(xml);
            // Acceder a GetUsuarioByIdResult usando el namespace correcto
            var usuarioElement = xdoc.Descendants().FirstOrDefault(e =>
                e.Name.LocalName == "DeleteResult" &&
                e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");


            result.Correct = bool.TryParse(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/SL_WCF}Correct")?.Value, out bool theresult);
            result.Correct = Convert.ToBoolean(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/SL_WCF}Correct")?.Value);
            result.ErrorMessage = (string)(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}ErrorMessage")?.Value ?? string.Empty);
            var newEx = (string)(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Ex")?.Value ?? string.Empty);
            result.Ex = new Exception(newEx);


            return result;
        }

        [NonAction]
        private ML.Usuario GetAllUsuarios(string xml)
        {
            var usuario = new ML.Usuario();
            ML.Result result = new ML.Result
            {
                Objects = new List<object>()
            };
            usuario.Usuarios = new List<object>();


            var xdoc = XDocument.Parse(xml);

            // Acceder a GetAllUsuarioResult 
            var objects = xdoc.Descendants("{http://schemas.microsoft.com/2003/10/Serialization/Arrays}anyType");

            foreach (var elem in objects)
            {
                var usuarioXML = new ML.Usuario();
                usuarioXML.Rol = new ML.Rol();
                usuarioXML.Direccion = new ML.Direccion();
                // Manejo de IdUsuario null  kjhfdkj 
                //byte[] 
                int idUsuario;

                if (elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value != null)
                {
                    idUsuario = int.Parse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value);
                }
                else
                {
                    idUsuario = 0;
                }

                //int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value, out idUsuario); //0 
                usuarioXML.IdUsuario = idUsuario;

                // Acceso a otros campos 
                usuarioXML.Nombre = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                usuarioXML.ApellidoPaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoPaterno")?.Value ?? string.Empty);
                usuarioXML.ApellidoMaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoMaterno")?.Value ?? string.Empty);
                usuarioXML.Email = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Email")?.Value ?? string.Empty);
                usuarioXML.UserName = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}UserName")?.Value ?? string.Empty);
                usuarioXML.Password = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Password")?.Value ?? string.Empty);
                usuarioXML.Sexo = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Sexo")?.Value ?? string.Empty);
                usuarioXML.Telefono = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Telefono")?.Value ?? string.Empty);
                usuarioXML.Celular = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Celular")?.Value ?? string.Empty);
                usuarioXML.FechaNacimiento = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}FechaNacimiento")?.Value ?? string.Empty);
                usuarioXML.Curp = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Curp")?.Value ?? string.Empty);
                //int idRol;
                //int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdRol")?.Value, out idRol); //0 
                //usuarioXML.Rol.IdRol = idRol;
                var rolEtiqueta = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Rol");
                usuarioXML.Rol.Nombre = (string)rolEtiqueta.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre");
                var direccionEtiqueta = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion");
                usuarioXML.Direccion.Calle = (string)(direccionEtiqueta.Element("{http://schemas.datacontract.org/2004/07/ML}Calle")?.Value ?? string.Empty);
                usuarioXML.Direccion.NumeroInterior = (string)(direccionEtiqueta.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroInterior")?.Value ?? string.Empty);
                usuarioXML.Direccion.NumeroExterior = (string)(direccionEtiqueta.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroExterior")?.Value ?? string.Empty);

                var imagen = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Imagen")?.Value;

                if (!string.IsNullOrEmpty(imagen))
                {
                    usuarioXML.Imagen = Convert.FromBase64String(imagen);
                }
                else
                {
                    usuarioXML.Imagen = null;
                }


                usuario.Usuarios.Add(usuarioXML);

            }

            return usuario; // Devuelve el objeto completo 
        }

        // SOAAP }}

        [NonAction]
        private ML.Usuario GetAllSoap()
        {

            string action = "http://tempuri.org/IUsuario/GetAll";
            string url = "http://localhost:56412/Usuario.svc"; // Cambia a la URL del servicio 

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST"; // Cambia a POST ya que estás usando un servicio SOAP 

            // Crear el sobre SOAP 

            string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?> 
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
                   <soapenv:Header/>
                   <soapenv:Body>
                      <tem:GetAll>
                         <tem:Usuario>
                            <ml:ApellidoMaterno></ml:ApellidoMaterno>
                            <ml:ApellidoPaterno></ml:ApellidoPaterno>
                            <ml:Nombre></ml:Nombre>
                            <ml:Rol>
                               <ml:IdRol>0</ml:IdRol>
                            </ml:Rol>
                         </tem:Usuario>
                      </tem:GetAll>
                   </soapenv:Body>
                </soapenv:Envelope>";


            // Enviar la solicitud 

            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }

            // Obtener la respuesta 

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        // Deserializar el XML 
                        var usuarios = GetAllUsuarios(result); // Captura el objeto completo 
                        usuarios.Rol = new ML.Rol();

                        ML.Result resultRolXml = _rol.GetAll();
                        if (resultRolXml.Correct)
                        {
                            usuarios.Rol.Roles = resultRolXml.Objects;
                        }


                        return usuarios; // Asegúrate de que tu vista esté lista para recibir este objeto 
                    }
                }
            }

            catch (WebException ex)
            {
                ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario 
            }

            return null;
        }

        [NonAction]
        private ML.Result AddUpdateSoap(ML.Usuario usuarioSoap)
        {

            string url = "http://localhost:56412/Usuario.svc"; // URL del servicio 
            string soapEnvelope;
            string action; // Declarar la variable action aquí 

            // Verificar si IdUsuario es null o 0 (o algún valor que determines como "nuevo") 

            if (usuarioSoap.IdUsuario == 0)
            {
                // Crear el sobre SOAP para agregar un nuevo usuario 
                action = "http://tempuri.org/IUsuario/Add";
                soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?> 
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
                       <soapenv:Header/>
                       <soapenv:Body>
                          <tem:Add>
                             <tem:usuario>
                                <ml:ApellidoMaterno>{usuarioSoap.ApellidoPaterno}</ml:ApellidoMaterno>
                                <ml:ApellidoPaterno>{usuarioSoap.ApellidoPaterno}</ml:ApellidoPaterno>
                                <ml:Celular>{usuarioSoap.Celular}</ml:Celular>
                                <ml:Curp>{usuarioSoap.Curp}</ml:Curp>
                                <ml:Direccion>
                                   <ml:Calle>{usuarioSoap.Direccion.Calle}</ml:Calle>
                                   <ml:Colonia>
                                      <ml:IdColonia>{usuarioSoap.Direccion.Colonia.IdColonia}</ml:IdColonia>
                                   </ml:Colonia>
                                   <ml:NumeroExterior>{usuarioSoap.Direccion.NumeroExterior}</ml:NumeroExterior>
                                   <ml:NumeroInterior>{usuarioSoap.Direccion.NumeroInterior}</ml:NumeroInterior>
                                   <ml:Usuario/>
                                </ml:Direccion>
                                <ml:Email>{usuarioSoap.Email}</ml:Email>
                                <ml:FechaNacimiento>{usuarioSoap.FechaNacimiento}</ml:FechaNacimiento>
                                <ml:Imagen>{Convert.ToBase64String(usuarioSoap.Imagen)}</ml:Imagen>
                                <ml:Nombre>{usuarioSoap.Nombre}</ml:Nombre>
                                <ml:Password>{usuarioSoap.Password}</ml:Password>
                                <ml:Rol>
                                   <ml:IdRol>{usuarioSoap.Rol.IdRol}</ml:IdRol>
                                </ml:Rol>
                                <ml:Sexo>{usuarioSoap.Sexo}</ml:Sexo>
                                <ml:Telefono>{usuarioSoap.Telefono}</ml:Telefono>
                                <ml:UserName>{usuarioSoap.UserName}</ml:UserName>
                             </tem:usuario>
                          </tem:Add>
                       </soapenv:Body>
                    </soapenv:Envelope>";
            }
            else
            {
                // Crear el sobre SOAP para actualizar un usuario existente 
                action = "http://tempuri.org/IUsuario/Update";
                soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?> 
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
               <soapenv:Header/>
               <soapenv:Body>
                  <tem:Update>
                     <tem:idUsuario>{usuarioSoap.IdUsuario}</tem:idUsuario>
                     <tem:usuario>
                        <ml:ApellidoMaterno>{usuarioSoap.ApellidoMaterno}</ml:ApellidoMaterno>
                        <ml:ApellidoPaterno>{usuarioSoap.ApellidoPaterno}</ml:ApellidoPaterno>
                        <ml:Celular>{usuarioSoap.Celular}</ml:Celular>
                        <ml:Curp>{usuarioSoap.Curp}</ml:Curp>
                        <ml:Direccion>
                           <ml:Calle>{usuarioSoap.Direccion.Calle}</ml:Calle>
                           <ml:Colonia>
                              <ml:IdColonia>{usuarioSoap.Direccion.Colonia.IdColonia}</ml:IdColonia>
                           </ml:Colonia>
                           <ml:NumeroExterior>{usuarioSoap.Direccion.NumeroExterior}</ml:NumeroExterior>
                           <ml:NumeroInterior>{usuarioSoap.Direccion.NumeroInterior}</ml:NumeroInterior>
                           <ml:Usuario/>
                        </ml:Direccion>
                        <ml:Email>{usuarioSoap.Email}</ml:Email>
                        <ml:FechaNacimiento>{usuarioSoap.FechaNacimiento}</ml:FechaNacimiento>
                        <ml:Imagen>{Convert.ToBase64String(usuarioSoap.Imagen)}</ml:Imagen>
                        <ml:Nombre>{usuarioSoap.Nombre}</ml:Nombre>
                        <ml:Password>{usuarioSoap.Password}</ml:Password>
                        <ml:Rol>
                           <ml:IdRol>{usuarioSoap.Rol.IdRol}</ml:IdRol>
                        </ml:Rol>
                        <ml:Sexo>{usuarioSoap.Sexo}</ml:Sexo>
                        <ml:Telefono>{usuarioSoap.Telefono}</ml:Telefono>
                        <ml:UserName>{usuarioSoap.UserName}</ml:UserName>
                     </tem:usuario>
                  </tem:Update>
               </soapenv:Body>
            </soapenv:Envelope>";
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action); // Aquí ya existe la variable action 
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";

            // Enviar la solicitud 
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }
            // Obtener la respuesta 
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        Console.WriteLine("Respuesta SOAP:");
                        Console.WriteLine(result);
                        // Aquí puedes manejar la respuesta según sea necesario 
                        //Deserializar xml result 
                        if (usuarioSoap.IdUsuario > 0)
                        {
                            return UpdateResult(result);
                        }
                        else
                        {
                            return AddResult(result);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario 
            }

            return null;
        }
        [NonAction]
        private ML.Result DeleteSoap(int IdUsuario)
        {

            string action = "http://tempuri.org/IUsuario/Delete";
            string url = "http://localhost:56412/Usuario.svc"; // Cambia a la URL del servicio 

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST"; // Cambia a POST ya que estás usando un servicio SOAP 

            // Crear el sobre SOAP 
            string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?> 
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
               <soapenv:Header/>
               <soapenv:Body>
                  <tem:Delete>
                     <!--Optional:-->
                     <tem:IdUsuario>{IdUsuario}</tem:IdUsuario>
                  </tem:Delete>
               </soapenv:Body>
            </soapenv:Envelope>";

            // Enviar la solicitud 
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }
            // Obtener la respuesta 
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        // Deserializar el XML 
                        ML.Result resultDelete = DeleteResult(result); // Captura el objeto completo 

                        return resultDelete; // Asegúrate de que tu vista esté lista para recibir este objeto 
                    }
                }
            }

            catch (WebException ex)
            {
                ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario 
            }

            return null;
        }

        [NonAction]
        private ML.Result GetByIdSoap(int IdUsuario)
        {

            ML.Result resultUser = new ML.Result();
            if (IdUsuario > 0)
            {
                // Obtener el usuario por ID 
                string action = "http://tempuri.org/IUsuario/GetById";
                string url = "http://localhost:56412/Usuario.svc";

                // Crear el sobre SOAP para obtener un usuario por su ID 
                string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?> 
                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                   <soapenv:Header/>
                   <soapenv:Body>
                      <tem:GetById>
                         <tem:IdUsuario>{IdUsuario}</tem:IdUsuario>
                      </tem:GetById>
                   </soapenv:Body>
                </soapenv:Envelope>";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("SOAPAction", action);
                request.ContentType = "text/xml;charset=\"utf-8\"";
                request.Accept = "text/xml";
                request.Method = "POST";

                // Enviar la solicitud 
                using (Stream stream = request.GetRequestStream())
                {
                    byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                    stream.Write(content, 0, content.Length);
                }
                // Obtener la respuesta 
                try
                {
                    using (WebResponse response = request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            string result = reader.ReadToEnd();
                            Console.WriteLine("Respuesta SOAP:");
                            Console.WriteLine(result);
                            // Deserializar el usuario 
                            var usuario = GetUsuarioById(result);
                            resultUser.Object = usuario;

                            return resultUser;
                        }
                    }
                }
                catch (WebException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario 
                }


            }
            return null;
        }
        // Devuelve la vista con el usuario si existe 

        [NonAction]
        private ML.Usuario GetUsuarioById(string xml)

        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

            var xdoc = XDocument.Parse(xml);
            // Acceder a GetUsuarioByIdResult usando el namespace correcto 
            var usuarioElement = xdoc.Descendants().FirstOrDefault(e =>
                e.Name.LocalName == "Object" &&
                e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");
            if (usuarioElement != null)
            {
                int.TryParse(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value, out int idUsuario); //0 
                usuario.IdUsuario = idUsuario;

                // Acceso a otros campos 
                usuario.Nombre = (string)(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                usuario.ApellidoPaterno = (string)(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoPaterno")?.Value ?? string.Empty);
                usuario.ApellidoMaterno = (string)(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoMaterno")?.Value ?? string.Empty);
                usuario.Email = (string)(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Email")?.Value ?? string.Empty);
                usuario.UserName = (string)(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}UserName")?.Value ?? string.Empty);
                usuario.Password = (string)(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Password")?.Value ?? string.Empty);
                usuario.Sexo = (string)(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Sexo")?.Value ?? string.Empty);
                usuario.Telefono = (string)(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Telefono")?.Value ?? string.Empty);
                usuario.Celular = (string)(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Celular")?.Value ?? string.Empty);
                usuario.FechaNacimiento = (string)(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}FechaNacimiento")?.Value ?? string.Empty);
                usuario.Curp = (string)(usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Curp")?.Value ?? string.Empty);

                var rolEtiqueta = usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Rol");
                usuario.Rol.Nombre = (string)rolEtiqueta.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre");
                var direccionEtiqueta = usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion");
                usuario.Direccion.Calle = (string)(direccionEtiqueta.Element("{http://schemas.datacontract.org/2004/07/ML}Calle")?.Value ?? string.Empty);
                usuario.Direccion.NumeroInterior = (string)(direccionEtiqueta.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroInterior")?.Value ?? string.Empty);
                usuario.Direccion.NumeroExterior = (string)(direccionEtiqueta.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroExterior")?.Value ?? string.Empty);
                var etiquetaColonia = direccionEtiqueta.Element("{http://schemas.datacontract.org/2004/07/ML}Colonia");
                int.TryParse(etiquetaColonia.Element("{http://schemas.datacontract.org/2004/07/ML}IdColonia")?.Value, out int IdColonia); //0 
                usuario.Direccion.Colonia.IdColonia = IdColonia;
                var etiquetaMunicipio = etiquetaColonia.Element("{http://schemas.datacontract.org/2004/07/ML}Municipio");
                int.TryParse(etiquetaMunicipio.Element("{http://schemas.datacontract.org/2004/07/ML}IdMunicipio")?.Value, out int IdMunicipio); //0 
                usuario.Direccion.Colonia.Municipio.IdMunicipio = IdMunicipio;
                var etiquetaEstado = etiquetaMunicipio.Element("{http://schemas.datacontract.org/2004/07/ML}Estado");
                int.TryParse(etiquetaEstado.Element("{http://schemas.datacontract.org/2004/07/ML}IdEstado")?.Value, out int IdEstado); //0 
                usuario.Direccion.Colonia.Municipio.Estado.IdEstado = IdEstado;
                var imagen = usuarioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Imagen")?.Value;
                if (!string.IsNullOrEmpty(imagen))
                {
                    usuario.Imagen = Convert.FromBase64String(imagen);
                }
                else
                {
                    usuario.Imagen = null;
                }
                return usuario;

            }

            return null; // O lanzar una excepción si no se encontró el usuario 
        }

        // ====================== METODOS CONSUMIR APIs ================================ \\

        [NonAction]
        public  ML.Result GetAllByAPI(ML.Usuario Usuario)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (var client = new HttpClient())
                {

                    //RecuperarBaseAddress de AppSettings  
                    var userEndPoint = _configuration.GetValue<string>("ApiEndPoint");
                    client.BaseAddress = new Uri(userEndPoint);
                    var responseTask = client.PostAsJsonAsync("GetAll", Usuario);
                    responseTask.Wait(); //abrir otro hilo 
                    var resultServicio = responseTask.Result;
                    if (resultServicio.IsSuccessStatusCode) //200 - 299
                    {
                        result.Correct = true;
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        //[NonAction]
        //public static ML.Result AddByAPI(ML.Usuario Usuario)
        //{
        //    ML.Result resultAdd = new Result();

        //    if (Usuario.Imagen != null)
        //    {
        //        Usuario.ImagenBase64 = Convert.ToBase64String(Usuario.Imagen);
        //    }
        //    else
        //    {
        //        Usuario.ImagenBase64 = "";
        //    }
        //    Usuario.Imagen = null;

        //    using (var client = new HttpClient())
        //    {
        //        var userEndPoint = ConfigurationManager.AppSettings["UsuarioEndPoint"];
        //        client.BaseAddress = new Uri(userEndPoint);
        //        //HTTP POST 
        //        var postTask = client.PostAsJsonAsync<ML.Usuario>("Add", Usuario); //Serializar 
        //        postTask.Wait();
        //        var result = postTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            //MODAL 
        //            //ViewBag.ErrorMessage;
        //            resultAdd.Object = result.Content;
        //            return resultAdd;
        //        }
        //    }
        //    return resultAdd;
        //}
        //[NonAction]
        //public static ML.Result UpdateByAPI(ML.Usuario Usuario)
        //{
        //    ML.Result resultUpdate = new Result();

        //    if (Usuario.Imagen != null)
        //    {
        //        Usuario.ImagenBase64 = Convert.ToBase64String(Usuario.Imagen);
        //    }
        //    else
        //    {
        //        Usuario.ImagenBase64 = "";
        //    }
        //    Usuario.Imagen = null;


        //    using (var client = new HttpClient())
        //    {
        //        var userEndPoint = ConfigurationManager.AppSettings["UsuarioEndPoint"];
        //        client.BaseAddress = new Uri(userEndPoint);
        //        //HTTP POST 
        //        var postTask = client.PutAsJsonAsync<ML.Usuario>($"Update/{Usuario.IdUsuario}", Usuario); //Serializar 
        //        postTask.Wait();
        //        var result = postTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            //MODAL 
        //            //ViewBag.ErrorMessage;
        //            resultUpdate.Object = result.Content;
        //            return resultUpdate;
        //        }
        //    }
        //    return resultUpdate;
        //}
        //[NonAction]
        //public static ML.Result DeleteByAPI(int IdUsuario)
        //{

        //    ML.Result result = new ML.Result();
        //    using (var client = new HttpClient())
        //    {
        //        var userEndPoint = ConfigurationManager.AppSettings["UsuarioEndPoint"];
        //        client.BaseAddress = new Uri(userEndPoint);

        //        var deleteTask = client.DeleteAsync($"Delete/{IdUsuario}");
        //        deleteTask.Wait();
        //        var resultAPI = deleteTask.Result;
        //        if (resultAPI.IsSuccessStatusCode)
        //        {
        //            result.Object = resultAPI.Content;
        //            return result;
        //        }
        //        else
        //        {
        //            result.Correct = false;
        //            return result;
        //        }

        //    }

        //}

        //[NonAction]
        //public static ML.Result GeyByIdAPI(int IdUsuario)
        //{

        //    ML.Result resultGetById = new ML.Result();

        //    using (var client = new HttpClient())
        //    {

        //        var userEndPoint = ConfigurationManager.AppSettings["UsuarioEndPoint"];
        //        client.BaseAddress = new Uri(userEndPoint);

        //        var getIdTask = client.GetAsync($"GetById/{IdUsuario}");
        //        getIdTask.Wait();
        //        var resultAPI = getIdTask.Result;



        //        if (resultAPI.IsSuccessStatusCode)
        //        {
        //            resultGetById.Correct = true;
        //            var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
        //            readTask.Wait();
        //            ML.Usuario newUser = JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());

        //            resultGetById.Object = newUser;
        //            return resultGetById;
        //        }
        //        else
        //        {
        //            resultGetById.Correct = false;
        //            resultGetById.Object = resultAPI.Content;
        //            return resultGetById;
        //        }

        //    }
        //}


    }
}
