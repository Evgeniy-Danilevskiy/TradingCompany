using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL.ADO
{
    public class RolesDAL : IRolesDAL
    {
        private string _connStr;

        public RolesDAL(string connStr)
        {
            this._connStr = connStr;
        }

        public RolesDTO CreatRoles(RolesDTO role)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "INSERT into Roles (RoleName) output INSERTED.ID_Role values (@roleName)";

                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@roleName", role.RoleName);

                role.ID_Role = (int)comm.ExecuteScalar();
                conn.Close();

                return role;

            }
        }

        public void DeleteRoles(int rolesId)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = "DELETE from Roles WHERE ID_Role = @id";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@id", rolesId);
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }

        public List<RolesDTO> GetAllRoles()
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = "select * from Roles";
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                var roles = new List<RolesDTO>();
                while (reader.Read())
                {
                    roles.Add(new RolesDTO
                    {
                        ID_Role = (int)reader["ID_Role"],
                        RoleName = reader["RoleName"].ToString(),
                        RowInsertTime = DateTime.Parse(reader["RowInsertTime"].ToString()),
                    });
                }
                conn.Close();
                return roles;
            }
        }

        public RolesDTO GetRolesById(int rolesId)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = $"select * from Roles where ID_Role = {rolesId}";
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();

                RolesDTO myRoles = new RolesDTO();
                while (reader.Read())
                {
                    myRoles = new RolesDTO
                    {
                        ID_Role = (int)reader["ID_Role"],

                    };
                }
                return myRoles;
            }
        }

        public RolesDTO UpdateRoles(RolesDTO roles, int id)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = $"UPDATE Roles set  RoleName = @rol, RowUpdateTime = @rowup WHERE ID_Role = {id} ";

                comm.Parameters.Clear();

                comm.Parameters.AddWithValue("@rol", roles.RoleName);
                comm.Parameters.AddWithValue("@rowup", roles.RowUpdateTime);


                conn.Open();

                roles.ID_Role = comm.ExecuteNonQuery();
                conn.Close();
                return roles;
            }
        }
    }
}
