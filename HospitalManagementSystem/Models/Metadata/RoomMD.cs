using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models.Metadata
{
    public partial class RoomMD
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "نام ساختمان")]
        [DisplayName("نام ساختمان")]
        [Required(ErrorMessage = "نام اتاق نباید خالی باشد")]
        [StringLength(10, ErrorMessage = "باید بین 3 تا 10 کاراکتر باشد", MinimumLength = 3)]
        public string Location { get; set; }
    }
}

namespace HospitalManagementSystem.Models.Domain
{
    [MetadataType(typeof(Metadata.RoomMD))]
    public partial class Room
    {

    }
}

