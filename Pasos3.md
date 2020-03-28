# Llaves foraneas, patron de repositorio e inyeccion de dependencias

## Requerimientos

* Estar en un sistema *Linux* (en una distribución basada en *Debian*, *Ubuntu* o *Fedora*).
* Tener *.NetCore* instalado en el sistema, en su última [version LTS](https://dotnet.microsoft.com/download) (al momento la elaboración de esta guía es la versión 3.1).
* Tener *Git* instalado en el sistema.
* Un editor de texto o IDE.
* *Postman* o cualquier herramienta para probar APIs.
* Gestor de Base de Datos.
* Tener instalado EntityFrameworkCore en nuestro proyecto.

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
Todos los pasos hecho en el tema pasado fueron para crear una relacion de 1 a 1

### Relacion de 1 a muchos

### Relacion de muchos a muchos

## Patron repositorio

El patrón de repositorio tiene por objetivo crear una capa de abstracción entre la capa de acceso a los datos y la capa de lógica de negocio de una aplicación. La adición, eliminación, actualización y selección de elementos de esta colección se realiza a través de una serie de métodos sencillos, sin necesidad de tratar con asuntos de la base de datos como conexiones, comandos, cursores o lectores. Ademas su implementación puede ayudar a aislar nuestra aplicación de los cambios en el gestor de datos y a su vez facilita las pruebas de unidad automatizadas o el desarrollo dirigido por pruebas (TDD).

## Inyeccion de dependencias
