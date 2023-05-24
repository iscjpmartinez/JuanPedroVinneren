using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestVinneren.Datos;
using TestVinneren.Entidades;
using TestVinneren.Negocio;
using TestVinneren.Negocio.DTOs.Categoria;
using TestVinneren.Negocio.DTOs.Producto;

namespace TestVinneren.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly NProductos _nProductos;
        private readonly IMapper _mapper;

        public ProductosController(NProductos nProductos, IMapper mapper)
        {
            _nProductos = nProductos;
            _mapper = mapper;
        }
        // GET: api/<ProductosController>
        [HttpGet]
        public async Task<List<ProductoDTO>> Get()
        {
            var productos = await _nProductos.ObtenerProductos();
            var productosDTO = _mapper.Map<List<ProductoDTO>>(productos);
            return productosDTO;
        }

        // GET api/<ProductosController>/5
        [HttpGet("{id}", Name = "obtenerProducto")]
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {
            Producto producto = await _nProductos.ObtenerProductoPorId(id);

            if (producto.IdProducto == 0)
            {
                return NotFound("Producto no encontrado");
            }
            else
            {
                var productoDTO = _mapper.Map<ProductoDTO>(producto);
                return productoDTO;
            }

        }

        // POST api/<ProductosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearProductoDTO crearProductoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var producto = _mapper.Map<Producto>(crearProductoDTO);
                var idProductoCreado = await _nProductos.AgregarProducto(producto);
                Producto productoCreado = await _nProductos.ObtenerProductoPorId(idProductoCreado);

                return CreatedAtRoute("obtenerProducto", new { id = idProductoCreado }, productoCreado);
            }
            catch (Exception ex)
            {
                // Puedo guardar la excepción para futuras referencias por el momento la muestro en consola.
                Console.WriteLine(ex);

                // Regreso un error sin mucha especificación.
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al procesar la solicitud.");
            }

        }

        // PUT api/<ProductosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductoDTO productoDTO)
        {
            if (id != productoDTO.IdProducto)
            {
                return NotFound("El id no coincide");
            }

            Producto productoEncontrado = await _nProductos.ObtenerProductoPorId(id);

            if (productoEncontrado.IdProducto == 0)
            {
                return NotFound("Producto no encontrado");
            }

            try
            {
                var producto = _mapper.Map<Producto>(productoDTO);
                await _nProductos.ModificarProducto(producto);
            }
            catch (Exception ex)
            {
                // Puedo guardar la excepción para futuras referencias por el momento la muestro en consola.
                Console.WriteLine(ex);

                // Regreso un error sin mucha especificación.
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al procesar la solicitud.");
            }

            return Ok("Producto modificado");
        }

        // DELETE api/<ProductosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Producto ProductoEncontrado = await _nProductos.ObtenerProductoPorId(id);

            if (ProductoEncontrado.IdProducto == 0)
            {
                return NotFound("Producto no encontrado");
            }

            try
            {
                await _nProductos.EliminarProducto(id);
            }
            catch (Exception ex)
            {
                // Puedo guardar la excepción para futuras referencias por el momento la muestro en consola.
                Console.WriteLine(ex);

                // Regreso un error sin mucha especificación.
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al procesar la solicitud.");
            }

            return Ok("Producto eliminado");
        }

        [HttpGet("{rangoInicial:int}/{rangoFinal:int}")]
        public async Task<List<ProductoDTO>> GetPorRangoInventario(int rangoInicial, int rangoFinal)
        {
            var productos = await _nProductos.ObtenerProductosRango(rangoInicial, rangoFinal);
            var productosDTO = _mapper.Map<List<ProductoDTO>>(productos);
            return productosDTO;
        }

        [HttpGet("{idCategoria:int}")]
        public async Task<List<ProductoDTO>> GetPorCategoria(int idCategoria)
        {
            var productos = await _nProductos.ObtenerProductosPorCategoria(idCategoria);
            var productosDTO = _mapper.Map<List<ProductoDTO>>(productos);
            return productosDTO;
        }
    }
}
