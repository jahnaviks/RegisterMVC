using RegisterMVC.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RegisterMVC.DataAccess
{
    public class RegisterMapper
    {
        public string Register(string email, string fname, string lname, string address, string mobile, string password)
        {
            string pswd = null;
            int tempId = 0;
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RegisterDBEntities"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_getpassword", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", email);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                pswd = dr.GetValue(0).ToString();
            }
            dr.Close();
            con.Close();
            if (pswd == null)
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("sp_getId", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                tempId = Convert.ToInt32(cmd1.ExecuteScalar());
                tempId++;
                con.Close();

                var command = new SqlCommand("sp_RegisterQueries", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", tempId);
                command.Parameters.AddWithValue("@FirstName", fname);
                command.Parameters.AddWithValue("@LastName", lname);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@MobileNumber", mobile);
                command.Parameters.AddWithValue("@Password", password);
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
            return pswd;
        }

        public string Update(string email, string password)
        {
            string pswd = null;
            Registration reg = new Registration();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RegisterDBEntities"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_getpassword", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", email);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                pswd = dr.GetValue(0).ToString();
            }
            dr.Close();
            con.Close();

            if (pswd == password)
            {
                return "success";
            }
            else
            {
                return "failed";
            }
            
        }

        public Registration GetUserDetails(string email)
        {
            Registration reg = new Registration();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RegisterDBEntities"].ConnectionString);
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select * from RegisterMVC where EmailId ='" + email + "'", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                reg.FirstName = dr1.GetValue(1).ToString();
                reg.LastName = dr1.GetValue(2).ToString();
                reg.Email = dr1.GetValue(4).ToString();
                reg.PhoneNumber = dr1.GetValue(5).ToString();
                reg.Address = dr1.GetValue(3).ToString();
                reg.Password = dr1.GetValue(6).ToString();
            };
            con.Close();
            return reg;
        }

        public void UpdateClick(string email, string fname, string lname, string address, string mobile, string password)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RegisterDBEntities"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", fname);
            cmd.Parameters.AddWithValue("@LastName", lname);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@MobileNumber", mobile);
            cmd.Parameters.AddWithValue("@Password", password);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteClick(string email)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RegisterDBEntities"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_deleteRow", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", email);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}