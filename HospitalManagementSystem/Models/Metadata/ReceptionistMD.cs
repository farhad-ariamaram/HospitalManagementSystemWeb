using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models.Metadata
{
    public partial class ReceptionistMD
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "نام پذیرش")]
        [DisplayName("نام پذیرش")]
        [Required(ErrorMessage = "نام پذیرش نباید خالی باشد")]
        [StringLength(10, ErrorMessage = "باید بین 3 تا 10 کاراکتر باشد", MinimumLength = 3)]
        public string Name { get; set; }
    }
}

namespace HospitalManagementSystem.Models.Domain
{
    [MetadataType(typeof(Metadata.ReceptionistMD))]
    public partial class Receptionist
    {

    }
}

