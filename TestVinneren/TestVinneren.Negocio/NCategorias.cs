using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestVinneren.Datos;

namespace TestVinneren.Negocio
{
    public class NCategorias
    {
        private readonly DCategorias _dCategorias;

        public NCategorias(DCategorias dCategorias)
        {
            _dCategorias = dCategorias;
        }

        public async Task<List<Categoria>> ObtenerCategorias()
        {
            return await _dCategorias.ObtenerCategorias();
        }

        public async Task<Categoria> ObtenerCategoriaPorId(int id)
        {
            return await _dCategorias.ObtenerCategoriaPorId(id);
        }

        public async Task<int> AgregarCategoria(Categoria categoria)
        {
            return await _dCategorias.AgregarCategoria(categoria);
        }

        public async Task ModificarCategoria(Categoria categoria)
        {
            await _dCategorias.ModificarCategoria(categoria);
        }

        public async Task EliminarCategoria(int id)
        {
            await _dCategorias.EliminarCategoria(id);
        }
    }
}
