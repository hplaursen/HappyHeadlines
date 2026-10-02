namespace DraftService.Models;

public class Draft
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
    public DateTime PublishedAt { get; set; }
}