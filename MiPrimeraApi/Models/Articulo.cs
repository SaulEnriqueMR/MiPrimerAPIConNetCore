using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraApi.Models
{
    public class Articulo
    {
        public int Id { set; get; }
        public string Nombre { set; get; }
        public string Descripcion { set; get; }
        public double Precio { set; get; }
        public DateTime FechaRegistro { set; get; }
    }
}