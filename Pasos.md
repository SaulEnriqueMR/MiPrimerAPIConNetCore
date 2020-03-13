# Mi primer API con .Net Core

## Requerimientos

* Estar en un sistema Linux (una distribucion basada en Debian, Ubuntu o Fedora).
* Tener .Net Core instalado en el sistema, en su ultima [version LTS](https://dotnet.microsoft.com/download), al momento la elaboracion de esta guia es la version 3.1.
* Tener Git instalado en el sistema.
* Un editor de texto o IDE.
* Postman o cualquier herramienta para probar APIs

> Para esta guia se utilizara Ubuntu con VSCode

## Configurando entorno de trabajo

Primero necesitamos crear nuestro directorio donde residira nuestro proyecto. Una vez dentro de nuestro nuevo directorio crear repositorio correspondiente:

```bash
    mkdir MiPrimerApi
    cd MiPrimerApi
    git init
```

Luego procederemos a crear nuestro archivo .gitignore y lo llenaremos con las reglas necesarias. Ademas si asi se desea se puede crear el archivo README.

> Sugerencia: Para esta guia se utilizaran las [reglas especificadas en este enlace](https://github.com/dotnet/core/blob/master/.gitignore), pero el/la estudiante es libre ocupar las reglas que el considere.

```bash
    touch .gitignore
    touch README.md
```

Una vez que hayamos puesto nuestras reglas, haremos commit.

> El/la estudiante es libre de escoger si hacer commit por archivo individual o agregar los dos archivos en un mismo commit

```bash
    git add README.md
    git commit -m "Agregado README"
    git add .gitignore
    git commit -m "Agregado archivo .gitignore"
```

Comprobamos que en efecto, tengamos la version 3.1 de .Net Core instalada y procedemos a crear nuestro proyecto

```bash
    dotnet --version
    dotnet new webapi
```
