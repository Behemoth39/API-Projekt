using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace westcoasteducation.api.Models;

public class CourseModel
{
    [Key]
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int TeacherId { get; set; }
    public string? CourseNumber { get; set; }
    public string? CourseTitle { get; set; }
    public CourseStatusEnum? Status { get; set; }
    public DateOnly? CourseStartDate { get; set; }


    [ForeignKey("StudentId")]
    public StudentModel Student { get; set; } = new StudentModel();

    [ForeignKey("TeacherId")]
    public TeacherModel Teacher { get; set; } = new TeacherModel();
}
