using System.ComponentModel.DataAnnotations;

namespace CatalogService.Domain.ModelConfigs;

public abstract class Entity<T>
{
    [Key]
    public required T Id { get; set; }
}
