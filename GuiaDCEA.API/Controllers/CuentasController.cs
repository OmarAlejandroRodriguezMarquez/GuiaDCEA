using GuiaDCEA.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace GuiaDCEA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public CuentasController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<TokenUsuario>> CrearUsuario([FromBody] DatosUsuario datosUsuario)
        {
            var user = new IdentityUser { Email = datosUsuario.Email, UserName= datosUsuario.Email };

            var resultado = await userManager.CreateAsync(user,datosUsuario.Password);

            if(resultado.Succeeded) 
            {
                return GenerarToken(datosUsuario);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenUsuario>> Login([FromBody] DatosUsuario datosUsuario)
        {
            var result = await signInManager.PasswordSignInAsync(datosUsuario.Email, datosUsuario.Password, isPersistent:false, lockoutOnFailure:false);

            if (result.Succeeded)
            {
                return GenerarToken(datosUsuario);
            }
            else
            {
                return BadRequest();
            }
        }

        private TokenUsuario GenerarToken(DatosUsuario datosUsuario)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, datosUsuario.Email),
                new Claim("ColorFavorito","Verde"),
                new Claim("Genero","Masculino")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtkey"]!));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddDays(7);

            var token = new JwtSecurityToken(
                issuer:null,
                audience:null,
                claims:claims,
                expires:expiracion,
                signingCredentials:credential
                );

            return new TokenUsuario
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracion = expiracion,
            };
        }
    }
}
