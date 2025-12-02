# Funcionalidad de Geolocalización por IP

## Descripción
Se ha implementado una funcionalidad completa para obtener la geolocalización del usuario basada en su dirección IP usando la API externa de ipapi.com.

## Archivos Creados

### 1. DTO - LocationResponse.cs
**Ubicación:** `Shared/Domain/Model/LocationResponse.cs`

Record que representa la respuesta de la API ipapi.com con las propiedades:
- `Ip`: Dirección IP consultada
- `Region_Name`: Nombre de la región/estado
- `Country_Name`: Nombre del país

### 2. Interfaz - IGeoLocationService.cs
**Ubicación:** `Shared/Domain/Services/IGeoLocationService.cs`

Interfaz que define el contrato del servicio de geolocalización con el método:
- `GetLocationAsync(string ip)`: Obtiene información de ubicación para una IP dada

### 3. Servicio - GeoLocationService.cs
**Ubicación:** `Shared/Infrastructure/Services/GeoLocationService.cs`

Implementación del servicio que:
- Usa `HttpClient` para llamar a la API de ipapi.com
- Detecta IPs locales (::1, 127.0.0.1, localhost) y las reemplaza con una IP de prueba (161.185.160.93)
- Maneja errores y registra información en logs
- API Key incluida: `fa7cfc7586d347d5f8338192c1960405`

### 4. Controlador - LocationController.cs
**Ubicación:** `Shared/Interfaces/REST/LocationController.cs`

Controlador REST con:
- **Endpoint:** `GET /api/v1/location`
- **Funcionalidad:** 
  - Obtiene la IP del usuario desde `HttpContext.Connection.RemoteIpAddress`
  - Llama al servicio de geolocalización
  - Devuelve JSON simple: `{ region: "...", country: "..." }`

### 5. Configuración - Program.cs
Se agregó:
```csharp
// HttpClient for GeoLocation Service
builder.Services.AddHttpClient<IGeoLocationService, GeoLocationService>();
```

## Uso desde el Frontend

### Endpoint
```
GET http://localhost:5000/api/v1/location
```

### Respuesta Exitosa (200 OK)
```json
{
  "region": "New York",
  "country": "United States"
}
```

### Respuesta de Error (500 Internal Server Error)
```json
{
  "error": "Unable to retrieve location information"
}
```

## Ejemplo de Uso en JavaScript/TypeScript

```javascript
async function getUserLocation() {
  try {
    const response = await fetch('http://localhost:5000/api/v1/location');
    const data = await response.json();
    
    if (response.ok) {
      console.log(`Usuario ubicado en: ${data.region}, ${data.country}`);
      return data;
    } else {
      console.error('Error al obtener ubicación:', data.error);
      return null;
    }
  } catch (error) {
    console.error('Error de red:', error);
    return null;
  }
}

// Uso
getUserLocation().then(location => {
  if (location) {
    document.getElementById('location').textContent = 
      `${location.region}, ${location.country}`;
  }
});
```

## Notas Importantes

1. **Desarrollo Local:** Cuando ejecutes en localhost, el sistema detectará automáticamente la IP local y usará una IP de prueba para obtener resultados válidos.

2. **Producción:** En producción, la API recibirá la IP real del cliente y devolverá su ubicación real.

3. **API Key:** La clave de acceso de ipapi.com está incluida en el código. Para producción, considera moverla a `appsettings.json`.

4. **Swagger:** El endpoint estará disponible en Swagger UI en `/swagger` para pruebas.

## Prueba el Endpoint

1. Ejecuta el proyecto: `dotnet run`
2. Abre Swagger: `http://localhost:5000/swagger`
3. Busca el endpoint `GET /api/v1/location`
4. Haz clic en "Try it out" y luego "Execute"
5. Deberías ver una respuesta con región y país

## Arquitectura

La implementación sigue el patrón de arquitectura limpia del proyecto:
- **Domain:** Modelos y servicios (interfaces)
- **Infrastructure:** Implementación de servicios externos
- **Interfaces/REST:** Controladores API

## Logs

El servicio registra información útil para debugging:
- IP detectada del cliente
- IP usada para la consulta (incluye si se usó IP de prueba)
- Región y país obtenidos
- Errores si ocurren

