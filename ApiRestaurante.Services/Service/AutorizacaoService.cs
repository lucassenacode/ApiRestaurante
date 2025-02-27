using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Domain.Models.Enuns;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApiRestaurante.Services.Service
{
    public class AutorizacaoService : IAutorizacaoService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _config;

        public AutorizacaoService(IUsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _config = configuration;
        }

        public Token Login(Usuario model)
        {
            var usuario = _usuarioService.ObterUsuarioPorCredenciais(model.Email, model.Senha, true);

            if (usuario is null)
            {
                throw new InvalidOperationException("Usuário ou senha inválidos");
            }

            var senhaJwt = Encoding.ASCII.GetBytes(_config["Jwt:SenhaJWT"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim ("Email", usuario.Email),
                        new Claim (ClaimTypes.Role, usuario.Perfil.GetHashCode().ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(senhaJwt),
                SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);

            return new Token()
            {
                Bearer = stringToken,
                Validade = tokenDescriptor.Expires.Value,
                NivelAcesso = usuario.Perfil.GetHashCode(),
                NomeUsuario = usuario.Nome
            };
        }
    }

}
