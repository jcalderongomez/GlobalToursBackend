using Core.Entidades;

namespace Core.Especificacion
{
    public class LugaresConPaisCategoriaEspecificacion : EspecificacionBase <Lugar>
    {
        public LugaresConPaisCategoriaEspecificacion()
        {   
            AgregarInclude(x => x.Pais);
            AgregarInclude(x=> x.Categoria);
        }


        public LugaresConPaisCategoriaEspecificacion(int id) : base(x=> x.Id== id)
        {   
            AgregarInclude(x => x.Pais);
            AgregarInclude(x=> x.Categoria);
        }

        
    }
}