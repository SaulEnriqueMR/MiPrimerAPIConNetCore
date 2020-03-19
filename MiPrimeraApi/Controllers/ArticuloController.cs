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
        List<Articulo> articulos = new List<Articulo>()
        {
            new Articulo { Id = 1, Nombre = "Laptop", Descripcion = "Laptop HP", Precio = 15000.00, FechaRegistro = DateTime.Now },
            new Articulo { Id = 2, Nombre = "Impresora", Descripcion = "Impresora Epson", Precio = 8700.00, FechaRegistro = DateTime.Now },
            new Articulo { Id = 3, Nombre = "Monito", Descripcion = "Monitor ASUS", Precio = 1600.00, FechaRegistro = DateTime.Now },
            new Articulo { Id = 4, Nombre = "Cable USB", Descripcion = "Cable USB Generico", Precio = 193.00, FechaRegistro = DateTime.Now }
        };

        public ArticuloController()
        {
            
        }

        // GET api/articulo
        [HttpGet]
        [Route("")]
        public IActionResult Obtener()
        {
            return Ok(articulos);
        }

        // GET api/articulo/5
        [HttpGet]
        [Route("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var articulo = articulos.FirstOrDefault(a => a.Id == id);
            if (articulo == null)
            {
                return NotFound();   
            }
            return Ok(articulo);
        }

        [HttpGet]
        [Route("nombre/{nombre}")]
        public IActionResult BuscarPorAtributo(string nombre) {
            return Ok(nombre);
        }

        [HttpGet]
        [Route("buscar")]
        public IActionResult BuscarPorQueryParameter(string nombre) {
            return Ok(nombre);
        }

        // POST api/articulo
        [HttpPost]
        [Route("")]
        public IActionResult Registrar(Articulo articulo)
        {
            articulos.Add(articulo);
            if (articulo.Descripcion == null)
            {
                return BadRequest(new 
                {
                    errors = new 
                    {
                        Descripcion = new List<string>()
                        {
                            "Ingrese una descripcion"
                        }
                    }
                });
            }
            articulo.FechaRegistro = DateTime.Now;
            // return CreatedAtAction(nameof(Registrar), new {articulo.Id}, articulo);
            return Ok(articulos);
        }

        // PUT api/articulo/5
        [HttpPut]
        public IActionResult Editar(int id, Articulo articulo)
        {
            articulo.Id = id;
            var indice = articulos.IndexOf(articulo);
            articulos[indice].Nombre = articulo.Nombre;
            articulos[indice].Descripcion = articulo.Descripcion;
            articulos[indice].Precio = articulo.Precio;
            return Ok();
        }

        // DELETE api/articulo/5
        [HttpDelete]
        public IActionResult Borrar(int id)
        {
            var articulo = articulos.FirstOrDefault(a => a.Id == id);
            articulos.Remove(articulo);
            return Ok();
        }
    }
}