using Core.Entidades;
using Core.Especificacion;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infraestructura.Datos
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task Crear(T entidad)
        {
            await dbSet.AddAsync(entidad);
            await Grabar();
        }

        public async Task Grabar() {
            await _db.SaveChangesAsync();
        }

        public async Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked) {
                query = query.AsNoTracking();
            }

            if (filtro != null) {
                query = query.Where(filtro);
            }
            return await query.FirstOrDefaultAsync();
        }

        public Task<List<T>> ObtenerEspec(IEspecificacion<T> espec)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> ObtenerTodos(
                            Expression<Func<T, bool>> filtro = null, 
                            Func<IQueryable<T>,
                            IOrderedQueryable<T>> orderBy = null, 
                            string incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;
            
            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            if (incluirPropiedades != null) {
                foreach (var ip in incluirPropiedades.Split(
                                   new char[] { ',' },
                                   StringSplitOptions.RemoveEmptyEntries)) {
                    query = query.Include(ip);
                }
            }

            if (orderBy != null) {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<T> ObtenerTodosEspec(IEspecificacion<T> espec)
        {
            return await AplicarEspecificacion(espec).FirstOrDefaultAsync();
        }

        private IQueryable<T> AplicarEspecificacion(IEspecificacion<T> espec) {
            return EvaluadorEspecificacion<T>.GetQuery(_db.Set<T>().AsQueryable(), espec);
        }

        public async Task Remover(T entidad)
        {
            dbSet.Remove(entidad);
            await Grabar();
        }
    }
}




    



