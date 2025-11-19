﻿using FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

namespace FruTech.Backend.API.Fields.Interfaces.REST.Resources;

/// <summary>
/// Read resource for Field. Exposes ID references, task objects, and enriched crop data.
/// </summary>
public record FieldResource(
    int Id,
    int UserId,
    string ImageUrl,
    string Name,
    string Location,
    string FieldSize,
    int? ProgressHistoryId,
    int? CropFieldId,
    IReadOnlyList<TaskResource> Tasks,
    string CropName,
    string SoilType,
    string Sunlight,
    string Watering,
    string PlantingDate,
    string HarvestDate,
    string DaysSincePlanting,
    string CropStatus
);
