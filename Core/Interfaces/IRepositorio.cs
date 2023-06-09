using Core.Especificacion;
using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IRepositorio<T> where T : class

    {
        Task Crear(T entidad);


        Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null);

        //        Task <List<T>> ObtenerTodos(Expression<Func<T,bool>>? filtro = null);

        //        Task<List<T>> ObtenerEspec(IEspecificacion<T> espec);


        //Task<IReadOnlyList<T>> ObtenerTodosEspec(IEspecificacion<T> espec);

        Task<T> ObtenerPrimero(Expression<Func<T,bool>> filtro = null,string incluirPropiedades = null);
        Task<T> Obtener(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null);
        Task Remover(T entidad);

        Task Grabar();
    }
}