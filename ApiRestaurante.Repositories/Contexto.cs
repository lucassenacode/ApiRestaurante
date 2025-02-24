using System;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ApiRestaurante.Repositories.Repository
{
    public class Contexto : IDisposable
    {
        internal readonly MySqlConnection _conn;
        private readonly IConfiguration _configuration;

        public Contexto(IConfiguration configuration)
        {
            _configuration = configuration;
            string connectionString = _configuration.GetConnectionString("MySqlConnection");
            _conn = new MySqlConnection(connectionString);
        }

        public void AbrirConexao()
        {
            if (_conn.State == System.Data.ConnectionState.Closed)
            {
                _conn.Open();
            }
        }

        public void FecharConexao()
        {
            if (_conn.State == System.Data.ConnectionState.Open)
            {
                _conn.Close();
            }
        }

        public void Dispose()
        {
            if (_conn != null)
            {
                if (_conn.State == System.Data.ConnectionState.Open)
                {
                    _conn.Close();
                }
                _conn.Dispose();
            }
        }
    }
}