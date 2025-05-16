using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest_Ev4.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest_Ev4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        
        [HttpGet]
        public async Task<IActionResult>  GenerateToken()
        {
            var token = await _tokenService.GenerateToken();
            if (token != null)
            {
                return Ok(token);
            }
            else
            {
                return BadRequest("Error generando el token");
            }
        }
    }
}