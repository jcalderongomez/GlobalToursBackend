using System.Linq.Expressions;

namespace Core.Especificacion
{
    public class EspecificacionBase<T> : IEspecificacion<T>
    {
        public EspecificacionBase(Expression<Func<T, bool>> filtro)
        {
            Filtro = filtro;
        }

        public EspecificacionBase()
        {
            
        }
        public Expression<Func<T, bool>> Filtro { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public void AgregarInclude(Expression<Func<T, object>> includExpression) { 
            Includes.Add(includExpression);
        }
    }
}
