﻿using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Interfaces.REST.Resources;
using FruTech.Backend.API.Tasks.Interfaces.REST.Transform;
using FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

namespace FruTech.Backend.API.Fields.Interfaces.REST.Transform;

public static class FieldResourceFromEntityAssembler
{
    public static FieldResource ToResource(Field entity)
    {
        var progressHistoryId = entity.ProgressHistory?.Id;
        var tasks = entity.Tasks?.Select(t => TaskResourceFromEntityAssembler.ToResourceFromEntity(t)).ToList() ?? new List<TaskResource>();

        var imageUrl = BuildImageUrl(entity);
        
        // Map ProgressHistory data if available
        ProgressHistoryResource? progressHistoryResource = null;
        if (entity.ProgressHistory != null)
        {
            progressHistoryResource = new ProgressHistoryResource(
                entity.ProgressHistory.Id,
                entity.ProgressHistory.Watered,
                entity.ProgressHistory.Fertilized,
                entity.ProgressHistory.Pests
            );
        }
        
        // Check if CropField exists and is not soft-deleted
        var hasCropField = entity.CropField != null && !entity.CropField.Deleted;
        
        // Extract crop data only if CropField is not deleted
        var cropFieldId = hasCropField ? entity.CropField?.Id : null;
        var cropName = hasCropField ? entity.CropField?.Crop ?? string.Empty : string.Empty;
        var soilType = hasCropField ? entity.CropField?.SoilType ?? string.Empty : string.Empty;
        var sunlight = hasCropField ? entity.CropField?.Sunlight ?? string.Empty : string.Empty;
        var watering = hasCropField ? entity.CropField?.Watering ?? string.Empty : string.Empty;
        var plantingDate = hasCropField ? entity.CropField?.PlantingDate?.ToString("yyyy-MM-dd") ?? string.Empty : string.Empty;
        var harvestDate = hasCropField ? entity.CropField?.HarvestDate?.ToString("yyyy-MM-dd") ?? string.Empty : string.Empty;
        var daysSincePlanting = hasCropField && entity.CropField?.PlantingDate != null 
            ? ((int)(DateTime.UtcNow - entity.CropField.PlantingDate.Value).TotalDays).ToString()
            : string.Empty;
        var cropStatus = hasCropField ? entity.CropField?.Status.ToString() ?? string.Empty : string.Empty;

        return new FieldResource(
            entity.Id,
            entity.UserId,
            imageUrl,
            entity.Name,
            entity.Location,
            entity.FieldSize,
            progressHistoryId,
            progressHistoryResource,
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

