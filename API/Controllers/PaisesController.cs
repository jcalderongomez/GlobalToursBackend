using API.Dtos;
using Core.Entidades;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaisesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        private ResponseDto _response;

        public PaisesController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pais>>> GetPaises()
        {
            var lista = await _db.Pais.ToListAsync();
            _response.Resultado = lista;
            _response.Mensaje = "Listado de paiss";
            return Ok(_response);
        }

        [HttpGet("{id}", Name = "GetPais")]
        public async Task<ActionResult<Pais>> GetPais(int id)
        {
            var pais = await _db.Pais.FindAsync(id);
            _response.Resultado = pais;
            _response.Mensaje = "Datos de la pais " + pais.Id;
            return Ok(_response); //Status code = 200

        }

        [HttpPost]
        public async Task<ActionResult<Pais>> PostPais([FromBody] Pais pais)
        {
            await _db.Pais.AddAsync(pais);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetPais", new { id = pais.Id }, pais); //status Code = 201
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> PutPais(int id, [FromBody] Pais pais)

        {
            if (id != pais.Id)
            {
                return BadRequest("Id del pais no coincide");
            }
            _db.Update(pais);
            await _db.SaveChangesAsync();
            return Ok(pais);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePais(int id)
        {
            var pais = await _db.Pais.FindAsync(id);
            if (pais == null)
            {
                return NotFound();
            }
            _db.Pais.Remove(pais);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}