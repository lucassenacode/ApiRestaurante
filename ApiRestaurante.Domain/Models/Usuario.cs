using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRestaurante.Domain.Models.Enuns;

namespace ApiRestaurante.Domain.Models
{
    public class Usuario
    {

        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public PerfilUsuario Perfil { get; set; }
    }
}