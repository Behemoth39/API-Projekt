using System.ComponentModel.DataAnnotations;

namespace westcoasteducation.api.Models;

public class CourseModel
{
    [Key]
    public int Id { get; set; }
    public CourseStatusEnum Status { get; set; }
    public string? CourseNumber { get; set; }
    public string? courseTitle { get; set; }
    public DateOnly? CourseStartDate { get; set; }
}