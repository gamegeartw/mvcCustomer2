using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc.Html
{
    public static class MyHelper
    {
        //TODO:自訂擴充CheckBox Helper(finish)
        /// <summary>
        /// 自訂擴充CheckBox Helper
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">回傳的欄位名稱</param>
        /// <param name="check">是否已選取</param>
        /// <param name="value">選取的值</param>
        /// <param name="displayName">呈現文字</param>
        /// <returns></returns>
        public static MvcHtmlString MyCheckBox(this HtmlHelper helper,string name,bool check,string value,string displayName)
        {
            return MvcHtmlString.Create(String.Format(@"
                <input type='checkbox' name='{0}' checked='{1}' value='{2}'/>{3}    
                ",name,check,value,displayName));
        }
    }
}