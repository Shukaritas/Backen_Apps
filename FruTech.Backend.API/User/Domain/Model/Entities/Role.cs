namespace FruTech.Backend.API.User.Domain.Model.Entities;

/// <summary>
/// Representa un rol en el sistema.
/// </summary>
public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // Relación muchos a muchos con User
    public ICollection<Aggregates.User> Users { get; set; } = new List<Aggregates.User>();
}

