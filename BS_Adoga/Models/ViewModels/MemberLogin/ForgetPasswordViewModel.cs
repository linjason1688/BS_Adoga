using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.MemberLogin
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "您必須輸入電子郵件，這樣才能找到您的密碼")]
        [Display(Name = "電子信箱")]
        [DataType(DataType.EmailAddress, ErrorMessage = "請輸入正確的電子信箱格式")]
        [StringLength(256)]
        public string Email { get; set; }

    }
}