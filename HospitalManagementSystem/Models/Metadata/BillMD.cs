using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models.Metadata
{
    public partial class BillMD
    {

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "مبلغ")]
        [DisplayName("مبلغ")]
        [Required(ErrorMessage = "مبلغ نباید خالی باشد")]
        [RegularExpression("([0-9]+)", ErrorMessage = "فقط عدد وارد کنید")]
        public string Amount { get; set; }

        [Display(Name = "شماره قبض")]
        [DisplayName("شماره قبض")]
        [Required(ErrorMessage = "شماره قبض نباید خالی باشد")]
        public string BillNo { get; set; }

        [Display(Name = "بیمار")]
        [DisplayName("بیمار")]
        [ScaffoldColumn(false)]
        public int PatientId { get; set; }

        [Display(Name = "پذیرش")]
        [DisplayName("پذیرش")]
        [ScaffoldColumn(false)]
        public int ReceptionistId { get; set; }
    }
}

namespace HospitalManagementSystem.Models.Domain
{
    [MetadataType(typeof(Metadata.BillMD))]
    public partial class Bill
    {

    }
}

