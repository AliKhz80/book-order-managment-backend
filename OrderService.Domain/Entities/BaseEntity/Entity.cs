using System.ComponentModel.DataAnnotations;

namespace OrderService.Domain.Entities.BaseEntity;

public abstract class Entity<T>
{
    [Key]
    public T Id { get; set; } = default!;
}
