using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace BS_Adoga.Models.ViewModels.CheckOut
{
    public class CheckOutListViewModel
    {
        

        [Required(ErrorMessage = "您必須輸入英文姓氏")]
        [Display(Name = "英文護照名")]
        [StringLength(50)]
        [RegularExpression(@"[a-zA-Z -]*$", ErrorMessage = "僅能有英文大小寫和空白和-符號！")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "您必須輸入英文姓名")]
        [Display(Name = "英文護照姓")]
        [StringLength(50)]
        [RegularExpression(@"[a-zA-Z -]*$", ErrorMessage = "僅能有英文大小寫和空白和-符號！")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "您必須輸入電子郵件")]
        [Display(Name = "電子信箱")]
        [DataType(DataType.EmailAddress, ErrorMessage = "請輸入正確的電子信箱格式")]
        [StringLength(50)]
        public string Email { get; set; }

        [NotMapped]
        [Required]
        [Display(Name = "確再次輸入電子信箱")]
        [DataType(DataType.EmailAddress)]
        [Compare("Email", ErrorMessage = "兩次輸入的電子郵件必須相符！")]
        public string ConfirmEmail { get; set; }

        [Display(Name = "手機號碼")]
        [RegularExpression(@"^\d{4}\-?\d{3}\-?\d{3}$", ErrorMessage = "需為09xx-xxx-xxx格式")]
        public string PhoneNumber { get; set; }

        public string countries { get; set; }

        public string SmokingPreference { get; set; }

        public string BedPreference { get; set; }

        public string ArrivingTime { get; set; }
    }
}