using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.MemberLogin
{
    public class MemberLoginViewModel
    {
        [Required(ErrorMessage = "您必須輸入電子郵件，此Email為後續您登入的帳號")]
        [Display(Name = "電子信箱")]
        [DataType(DataType.EmailAddress, ErrorMessage = "請輸入正確的電子信箱格式")]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密碼的長度需再6~20個字元內！")]
        //(?!.*[^\x21-\x7e])不允許特殊符號或數字英文字母以外的東西輸入
        //(?=.*[\W])至少有一個特殊符號
        //(?=.*[a-zA-Z])大小寫至少各一
        //(?=.*\d)0-9的數字各一
        [RegularExpression(@"^(?!.*[^\x21-\x7e])(?=.*[\W])(?=.*[a-zA-Z])(?=.*\d).*$", ErrorMessage = "密碼需要包含最少一位的大小寫英文字母與數字與一個以上的特殊符號")]
        public string Password { get; set; }

        [Display(Name = "記得我")]
        public bool Remember { get; set; }
    }
}