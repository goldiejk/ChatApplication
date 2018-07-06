using System;
using System.Data.SqlClient;
using ChatServer.SQLQueries;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection _Connection = null;
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\DEV\ChatApp\WpfChat\WpfChat\DBChat.mdf;Integrated Security=True";

            _Connection = new SqlConnection(ConnectionString);

            try
            {
                _Connection.Open();
                Console.WriteLine("Pripojeni do DB OK!!!");
            }
            catch
            {
                Console.WriteLine("Nepodarilo se pripojeni do DB!!!");
            }

            bool check = SQLUser.CheckEmail("ahoj@ahoj.com", _Connection);
            Console.WriteLine("check: " + check);
            if (!check)
            {
                bool register = SQLUser.RegisterUser("ahoj@ahoj.com", "123456789", _Connection);
                Console.WriteLine("register: " + register);
            }

            Console.ReadLine();
        }
    }
}
