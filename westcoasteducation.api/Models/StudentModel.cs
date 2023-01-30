using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace westcoasteducation.api.Models;

public class StudentModel
{
    [Key]
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string? SocialSecurity { get; set; }
    public string? StudentEmail { get; set; }

    [ForeignKey("CourseId")]
    public CourseModel Course { get; set; } = new CourseModel();
}
