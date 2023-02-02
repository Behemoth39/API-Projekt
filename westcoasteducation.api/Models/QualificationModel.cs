using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace westcoasteducation.api.Models;

public class QualificationModel
{
    [Key]
    public int Id { get; set; }
    public string? Qualification { get; set; }
    public string? Description { get; set; }


    public int TeacherId { get; set; }

    [ForeignKey("TeacherId")]
    public TeacherModel Teacher { get; set; } = new TeacherModel();
}
