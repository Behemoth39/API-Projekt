namespace westcoasteducation.api.ViewModels;

public class CourseListViewModel
{
    public int Id { get; set; }
    public string? CourseNumber { get; set; }
    public string? CourseTitle { get; set; }
    public DateOnly? CourseStartDate { get; set; }
    public IList<StudentListViewModel>? Students { get; set; }
    public IList<TeacherListViewModel>? Teachers { get; set; }
}