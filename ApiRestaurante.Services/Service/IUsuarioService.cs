using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRestaurante.Domain.Models;

namespace ApiRestaurante.Services.Service
{
    public interface IUsuarioService
    {
        Usuario? ObterUsuarioPorCredenciais(string email, string senha, bool isDescriptografado);
    }
}