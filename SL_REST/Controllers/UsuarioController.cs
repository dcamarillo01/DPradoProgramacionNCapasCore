using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SL_REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly BL.Usuario _usuario;
        public UsuarioController(BL.Usuario usuario) 
        {
            _usuario = usuario;
        }


        [HttpPost]
        [Route("Add")]
        [Authorize]
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {

            //usuario.Rol = new ML.Rol();
            //usuario.Direccion = new ML.Direccion();
            //usuario.Direccion.Colonia = new ML.Colonia();

            usuario.Imagen = Convert.FromBase64String(usuario.ImagenBase64);
            if (usuario.Imagen.SequenceEqual(new byte[0]))
            {
                usuario.Imagen = null;
            }
            usuario.ImagenBase64 = null;

            if (ModelState.IsValid)
            {
                ML.Result result = _usuario.Add(usuario);
                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("Update/{IdUsuario}")]
        [Authorize]
        public IActionResult Update(int IdUsuario, [FromBody] ML.Usuario usuario)
        {

            //usuario.Rol = new ML.Rol();
            //usuario.Direccion = new ML.Direccion();
            //usuario.Direccion.Colonia = new ML.Colonia();
            usuario.IdUsuario = IdUsuario;

            usuario.Imagen = Convert.FromBase64String(usuario.ImagenBase64);
            usuario.ImagenBase64 = null;

            if (ModelState.IsValid)
            {
                ML.Result result = _usuario.Update(usuario.IdUsuario, usuario);

                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }

            return BadRequest();

        }

        [HttpDelete]
        [Route("Delete/{IdUsuario}")]
        [Authorize]

        public IActionResult Delete(int IdUsuario)
        {

            ML.Result result = _usuario.Delete(IdUsuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }

        [HttpPost]
        [Route("GetAll")]
        
        public IActionResult GetAll(ML.Usuario? usuario)
        {

            ML.Result result = _usuario.GetAll(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }

        [HttpGet]
        [Route("GetById/{IdUsuario}")]
        

        public IActionResult GetById(int IdUsuario)
        {

            ML.Result result = _usuario.GetById(IdUsuario);

            if (result.Correct)
            {
                 
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }


    }
}
