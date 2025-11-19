# Instrucciones de Migración de Base de Datos - Field Module Refactoring

## Cambios Realizados

### 1. **Entidad Field.cs**
- ✅ **ELIMINADA** la propiedad `ImageUrl` de la entidad
- ✅ Mantenidas las propiedades `ImageContent` (byte[]) y `ImageContentType` (string)

### 2. **CreateFieldCommand.cs**
- ✅ Ya no incluye `ImageUrl`
- ✅ Mantiene `ImageContent` e `ImageContentType`

### 3. **FieldCommandService.cs**
- ✅ Eliminada la asignación de `ImageUrl` al crear Field

### 4. **FieldResource.cs (DTO de Salida)**
- ✅ **MANTIENE** `ImageUrl` para el frontend (será calculada como Data URI)
- ✅ **AGREGADAS** nuevas propiedades enriquecidas del CropField:
  - `CropName`
  - `SoilType`
  - `Sunlight`
  - `Watering`
  - `PlantingDate`
  - `HarvestDate`
  - `DaysSincePlanting`
  - `CropStatus`

### 5. **FieldResourceFromEntityAssembler.cs**
- ✅ Genera `ImageUrl` como Data URI (Base64) desde `ImageContent`
- ✅ Mapea todas las propiedades del `CropField` asociado
- ✅ Calcula `DaysSincePlanting` automáticamente

### 6. **FieldQueryService.cs**
- ✅ Simplificado para usar el assembler directamente
- ✅ Eliminadas dependencias innecesarias (TaskRepository, ProgressHistoryRepository, CropFieldRepository)

### 7. **FieldRepository.cs**
- ✅ `FindByIdAsync` ahora incluye `.Include(f => f.CropField)`, `.Include(f => f.Tasks)`, `.Include(f => f.ProgressHistory)`
- ✅ `FindByUserIdAsync` ahora incluye las mismas relaciones con `.Include()`

### 8. **AppDbContext.cs**
- ✅ Eliminado el mapeo de `ImageUrl` (columna `image_url` será removida de la BD)

---

## 🔧 Pasos para Actualizar la Base de Datos

### Opción A: Usando Migraciones de EF Core (Recomendado para Producción)

1. **Agregar una nueva migración**:
   ```powershell
   dotnet ef migrations add RemoveImageUrlFromField --project FruTech.Backend.API
   ```

2. **Revisar la migración generada**:
   - Verifica que la migración elimine la columna `image_url` de la tabla `fields`
   - Verifica que mantenga las columnas `image_content` e `image_content_type`

3. **Aplicar la migración a la base de datos**:
   ```powershell
   dotnet ef database update --project FruTech.Backend.API
   ```

### Opción B: Eliminar y Recrear la BD (Solo para Desarrollo)

Si estás usando `EnsureCreated()` en `Program.cs` y estás en desarrollo con datos de prueba:

1. **Eliminar la base de datos actual**:
   ```sql
   DROP DATABASE frutech_database;
   ```

2. **Ejecutar la aplicación**:
   ```powershell
   dotnet run --project FruTech.Backend.API
   ```
   - La base de datos se recreará automáticamente con el esquema actualizado (sin la columna `image_url`)

---

## 📋 Verificación Post-Migración

1. **Verificar el esquema de la tabla `fields`**:
   ```sql
   DESCRIBE fields;
   ```
   - Debe **NO** contener la columna `image_url`
   - Debe contener `image_content` (LONGBLOB) e `image_content_type` (varchar(100))

2. **Probar el endpoint POST /api/v1/Fields**:
   - Enviar multipart/form-data con una imagen
   - Verificar que la respuesta JSON incluya:
     - `imageUrl` como Data URI (data:image/jpeg;base64,...)
     - Propiedades del cultivo si existe un `CropField` asociado

3. **Probar el endpoint GET /api/v1/Fields/{id}**:
   - Verificar que devuelva todas las propiedades enriquecidas del `CropField`
   - Verificar que `ImageUrl` sea una Data URI válida o string vacío

---

## 🎯 Beneficios de esta Refactorización

- ✅ **Limpieza de BD**: Eliminada columna redundante `image_url`
- ✅ **Datos enriquecidos**: El frontend recibe todos los datos del cultivo sin hacer llamadas adicionales
- ✅ **Performance**: Reducidas las queries con `.Include()` en el repositorio
- ✅ **Mantenibilidad**: Código más simple y assembler centralizado
- ✅ **Compatibilidad**: El frontend sigue usando `ImageUrl` sin cambios (Data URI automático)

---

## ⚠️ Notas Importantes

- La propiedad `ImageUrl` **NO** existe más en la entidad `Field`, solo en el DTO `FieldResource`
- `ImageUrl` se calcula dinámicamente desde `ImageContent` al mapear con el assembler
- Si `ImageContent` es nulo, `ImageUrl` será un string vacío
- El frontend puede seguir usando `ImageUrl` directamente en etiquetas `<img src="...">`

