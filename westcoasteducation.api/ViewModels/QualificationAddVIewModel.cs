using System.ComponentModel.DataAnnotations;

namespace westcoasteducation.api.ViewModels
{
    public class QualificationAddVIewModel
    {
        [Required(ErrorMessage = "Kompetensområde måste finnas")]
        public string Qualification { get; set; }

        [Required(ErrorMessage = "Beskrivning måste finnas")]
        public string Description { get; set; }
    }
}