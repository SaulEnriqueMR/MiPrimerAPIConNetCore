# Integrando un Sistema Gestor de Base de Datos a .NetCore

# Requerimientos

* Estar en un sistema *Linux* (en una distribución basada en *Debian*, *Ubuntu* o *Fedora*).
* Tener *.NetCore* instalado en el sistema, en su última [version LTS](https://dotnet.microsoft.com/download) (al momento la elaboración de esta guía es la versión 3.1).
* Tener *Git* instalado en el sistema.
* Un editor de texto o IDE.
* *Postman* o cualquier herramienta para probar APIs.

# Instalacion del Gestor de Base de Datos SQL

## Instalacion del Gestor de Base de Datos

Primero tenemos que instalar un gestor de base de datos SQL:

```bash
    sudo apt install mariadb-server
```

> Para este ejemplo se instalara MariaDB

Una vez se instale nuestro Gestor de Base de Datos, ejecutamos las siguientes sentencias *SQL*:

```sql
    DROP USER IF EXISTS gestionarticulos@localhost;
    CREATE USER IF NOT EXISTS gestionarticulos@localhost IDENTIFIED BY 'gestionarticulos';

    DROP DATABASE IF EXISTS gestionarticulos;
    CREATE DATABASE IF NOT EXISTS gestionarticulos DEFAULT CHARACTER SET utf8;

    GRANT ALL PRIVILEGES ON gestionarticulos.* TO gestionarticulos@localhost;
    FLUSH PRIVILEGES;
```

Para simpleza de esta guia ocuparemos el mismo nombre para usuario, nombre de la base de datos y contrasena.

## Instalacion de EntityFrameworkCore

En nuestro proyecto ejecutamos el siguiente comando:

```bash
    dotnet tool install --global dotnet-ef
    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet add package Pomelo.EntityFrameworkCore.MySql --version 3.1.1
    dotnet add package Microsoft.EntityFrameworkCore.Proxies --version 3.1.3
```

### *ConnectionStrings*

En nuestro *appsettings.Development.json* introducimos las siguientes lineas

```json
    "ConnectionStrings": {
        "DefaultConnection": "server=localhost;database=gestionarticulos;uid=gestionarticulos;password=gestionarticulos"
    },
```

> NOTA: Esta *ConnectionString* solo servira para el el perfil de desarrollo (el que se ejecuta con: *dotnet run*), para el despliegue el *ConnectionString* debe ir en *appsettings.json*

### Creando un DBContext

En nuestra carpeta de Models, crearemos la clase *GestionArticulosContext*, esta deberia verse algo asi:

```c#
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace MiPrimeraApi.Models
    {
        public class GestionArticulosContext : DbContext
        {
            public GestionArticulosContext(DbContextOptions<GestionArticulosContext> opciones) : base(opciones) { }

            // Este es el mapeo objeto-base de datos
            DbSet<Articulo> Articulos { set; get; }
        }
    }

```

### Obteniendo conexion

Agregamos las siguientes dependencias: 

```c#
    using Microsoft.EntityFrameworkCore;
```

En nuestro archivo *Startup.cs* en el metodo *ConfigureServices* agregamos las siguientes lineas al inicio de nuestro metodo:

```c#
    services.AddDbContextPool<GestionArticulosContext>(options =>
                        options.UseLazyLoadingProxies()
                            .UseMySql(Configuration.
                                GetConnectionString("DefaultConnection")));
```

### Haciendo la primera migracion

Las migraciones se pueden ver similar a un *commit*, donde todos los cambios que se efectuen en nuestro *GestionarArticulosContext* asi como todas las clases que se agreguen a un *DbSet* se veran reflejadas en la migracion.

Para hacer una migracion se ocupa el siguiente comando:

```bash
    dotnet ef migrations add CreacionArticulo
```

Este comando creara una carpeta Migrations con varias clase, si accedemos al archivo *###_CreacionArticulo.cs*, se puede observar que se define la creacion de la tabla *Articulos*.

Una vez que hemos hecho nuestra migracion, solo queda actualizar con la base de datos, para eso se ejecuta el siguiente comando

```bash
    dotnet ef database update
```

Una vez ejecutado ese comando, si se desea se puede observar en el gestor de base de datos que se creo esta tabla.

> NOTA: Al igual que con los commits, las migraciones deben de ser pequenas para facilitar su control

