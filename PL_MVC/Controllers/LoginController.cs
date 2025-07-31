using Azure;
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
        public IActionResult Login() {

            ML.Login login = new();
            return View(login);
        }

        [HttpPost]
        public IActionResult Login(ML.Login Login) {


            var token = LoginApi(Login);

            if (token != "")
            {
                HttpContext.Session.SetString("token", token);

                return RedirectToAction("GetAll", "Usuario");
            }
            else { 
                return View(Login);
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
                //else {
                //    return loginTask.Result.Content.ErrorMessage.ToString();
                //}

            }

            return string.Empty;
        }

    }
}
