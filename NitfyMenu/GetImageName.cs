using NitfyMenu.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NitfyMenu
{
    public class ImageName
    {
        public static string Get()
        {
            if (string.IsNullOrEmpty(Breadcrumb.curPage))
                return "premium/boxed-bg/abstract/bg/1.jpg";
            else
                return $"premium/boxed-bg/abstract/bg/{ Breadcrumb.curPage.Replace("Page", "")}.jpg";
        }
    }
}
