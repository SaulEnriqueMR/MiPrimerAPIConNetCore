using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraApi.Models
{
    class Proveedor
    {
        public int Id {set;get;}
        public string Nombre {set;get;}

        public string Telefono {set;get;}
    }

}