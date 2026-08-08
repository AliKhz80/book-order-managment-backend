using System.ComponentModel.DataAnnotations;

namespace CatalogService.Domain.Entities.BaseEntity;

public abstract class Entity<T>
{
    [Key]
    public required T Id { get; set; }
}
