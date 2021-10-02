using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ProductCRUDCart.Models.Tables;

namespace ProductCRUDCart.Models
{
    public class Database
    {
        SqlConnection conn;
        public Products Products { get; set; }
        public Database()
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Database.mdf;Integrated Security=True;Connect Timeout=30";
            conn = new SqlConnection(connString);

            Products = new Products(conn);
        }

    }
}