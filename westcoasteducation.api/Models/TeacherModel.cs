using System.ComponentModel.DataAnnotations;

namespace westcoasteducation.api.Models;

public class TeacherModel
{
    [Key]
    public int Id { get; set; }
    public string? TeacherEmail { get; set; }

    public ICollection<CourseModel>? Courses { get; set; }
}
