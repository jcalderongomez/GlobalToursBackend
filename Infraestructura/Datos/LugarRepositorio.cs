using Core.Entidades;
using Core.Interfaces;

namespace Infraestructura.Datos
{
    public class LugarRepositorio : Repositorio<Lugar>, ILugarRepositorio
    {
        private readonly ApplicationDbContext _db;

        public LugarRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Lugar> Actualizar(Lugar entidad)
        {
            _db.Lugar.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Core.Entidades;
//using Core.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace Infraestructura.Datos
//{
//    public class LugarRepositorio : ILugarRepositorio
//    {
//        private readonly ApplicationDbContext _db;

//        public LugarRepositorio(ApplicationDbContext db)
//        {
//            _db= db;
//        }
//        public async Task<Lugar> GetLugarAsync(int id)
//        {
//            return await _db.Lugar
//                            .Include(p => p.Pais)
//                            .Include(c => c.Categoria)
//                            .FirstOrDefaultAsync(l => l.Id == id);
//        }

//        public async Task<IReadOnlyList<Lugar>> GetLugaresAsync()
//        {
//              return await _db.Lugar
//                            .Include(p => p.Pais)
//                            .Include(c => c.Categoria)
//                            .ToListAsync();
//        }
//    }
//}