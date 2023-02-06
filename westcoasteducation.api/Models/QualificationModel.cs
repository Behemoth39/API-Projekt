using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace westcoasteducation.api.Models;

public class QualificationModel
{
    [Key]
    public int Id { get; set; }
    public string Qualification { get; set; }

    public int TeacherId { get; set; }
    public TeacherModel Teacher { get; set; }
}
