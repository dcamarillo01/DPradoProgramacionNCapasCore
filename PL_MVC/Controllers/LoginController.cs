using Microsoft.AspNetCore.Mvc;

namespace PL_MVC.Controllers
{
    public class LoginController : Controller
    {
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


            return View();
        }



        ///  Consumo de API Login
       

        public ML.Result LoginApi()
        {

            ML.Result result = new();

            return result;
        }

    }
}
