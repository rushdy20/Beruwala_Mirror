#pragma checksum "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\News\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bfb84143977613d38c9f402d951a0281a36a536c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_News_Index), @"mvc.1.0.view", @"/Views/News/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\_ViewImports.cshtml"
using Beruwala_Mirror;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\_ViewImports.cshtml"
using Beruwala_Mirror.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bfb84143977613d38c9f402d951a0281a36a536c", @"/Views/News/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a2393882dc5344e9946d8de621b629c415a1220d", @"/Views/_ViewImports.cshtml")]
    public class Views_News_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Beruwala_Mirror.Models.News.NewsViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\News\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-group\">\r\n\r\n");
#nullable restore
#line 7 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\News\Index.cshtml"
         foreach (var item in Model.News)
        {


#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"row float-left\" style=\"margin-right: 10px; margin-bottom: 10px;\">\r\n                <div class=\"col-md-5\">\r\n                    ");
#nullable restore
#line 12 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\News\Index.cshtml"
               Write(Html.Partial("_NewsCard", item));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n\r\n            </div>\r\n");
#nullable restore
#line 16 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\News\Index.cshtml"

        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Beruwala_Mirror.Models.News.NewsViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
