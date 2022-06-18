using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accontService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accontService,
                                ITokenService tokenService)
        {
            _accontService = accontService;
            _tokenService = tokenService;
        }

        [HttpGet("GetUser/{userName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser(string userName)
        {
            try
            {
                var user = await _accontService.GetUserByUserNameAsync(userName);
                return Ok(user);
            }
            catch (Exception ex)
            { 
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar Usuário. Erro: {ex.Message}");
            }
        }
    }
}