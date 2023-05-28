using Core.Entidades;

namespace Core.Interfaces
{
    public interface ILugarRepositorio : IRepositorio<Lugar>
    {
        Task<Lugar> Actualizar(Lugar entidad);

    }
}