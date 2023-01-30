using System.ComponentModel.DataAnnotations;
using westcoasteducation.api.Models;

namespace westcoasteducation.api.ViewModels;

public class CourseAddViewModel
{
    [Required(ErrorMessage = "Kursnummer måste finnas")]
    public string? CourseNumber { get; set; }

    [Required(ErrorMessage = "Kurstitel måste finnas")]
    public string? CourseTitle { get; set; }

    [Required(ErrorMessage = "Startdatum måste finnas")]
    public DateOnly? CourseStartDate { get; set; }
}
