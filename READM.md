## para crear la bd
# Eliminar migración anterior
Remove-Migration

# Crear nueva migración sin el problema de cascada
Add-Migration InitialCreate

# Actualizar base de datos
Update-Database