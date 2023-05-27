using API.Dtos;
using AutoMapper;
using Core.Entidades;
using Core.Especificacion;
using Core.Interfaces;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LugaresController : ControllerBase
    {
        
        private readonly IRepositorio<Lugar> _lugarRepo;
        private readonly IRepositorio<Pais> _paisRepo;
        private readonly IRepositorio<Categoria> _categoriaRepo;
        private readonly IMapper _mapper;

        public LugaresController(IRepositorio<Lugar> lugarRepo, IRepositorio<Pais> paisRepo, IRepositorio<Categoria> categoriaRepo, IMapper mapper)
        {
            _lugarRepo = lugarRepo;
            _paisRepo = paisRepo;
            _categoriaRepo = categoriaRepo;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<ActionResult<IReadOnlyList<LugarDto>>> GetLugares()
        //{
        //    var espec = new LugaresConPaisCategoriaEspecificacion();
        //    //var lugares = await _lugarRepo.ObtenerTodosEspec(espec);

        //    //return Ok(_mapper.Map<IReadOnlyList<Lugar>, IReadOnlyList<LugarDto>>(lugares));

        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<LugarDto>> GetLugar(int id)
        //{
        //    var espec = new LugaresConPaisCategoriaEspecificacion(id);
        //    var lugar = await _lugarRepo.ObtenerEspec(espec);
        //    return _mapper.Map<Lugar, LugarDto>(lugar);
        //}



        //[HttpDelete("{id}")]
        //public async Task<bool> DeleteLugar(int id)
        //{
        //    try
        //    {
        //        Lugar lugar = await _lugarRepo.ObtenerAsync(id);
        //        if (lugar == null)
        //            return false;

        //        _lugarRepo.Remover(lugar);


        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error al eliminar el objeto: {ex.Message}");
        //        return false;
        //    }
        //    return true;
        //}


        
    }
}