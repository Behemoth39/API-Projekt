using System.ComponentModel.DataAnnotations;

namespace westcoasteducation.api.Models;

public class StudentModel
{
    [Key]
    public int Id { get; set; }
    public string? SocialSecurity { get; set; }
    public string? StudentEmail { get; set; }

    public ICollection<CourseModel>? Courses { get; set; }
}
