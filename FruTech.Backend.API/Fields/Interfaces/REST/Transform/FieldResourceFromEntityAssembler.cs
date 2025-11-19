using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Interfaces.REST.Resources;
using FruTech.Backend.API.Tasks.Interfaces.REST.Transform;
using FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

namespace FruTech.Backend.API.Fields.Interfaces.REST.Transform;

public static class FieldResourceFromEntityAssembler
{
    public static FieldResource ToResource(Field entity)
    {
        // Extract IDs without loading heavy collections
        var progressHistoryId = entity.ProgressHistory?.Id;
        var cropFieldId = entity.CropFieldId; // explicitly mapped in AppDbContext
        var tasks = entity.Tasks?.Select(t => TaskResourceFromEntityAssembler.ToResourceFromEntity(t)).ToList() ?? new List<TaskResource>();

        var imageUrl = BuildImageUrl(entity);

        return new FieldResource(
            entity.Id,
            entity.UserId,
            imageUrl,
            entity.Name,
            entity.Location,
            entity.FieldSize,
            progressHistoryId,
            cropFieldId,
            tasks
        );
    }

    public static FieldResource ToResource(Field entity, IReadOnlyList<TaskResource> tasks, int? progressHistoryId)
    {
        var imageUrl = BuildImageUrl(entity);
        return new FieldResource(
            entity.Id,
            entity.UserId,
            imageUrl,
            entity.Name,
            entity.Location,
            entity.FieldSize,
            progressHistoryId,
            entity.CropFieldId,
            tasks
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
        // fallback to existing ImageUrl (may be empty string)
        return entity.ImageUrl;
    }
}
