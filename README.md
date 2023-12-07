# Consultorio de Seguros ASP.NET MVC

Este proyecto ASP.NET MVC está diseñado para gestionar un consultorio de seguros, proporcionando operaciones CRUD (Crear, Leer, Actualizar, Eliminar) para clientes y seguros, así como la capacidad de establecer relaciones muchos a muchos entre clientes y seguros. Además, ofrece funcionalidades para cargar datos masivos desde archivos Excel o TXT. También permite realizar consultas para conocer qué seguros están asociados a una persona a través de su número de cédula o qué asegurados están asociados a un seguro mediante su código.

## Requisitos del Sistema

- .NET SDK 6.0
- SQL Server (o la base de datos que estás utilizando)

## Configuración del Proyecto

Asegúrate de tener instalado el .NET SDK 6.0 antes de continuar.

## Funcionalidades Destacadas
1. **CRUD de Clientes:**
    - Agregar, ver, actualizar y eliminar información de clientes.
2. **CRUD de Seguros:**
    - Realizar operaciones CRUD en la información de seguros.
3. **Relación Muchos a Muchos:**
    - Asociar clientes con seguros y viceversa.
4. **Carga Masiva de Datos:**
    - Cargar datos masivos en clientes desde un archivo Excel o TXT.
5. **Consultas Específicas:**
    - Consultar los seguros asociados a un cliente.
    - Consultar los clientes asociados a un seguro.

## Estructura del Proyecto
- **Controllers:** Contiene controladores que manejan las solicitudes HTTP y gestionan las operaciones CRUD.
- **Models:** Define las entidades del dominio, incluyendo Cliente, Seguro, y SegurosCliente para la relación muchos a muchos.
- **Views:** Contiene vistas que definen la interfaz de usuario del sistema.
- **wwwroot:** Almacena archivos estáticos, como hojas de estilo CSS y scripts JavaScript.

### Instalación y Ejecución
1. Clona este repositorio: `git clone https://github.com/Joguisa/ConsultorioSeguros.git`
3. Abre el proyecto en tu entorno de desarrollo (Visual Studio, Rider, etc.).
4. Configura la cadena de conexión en SegurosAPI/appsettings.json.
5. Ejecuta las migraciones para crear la base de datos: dotnet ef database update.
6. Restaura las dependencias: `dotnet restore`
7. Sino usar migraciones crea la database que esta en el repo y ejecuta el comando `Scaffold-DbContext "Server="nombre de tu server"; database="tu database"; Integrated Security=true" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models`
8. Ejecuta la aplicación: dotnet run o f5.

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


