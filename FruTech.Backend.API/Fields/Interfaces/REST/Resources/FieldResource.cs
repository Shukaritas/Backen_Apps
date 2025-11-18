using FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

namespace FruTech.Backend.API.Fields.Interfaces.REST.Resources;

/// <summary>
/// Read resource for Field. Exposes only ID references and task objects.
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
    IReadOnlyList<TaskResource> Tasks
);
