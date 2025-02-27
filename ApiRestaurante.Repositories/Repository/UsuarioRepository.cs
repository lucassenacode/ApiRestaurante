using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Domain.Models.Enuns;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ApiRestaurante.Repositories.Repository
{
    public class UsuarioRepository : Contexto, IUsuarioRepository
    {
        public UsuarioRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Usuario ObterUsuarioPorCredenciais(string email, string senha)
        {


            string comandoSql = @"SELECT  u.Email, u.Nome, u.IdPerfil FROM Usuario u  
                                join Perfil p on u.IdPerfil = p.IdPerfil 
                                WHERE u.Email = @email and u.Senha = @senha";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@senha", senha);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new Usuario()
                        {
                            Email = rdr["Email"].ToString(),
                            Nome = rdr["Nome"].ToString(),
                            Perfil = (PerfilUsuario)Convert.ToInt32(rdr["IdPerfil"])
                        };
                    }
                    else
                    {
                        Console.WriteLine("Usuário não encontrado!");
                        return null;
                    }
                }
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

                return hashedInputStringBuilder.ToString();
            }
        }

    }
}
