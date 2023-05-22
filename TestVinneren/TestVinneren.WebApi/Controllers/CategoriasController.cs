using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            Categoria categoria = await _nCategorias.ObtenerCategoriaPorId(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
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
                await _nCategorias.AgregarCategoria(categoria);
                return Ok();
            }
            catch (Exception )
            {

                throw;
            }

        }

        // PUT api/<CategoriasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
