using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models.External_Models
{
    public class Settings
    {
        [Display(Name = "نام مدیر")]
        [DisplayName("نام مدیر")]
        [Required(ErrorMessage = "نام مدیر نمی‌تواند خالی باشد")]
        [StringLength(30, ErrorMessage = "باید بین 3 تا 30 کاراکتر باشد", MinimumLength = 3)]
        public String ManagerName { get; set; }

        [Display(Name = "نام بیمارستان")]
        [DisplayName("نام بیمارستان")]
        [Required(ErrorMessage = "نام بیمارستان نمی‌تواند خالی باشد")]
        [StringLength(30, ErrorMessage = "باید بین 3 تا 30 کاراکتر باشد", MinimumLength = 3)]
        public String HospitalName { get; set; }
    }
}