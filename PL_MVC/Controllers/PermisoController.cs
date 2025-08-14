using BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Claims;

namespace PL_MVC.Controllers
{
    public class PermisoController : Controller
    {

        private readonly BL.Permiso _permiso;
        private readonly BL.HistorialPermiso _permisoHistorial;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PermisoController(BL.Permiso permiso, BL.HistorialPermiso permisoHistorial, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {

            _permiso = permiso;
            _permisoHistorial = permisoHistorial;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles ="Administrador, JefeDirecto")]
        public IActionResult PermisoGetAll() {

            ML.Permiso permiso = new();
            permiso.Empleado = new();
            permiso.EmpleadoAutorizador = new();
            permiso.Permisos = new List<object>();

            ML.Result resultGetPermiso = _permiso.GetAll();

            if (resultGetPermiso.Correct) {

                permiso.Permisos = resultGetPermiso.Objects;
            }



            return View(permiso);
        }


        [HttpGet]
        public IActionResult PermisoForm() { 
            
            ML.Permiso permiso = new ML.Permiso();
            permiso.Empleado = new ML.Empleado();
            permiso.EmpleadoAutorizador = new ML.Empleado();

            ML.Result resultBoss = _permiso.GetBoss();
            if (resultBoss.Correct) {

                permiso.EmpleadoAutorizador.Empleados = resultBoss.Objects;
            }

            return View(permiso);
        }

        [HttpPost]
        public IActionResult PermisoForm(ML.Permiso permiso) {

            var userNameClaim = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            permiso.Empleado.IdEmpleado = Convert.ToInt32(userNameClaim);

            var FechaPermiso = permiso.FechaInicio.Split("to");
            permiso.FechaInicio = FechaPermiso[0];
            permiso.FechaFin = FechaPermiso[1];

            

            permiso.StatusPermiso = new ML.StatusPermiso();

            if (permiso.IdPermiso == 0) {

                permiso.StatusPermiso.IdStatusPermiso = 1;
            }

            ML.Result resultPermiso = _permiso.Add(permiso);

            if (resultPermiso.Correct)
            {

                return RedirectToAction("Index","Home");
            }


            return View(permiso);
        }



        //public ActionResult HistorialPermiso(ML.HistorialPermiso)
        //{
        //    var model = repository.GetThingByParameter(parameter1);
        //    var partialViewModel = new PartialViewModel(model);
        //    return PartialView(partialViewModel);
        //}

        public JsonResult HistorialPermiso(ML.HistorialPermiso historial) {

            historial.AprovoRechazo = new();

            var userNameClaim = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            historial.AprovoRechazo.IdEmpleado = Convert.ToInt32(userNameClaim);

            ML.Result result = _permisoHistorial.AprobarRechazarSolicitud(historial);

            ML.Result resultEmailTo = _permisoHistorial.GetEmailByIdPermiso(historial.Permiso.IdPermiso);

            if (resultEmailTo.Correct) {

                enviarCorreoSolicitud(resultEmailTo.Object.ToString(), historial);
            }


            return Json(result);
        }

        [HttpGet]
        public IActionResult HistorialGetAll() {

            return View();
        }

        [HttpPost]
        public JsonResult HistorialGetAllJson(ML.HistorialPermiso historial) {

            historial.StatusPermiso.Descripcion = historial.StatusPermiso.Descripcion ?? "";
            historial.AprovoRechazo.Nombre = historial.AprovoRechazo.Nombre ?? "";

            ML.Result result = _permisoHistorial.GetAll(historial);

            return Json(result);
        }


        [NonAction]
        public IActionResult enviarCorreoSolicitud(string EmailTo, ML.HistorialPermiso historial) {

            try {

                var Email = _configuration.GetValue<string>("EmailSMTP");
                var Password = _configuration.GetValue<string>("AppSMTPPassword");

                string body = "";

                string webRootPath = _webHostEnvironment.WebRootPath;
                string contentRootPath = _webHostEnvironment.ContentRootPath;

                var AprovoRechazo = historial.StatusPermiso.IdStatusPermiso == 2 ? "Aprovada" : "Rechazada";

                string pathImg = "";

                if (AprovoRechazo == "Aprovada")
                {
                    pathImg = Path.Combine(webRootPath, "Imagenes\\Approved2.png");

                }
                else { 
                pathImg = Path.Combine(webRootPath, "Imagenes\\Rejected.png");

                }


                //string html = @"<html><body><img src=""cid:YourPictureId""></body></html>";







                string path = "";
                path = Path.Combine(webRootPath, "EmailTemplates\\Clock.html");

                StreamReader reader = new StreamReader(path);

                

                body = reader.ReadToEnd();
                //body = body.Replace("{{Nombre}}",EmailTo);
                body = body.Replace("{{AprovoRechazo}}", AprovoRechazo);
                body = body.Replace("{{Observaciones}}", historial.Observaciones);
                body = body.Replace("{{UrlAction}}" , Url.Action("Index","Home"));
                //body = body.Replace("{{imgStatus}}", pathImg);

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Email,Password),
                    EnableSsl = true
                };


                var message = new MailMessage
                {
                    From = new MailAddress(Email, "FakeCompany"),
                    Subject = "Solicitud de Ausencia",
                    Body = body,
                    IsBodyHtml = true,
                };

                AlternateView altView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                LinkedResource yourPictureRes = new LinkedResource(pathImg, MediaTypeNames.Image.Jpeg);
                yourPictureRes.ContentId = "YourPictureId";
                altView.LinkedResources.Add(yourPictureRes);


                message.AlternateViews.Add(altView);
                message.To.Add(Email);
                smtpClient.Send(message);


            }
            catch (Exception ex) { 
            
            }

            return Empty;
        }

    }
}
