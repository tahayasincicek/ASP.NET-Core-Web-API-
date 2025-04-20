using System.Data.SqlClient;
using System.Data;
using UrunAPI.Models;

namespace UrunAPI.Data
{
    public class DbHelper
    {
        private readonly string _connectionString;

        public DbHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Products", conn);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString()!,
                    Price = (decimal)reader["Price"],
                    Stock = (int)reader["Stock"]
                });
            }

            return products;
        }

        public Product? GetProductById(int id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE Id = @Id", conn);

            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();

            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Product
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString()!,
                    Price = (decimal)reader["Price"],
                    Stock = (int)reader["Stock"]
                };
            }

            return null;
        }

        public void AddProduct(Product product)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("INSERT INTO Products (Name, Price, Stock) VALUES (@Name, @Price, @Stock)", conn);

            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@Stock", product.Stock);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdateProduct(int id, Product product)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("UPDATE Products SET Name = @Name, Price = @Price, Stock = @Stock WHERE Id = @Id", conn);

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@Stock", product.Stock);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteProduct(int id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE Id = @Id", conn);

            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
