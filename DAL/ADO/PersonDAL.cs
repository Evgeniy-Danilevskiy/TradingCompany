using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DAL.ADO
{
    public class PersonDAL : IPersonDAL
    {
        private string _connStr;

        public PersonDAL(string connStr)
        {
            this._connStr = connStr;
        }

        public PersonDTO CreatPerson(PersonDTO person)
        {
            var personIdentical = GetAllPerson().SingleOrDefault(x => x.Login == person.Login);

            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                if (personIdentical != null)
                {
                    throw new Exception("Already exists!");
                }
                
                conn.Open();
                person.Salt = Guid.NewGuid();
                comm.CommandText = "INSERT into Person (ID_Role,FirstName, LastName,Login,Password, Salt ) output INSERTED.ID_Person values (@idRole,@firstName,@lastName,@log,@passw,@salt)";
                byte[] b = hash(Encoding.UTF8.GetString(person.Password), person.Salt.ToString());

                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@idRole", person.ID_Role);
                comm.Parameters.AddWithValue("@firstName", person.FirstName);
                comm.Parameters.AddWithValue("@lastName", person.LastName);
                comm.Parameters.AddWithValue("@log", person.Login);
                comm.Parameters.AddWithValue("@passw", b);
                comm.Parameters.AddWithValue("@salt", person.Salt);


                person.ID_Person = (int)comm.ExecuteScalar();
                conn.Close();

                return person;

            }
        }

        private byte[] hash(string password, string salt)
        {
            var pass = SHA512.Create();
            byte[] b = pass.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            return b;
        }

        public bool Login(string login, string password)
        {
            foreach(PersonDTO p in GetAllPerson())
            {
                if (p.Password.SequenceEqual(hash(password, p.Salt.ToString())) == true) { return true; }               
            }
            return false;
        }

        public void DeletePerson(int personId)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = "DELETE from Person WHERE ID_Person = @id";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@id", personId);
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }

        public List<PersonDTO> GetAllPerson()
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = "select * from Person";
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                var person = new List<PersonDTO>();
                while (reader.Read())
                {
                    person.Add(new PersonDTO
                    {
                        ID_Person = (int)reader["ID_Person"],
                        ID_Role = (int)reader["ID_Role"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Login = reader["Login"].ToString(),
                        Password = (byte[])reader["Password"],
                        Salt = (Guid)reader["Salt"],
                        RowInsertTime = DateTime.Parse(reader["RowInsertTime"].ToString()),
                        RowUpdateTime = DateTime.Parse(reader["RowUpdateTime"].ToString())
                    });
                }
                conn.Close();
                return person;
            }
        }

        public PersonDTO GetPersonByLogin(string login)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = $"select * from Person where Login = {login}";
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();

                PersonDTO myPerson = new PersonDTO();
                while (reader.Read())
                {
                    myPerson = new PersonDTO
                    {
                        ID_Person = (int)reader["ID_Person"],
                        ID_Role = (int)reader["ID_Role"],
                        Login = reader["Login"].ToString(),
                    };
                }
                return myPerson;
            }
        }

        public PersonDTO GetPersonById(int personId)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = $"select * from Person where ID_Person = {personId}";
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();

                PersonDTO myPerson = new PersonDTO();
                while (reader.Read())
                {
                    myPerson = new PersonDTO
                    {
                        ID_Person = (int)reader["ID_Person"],
                    };
                }
                return myPerson;
            }
        }

        public PersonDTO UpdatePerson(PersonDTO p, int id)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = $"UPDATE Person set ID_Role = @id_rol, FirstName = @firsn, LastName = @lastn, Login = @log ,Password = @passw, RowUpdateTime = @rowup WHERE ID_Person = {id} "; 

                comm.Parameters.Clear();

                comm.Parameters.AddWithValue("@id_rol", p.ID_Role);
                comm.Parameters.AddWithValue("@firsn", p.FirstName);
                comm.Parameters.AddWithValue("@lastn", p.LastName);
                comm.Parameters.AddWithValue("@log", p.Login);
                comm.Parameters.AddWithValue("@passw", p.Password);
                comm.Parameters.AddWithValue("@rowup", p.RowUpdateTime);

                conn.Open();

                p.ID_Person = comm.ExecuteNonQuery();
                conn.Close();
                return p;
            }
        }
    }
}
