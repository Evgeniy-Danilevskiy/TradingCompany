using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace DAL.ADO
{
    public class CategoryDAL : ICategoryDAL
    {
        private string _connStr;

        public CategoryDAL(string connStr)
        {
            this._connStr = connStr;
        }

        public CategoryDTO CreateCategory(CategoryDTO category)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "INSERT into Category (CategoryName) output INSERTED.ID_Category values (@categoryName)";

                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@categoryName", category.CategoryName);

                category.ID_Category = (int)comm.ExecuteScalar();
                conn.Close();

                return category;

            }
        }

        public void DeleteCategory(int сategoryId)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = "DELETE from Category WHERE ID_Category = @id";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@id", сategoryId);
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }

        public List<CategoryDTO> GetAllCategory()
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = "select * from Category";
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                var roles = new List<CategoryDTO>();
                while (reader.Read())
                {
                    roles.Add(new CategoryDTO
                    {
                        ID_Category = (int)reader["ID_Category"],
                        CategoryName = reader["CategoryName"].ToString(),
                        RowInsertTime = DateTime.Parse(reader["RowInsertTime"].ToString()),
                        RowUpdateTime = DateTime.Parse(reader["RowUpdateTime"].ToString())
                    });
                }
                conn.Close();
                return roles;
            }
        }

        public CategoryDTO GetCategoryById(int сategoryId)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = $"select * from Category where ID_Category = {сategoryId}";
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();

                CategoryDTO myCategory = new CategoryDTO();
                while (reader.Read())
                {
                    myCategory = new CategoryDTO
                    {
                        ID_Category = (int)reader["ID_Category"],
                        
                    };
                }
                return myCategory;
            }
        }

        public CategoryDTO UpdateCategory(CategoryDTO сategory,int id)
        {
     
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = $"UPDATE Category set  CategoryName = @cat, RowUpdateTime = @rowup WHERE ID_Category = {id} ";

                comm.Parameters.Clear();

                comm.Parameters.AddWithValue("@cat", сategory.CategoryName);
                comm.Parameters.AddWithValue("@rowup", сategory.RowUpdateTime);


                conn.Open();

                сategory.ID_Category = comm.ExecuteNonQuery();  
                conn.Close();
                return сategory;
            }
        }



    }
}
