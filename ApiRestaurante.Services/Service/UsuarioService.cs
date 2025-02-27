
using System.Text;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Repositories.Repository;

namespace ApiRestaurante.Services.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repositorio;

        public UsuarioService(IUsuarioRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public Usuario? ObterUsuarioPorCredenciais(string email, string senha, bool isDescriptografado)
        {
            try
            {
                ((Contexto)_repositorio).AbrirConexao();
                if (isDescriptografado)
                {
                    senha = CriptografarSha512(senha);
                }

                return _repositorio.ObterUsuarioPorCredenciais(email, senha);
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }

        private static string CriptografarSha512(string texto)
        {
            var bytes = Encoding.UTF8.GetBytes(texto);

            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new StringBuilder(128);

                foreach (var b in hashedInputBytes)
                {
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                }

                return hashedInputStringBuilder.ToString().ToLower();
            }
        }
    }
}