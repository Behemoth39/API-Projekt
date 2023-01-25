namespace westcoasteducation.api.Models;

public class CourseModel
{
    public int Id { get; set; }
    public string? CourseNumber { get; set; }
    public string? courseTitle { get; set; }
    public DateOnly? CourseStartDate { get; set; }
}
