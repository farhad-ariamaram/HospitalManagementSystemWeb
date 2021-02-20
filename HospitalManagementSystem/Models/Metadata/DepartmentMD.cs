using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models.Metadata
{
    public partial class DepartmentMD
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "نام ساختمان")]
        [DisplayName("نام ساختمان")]
        [Required(ErrorMessage = "نام ساختمان نباید خالی باشد")]
        [StringLength(30, ErrorMessage = "باید بین 3 تا 30 کاراکتر باشد", MinimumLength = 3)]
        public string Name { get; set; }
    }
}

namespace HospitalManagementSystem.Models.Domain
{
    [MetadataType(typeof(Metadata.DepartmentMD))]
    public partial class Department
    {

    }
}

