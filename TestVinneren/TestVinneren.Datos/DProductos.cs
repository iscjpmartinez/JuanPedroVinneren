using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestVinneren.Entidades;
using TestVinneren.Negocio;

namespace TestVinneren.Datos
{
    public class DProductos
    {
        public static IConfiguration Configuration { get; set; } = null!;

        SqlCommand _command = null!;
        string _query = "";

        List<Producto> productos = new List<Producto>();
        Producto producto = new Producto();

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory
                ()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            return Configuration.GetConnectionString("TestVinnerenConnection");
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            _query = $"SP_ConsultarProductos";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                _command = new SqlCommand(_query, connection);
                _command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = await _command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    productos.Add(
                        new Producto()
                        {
                            IdProducto = Convert.ToInt32(reader["idProducto"]),
                            NombreProducto = (string)reader["nombreProducto"],
                            NumMaterial = (string)reader["numMaterial"],
                            CantidadUnidadades = Convert.ToInt32(reader["cantidadUnidades"]),
                            IdCategoria = Convert.ToInt32(reader["idCategoria"])
                        }
                     );
                }
                connection.Close();
            }
            return productos;

        }

        public async Task<Producto> ObtenerProductoPorId(int id)
        {
            _query = $"SP_ConsultarProductoPorId";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                _command = new SqlCommand(_query, connection);
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue("id", id);

                connection.Open();
                SqlDataReader reader = await _command.ExecuteReaderAsync();
                reader.Read();
                if (reader.HasRows)
                {
                    producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                    producto.NombreProducto = (string)reader["nombreProducto"];
                    producto.NumMaterial = (string)reader["numMaterial"];
                    producto.CantidadUnidadades = Convert.ToInt32(reader["cantidadUnidades"]);
                    producto.IdCategoria = Convert.ToInt32(reader["idCategoria"]);
                }
                connection.Close();
            }
            return producto;

        }

        public async Task<int> AgregarProducto(Producto producto)
        {
            int idProductoCreado;
            _query = $"SP_InsertarProducto";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                _command = new SqlCommand(_query, connection);
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue("nombreProducto", producto.NombreProducto);
                _command.Parameters.AddWithValue("numMaterial", producto.NumMaterial);
                _command.Parameters.AddWithValue("cantidadUnidades", producto.CantidadUnidadades);
                _command.Parameters.AddWithValue("idCategoria", producto.IdCategoria);

                connection.Open();
                SqlDataReader reader = await _command.ExecuteReaderAsync();
                reader.Read();
                idProductoCreado = Convert.ToInt32(reader["id"]);
                connection.Close();
            }
            return idProductoCreado;
        }

        public async Task ModificarProducto(Producto producto)
        {

            _query = $"SP_ModificarProducto";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                _command = new SqlCommand(_query, connection);
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue("idProducto", producto.IdProducto);
                _command.Parameters.AddWithValue("nombreProducto", producto.NombreProducto);
                _command.Parameters.AddWithValue("numMaterial", producto.NumMaterial);
                _command.Parameters.AddWithValue("cantidadUnidades", producto.CantidadUnidadades);
                _command.Parameters.AddWithValue("idCategoria", producto.IdCategoria);

                connection.Open();
                await _command.ExecuteNonQueryAsync();
                connection.Close();
            }

        }

        public async Task EliminarProducto(int id)
        {
            _query = $"SP_EliminarProducto";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                _command = new SqlCommand(_query, connection);
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue("idProducto", id);

                connection.Open();
                await _command.ExecuteNonQueryAsync();
                connection.Close();
            }

        }

        public async Task<List<Producto>> ObtenerProductosRango(int rangoInicial, int rangoFinal)
        {
            productos = new List<Producto>();
            _query = $"SP_ObtenerProductosRango";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                _command = new SqlCommand(_query, connection);
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue("rangoInicial", rangoInicial);
                _command.Parameters.AddWithValue("rangoFinal", rangoFinal);
                connection.Open();
                SqlDataReader reader = await _command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    productos.Add(
                        new Producto()
                        {
                            IdProducto = Convert.ToInt32(reader["idProducto"]),
                            NombreProducto = (string)reader["nombreProducto"],
                            NumMaterial = (string)reader["numMaterial"],
                            CantidadUnidadades = Convert.ToInt32(reader["cantidadUnidades"]),
                            IdCategoria = Convert.ToInt32(reader["idCategoria"])
                        }
                     );
                }
                connection.Close();
            }
            return productos;

        }

        public async Task<List<Producto>> ObtenerProductosPorCategoria(int idCategoria)
        {
            productos = new List<Producto>();
            _query = $"SP_ObtenerProductosPorCategoria";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                _command = new SqlCommand(_query, connection);
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue("idCategoria", idCategoria);
                connection.Open();
                SqlDataReader reader = await _command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    productos.Add(
                        new Producto()
                        {
                            IdProducto = Convert.ToInt32(reader["idProducto"]),
                            NombreProducto = (string)reader["nombreProducto"],
                            NumMaterial = (string)reader["numMaterial"],
                            CantidadUnidadades = Convert.ToInt32(reader["cantidadUnidades"]),
                            IdCategoria = Convert.ToInt32(reader["idCategoria"])
                        }
                     );
                }
                connection.Close();
            }
            return productos;

        }
    }
}
