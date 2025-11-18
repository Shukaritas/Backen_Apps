using Microsoft.AspNetCore.Mvc.ModelBinding;
using FruTech.Backend.API.Shared.Domain.Model.ValueObjects;

namespace FruTech.Backend.API.CropFields.Domain.Model.Entities;

public partial class CropField : IEntityWithCreatedUpdatedDate
{
    [BindNever]
    public DateTimeOffset? CreatedDate { get; set; }
    
    [BindNever]
    public DateTimeOffset? UpdatedDate { get; set; }

    // Fecha de borrado para soft-delete (opcional, útil para auditoría)
    [BindNever]
    public DateTimeOffset? DeletedDate { get; set; }
}
