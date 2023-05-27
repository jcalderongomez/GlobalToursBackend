using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;

namespace Core.Interfaces
{
    public interface IPaisRepositorio : IRepositorio<Pais>
    {
        Task<Pais> Actualizar(Pais entidad);

    }
}