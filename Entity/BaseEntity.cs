public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.MinValue;
    public DateTime DeletedAt { get; set; } = DateTime.MinValue;
    public bool IsDeleted { get; set; } = false;
    public long Version { get; set; }
}