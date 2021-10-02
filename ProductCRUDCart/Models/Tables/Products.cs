using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ProductCRUDCart.Models.Entities;

namespace ProductCRUDCart.Models.Tables
{
    public class Products
    {
        SqlConnection conn;
        public Products(SqlConnection conn)
        {
            this.conn = conn;
        }
        public void Create(Product p)
        {
            conn.Open();
            string query = String.Format("insert into Products values ('{0}','{1}','{2}',0.0)", p.Name, p.Qty, p.Price);
            SqlCommand cmd = new SqlCommand(query, conn);
            int r = cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<Product> Get()
        {
            conn.Open();
            string query = "select * from Products";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Product> products = new List<Product>();
            while (reader.Read())
            {
                Product p = new Product()
                {

                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Qty = reader.GetInt32(reader.GetOrdinal("Qty")),
                    Price = (float)reader.GetDouble(reader.GetOrdinal("Price"))

                };
                products.Add(p);
            }

            conn.Close();
            return products;
        }
        public Product Get(int id)
        {
            conn.Open();
            string query = String.Format("Select * from Products where Id={0}", id);
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            Product p = null;
            while (reader.Read())
            {
                p = new Product()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Qty = reader.GetInt32(reader.GetOrdinal("Qty")),
                    Price = (float)reader.GetDouble(reader.GetOrdinal("Price"))
                };
            }
            conn.Close();
            return p;
        }

        public void Delete(int id)
        {
            conn.Open();
            string query = String.Format("Delete from Products where Id={0}", id);
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Update(Product p)
        {
            conn.Open();
            string query = String.Format("Update Products Set Name='{0}', Qty='{1}', Price='{2}' where Id={3}", p.Name, p.Qty, p.Price, p.Id);
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Order(Product p)
        {
            conn.Open();
            string query = String.Format("insert into Orders values ('{0}', '{1}', '{2}', '{3}')", p.Id, p.Name, p.Price, DateTime.Now.ToString("hh:mm:ss"));
            SqlCommand cmd = new SqlCommand(query, conn);
            int r = cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}