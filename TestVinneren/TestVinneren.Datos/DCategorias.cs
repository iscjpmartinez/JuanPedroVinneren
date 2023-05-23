using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Extensions.Configuration;
using TestVinneren.Negocio;
using System.Collections;
using System.Data;

namespace TestVinneren.Datos
{
    public class DCategorias
    {
        public static IConfiguration Configuration { get; set; } = null!;

        SqlCommand _command = null!;
        string _query = "";

        List<Categoria> categorias = new List<Categoria>();
        Categoria categoria = new Categoria();

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory
                ()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            return Configuration.GetConnectionString("TestVinnerenConnection");
        }

         public async Task<List<Categoria>> ObtenerCategorias()
         {
            _query = $"SP_ConsultarCategorias";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                _command = new SqlCommand(_query, connection);
                _command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader =  await _command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    categorias.Add(
                        new Categoria()
                        {
                            IdCategoria = Convert.ToInt32(reader["idCategoria"]),
                            NombreCategoria = (string)reader["nombreCategoria"],
                            IdCategoriaPadre = Convert.IsDBNull(reader["idCategoriaPadre"]) ? null : Convert.ToInt32(reader["idCategoriaPadre"])
                }
                     );
                }
                connection.Close();
            }
            return categorias;

         }

        public async Task<Categoria> ObtenerCategoriaPorId(int id)
        {
            _query = $"SP_ConsultarCategoriaPorId";
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
                    categoria.IdCategoria = Convert.IsDBNull(reader["idCategoria"]) ? null : Convert.ToInt32(reader["idCategoria"]);
                    categoria.NombreCategoria = (string)reader["nombreCategoria"];
                    categoria.IdCategoriaPadre = Convert.IsDBNull(reader["idCategoriaPadre"]) ? null : Convert.ToInt32(reader["idCategoriaPadre"]);
                }
               
                connection.Close();
            }
            return categoria;

        }

        public async Task<int> AgregarCategoria(Categoria categoria)
        {
            int idCategoriaCreada;
            _query = $"SP_InsertarCategoria";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                _command = new SqlCommand(_query, connection);
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue("nombreCategoria", categoria.NombreCategoria);
                _command.Parameters.AddWithValue("idCategoriaPadre", categoria.IdCategoriaPadre);

                connection.Open();
                SqlDataReader reader = await _command.ExecuteReaderAsync();
                reader.Read();
                idCategoriaCreada = Convert.ToInt32(reader["id"]);
                connection.Close();
            }
            return idCategoriaCreada;
        }

        public async Task ModificarCategoria(Categoria categoria)
        {
            
            _query = $"SP_ModificarCategoria";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                _command = new SqlCommand(_query, connection);
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue("idCategoria", categoria.IdCategoria);
                _command.Parameters.AddWithValue("nombreCategoria", categoria.NombreCategoria);
                _command.Parameters.AddWithValue("idCategoriaPadre", categoria.IdCategoriaPadre);

                connection.Open();
                await _command.ExecuteNonQueryAsync();
                connection.Close();
            }
            
        }

        public async Task EliminarCategoria(int id)
        {
            _query = $"SP_EliminarCategoria";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                _command = new SqlCommand(_query, connection);
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue("idCategoria", id);
              
                connection.Open();
                await _command.ExecuteNonQueryAsync();
                connection.Close();
            }

        }

    }
}
