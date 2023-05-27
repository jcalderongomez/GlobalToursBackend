using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;

namespace Core.Interfaces
{
    public interface IPaisRepositorio
    {
        //firmas de nuestros méodos

        Task<Pais> GetPaisAsync(int id);
        Task<IReadOnlyList<Pais>> GetPaisesAsync();
        
    }
}