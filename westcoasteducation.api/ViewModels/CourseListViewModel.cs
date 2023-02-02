namespace westcoasteducation.api.ViewModels;

public class CourseListViewModel
{
    public string? CourseTitle { get; set; }
    public DateOnly? CourseStartDate { get; set; }
    public string? Teacher { get; set; }
    public IList<StudentListViewModel>? Students { get; set; }

}
