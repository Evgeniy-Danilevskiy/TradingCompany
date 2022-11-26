using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL.ADO
{
    public class SupplyDAL : ISupplyDAL
    {
        private string _connStr;

        public SupplyDAL(string connStr)
        {
            this._connStr = connStr;
        }

        public SupplyDTO CreatSupply(SupplyDTO supply)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "INSERT into Supply (ID_Person,ID_Category,NameGoods,Number,PriceUnit) output INSERTED.ID_Supply values (@idPerson,@idCateg,@nameGods,@numb,@priceUn)";

                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@idPerson", supply.ID_Person);
                comm.Parameters.AddWithValue("@idCateg", supply.ID_Category);
                comm.Parameters.AddWithValue("@nameGods", supply.NameGoods);
                comm.Parameters.AddWithValue("@numb", supply.Number);
                comm.Parameters.AddWithValue("@priceUn", supply.PriceUnit);


                supply.ID_Supply = (int)comm.ExecuteScalar();
                conn.Close();

                return supply;
            }
        }

        public void DeleteSupply(int supplyId)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "DELETE from Supply WHERE ID_Supply = @id";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@id", supplyId);
                comm.ExecuteNonQuery();
            }
        }

        public List<SupplyDTO> GetAllSupply()
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = "select * from Supply";
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                var supply = new List<SupplyDTO>();
                while (reader.Read())
                {
                    supply.Add(new SupplyDTO
                    {
                        ID_Supply = (int)reader["ID_Supply"],
                        ID_Person = (int)reader["ID_Person"],
                        ID_Category = (int)reader["ID_Category"],
                        NameGoods = reader["NameGoods"].ToString(),
                        Number = (int)reader["Number"],
                        PriceUnit = (int)reader["PriceUnit"],
                        RowInsertTime = DateTime.Parse(reader["RowInsertTime"].ToString()),
                        RowUpdateTime = DateTime.Parse(reader["RowUpdateTime"].ToString())
                    }); 
                }
                conn.Close();
                return supply;
            }
        }

        public SupplyDTO GetSupplyById(int supplyId)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = $"select * from Supply where ID_Supply = {supplyId}";
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();

               SupplyDTO mySupply = new SupplyDTO();
                while (reader.Read())
                {
                    mySupply = new SupplyDTO
                    {
                        ID_Supply = (int)reader["ID_Supply"],
                    };
                }
                return mySupply;
            }
        }

        public List<SupplyDTO> GetSupplyByIdCategory(int CatId)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = $"SELECT * FROM Supply WHERE ID_Category = {CatId}";
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();

                var mySupply = new List<SupplyDTO>();
                while (reader.Read())
                {
                    mySupply.Add(new SupplyDTO
                    {
                        ID_Supply = (int)reader["ID_Supply"],
                        ID_Person = (int)reader["ID_Person"],
                        ID_Category = (int)reader["ID_Category"],
                        NameGoods = (string)reader["NameGoods"],
                        Number = (int)reader["Number"],
                        PriceUnit = (int)reader["PriceUnit"],
                        RowInsertTime=(DateTime)reader["RowInsertTime"]
                    });
                }
                return mySupply;
            }
        }

        public SupplyDTO UpdateSupply(SupplyDTO s, int id)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = $"UPDATE Supply set ID_Person = @id_per, ID_Category = @id_cat, NameGoods = @nameg, Number = @num ,PriceUnit = @pu, RowUpdateTime = @rowup WHERE ID_Sypply = {id} "; 

                comm.Parameters.Clear();

                comm.Parameters.AddWithValue("@id_per", s.ID_Person);
                comm.Parameters.AddWithValue("@id_cat", s.ID_Category);
                comm.Parameters.AddWithValue("@nameg", s.NameGoods);
                comm.Parameters.AddWithValue("@num", s.Number);
                comm.Parameters.AddWithValue("@pu", s.PriceUnit);
                comm.Parameters.AddWithValue("@rowup", s.RowUpdateTime);



                conn.Open();

                s.ID_Supply = comm.ExecuteNonQuery();
                conn.Close();
                return s;
            }
        }
       

    }
}
