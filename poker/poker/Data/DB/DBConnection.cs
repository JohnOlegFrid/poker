using MySql.Data;
using MySql.Data.MySqlClient;
using System;

namespace poker.Data.DB
{
    public class DBConnection
    {
        private MySqlConnection connection = null;
        private string database;
        private string username;
        private string password;
        private string server;

        public DBConnection(string server ,string database, string username, string password)
        {
            this.server = server;
            this.database = database;
            this.username = username;
            this.password = password;
        }

        public MySqlConnection Connection
        {
            get { return connection; }
        }

        public void Connect()
        {
            if (Connection == null)
            {
                string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}",server,database,username,password);
                connection = new MySqlConnection(connstring);
                connection.Open();
            }
        }

        public MySqlConnection OpenMoreConnection()
        {
            MySqlConnection connection;
            string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", server, database, username, password);
            connection = new MySqlConnection(connstring);
            connection.Open();
            return connection;
        }

            

        public void Close()
        {
            connection.Close();
        }
    }
}

