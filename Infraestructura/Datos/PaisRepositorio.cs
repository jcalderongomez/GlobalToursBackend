using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Datos
{
    public class PaisRepositorio : IPaisRepositorio
    {
        private readonly ApplicationDbContext _db;

        public PaisRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Pais> GetPaisAsync(int id)
        {
            return await _db.Pais.FindAsync(id);
        }

        public async Task<IReadOnlyList<Pais>> GetPaisesAsync()
        {
            return await _db.Pais.ToListAsync();
        }
    }
}