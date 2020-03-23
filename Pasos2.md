# Integrando MySQL a .NetCore

# Requerimientos

* Estar en un sistema *Linux* (en una distribución basada en *Debian*, *Ubuntu* o *Fedora*).
* Tener *.NetCore* instalado en el sistema, en su última [version LTS](https://dotnet.microsoft.com/download) (al momento la elaboración de esta guía es la versión 3.1).
* Tener *Git* instalado en el sistema.
* Un editor de texto o IDE.
* *Postman* o cualquier herramienta para probar APIs.

# Instalacion del Gestor de Base de Datos SQL

Primero tenemos que instalar un gestor de base de datos SQL:

```bash
    sudo apt install mariadb-server
```

> Para este ejemplo se instalara MariaDB

Una vez que se instalo, en nuestro proyecto ejecutaremos el siguiente comando:
