# Nombre del Proyecto

Este proyecto ASP.NET MVC está diseñado para gestionar un consultorio de seguros, proporcionando operaciones CRUD (Crear, Leer, Actualizar, Eliminar) para clientes y seguros, así como la capacidad de establecer relaciones muchos a muchos entre clientes y seguros. Además, ofrece funcionalidades para cargar datos masivos desde archivos Excel o TXT. También permite realizar consultas para conocer qué seguros están asociados a una persona a través de su número de cédula o qué asegurados están asociados a un seguro mediante su código.

## Requisitos del Sistema

- .NET SDK 6.0
- SQL Server (o la base de datos que estás utilizando)

## Configuración del Proyecto

Asegúrate de tener instalado el .NET SDK 6.0 antes de continuar.

### Instalación y Ejecución
1. Clona este repositorio: `git clone https://github.com/Joguisa/ConsultorioSeguros.git`
3. Abre el proyecto en tu entorno de desarrollo (Visual Studio, Rider, etc.).
4. Configura la cadena de conexión en SegurosAPI/appsettings.json.
5. Ejecuta las migraciones para crear la base de datos: dotnet ef database update.
6. Restaura las dependencias: `dotnet restore`
7. Sino usar migraciones crea la database que esta en el repo y ejecuta el comando `Scaffold-DbContext "Server="nombre de tu server"; database="tu database"; Integrated Security=true" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models`
8. Ejecuta la aplicación: dotnet run o f5.
  
## Estructura del Proyecto
Explicación breve de la estructura del proyecto.

- `Controllers/:` Controladores de la aplicación.
- `Models/:` Modelos de datos.
- `Views/:` Vistas de la aplicación.
- `Data/:` Configuración de la base de datos y contexto de Entity Framework.

## Configuración de Base de Datos
Este proyecto utiliza Entity Framework Core con SQL Server como proveedor de base de datos. Asegúrate de actualizar la cadena de conexión en appsettings.json con los detalles correctos de tu base de datos.

## Contribuir
Si deseas contribuir a este proyecto, sigue estos pasos:

- Realiza un fork del proyecto.
- Crea una nueva rama para tus cambios.
- Realiza tus cambios y haz commits.
- Abre un pull request.

## Licencia
Este proyecto está bajo la Licencia ISC.


