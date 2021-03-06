# Mi primer API con .NetCore

## Requerimientos

* Estar en un sistema *Linux* (en una distribución basada en *Debian*, *Ubuntu* o *Fedora*).
* Tener *.NetCore* instalado en el sistema, en su última [version LTS](https://dotnet.microsoft.com/download) (al momento la elaboración de esta guía es la versión 3.1).
* Tener *Git* instalado en el sistema.
* Un editor de texto o IDE.
* *Postman* o cualquier herramienta para probar APIs
* Tener instalado *Docker*.

## Entregables

* Enlace al repositorio de *GitHub*
* Enlace al repositorio de *DockerHub*

## Configurando entorno de trabajo

Primero necesitamos crear nuestro directorio donde estará nuestro proyecto. Una vez dentro del nuevo directorio, creamos el repositorio correspondiente:

```bash
    mkdir MiPrimerApi
    cd MiPrimerApi
    git init
```

Luego procederemos a crear nuestro archivo *.gitignore* y le ingresaremos las reglas necesarias para un proyecto en *.NetCore*. Además, si así se desea, se puede crear el archivo *README*.

> Sugerencia: Para esta guía se utilizaran las [reglas especificadas en este enlace](https://github.com/dotnet/core/blob/master/.gitignore), pero el/la estudiante es libre ocupar las reglas que considere.

```bash
    touch .gitignore
    touch README.md
```

Ahora haremos commit.

```bash
    git add README.md
    git commit -m "Agregado README"
    git add .gitignore
    git commit -m "Agregado archivo .gitignore"
```

## Creando el proyecto

Para crear un proyecto ejecutamos el siguiente comando:

```bash
    dotnet new webapi
```

Ahora, haremos commit para declarar que hemos creado nuestro proyecto.

```bash
    git add .
    git commit -m "Creacion del proyecto"
```

> A partir de aquí el/la estudiante es libre de escoger como manejar sus commits.

Ejecutamos nuestro proyecto recien creado con el siguiente comando:

```bash
    dotnet run
```

Para acceder a nuestro proyecto ejecutandose vamos al siguiente enlace [https://localhost:5001/WeatherForecast/](https://localhost:5001/WeatherForecast/).

> Este enlace se puede abrir en *Postman* o en nuestro navegador.
> Si quisieramos modificar el puerto en donde se se ejecutará debemos modificar el archivo *launchSettings.json*, ubicado en *MiPrimeraApi/Properties/*.

## Trabajando con el proyecto

En la raíz de nuestro directorio, crearemos un directorio con el nombre *Models* y dentro de este se crearemos la clase *Articulo*, esta clase contiene lo siguiente:

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

Una vez se creamos nuestro modelo, en la carpeta de *Controllers* agregaremos nuestro controlador *ArticuloController*. En este archivo agregar lo siguiente

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

Después de eso, ejecutaremos nuestro proyecto y lo probaremo, ingresando al siguiente enlace: [https://localhost:5001/api/articulo](https://localhost:5001/api/articulo), esto nos debería dar algo similiar a lo siguiente:

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

## Creando nuestra primer imagen

En el proyecto, creamos un archivo *Dockerfile* y agregamos las siguiente líneas:

```docker
    # Esto va a crear la imagen del SDK de Microsoft

    FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
    WORKDIR /app

    # Esto va a copiar el archivo csproj e instala/restaura las dependencias (Via gestor de paquetes NUGET)

    COPY *.csproj ./
    RUN dotnet restore

    # Esto copia los archivos del proyecto y crea el lanzamiento (release)

    COPY . ./
    RUN dotnet publish -c Release -o out
    # Si este último comando se ejecuta directamente desde la consola, creará una carpeta con el nombre out y en esta carpeta se alojará la versión de producción

    # Genera nuestra imagen

    FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
    WORKDIR /app
    EXPOSE 80
    COPY --from=build-env /app/out .
    ENTRYPOINT [ "dotnet", "MiPrimeraApi.dll" ]
```

Después de eso, crearemos nuestro archivo *.dockerignore*. El cuál le dirá a Docker que ignore esos archivos (similar al *.gitignore* de *Git*). El contenido de este archivo es el siguiente:

```docker
    bin/
    obj/
```

Una vez tengamos nuestros archivos, ejecutamos el siguiente comando:

```bash
    sudo docker build -t <TuDockerHubID>/<NombreDelProyecto>:<Version> .
```

*Nota*: El *TuDockerHubID* y *NombreDelProyecto* deben ir en minúsculas.

En mi caso ejecutaría el siguiente comando:

```bash
    sudo docker build -t saulenriquemr/miprimeraapi:1.0 .
```

*Nota*: Si no se especifica una versión, *Docker* por defecto asignará el tag *latest*, y si no se pone DockerHubID ni nombre del proyecto, *Docker* le pondra un ID único.

Ahora, si ejecutamos el siguiente comando, podremos ver que ya existe nuestra imagen:

```bash
    sudo docker images
```

## Ejecutando nuestra imagen

Ya hemos construido nuestra imagen de *Docker*, ahora el siguiente paso es crear un contenedor a partir de nuestra imagen.

Ejecutamos el siguiente comando:

```bash
    sudo docker run -p <PuertoDeNuestraMaquinaDondeSeEjecutara>:<PuertoExpuestoEnNuestroDockerFile> <TuDockerHubID>/<NombreDelProyecto>:<VersionSiSeEspecifico>
```

En mi caso quedaría algo así:

```bash
    sudo docker run -p 8080:80 saulenriquemr/miprimeraapi:1.0
```

Ahora si accedemos a la siguiente dirección [http://localhost:8080/api/articulo](http://localhost:8080/api/articulo), nos debería dar el *json* que nos regreso la primera vez.

## Publicando la imagen en DockerHub

Primero debemos iniciar sesión en *Docker* con el siguiente comando:

```bash
    sudo docker login
```

Luego debemos ejecutar el siguiente comando:

```bash
    sudo docker push <TuDockerHubID>/<NombreDelProyecto>:<VersionSiSeEspecifico>
```

En mi caso sería así:

```bash
    sudo docker push saulenriquemr/miprimeraapi:1.0
```

Una vez terminado, podemos revisar el repositorio en [*DockerHub*](https://hub.docker.com/).
