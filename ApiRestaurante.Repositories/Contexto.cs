using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ApiRestaurante.Repositories.Repository
{
    public class Contexto
    {
        internal readonly MySqlConnection _conn;

        public Contexto()
        {
            // String de conexão com o banco de dados
            string connectionString = "Server=sql10.freesqldatabase.com;Database=sql10763453;User Id=sql10763453;Password=gJ17hUSBpv;Port=3306;";
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
    }
}