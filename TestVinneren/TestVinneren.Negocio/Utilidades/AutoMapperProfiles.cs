using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestVinneren.Entidades;
using TestVinneren.Negocio.DTOs.Categoria;
using TestVinneren.Negocio.DTOs.Producto;

namespace TestVinneren.Negocio.Utilidades
{
    public class AutoMapperProfiles : Profile  
    {
        public AutoMapperProfiles() 
        {
            CreateMap<CrearCategoriaDTO, Categoria>();
            CreateMap<Categoria, CategoriaDTO>();
            CreateMap<CategoriaDTO, Categoria>();

            CreateMap<CrearProductoDTO, Producto>();
            CreateMap<Producto, ProductoDTO>();
            CreateMap<ProductoDTO, Producto>();

        }
    }
}
