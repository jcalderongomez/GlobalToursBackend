using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;

namespace Core.Interfaces
{
    public interface ICategoriaRepositorio : IRepositorio<Categoria>
    {
        
        Task<Categoria> Actualizar(Categoria entidad);
        

 
        
    }
}