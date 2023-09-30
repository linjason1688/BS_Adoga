
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Spatial;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BS_Adoga.Models.ViewModels.MemberLogin
{
    public class MemberRegisterViewModel
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



        [NotMapped]
        [Required]
        [Display(Name = "確認密碼")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "兩次輸入的密碼必須相符！")]
        public string ConfirmPassword { get; set; }



    }
}