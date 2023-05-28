using API.Dtos;
using AutoMapper;
using Core.Entidades;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LugaresController : ControllerBase
    {
        private readonly ILogger<LugaresController> _logger;
        private readonly ILugarRepositorio _lugarRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        //private readonly IRepositorio<Lugar> _lugarRepo;

        public LugaresController(ILogger<LugaresController> logger, ILugarRepositorio lugarRepo, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _lugarRepo = lugarRepo;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetLugares()
        {
            try
            {
                _logger.LogInformation("Obtener Lugares");
                IEnumerable<Lugar> lugarList = await _lugarRepo.ObtenerTodos(incluirPropiedades: "Categoria,Pais",
                                                                            orderBy: l => l.OrderBy(l=>l.Nombre));

                _response.Resultado = _mapper.Map<IEnumerable<LugarDto>>(lugarList);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpGet("{id}", Name = "GetLugar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LugarDto>> GetLugar(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer el Lugar con el ID " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                var lugar = await _lugarRepo.Obtener(c => c.Id == id);

                if (lugar == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<LugarDto>(lugar);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CrearLugar([FromBody] LugarCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _lugarRepo.Obtener(c => c.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
                {
                    ModelState.AddModelError("NombreExiste", "El lugar con ese nombre ya existe!");
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                Lugar modelo = _mapper.Map<Lugar>(createDto);

                await _lugarRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetLugar", new { id = modelo.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLugar(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var lugar = await _lugarRepo.Obtener(c => c.Id == id);

                if (lugar == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _lugarRepo.Remover(lugar);

                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateLugar(int id, [FromBody] LugarDto updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            Lugar modelo = _mapper.Map<Lugar>(updateDto);

            await _lugarRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }
    }
}
