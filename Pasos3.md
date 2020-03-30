# Llaves foraneas, patron de repositorio e inyeccion de dependencias

## Requerimientos

* Estar en un sistema *Linux* (en una distribución basada en *Debian*, *Ubuntu* o *Fedora*).
* Tener *.NetCore* instalado en el sistema, en su última [version LTS](https://dotnet.microsoft.com/download) (al momento la elaboración de esta guía es la versión 3.1).
* Tener *Git* instalado en el sistema.
* Un editor de texto o IDE.
* *Postman* o cualquier herramienta para probar APIs.
* Gestor de Base de Datos.
* Tener instalado EntityFrameworkCore en nuestro proyecto.

## Configurando

Primero tenemos que instalar el siguiente paquete:

```c#
    dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 3.1.2
```

Despues en nuestro *Startup.cs*, en el metodo *ConfigureServices* lo modificamos y deberia quedar de la siguiente manera:

```c#
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContextPool<GestionArticulosContext>(options =>
            options.UseLazyLoadingProxies()
                .UseMySql(Configuration.
                    GetConnectionString("DefaultConnection")));
        services.AddControllers().AddNewtonsoftJson(options => 
            options.SerializerSettings.ReferenceLoopHandling = 
                Newtonsoft.Json.ReferenceLoopHandling.Ignore);
    }
```

## Agregando llaves foraneas

### Creando una relacion

Para definir una relacion, en nuestro caso de Articulo a Proveedor. En la clase Articulo agregaremos los siguientes atributos:

```c#
    // Este define la columna donde estara la llave foranea
    public int ProveedorID { set; get; }
    // Es objeto sera creado al obtener el articulo
    public virtual Proveedor Proveedor { set; get; }
```

Una vez hecha la relacion podemos hacer nuestra migracion:

```bash
    dotnet ef migrations add RelacionArticuloProveedor
```

Si se desea podemos verificar que la migracion fue hecha correctamente, una vez hecho eso sincronizamos la base de datos

```bash
    dotnet ef database update
```

Esto generara una columna en Articulos con el nombre ProveedorID.

#### Probando

Ejecutamos nuestro proyecto:

```c#
    dotnet run
```

En *Postman* insertamos el siguiente cuerpo de solicitud para registrar un Articulo

```json
    {
	    "Nombre": "Laptop",
	    "Descripcion": "Laptop HP",
	    "Precio": 15000.00,
	    "ProveedorID": 1
    }
```

Esto deberia crearnos un Articulo que hace referencia a Proveedor con id = 1

### Relacion 1 a 1
Todos los pasos hecho en el tema pasado fueron para crear una relacion de 1 a 1.

Si se hace la consulta desde Postman al Articulo recien registrado, deberia devolvermos algo similar a esto:

```json
    /* Este es el objeto relacionado */
    {
    "proveedor": {
        "id": 1,
        "nombre": "CompuSolucion",
        "telefono": "232131"
    },
    "lazyLoader": {},
    "id": 3,
    "nombre": "Laptop",
    "descripcion": "Laptop HP",
    "precio": 15000,
    "fechaRegistro": "2020-03-27T21:34:10.883827",
    "proveedorID": 1
}
```

### Relacion de 1 a muchos

Para declarar una relacion de 1 a muchos, solo se debe de hacer lo siguiente a la clase de Proveedor:

```c#
    public virtual ICollection<Articulo> Articulos { set; get; }
```

Despues de eso, hacemos la migracion y sincronizamos con la base de datos

```bash
    dotnet ef migrations add RelacionArticuloProveedorUnoAMuchos
    dotnet ef database update
```

Ahora si queremos probar, en Postman podemos consultar al Proveedor que le hayamos registrado los Articulos y nos deberia dar algo similiar a lo siguiente:

```c#
    [
        {
            "articulos": [
                {
                    "id": 3,
                    "nombre": "Laptop",
                    "descripcion": "Laptop HP",
                    "precio": 15000.0,
                    "fechaRegistro": "2020-03-27T21:34:10.883827",
                    "proveedorID": 1
                },
                {
                    "id": 4,
                    "nombre": "Laptop",
                    "descripcion": "Laptop HP 2",
                    "precio": 15500.0,
                    "fechaRegistro": "2020-03-29T13:50:30.712347",
                    "proveedorID": 1
                }
            ],
            "id": 1,
            "nombre": "CompuSolucion",
            "telefono": "232131"
        }
    ]
```

Como se puede notar, al obtener el Proveedor, tambien nos da una lista de articulos registrados

### Relacion de muchos a muchos

>Nota: este no es necesario que se sigan estos pasos, esto solo tiene como proposito la demostracion de una relacion de muchos a muchos.

Suponiendo que entre Articulos y Proveedores hubiera una relacion de muchos a muchos.

Primero tenemos que hacer una clase que va representar a nuestra tabla intermedia, esta quedaria de la siguiente manera:

```c#
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
```

Despues de eso en nuestra clase de Proveedor y Articulo agregamos el siguiente atributo:

```c#
    public virtual ICollection<ArticuloTieneProveedor> ArticuloTieneProveedores { set; get; }
```

>Nota: Puede tener el mismo nombre en las dos clases o distinto, lo importante es que sea un dato de tipo *virtual ICollection*

Una vez hecho eso, solo debemos agregar este atributo a nuestra clase *GestionArticulosContext*:

```c#
    public DbSet<ArticuloTieneProveedor> ArticulosTienenProveedores { set; get; }
```

Y por ultimo, debemos hacer la migracion y sincronizar la base de datos

```bash
    dotnet ef migrations add RelacionArticuloProveedorMuchosAMuchos
    dotnet ef database update
```

## Patron repositorio

El patrón de repositorio tiene por objetivo crear una capa de abstracción entre la capa de acceso a los datos y la capa de lógica de negocio de una aplicación. La adición, eliminación, actualización y selección de elementos de esta colección se realiza a través de una serie de métodos sencillos, sin necesidad de tratar con asuntos de la base de datos como conexiones, comandos, cursores o lectores. Ademas su implementación puede ayudar a aislar nuestra aplicación de los cambios en el gestor de datos y a su vez facilita las pruebas de unidad automatizadas o el desarrollo basado en pruebas (TDD).

Para implementar el patron repositorio solo hace falta hacer lo siguiente:

Crear una carpeta donde se alojaran todas las *Interfaces* y sus implementaciones.

```bash
    mkdir Repositories
```

En mi caso lo llamare a esta carpeta *Repositories*

En esta carpeta vamos a crear nuestra primera interfaz, la cual voy a llamar *IArticuloRepository* y en esta interfaz agregaremos los metodos que necesitaremos, en este ejemplo solo agregare el metodo de obtener todos y obtener por Id:

```c#
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
```

Despues de eso, creamos la clase que va a implementar nuestra interfaz, en este caso la llamare *ArticuloRepository* y deberia de verse de la siguiente manera:

```c#
    using MiPrimeraApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace MiPrimeraApi.Repositories
    {
        public class ArticuloRepository : IArticuloRepository
        {
            private readonly GestionArticulosContext _contexto;

            public ArticuloRepository(GestionArticulosContext contexto) {
                _contexto = contexto;
            }

            public List<Articulo> ObtenerTodos()
            {
                var articulos = _contexto.Articulos.ToList();
                return articulos;
            }

            public Articulo ObtenerPorId(int id)
            {
                var articulo = _contexto.Articulos.FirstOrDefault(x => x.Id == id);
                return articulo;
            }
        }
    }
```

Una vez implementados los repositorios, solo hace falta aplicar inyeccion de dependencias en nuestros controladores para que se puedan ocupar.

## Inyeccion de dependencias

La inyección de dependencias es un patrón de diseño orientado a objetos, en el que se suministran objetos a una clase en lugar de ser la propia clase la que cree dichos objetos. Esos objetos cumplen contratos que necesitan nuestras clases para poder funcionar (de ahí el concepto de dependencia). Nuestras clases no crean los objetos que necesitan, sino que se los suministra otra clase *contenedora* que inyectará la implementación deseada a nuestro contrato.

Al momento, ya hemos creado una inyeccion de dependencias, la cual es nuestro *GestionArticulosContext*. Como se puede notar siempre que hemos ocupado *GestionArticulosContext* jamas se ha creado una instancia de esta, solo llega como parametro, esto es porque en nuestro metodo *ConfigureServices* en *Startup.cs* hemos declarado que aplique la inyeccion de dependencias con las lineas

```c#
    services.AddDbContextPool<GestionArticulosContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseMySql(Configuration.
                        GetConnectionString("DefaultConnection")));
```

Ahora, solo falta ocupar nuestras interfaces de repositorio como contratos para que sean inyectadas en nuestros controladores, para hacer esto se debe hacer lo siguiente:

En *Startup.cs*, agregamos el siguiente namespace:

```c#
    using MiPrimeraApi.Repositories;
```

Despues, en el metodo *ConfigureServices*, antes de *services.AddControllers()....* agregamos lo siguiente:

```c#
    // Solo es agregar servies.AddScoped<Interfaz, Implementacion>();
    services.AddScoped<IArticuloRepository, ArticuloRepository>();
```

Ahora, para ocupar el repositorio en nuestro controlado se tiene que hacer lo siguiente:

Agregar el siguiente namespace:

```c#
    using MiPrimeraApi.Repositories;
```

Agregar como atributo la interfaz del repositorio

```c#
    private readonly IArticuloRepository _repoArticulo;
```

Y ahora en el constructor del controlador lo dejamos de la siguiente manera:

```c#
    public ArticuloController(IArticuloRepository repoArticulo)
    {
        _repoArticulo = repoArticulo;
    }
```

Por ultimo, para acceder a estos solo se tiene que ocupar de la siguiente manera:

```c#
    [HttpGet]
    [Route("")]
    public IActionResult Obtener()
    {
        var articulos = _repoArticulo.ObtenerTodos();
        return Ok(articulos);
    }
```

```c#
    [HttpGet]
    [Route("{id}")]
    public IActionResult ObtenerPorId(int id)
    {
        var articulo = _repoArticulo.ObtenerPorId(id);
        return Ok(articulo);
    }
```

Para comprobar, si se desea se puede probar en Postman y deberia entregar los mismos resultados que cuando no ocupaba inyeccion de dependencias.
