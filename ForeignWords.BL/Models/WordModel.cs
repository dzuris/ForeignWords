using ForeignWords.Common;

namespace ForeignWords.BL.Models;

public record WordModel(
    string Content
) : BaseModel
{
    public string Content { get; set; } = Content;

    public static WordModel Empty => new(string.Empty);
}