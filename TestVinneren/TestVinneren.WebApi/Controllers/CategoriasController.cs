using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestVinneren.Datos;
using TestVinneren.Negocio;
using TestVinneren.Negocio.DTOs.Categoria;

namespace TestVinneren.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly NCategorias _nCategorias;
        private readonly IMapper _mapper;

        public CategoriasController(NCategorias nCategorias, IMapper mapper)
        {
            _nCategorias = nCategorias;
            _mapper = mapper;
        }

        // GET: api/<CategoriasController>
        [HttpGet]
        public async Task<List<CategoriaDTO>> Get()
        {
            var categorias = await _nCategorias.ObtenerCategorias();
            var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDTO;
        }

        // GET api/<CategoriasController>/5
        [HttpGet("{id}", Name = "obtenerCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            Categoria categoria = await _nCategorias.ObtenerCategoriaPorId(id);

            if (categoria.IdCategoria == null)
            {
                return NotFound("Categoría no encontrada");
            }
            else
            {
                var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
                return categoriaDTO;
            }
    
        }

        // POST api/<CategoriasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearCategoriaDTO crearCategoriaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var categoria = _mapper.Map<Categoria>(crearCategoriaDTO);
                var idCategoriaCreada = await _nCategorias.AgregarCategoria(categoria);
                Categoria categoriaCreada = await _nCategorias.ObtenerCategoriaPorId(idCategoriaCreada);

                return CreatedAtRoute("obtenerCategoria", new {id = idCategoriaCreada }, categoriaCreada);
            }
            catch (Exception ex)
            {
                // Puedo guardar la excepción para futuras referencias por el momento la muestro en consola.
                Console.WriteLine(ex);

                // Regreso un error sin mucha especificación.
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al procesar la solicitud.");
            }

        }

        // PUT api/<CategoriasController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoriaDTO categoriaDTO)
        {
            if(id != categoriaDTO.IdCategoria)
            {
                return NotFound("El id no coincide");
            }

            Categoria categoriaEncontrada = await _nCategorias.ObtenerCategoriaPorId(id);

            if (categoriaEncontrada.IdCategoria == null)
            {
                return NotFound("Categoría no encontrada");
            }

            try
            {
                var categoria = _mapper.Map<Categoria>(categoriaDTO);
                await _nCategorias.ModificarCategoria(categoria);
            }
            catch (Exception ex)
            {
                // Puedo guardar la excepción para futuras referencias por el momento la muestro en consola.
                Console.WriteLine(ex);

                // Regreso un error sin mucha especificación.
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al procesar la solicitud.");
            }

            return Ok("Categoría modificada");
        }

        // DELETE api/<CategoriasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Categoria categoriaEncontrada = await _nCategorias.ObtenerCategoriaPorId(id);

            if (categoriaEncontrada.IdCategoria == null)
            {
                return NotFound("Categoría no encontrada");
            }

            try
            {
                await _nCategorias.EliminarCategoria(id);
            }
            catch (Exception ex)
            {
                // Puedo guardar la excepción para futuras referencias por el momento la muestro en consola.
                Console.WriteLine(ex);

                // Regreso un error sin mucha especificación.
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al procesar la solicitud.");
            }

            return Ok("Categoría eliminada");
        }
    }
}
