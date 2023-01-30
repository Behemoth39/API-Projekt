using System.ComponentModel.DataAnnotations;

namespace westcoasteducation.api.Models;

public class StudentModel
{
    [Key]
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string? SocialSecurity { get; set; }
    public string? StudentEmail { get; set; }
}
