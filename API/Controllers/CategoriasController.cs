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
    public class CategoriasController : ControllerBase
    {
        private readonly ILogger<CategoriasController> _logger;
        private readonly ICategoriaRepositorio _categoriaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        //private readonly IRepositorio<Categoria> _categoriaRepo;

        public CategoriasController(ILogger<CategoriasController> logger, ICategoriaRepositorio categoriaRepo, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _categoriaRepo = categoriaRepo;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetCategorias()
        {
            try
            {
                _logger.LogInformation("Obtener Categorias");
                IEnumerable<Categoria> categoriaList = await _categoriaRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<CategoriaDto>>(categoriaList);
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

        [HttpGet("{id}", Name = "GetCategoria")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoriaDto>> GetCategoria(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer la Categoria con el ID " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                var categoria = await _categoriaRepo.Obtener(c => c.Id == id);

                if (categoria == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<CategoriaDto>(categoria);
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
        public async Task<ActionResult<APIResponse>> CrearCategoria([FromBody] CategoriaCreateDto createDto) 
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _categoriaRepo.Obtener(c => c.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
                {
                    ModelState.AddModelError("NombreExiste", "La categoria con ese nombre ya existe!");
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                Categoria modelo = _mapper.Map<Categoria>(createDto);

                await _categoriaRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetCategoria", new { id = modelo.Id }, _response);
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
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var categoria = await _categoriaRepo.Obtener(c => c.Id == id);

                if (categoria == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _categoriaRepo.Remover(categoria);

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
        public async Task<IActionResult> UpdateCategoria(int id, [FromBody] CategoriaDto updateDto)
        {
            if (updateDto == null || id!= updateDto.Id) {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            Categoria modelo = _mapper.Map<Categoria>(updateDto);
            
            await _categoriaRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }
    }
}
