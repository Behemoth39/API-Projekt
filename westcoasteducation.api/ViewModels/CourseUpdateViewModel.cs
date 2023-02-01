using System.ComponentModel.DataAnnotations;

namespace westcoasteducation.api.ViewModels;

public class CourseUpdateViewModel
{
    [Required(ErrorMessage = "Kursnummer måste finnas")]
    public string? CourseNumber { get; set; }

    [Required(ErrorMessage = "Kurstitel måste finnas")]
    public string? CourseTitle { get; set; }

    [Required(ErrorMessage = "Startdatum måste finnas")]
    public DateOnly? CourseStartDate { get; set; }
}