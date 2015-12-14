using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcCustomer.Models
{
    [MetadataType(typeof(BankData))]
    public partial class 客戶銀行資訊
    {
        public class BankData
        {
            public int Id { get; set; }
            [Display(Name = "客戶名稱")]
            public int 客戶Id { get; set; }
            [Required]
            public string 銀行名稱 { get; set; }
            [Required]
            public int 銀行代碼 { get; set; }
            public Nullable<int> 分行代碼 { get; set; }
            [Required]
            public string 帳戶名稱 { get; set; }
            [Required]
            [RegularExpression(@"\d{10,16}", ErrorMessage = "長度不符合")]
            public string 帳戶號碼 { get; set; }
        }
    }
}