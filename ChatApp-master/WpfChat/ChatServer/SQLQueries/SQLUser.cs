using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;

namespace ChatServer.SQLQueries
{
    public static class SQLUser
    {
        public static bool CheckEmail(string pEmail, SqlConnection pSqlConnection)
        {
            string query = "select Email from UserLogin where Email = @email";
            SqlCommand cmd = new SqlCommand(query, pSqlConnection);
            cmd.Parameters.AddWithValue("@email", pEmail);

            SqlDataReader dataReader = cmd.ExecuteReader();
            bool hasRows = dataReader.HasRows;

            dataReader.Dispose();

            return hasRows;
        }

        public static bool RegisterUser(string pEmail, string pPassword, SqlConnection pSqlConnection)
        {
            string passHash = MD5Hash(pPassword);
            string query = @"insert into UserLogin (Email, Password, Created, LastAccess, UserState) 
            values (@email, @pass, GETDATE(), GETDATE(), @userstate)";
            SqlCommand cmd = new SqlCommand(query, pSqlConnection);
            cmd.Parameters.AddWithValue("@email", pEmail);
            cmd.Parameters.AddWithValue("@pass", passHash);
            cmd.Parameters.AddWithValue("@userstate", 1);

            int rows = cmd.ExecuteNonQuery();
            return rows == 1;
        }

        public static long LoginUser(string pEmail, string pPassword, SqlConnection pConnection)
        {
            string passHash = MD5Hash(pPassword);
            string query = "select IDUser from UserLogin where Email = @email and Password = @pass";
            SqlCommand cmd = new SqlCommand(query, pConnection);
            cmd.Parameters.AddWithValue("@email", pEmail);
            cmd.Parameters.AddWithValue("@pass", passHash);

            long userID = -1;
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                dataReader.Read();
                userID = dataReader.GetInt64(0);
            }
            dataReader.Dispose();

            if (userID != -1)
            {
                UpdateUserState(userID, 1, pConnection);
            }

            return userID;
        }

        public static bool UpdateUserState(long pUserID, int pUserState, SqlConnection pConnection)
        {
            string query = "update UserLogin set LastAccess = GETDATE(), UserState = @state where IDUser = @id";
            SqlCommand cmd = new SqlCommand(query, pConnection);
            cmd.Parameters.AddWithValue("@id", pUserID);
            cmd.Parameters.AddWithValue("@state", pUserState);

            int rows = cmd.ExecuteNonQuery();

            return rows == 1;
        }

        public static bool LogoutUser(long pUserID, SqlConnection pConnection)
        {
            return UpdateUserState(pUserID, 2, pConnection);
        }

        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
    }
}
