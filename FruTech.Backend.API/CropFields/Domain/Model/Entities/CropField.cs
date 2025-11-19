using System.Text.Json.Serialization;
using FruTech.Backend.API.CropFields.Domain.Model.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruTech.Backend.API.CropFields.Domain.Model.Entities
{
    public partial class CropField
    {
        public int Id { get; set; }
        
        /// <summary>
        /// ID of the field this crop belongs to (1:1 relationship)
        /// </summary>
        public int FieldId { get; set; }
        [ForeignKey("FieldId")]
        public virtual FruTech.Backend.API.Fields.Domain.Model.Entities.Field? Field { get; set; }

        [JsonPropertyName("crop")]
        public string Crop { get; set; } = string.Empty;
        
        [JsonPropertyName("soil_type")]
        public string SoilType { get; set; } = string.Empty;
        
        [JsonPropertyName("sunlight")]
        public string Sunlight { get; set; } = string.Empty;
        
        [JsonPropertyName("watering")]
        public string Watering { get; set; } = string.Empty;
        
        [JsonPropertyName("planting_date")]
        public DateTime? PlantingDate { get; set; }
        
        [JsonPropertyName("harvest_date")]
        public DateTime? HarvestDate { get; set; }
        
        [JsonPropertyName("status")]
        public CropFieldStatus Status { get; set; } = CropFieldStatus.Healthy;

        // Nueva propiedad soft-delete (convención: campo booleano expuesto en JSON como "deleted")
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; } = false;
    }
}
