using Microsoft.AspNetCore.Mvc;
using TestVinneren.Datos;
using TestVinneren.Negocio;


namespace TestVinneren.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly NCategorias _nCategorias;
        
        public CategoriasController(NCategorias nCategorias)
        {
            _nCategorias = nCategorias;
        }

        // GET: api/<CategoriasController>
        [HttpGet]
        public async Task<IEnumerable<Categoria>> Get()
        {
            return await _nCategorias.ObtenerCategorias();
        }

        // GET api/<CategoriasController>/5
        [HttpGet("{id}", Name = "obtenerCategoria")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            Categoria categoria = await _nCategorias.ObtenerCategoriaPorId(id);

            if (categoria.IdCategoria == null)
            {
                return NotFound("Categoría no encontrada");
            }
            else
            {
                return categoria;
            }
    
        }

        // POST api/<CategoriasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
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
        public async Task<IActionResult> Put(int id, [FromBody] Categoria categoria)
        {
            if(id != categoria.IdCategoria)
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
