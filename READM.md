## descargar de GitHub

git clone https://github.com/SergioValleGarma/UniversidadCleanArquitcture.git



# estructura 
Universidad.API (Controllers, Program.cs)
├── Universidad.Application (Services, DTOs, Mappings)  
├── Universidad.Domain (Entities, Value Objects, Interfaces)
└── Universidad.Infrastructure (DbContext, Repositories, Configurations)


## para crear la bd

# Eliminar migración anterior
Remove-Migration

# Crear nueva migración sin el problema de cascada
Add-Migration InitialCreate

# Actualizar base de datos
Update-Database

# compilar 

revisar la url https://localhost:7012/swagger/index.html