using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcCustomer2.Models
{
    
    public class AccountModel
    {
        [Display(Name ="使用者GUID")]
        public string Id { get; set; }
        [Display(Name ="電子郵件")]
        public string Email { get; set; }
        [Display(Name ="顯示名稱")]
        public string UserName { get; set; }
        [Display(Name = "所屬角色")]
        public IList<string> UserRoles { get; set; }
        [Display(Name="密碼")]
        public string Password { get; set; }
    }
}