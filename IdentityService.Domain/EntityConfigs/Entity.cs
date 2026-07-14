using System.ComponentModel.DataAnnotations;

namespace IdentityService.Domain.EntityConfigs;

public abstract class Entity<T>
{
    [Key]
    public required T Id { get; set; }
}
