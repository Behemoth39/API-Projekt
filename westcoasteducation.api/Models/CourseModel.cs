using System.ComponentModel.DataAnnotations;

namespace westcoasteducation.api.Models;

public class CourseModel
{
    [Key]
    public int Id { get; set; }
    public CourseStatusEnum? Status { get; set; }
    public string? CourseNumber { get; set; }
    public string? CourseTitle { get; set; }
    public DateOnly? CourseStartDate { get; set; }

    public ICollection<StudentModel>? Students { get; set; }
    public ICollection<TeacherModel>? Teachers { get; set; }
}
