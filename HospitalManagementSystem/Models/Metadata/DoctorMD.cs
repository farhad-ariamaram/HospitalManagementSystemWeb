using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models.Metadata
{
    public partial class DoctorMD
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "نام")]
        [DisplayName("نام")]
        [Required(ErrorMessage = "نام پزشک نباید خالی باشد")]
        [StringLength(30, ErrorMessage = "باید بین 3 تا 30 کاراکتر باشد", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "تخصص")]
        [DisplayName("تخصص")]
        [Required(ErrorMessage = "تخصص پزشک نباید خالی باشد")]
        [StringLength(30, ErrorMessage = "باید بین 3 تا 30 کاراکتر باشد", MinimumLength = 3)]
        public string Special { get; set; }

        [Display(Name = "شماره تماس")]
        [DisplayName("شماره تماس")]
        [Required(ErrorMessage = "شماره تماس پزشک نباید خالی باشد")]
        [StringLength(11, ErrorMessage = "باید 11 کاراکتر باشد", MinimumLength = 11)]
        [RegularExpression("([0-9]+)", ErrorMessage = "فقط عدد وارد کنید")]
        public string PhoneNo { get; set; }

        [Display(Name = "ساختمان")]
        [DisplayName("ساختمان")]
        [ScaffoldColumn(false)]
        public int DepartmentId { get; set; }
    }
}

namespace HospitalManagementSystem.Models.Domain
{
    [MetadataType(typeof(Metadata.DoctorMD))]
    public partial class Doctor
    {

    }
}

