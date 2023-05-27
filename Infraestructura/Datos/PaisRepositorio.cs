using Core.Entidades;
using Core.Interfaces;

namespace Infraestructura.Datos
{
    public class PaisRepositorio : Repositorio<Pais>, IPaisRepositorio
    {
        private readonly ApplicationDbContext _db;

        public PaisRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Pais> Actualizar(Pais entidad)
        {
            _db.Pais.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}