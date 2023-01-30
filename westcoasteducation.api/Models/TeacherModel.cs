using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace westcoasteducation.api.Models;

public class TeacherModel
{
    [Key]
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string? TeacherEmail { get; set; }

    [ForeignKey("CourseId")]
    public CourseModel Course { get; set; } = new CourseModel();
}
