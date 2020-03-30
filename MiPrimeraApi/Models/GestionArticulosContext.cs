using Microsoft.EntityFrameworkCore;
using MiPrimeraApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraApi.Models
{
    public class GestionArticulosContext : DbContext
    {
        public GestionArticulosContext(DbContextOptions<GestionArticulosContext> opciones) : base(opciones) { }

        public DbSet<Articulo> Articulos { set; get; }
        public DbSet<Proveedor> Proveedores { set; get; }
        public DbSet<ArticuloTieneProveedor> ArticulosTienenProveedores { set; get; }
    }
}
