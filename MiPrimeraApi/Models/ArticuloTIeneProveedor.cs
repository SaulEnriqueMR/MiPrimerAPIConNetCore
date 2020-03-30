using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraApi.Models
{
    public class ArticuloTieneProveedor
    {
        public int ArticuloID { set; get; }
        public virtual Articulo Articulo { set; get; }
        public int ProveedorID{ set; get; }
        public virtual Proveedor Proveedor { set; get; }
    }
}