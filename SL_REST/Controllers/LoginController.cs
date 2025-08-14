using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SL_REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly BL.Login _login;

        public LoginController(BL.Login login) {

            _login = login;
        }



        [HttpPost]
        [Route("LoginUser")]
        public IActionResult LoginUser([FromBody] ML.Login login) {
            
            ML.Result result = _login.LoginUser(login);
            if (result.Correct)
            {
                ML.UserProfile userProfile = new()
                {
                    Rol = new()
                };

                userProfile = (ML.UserProfile) result.Object;
                //JWT
                var token = GenerateJWT(userProfile);

                return Ok(token);
            }
            else { 
                
                return BadRequest(result);
            }
        }



        [NonAction]
        public string GenerateJWT(ML.UserProfile userProfile) 
        {

            var claims = new[] 
            {
                new Claim(ClaimTypes.Role, userProfile.Rol.Nombre),
                new Claim(ClaimTypes.Name, userProfile.UserName),
                new Claim(ClaimTypes.Email, userProfile.Email),
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userProfile.Empleado.IdEmpleado))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("d4c9482eb6bab9aef587ff82afcb000d"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: "yourdomain.com",
                    audience: "yourdomain.com",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
