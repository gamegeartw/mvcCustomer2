using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcCustomer2.Models
{
    
    public class AccountModel
    {
        [Display(Name ="編號")]
        public string Id { get; set; }
        [Display(Name ="電子郵件")]
        public string  email { get; set; }
        [Display(Name ="顯示名稱")]
        public string UserName { get; set; }
        [Display(Name ="所屬角色")]
        public IList<string> Roles { get; set; }
    }
}