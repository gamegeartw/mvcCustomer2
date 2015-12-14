using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcCustomer.Models
{
    [MetadataType(typeof(Contact))]
    public partial class 客戶聯絡人
    {
        public class Contact
        {
            [Required]
            [Display(Name = "編號")]
            public int Id { get; set; }

            [Display(Name = "客戶名稱")]
            public int 客戶Id { get; set; }

            [Required]
            public string 職稱 { get; set; }

            [Required]
            public string 姓名 { get; set; }

            [Display(Name = "電子郵件信箱")]
            [Required]
            public string Email { get; set; }

            public string 手機 { get; set; }

            public string 電話 { get; set; }
        }
    }
}