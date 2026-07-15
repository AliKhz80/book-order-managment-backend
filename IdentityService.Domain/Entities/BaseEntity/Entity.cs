using System.ComponentModel.DataAnnotations;

namespace IdentityService.Domain.Entities.BaseEntity;

public abstract class Entity<T>
{
    [Key]
    public required T Id { get; set; }
}
