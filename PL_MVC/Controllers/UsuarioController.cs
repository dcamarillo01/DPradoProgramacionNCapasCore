using Microsoft.AspNetCore.Mvc;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly BL.Usuario _usuario;
        public UsuarioController(BL.Usuario usuario) { 
            
            _usuario = usuario;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
