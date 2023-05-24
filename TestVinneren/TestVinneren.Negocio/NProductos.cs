using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestVinneren.Datos;
using TestVinneren.Entidades;

namespace TestVinneren.Negocio
{
    public class NProductos
    {
        private readonly DProductos _dProductos;

        public NProductos(DProductos dProductos)
        {
            _dProductos = dProductos;
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            return await _dProductos.ObtenerProductos();
        }

        public async Task<Producto> ObtenerProductoPorId(int id)
        {
            return await _dProductos.ObtenerProductoPorId(id);
        }

        public async Task<int> AgregarProducto(Producto producto)
        {
            return await _dProductos.AgregarProducto(producto);
        }

        public async Task ModificarProducto(Producto producto)
        {
            await _dProductos.ModificarProducto(producto);
        }

        public async Task EliminarProducto(int id)
        {
            await _dProductos.EliminarProducto(id);
        }

        public async Task<List<Producto>> ObtenerProductosRango(int rangoInicio, int rangoFinal)
        {
            return await _dProductos.ObtenerProductosRango(rangoInicio, rangoFinal);
        }

        public async Task<List<Producto>> ObtenerProductosPorCategoria(int idCategoria)
        {
            return await _dProductos.ObtenerProductosPorCategoria(idCategoria);
        }
    }
}
