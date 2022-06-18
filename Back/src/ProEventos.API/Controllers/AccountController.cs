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

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                
            }
            catch (Exception ex)
            { 
                return Task.FromResult(this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar Usuário. Erro: {ex.Message}"));
            }
        }
    }
}