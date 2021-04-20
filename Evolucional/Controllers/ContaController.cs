using Evolucional.Data;
using Evolucional.Models;
using Evolucional.Models.Dto;
using Evolucional.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolucional.Controllers
{
    [ApiController]
    [Route("v1/conta")]
    public class ContaController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Autenticar([FromServices] EvolucionalContext context, [FromBody] UsuarioDto modelo)
        {
            var usuario = await context.Usuarios.Where(_ => _.Nome == modelo.Nome && _.Password == modelo.Password).FirstOrDefaultAsync();

            if (usuario == null)
                return NotFound(new { mensagem = "Usuário ou senha inválidos" });

            var token = TokenService.GerarToken(usuario);
            usuario.Password = "";
            return new
            {
                usuario = usuario,
                token = token
            };
        }
    }
}
