using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Models;

namespace MiPrimeraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly GestionArticulosContext _contexto;

        public ProveedorController(GestionArticulosContext contexto)
        {
            _contexto = contexto;
        }

        // GET api/proveedor
        [HttpGet]
        [Route("")]
        public IActionResult Obtener()
        {
            var proveedores = _contexto.Proveedores.ToList();
            return Ok(proveedores);
        }

        // GET api/proveedor/1
        [HttpGet]
        [Route("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var proveedor = _contexto.Proveedores.FirstOrDefault(x => x.Id == id);
            return Ok(proveedor);
        }
    }
}