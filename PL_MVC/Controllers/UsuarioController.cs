using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using ML;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
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
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _webHostEnvironment;
        public UsuarioController(BL.Usuario usuario, BL.Rol rol, BL.Colonia colonia, BL.Estado estado, BL.Municipio municipio, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {

            _usuario = usuario;
            _rol = rol;
            _colonia = colonia;
            _estado = estado;
            _municipio = municipio;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }



        public IActionResult Index()
        {
            return View();
        }


        // ======================== GET ALL ================================ \\

        [HttpGet]
        [Authorize]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new()
            {
                Rol = new ML.Rol(),
                Direccion = new ML.Direccion()
            };

 

            // ============= Funcionalidad desde BL ======================
            //ML.Result resultGetAll = _usuario.GetAll(usuario);
            //if (resultGetAll.Correct)
            //{
            //    usuario.Usuarios = new List<object>();
            //    usuario.Usuarios = resultGetAll.Objects;
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
        [Authorize]
        //, HttpPostedFileBase archivo
        public ActionResult GetAll(ML.Usuario Usuario, IFormFile ImportFile2)
        {
            //Traer roles 
            Usuario.Rol = new ML.Rol();

            //Busqueda Filtrada

            Usuario.Nombre ??= "";
            Usuario.ApellidoPaterno ??= "";
            Usuario.ApellidoMaterno ??= "";

            Usuario.Rol = new ML.Rol();


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

            var file = ImportFile2;
            if (file != null)
            {

                //Fetch the File.
                IFormFile postedFile = file;
                var fileExtension = System.IO.Path.GetExtension(file.FileName);

                if (fileExtension == ".txt")
                {

                    //Alternative way to use server.MapPath in .net core
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    string contentRootPath = _webHostEnvironment.ContentRootPath;

                    string path = "";
                    path = Path.Combine(webRootPath, "txt");
                    //or path = Path.Combine(contentRootPath , "wwwroot" ,"CSS" );

                    //Create the Directory.
                    //string path = Server.MapPath("~/Content/txt/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                        //Session[Correctos] = ruta
                    }

                    ////Fetch the File Name.
                    string fileName = Path.GetFileNameWithoutExtension(postedFile.FileName);

                    string fullPath = path + fileName + DateTime.Now.ToString("ddMMyyhhmmss") + ".txt";
                    //Session["RutaFile"] = fullPath;
                    //Save the File.
                    //postedFile.SaveAs(fullPath);
                    //============ .NET CORE SAVE FILE????? ===============\\
                    
                    using (FileStream fs = System.IO.File.Create(fullPath))
                    {
                        file.CopyTo(fs);
                    }


                    //Mandar como viewBag MOdelo Carga masiva 
                    ML.CargaMasiva cargaMasiva = new ML.CargaMasiva();
                    cargaMasiva.Errores = new List<string>();
                    cargaMasiva.Validados = new List<string>();

                    BL.CargaMasiva.LeerArchivo(fullPath, cargaMasiva.Errores, cargaMasiva.Validados);

                    ViewBag.CargaMasiva = cargaMasiva;

                    if (cargaMasiva.Errores.Count > 1)
                    {
                        //SEND PATH FILE 
                        System.IO.File.Delete(fullPath);


                        //Crear carpeteta errores en txt
                        //string pathErrores = Server.MapPath("~/Content/txt/Errores/");
                        string pathErrores = "";
                        pathErrores = Path.Combine(webRootPath, "txt\\Errores");
                        if (!Directory.Exists(pathErrores))
                        {
                            Directory.CreateDirectory(pathErrores);
                        }
                        string fullPathErrores = pathErrores + fileName + "Errors" + DateTime.Now.ToString("ddMMyyhhmmss") + ".txt";
                        //Session["ErroresFile"] = fullPathErrores;
                        //Session .Net Core
                        HttpContext.Session.SetString("ErroresFile", fullPathErrores);

                        //Crear nuevo archivo con errores
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(fullPathErrores)))
                        {
                            foreach (string line in cargaMasiva.Errores)
                                outputFile.WriteLine(line);
                        }

                        //AHORA VE COMO HACER PARA QUE EL USUARIO PUEDA DESCARGAR UN ARCHIVO
                        TempData["downloadPath"] = fullPathErrores;
                    }
                    else
                    {

                        //Session["noErrorFile"] = fullPath;
                        //Session["ErroresFile"] = null;
                        HttpContext.Session.SetString("noErrorFile", fullPath);
                        HttpContext.Session.SetString("noErrorFile", null);

                    }

                }
                else if (fileExtension == ".xlsx")
                {


                    //Alternative way to use server.MapPath in .net core
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    string contentRootPath = _webHostEnvironment.ContentRootPath;
                    //Create the Directory.
                    string pathExcel = "";
                    pathExcel = Path.Combine(webRootPath, "xlsx");
                    //string pathExcel = Server.MapPath("~/Content/xlsx/");
                    if (!Directory.Exists(pathExcel))
                    {
                        Directory.CreateDirectory(pathExcel);
                        //Session[Correctos] = ruta
                    }

                    ////Fetch the File Name.
                    string fileName = Path.GetFileNameWithoutExtension(postedFile.FileName);

                    string fullPath = pathExcel + fileName + DateTime.Now.ToString("ddMMyyhhmmss") + ".xlsx";
                    //Session["RutaFile"] = fullPath;
                    //Save the File.
                    //postedFile.SaveAs(fullPath);

                    using (FileStream fs = System.IO.File.Create(fullPath))
                    {
                        file.CopyTo(fs);
                    }


                    // Llamar a metodo para leer archivo excel
                    ML.CargaMasiva cargaMasiva = new ML.CargaMasiva();
                    cargaMasiva.Errores = new List<string>();
                    cargaMasiva.Validados = new List<string>();
                    ML.Result resultExcel = BL.CargaMasiva.LeerArchivoExcel(fullPath, cargaMasiva.Errores, cargaMasiva.Validados);

                    ViewBag.CargaMasiva = cargaMasiva;

                    ViewBag.Tabla = resultExcel.Object;
                    if (cargaMasiva.Errores.Count > 1)
                    {
                        //SEND PATH FILE 
                        System.IO.File.Delete(fullPath);

                        string pathErrores = "";
                        pathErrores = Path.Combine(webRootPath, "xlsx\\Errores");
                        //Crear carpeteta errores en txt
                        //string pathErrores = Server.MapPath("~/Content/xlsx/Errores/");
                        if (!Directory.Exists(pathErrores))
                        {
                            Directory.CreateDirectory(pathErrores);
                        }
                        string fullPathErrores = pathErrores + fileName + "Errors" + DateTime.Now.ToString("ddMMyyhhmmss") + ".xlsx";
                        //Session["ErroresFile"] = fullPathErrores;
                        HttpContext.Session.SetString("ErroresFile", fullPathErrores);

                        //Crear nuevo archivo con errores
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(fullPathErrores)))
                        {
                            foreach (string line in cargaMasiva.Errores)
                                outputFile.WriteLine(line);
                        }

                        //AHORA VE COMO HACER PARA QUE EL USUARIO PUEDA DESCARGAR UN ARCHIVO
                        TempData["downloadPath"] = fullPathErrores;
                    }
                    else
                    {
                        //Session["noErrorFile"] = fullPath;
                        //Session["ErroresFile"] = null;
                        HttpContext.Session.SetString("noErrorFile", fullPath);
                        HttpContext.Session.SetString("ErroresFile", "0");

                    }
                }
            }

            return View(Usuario);
        }


        // ============ METODOS CARGA MASIVA ============ \\

        [HttpGet]
        public FileResult Download()
        {
            string data = TempData["downloadPath"].ToString();
            // Session de errores
            //Session["noErrorFile"] = null;
            HttpContext.Session.SetString("noErrorFile", "0");



            return PhysicalFile(data, "text/plain", Path.GetFileNameWithoutExtension(data));
            //return File(data, "text/plain", Path.GetFileNameWithoutExtension(data));
        }

        

        //Guardar insertar datos
        [HttpGet]
        public ActionResult InsertarDatos()
        {

            //BL.Usuario.InsertUser(Session["noErrorFile"].ToString());
            var username = HttpContext.Session.GetString("noErrorFile");
            _usuario.InsertUser(username);
            //Session["noErrorFile"] = null;
            HttpContext.Session.SetString("noErrorFile", "0");


            return RedirectToAction("GetAll", "Usuario");
        }


        // ========================= FORMULARIO ================================ \\
        //GET : UsuarioFormulario
        [HttpGet]
        [Authorize(Roles = "Administrador")]

        public ActionResult Formulario(int? IdUsuario )
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

            //var identity = HttpContext.User.Identity as ClaimsIdentity;

            //Traer datos si usuario existe
            if (IdUsuario > 0)
            {
                // =================== IMPLEMENTACION DESDE BL ================ \\

                //ML.Result result = _usuario.GetById(IdUsuario.Value);
                //usuario = (ML.Usuario)result.Object;


          

                //var token = HttpContext.Session.GetString("token");
                // ==================== IMPLEMENTACION CON API ===================== \\
                // GetById Utilizando API REST
                ML.Result resultAPIGetId = GeyByIdAPI(IdUsuario.Value);

                if (resultAPIGetId.Correct)
                {
                    usuario = (ML.Usuario)resultAPIGetId.Object;

                }
                else {

                    Models.ErrorViewModel error = new Models.ErrorViewModel();
                    error.ErrorMessage = resultAPIGetId.ErrorMessage;
                    return View("Error", error);
                }


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
        [Authorize(Roles = "Administrador")]

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
                //var token = HttpContext.Session.GetString("token");


                if (usuario.IdUsuario > 0)
                {

                    // ==================== Implementacion desde BL ================= \\
                    //ML.Result resultUpdate = _usuario.Update(usuario.IdUsuario, usuario);


                    /// WEB SERVICE API REST
                    //UpdateByAPI(usuario);
                    // =================== IMPLEMENTACION CON API ==================\\
                    ML.Result resultUpdate = UpdateByAPI(usuario);
                    if (resultUpdate.Correct)
                    {
                        return RedirectToAction("GetAll", "Usuario");
                    }
                    else {

                        if (resultUpdate.ErrorMessage == "Unauthorized") {

                            Models.ErrorViewModel error = new Models.ErrorViewModel();
                            error.ErrorMessage = resultUpdate.ErrorMessage;

                            return View("Error", error);
                        }

                    }
                    

                }
                else
                {
                    // =========== Implementacion con BL ========== \\
                    //ML.Result resultAdd = _usuario.Add(usuario);
                    

                    // ====================== Implementacion con API ============ \\


                    ML.Result resultAdd = AddByAPI(usuario);

                    if (resultAdd.Correct)
                    {
                        return RedirectToAction("GetAll", "Usuario");
                    }
                    else { 
                        
                        if(resultAdd.ErrorMessage == "Unauthorized")
                        {
                            Models.ErrorViewModel error = new Models.ErrorViewModel();
                            error.ErrorMessage = resultAdd.ErrorMessage;

                            return View("Error", error);
                        }
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
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int IdUsuario)
        {
            // Delete desde BL
            //_usuario.Delete(IdUsuario);

            //Delete Usando API REST 

            var token = HttpContext.Session.GetString("token");


            ML.Result result = DeleteByAPI(IdUsuario);
            if (result.Correct)
            {
                return RedirectToAction("GetAll", "Usuario");

            }
            else {

                Models.ErrorViewModel error = new Models.ErrorViewModel();
                error.ErrorMessage = result.ErrorMessage;

                return View("Error", error);
            }


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
                    var token = Request.Cookies["session"];
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
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
        public  ML.Result AddByAPI(ML.Usuario Usuario)
        {
            ML.Result resultAdd = new Result();

            if (Usuario.Imagen != null)
            {
                Usuario.ImagenBase64 = Convert.ToBase64String(Usuario.Imagen);
            }
            else
            {
                Usuario.ImagenBase64 = "";
            }
            Usuario.Imagen = null;

            using (var client = new HttpClient())
            {
                var userEndPoint = _configuration.GetValue<string>("ApiEndPoint");
                client.BaseAddress = new Uri(userEndPoint);
                var token = Request.Cookies["session"];
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                //HTTP POST 
                var postTask = client.PostAsJsonAsync<ML.Usuario>("Add", Usuario); //Serializar 
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //MODAL 
                    //ViewBag.ErrorMessage;
                    resultAdd.Correct = true;
                    resultAdd.Object = result.Content;
                    return resultAdd;
                }
                else {

                    resultAdd.Correct = false;
                    resultAdd.ErrorMessage = result.StatusCode.ToString();
                }
            }
            return resultAdd;
        }
        //[NonAction]
        public ML.Result UpdateByAPI(ML.Usuario Usuario)
        {
            ML.Result resultUpdate = new Result();

            if (Usuario.Imagen != null)
            {
                Usuario.ImagenBase64 = Convert.ToBase64String(Usuario.Imagen);
            }
            else
            {
                Usuario.ImagenBase64 = "";
            }
            Usuario.Imagen = null;


            using (var client = new HttpClient())
            {
                var userEndPoint = _configuration.GetValue<string>("ApiEndPoint"); 
                client.BaseAddress = new Uri(userEndPoint);
                var token = Request.Cookies["session"];
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                //HTTP POST 
                var postTask = client.PutAsJsonAsync<ML.Usuario>($"Update/{Usuario.IdUsuario}", Usuario); //Serializar 
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //MODAL 
                    //ViewBag.ErrorMessage;
                    resultUpdate.Correct = true;
                    resultUpdate.Object = result.Content;
                    return resultUpdate;
                }
                else {

                    resultUpdate.Correct = false;
                    resultUpdate.ErrorMessage = result.StatusCode.ToString();
                
                }
            }
            return resultUpdate;
        }
        //[NonAction]
        public  ML.Result DeleteByAPI(int IdUsuario)
        {

            ML.Result result = new ML.Result();
            using (var client = new HttpClient())
            {
                var userEndPoint = _configuration.GetValue<string>("ApiEndPoint"); ;
                client.BaseAddress = new Uri(userEndPoint);
                var token = Request.Cookies["session"];
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);


                var deleteTask = client.DeleteAsync($"Delete/{IdUsuario}");
                deleteTask.Wait();
                var resultAPI = deleteTask.Result;
                if (resultAPI.IsSuccessStatusCode)
                {
                    var resultTask = resultAPI.Content.ReadAsAsync<ML.Result>();

                    result.Object = resultAPI.Content;
                    result.Correct = true;
                    return result;
                }
                else
                {
                    var resultTask = resultAPI.Content.ReadAsAsync<ML.Result>();

                    result.Correct = false;
                    result.ErrorMessage = resultAPI.StatusCode.ToString();
                    result.Object = resultAPI.Content;
                    return result;
                }

            }

        }

        //[NonAction]
        public  ML.Result GeyByIdAPI(int IdUsuario)
        {

            ML.Result resultGetById = new ML.Result();

            using (var client = new HttpClient())
            {

                var userEndPoint = _configuration.GetValue<string>("ApiEndPoint");
                client.BaseAddress = new Uri(userEndPoint);

                var token = Request.Cookies["session"];

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var getIdTask = client.GetAsync($"GetById/{IdUsuario}");
                getIdTask.Wait();
                var resultAPI = getIdTask.Result;



                if (resultAPI.IsSuccessStatusCode)
                {
                    resultGetById.Correct = true;
                    var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    ML.Usuario newUser = JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());

                    resultGetById.Object = newUser;
                    return resultGetById;
                }
                else
                {
                    resultGetById.Correct = false;
                    resultGetById.Object = resultAPI.Content;
                    resultGetById.ErrorMessage = resultAPI.StatusCode.ToString();
                    return resultGetById;
                }

            }
        }


    }
}
