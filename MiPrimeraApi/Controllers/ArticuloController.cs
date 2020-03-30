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
    public class ArticuloController : ControllerBase
    {
        private readonly GestionArticulosContext _contexto;

        public ArticuloController(GestionArticulosContext contexto)
        {
            _contexto = contexto;
        }

        // GET api/articulo
        [HttpGet]
        [Route("")]
        public IActionResult Obtener()
        {
            var articulos = _contexto.Articulos.ToList();
            return Ok(articulos);
        }

        // GET api/articulo/5
        [HttpGet]
        [Route("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var articulo = _contexto.Articulos.FirstOrDefault(x => x.Id == id);
            return Ok(articulo);
        }

        [HttpGet]
        [Route("nombre/{nombre}")]
        public IActionResult BuscarPorAtributo(string nombre) 
        {
            return Ok();
        }

        [HttpGet]
        [Route("buscar")]
        public IActionResult BuscarPorQueryParameter(string nombre) 
        {
            return Ok();
        }

        // POST api/articulo
        [HttpPost]
        [Route("")]
        public IActionResult Registrar(Articulo articulo)
        {
            articulo.FechaRegistro = DateTime.Now;
            _contexto.Articulos.Add(articulo);
            _contexto.SaveChanges();
            return CreatedAtAction(nameof(ObtenerPorId), new {articulo.Id}, articulo);
        }

        // PUT api/articulo/5
        [HttpPut]
        [Route("{id}")]
        public IActionResult Editar(int id, Articulo articulo)
        {
            return Ok();
        }

        // DELETE api/articulo/5
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Borrar(int id)
        {
            return Ok();
        }
    }
}