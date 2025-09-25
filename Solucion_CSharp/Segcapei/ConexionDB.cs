using MySqlConnector; // Si instalaste MySqlConnector
// using MySql.Data.MySqlClient; // Si instalaste MySql.Data

namespace Segcapei
{
    /// <summary>
    /// Clase estática para manejar la conexión a MariaDB/MySQL.
    /// </summary>
    public static class ConexionDB
    {
        // 🔹 Ajusta estos valores con los de tu servidor
        private const string Server = "10.100.83.161"; // IP o nombre del servidor
        private const int Port = 3306;           // Puerto (3306 por defecto)
        private const string Database = "segcapei";
        private const string User = "admin";
        private const string Password = "segcapei";

        public static string ConnectionString =>
            $"Server={Server};Port={Port};Database={Database};User Id={User};Password={Password};SslMode=Preferred;";

        /// <summary>
        /// Devuelve una conexión abierta.
        /// Recuerda usar "using" cuando la uses en tus consultas.
        /// </summary>
        public static MySqlConnection GetOpenConnection()
        {
            var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
