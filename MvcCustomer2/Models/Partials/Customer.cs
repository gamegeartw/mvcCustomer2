using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcCustomer2.Models
{
    [MetadataType(typeof(Customer))]
    public partial class 客戶資料
    {
        public class Customer
        {
            public string 客戶名稱 { get; set; }
            [RegularExpression(@"\d{8,8}", ErrorMessage = "長度錯誤或者是輸入非數字")]
            public string 統一編號 { get; set; }
            public string 電話 { get; set; }
            public string 傳真 { get; set; }
            public string 地址 { get; set; }
            [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", ErrorMessage = "電子郵件格式錯誤!")]
            //電子郵件Regex規格來源:
            //https://msdn.microsoft.com/en-us/library/01escwtf(v=vs.110).aspx
            public string Email { get; set; }
        }
    }
}