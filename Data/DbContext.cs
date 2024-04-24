using MySql.Data.MySqlClient;
using System.Data;

namespace Data
{
    public class DbContext
    {
        public DbContext()
        {

        }

        // Change this with your database credentials
        private static string ConnectionStrings = "Server=localhost;Port=3306;Database=Patient;Uid=root;Pwd=12341234;";

        private MySqlConnection conn = new MySqlConnection(ConnectionStrings);

        public ConnectionState GetConnectionStatus { get { return conn.State; } }

        public void OpenConnection() { conn.Open(); }
        public void CloseConnection() { conn.Close(); }
        public MySqlConnection GetConnection { get { return conn; } }
    }
}
