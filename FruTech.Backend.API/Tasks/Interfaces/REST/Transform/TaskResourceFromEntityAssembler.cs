using FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

namespace FruTech.Backend.API.Tasks.Interfaces.REST.Transform;

public static class TaskResourceFromEntityAssembler
{
    public static TaskResource ToResourceFromEntity(Domain.Model.Aggregate.Task entity)
    {
        var fieldName = entity.Field?.Name ?? "Unknown";
        
        return new TaskResource(
            entity.Id,
            entity.FieldId,
            fieldName,
            entity.Description,
            entity.DueDate,
            entity.CreatedDate,
            entity.UpdatedDate
        );
    }
}
