namespace ForeignWords.DAL.Entities;

public record WordEntity : IEntity
{
    public Guid Id { get; init; }
    public string Content { get; set; } = string.Empty;
}