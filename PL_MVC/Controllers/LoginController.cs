using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL_MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;


        public LoginController(IConfiguration configuration) { 
            
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() {

            ML.Login login = new();
            return View(login);
        }

        // TOKEN CON SESSIONES
        //[HttpPost]
        //[AllowAnonymous]
        //public IActionResult Login(ML.Login Login) {


        //    var token = LoginApi(Login);

        //    if (token != "")
        //    {
        //        HttpContext.Session.SetString("token", token);

        //        return RedirectToAction("GetAll", "Usuario");
        //    }
        //    else
        //    {
        //        return View(Login);
        //    }

        //}

        //TOKEN EN COOKIES

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(ML.Login Login)
        {


            var token = LoginApi(Login);

            using (var client = new HttpClient())
            {

                var loginEndPoint = _configuration.GetValue<string>("ApiLoginEndPoint");
                client.BaseAddress = new Uri(loginEndPoint);

                var loginTask = client.PostAsJsonAsync("LoginUser", Login);
                loginTask.Wait();

                var resultLogin = loginTask.Result;

                if (resultLogin.IsSuccessStatusCode)
                {

                    HttpContext.Session.Remove("WrongCredentiasl");


                    HttpContext.Response.Cookies.Append("session", token, new Microsoft.AspNetCore.Http.CookieOptions
                    { Expires = DateTime.Now.AddMinutes(30) }
                        );

                    return RedirectToAction("Index", "Home");

                }
                else
                {

                    HttpContext.Session.SetString("WrongCredentiasl", "La contraseña o correo son incorrectos");

                    return View();
                }

            }


        }

        ///  Consumo de API Login


        public string LoginApi(ML.Login Login)
        {
            ML.Result result = new();

            using(var client = new HttpClient()) {

                var loginEndPoint = _configuration.GetValue<string>("ApiLoginEndPoint");
                client.BaseAddress = new Uri(loginEndPoint);

                var loginTask = client.PostAsJsonAsync("LoginUser", Login);
                loginTask.Wait();

                var resultLogin = loginTask.Result;

                if (resultLogin.IsSuccessStatusCode)
                {

                   
                    return resultLogin.Content.ReadAsStringAsync().Result.ToString();

                }
                //else
                //{
                //    return loginTask.Result.Content.ReadAsStringAsync().Result.ToString();
                //}

            }

            return string.Empty;
        }





    }
}
