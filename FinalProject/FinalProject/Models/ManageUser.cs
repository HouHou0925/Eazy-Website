using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

//會員管理 顯示列表用 和 編輯會員  用的


namespace FinalProject.Models
{
    public class ManageUser
    {
        [Required] //必要
        public string Id { get; set; }

        [Required] //必要
        [StringLength(256, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 1)] //字元長度1~256
        [Display(Name = "暱稱")] 
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }
    }
}