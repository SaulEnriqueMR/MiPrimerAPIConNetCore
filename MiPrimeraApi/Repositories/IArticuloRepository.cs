using MiPrimeraApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraApi.Repositories
{
    public interface IArticuloRepository
    {
        List<Articulo> ObtenerTodos();
        Articulo ObtenerPorId(int id);
    }
}