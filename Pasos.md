# Mi primer API con .NetCore

## Requerimientos

* Estar en un sistema *Linux* (una distribucion basada en *Debian*, *Ubuntu* o *Fedora*).
* Tener *.NetCore* instalado en el sistema, en su ultima [version LTS](https://dotnet.microsoft.com/download), al momento la elaboracion de esta guia es la version 3.1.
* Tener *Git* instalado en el sistema.
* Un editor de texto o IDE.
* *Postman* o cualquier herramienta para probar APIs

> Para esta guia se utilizara Ubuntu con VSCode

## Configurando entorno de trabajo

Primero necesitamos crear nuestro directorio donde residira nuestro proyecto. Una vez dentro de nuestro nuevo directorio crear repositorio correspondiente:

```bash
    mkdir MiPrimerApi
    cd MiPrimerApi
    git init
```

Luego procederemos a crear nuestro archivo *.gitignore* y lo llenaremos con las reglas necesarias. Ademas si asi se desea se puede crear el archivo *README*.

> Sugerencia: Para esta guia se utilizaran las [reglas especificadas en este enlace](https://github.com/dotnet/core/blob/master/.gitignore), pero el/la estudiante es libre ocupar las reglas que el considere.

```bash
    touch .gitignore
    touch README.md
```

Una vez que hayamos puesto nuestras reglas, haremos commit.

> El/la estudiante es libre de escoger si hacer commit por archivo individual o agregar los dos archivos en un mismo commit.

```bash
    git add README.md
    git commit -m "Agregado README"
    git add .gitignore
    git commit -m "Agregado archivo .gitignore"
```

## Creando el proyecto

Comprobamos que en efecto, tengamos la version 3.1 de *.NetCore* instalada y procedemos a crear nuestro proyecto.

```bash
    dotnet --version
    dotnet new webapi
```

Hacemos commit para declarar que hemos creado nuestro proyecto.

```bash
    git add .
    git commit -m "Creacion del proyecto"
```

Corremos nuestro proyecto recien creado, este deberia correr en la siguiente direccion [https://localhost:5001/WeatherForecast/](https://localhost:5001/WeatherForecast/)

```bash
    dotnet run
```

> Si se quiere modificar el puerto en donde se quiere correr la aplicacion al ocupar el comando **dotnet run**, se debe modificar el archivo *MiPrimeraApi/Properties/launchSettings.json*.

## Trabajando con el proyecto

En la raiz de nuestro directorio se hara un directorio con el nombre *Models* y dentro de este se creara la clase *Articulo.cs*, esta clase contiene lo siguiente:

```c#
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
```

Una vez se creo el modelo, en la carpeta de *Controllers* agregar el archivo *ArticuloController*. En este archivo agregar lo siguiente

```c#
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
            List<Articulo> articulos { set; get; }

            public ArticuloController()
            {
                articulos = new List<Articulo>()
                {
                    new Articulo { Id = 1, Nombre = "Laptop", Descripcion = "Laptop HP", Precio = 15000.00, FechaRegistro = DateTime.Now },
                    new Articulo { Id = 2, Nombre = "Impresora", Descripcion = "Impresora Epson", Precio = 8700.00, FechaRegistro = DateTime.Now },
                    new Articulo { Id = 3, Nombre = "Monito", Descripcion = "Monitor ASUS", Precio = 1600.00, FechaRegistro = DateTime.Now },
                    new Articulo { Id = 4, Nombre = "Cable USB", Descripcion = "Cable USB Generico", Precio = 193.00, FechaRegistro = DateTime.Now }
                };
            }

            // GET api/articulo
            [HttpGet]
            [Route("")]
            public IActionResult Obtener()
            {
                return Ok(articulos);
            }
        }
    }
```

Despues de eso, correr el proyecto y probarlo, ingresando al siguiente enlace: [https://localhost:5001/api/articulo](https://localhost:5001/api/articulo), esto nos deberia dar la siguiente respuesta:

```json
    [
        {
            "id":1,
            "nombre":"Laptop",
            "descripcion":"Laptop HP",
            "precio":15000,
            "fechaRegistro":"2020-03-14T00:29:47.2435536-06:00"
        },
        {
            "id":2,
            "nombre":"Impresora",
            "descripcion":"Impresora Epson",
            "precio":8700,
            "fechaRegistro":"2020-03-14T00:29:47.2436054-06:00"
        },
        {
            "id":3,
            "nombre":"Monito",
            "descripcion":"Monitor ASUS",
            "precio":1600,
            "fechaRegistro":"2020-03-14T00:29:47.2436069-06:00"
        },
        {
            "id":4,
            "nombre":"Cable USB",
            "descripcion":"Cable USB Generico",
            "precio":193,
            "fechaRegistro":"2020-03-14T00:29:47.2436073-06:00"
        }
    ]
```
