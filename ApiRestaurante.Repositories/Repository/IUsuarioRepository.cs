using ApiRestaurante.Domain.Models;
using MySql.Data.MySqlClient;

namespace ApiRestaurante.Repositories.Repository
{
    public interface IUsuarioRepository
    {
        Usuario ObterUsuarioPorCredenciais(string email, string senha);
    }
}