using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Interfaces.REST.Resources;
using FruTech.Backend.API.Tasks.Interfaces.REST.Transform;
using FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

namespace FruTech.Backend.API.Fields.Interfaces.REST.Transform;

public static class FieldResourceFromEntityAssembler
{
    public static FieldResource ToResource(Field entity)
    {
        var progressHistoryId = entity.ProgressHistory?.Id;
        var cropFieldId = entity.CropField?.Id;
        var tasks = entity.Tasks?.Select(t => TaskResourceFromEntityAssembler.ToResourceFromEntity(t)).ToList() ?? new List<TaskResource>();

        var imageUrl = BuildImageUrl(entity);
        
        // Extract crop data if available
        var cropName = entity.CropField?.Crop ?? string.Empty;
        var soilType = entity.CropField?.SoilType ?? string.Empty;
        var sunlight = entity.CropField?.Sunlight ?? string.Empty;
        var watering = entity.CropField?.Watering ?? string.Empty;
        var plantingDate = entity.CropField?.PlantingDate?.ToString("yyyy-MM-dd") ?? string.Empty;
        var harvestDate = entity.CropField?.HarvestDate?.ToString("yyyy-MM-dd") ?? string.Empty;
        var daysSincePlanting = entity.CropField?.PlantingDate != null 
            ? ((int)(DateTime.UtcNow - entity.CropField.PlantingDate.Value).TotalDays).ToString()
            : string.Empty;
        var cropStatus = entity.CropField?.Status.ToString() ?? string.Empty;

        return new FieldResource(
            entity.Id,
            entity.UserId,
            imageUrl,
            entity.Name,
            entity.Location,
            entity.FieldSize,
            progressHistoryId,
            cropFieldId,
            tasks,
            cropName,
            soilType,
            sunlight,
            watering,
            plantingDate,
            harvestDate,
            daysSincePlanting,
            cropStatus
        );
    }

    private static string BuildImageUrl(Field entity)
    {
        if (entity.ImageContent != null && entity.ImageContent.Length > 0)
        {
            var base64 = Convert.ToBase64String(entity.ImageContent);
            var contentType = string.IsNullOrWhiteSpace(entity.ImageContentType) ? "image/jpeg" : entity.ImageContentType;
            return $"data:{contentType};base64,{base64}";
        }
        return string.Empty;
    }
}

