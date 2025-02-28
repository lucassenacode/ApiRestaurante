using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Domain.Models.Dto;
using ApiRestaurante.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ApiRestaurante.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class AutorizacaoController : ControllerBase
    {
        private readonly AutorizacaoService _service;
        public AutorizacaoController(AutorizacaoService service)
        {
            _service = service;
        }

        [HttpPost("restaurante/login")]
        public IActionResult Login(UsuarioDto model)
        {
            try
            {
                var usuario = new Usuario
                {
                    Email = model.Email,
                    Senha = model.Senha
                };
                var token = _service.Login(usuario);
                return StatusCode(200, token);
            }
            catch (Exception)
            {
                return StatusCode(401);
            }
        }
    }
}