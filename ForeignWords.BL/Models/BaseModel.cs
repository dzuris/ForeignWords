namespace ForeignWords.BL.Models;

public abstract record BaseModel : IModel
{
    public Guid Id { get; set; }
}