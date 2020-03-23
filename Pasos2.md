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
    dotnet add package Pomelo.EntityFrameworkCore.MySql --version 3.1.1
```
