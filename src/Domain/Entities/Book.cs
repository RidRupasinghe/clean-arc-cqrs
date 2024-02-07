using System.ComponentModel.DataAnnotations.Schema;

namespace clean_arc_api.Domain.Entities;

public class Book : BaseAuditableEntity
{
    public required string Title { get; set; }
    public string? ISBN { get; set; }
    [ForeignKey("BookCategory")] public int CategoryId { get; set; }
    public BookCategory? BookCategory { get; set; }
}
