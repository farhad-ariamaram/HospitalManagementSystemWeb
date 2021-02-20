using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models.Metadata
{
    public partial class PatientMD
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "نام")]
        [DisplayName("نام")]
        [Required(ErrorMessage = "نام بیمار نباید خالی باشد")]
        [StringLength(30, ErrorMessage = "باید بین 3 تا 30 کاراکتر باشد", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "شماره تماس")]
        [DisplayName("شماره تماس")]
        [Required(ErrorMessage = "شماره تماس بیمار نباید خالی باشد")]
        [StringLength(11, ErrorMessage = "باید 11 کاراکتر باشد", MinimumLength = 11)]
        [RegularExpression("([0-9]+)", ErrorMessage = "فقط عدد وارد کنید")]
        public string PhoneNo { get; set; }

        [Display(Name = "آدرس")]
        [DisplayName("آدرس")]
        [Required(ErrorMessage = "آدرس بیمار نباید خالی باشد")]
        [StringLength(100, ErrorMessage = "باید بین 10 تا 100 کاراکتر باشد", MinimumLength = 10)]
        public string Address { get; set; }

        [Display(Name = "سن")]
        [DisplayName("سن")]
        [Required(ErrorMessage = "سن بیمار نباید خالی باشد")]
        [Range(0, 150, ErrorMessage = "باید بین 0 تا 150 باشد")]
        public int Age { get; set; }

        [Display(Name = "جنسیت")]
        [DisplayName("جنسیت")]
        [Required(ErrorMessage = "جنسیت بیمار نباید خالی باشد")]
        public bool Gender { get; set; }

        [Display(Name = "پزشک")]
        [DisplayName("پزشک")]
        [ScaffoldColumn(false)]
        public int DoctorId { get; set; }

        [Display(Name = "اتاق")]
        [DisplayName("اتاق")]
        [ScaffoldColumn(false)]
        public int RoomId { get; set; }

        [Display(Name = "پذیرش")]
        [DisplayName("پذیرش")]
        [ScaffoldColumn(false)]
        public int ReceptionistId { get; set; }

    }
}

namespace HospitalManagementSystem.Models.Domain
{
    [MetadataType(typeof(Metadata.PatientMD))]
    public partial class Patient
    {

    }
}

