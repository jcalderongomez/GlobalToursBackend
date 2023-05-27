using Core.Entidades;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Datos
{
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepositorio(ApplicationDbContext db) :base (db)
        {
            _db = db;
        }
        
        public async Task<Categoria> Actualizar(Categoria entidad)
        {
            _db.Categoria.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}