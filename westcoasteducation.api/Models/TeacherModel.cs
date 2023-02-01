namespace westcoasteducation.api.Models;

public class TeacherModel : PersonModel
{
    public IList<CourseModel>? Courses { get; set; }
}
