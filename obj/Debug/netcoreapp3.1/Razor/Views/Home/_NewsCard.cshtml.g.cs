#pragma checksum "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Home\_NewsCard.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1759591c227d609896456848e483eda065ed1ac5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home__NewsCard), @"mvc.1.0.view", @"/Views/Home/_NewsCard.cshtml")]
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
#nullable restore
#line 1 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Home\_NewsCard.cshtml"
using Beruwala_Mirror.Models.Extensions;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1759591c227d609896456848e483eda065ed1ac5", @"/Views/Home/_NewsCard.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a2393882dc5344e9946d8de621b629c415a1220d", @"/Views/_ViewImports.cshtml")]
    public class Views_Home__NewsCard : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Beruwala_Mirror.Models.News.NewsModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"card size\" >\r\n    <img");
            BeginWriteAttribute("src", " src=\"", 124, "\"", 266, 1);
#nullable restore
#line 5 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Home\_NewsCard.cshtml"
WriteAttributeValue("", 130, Url.Action("Thumbnail",new {filename = "News/" +@Model.CreatedDate.ToString("dd-MM-yyyy") + @"/" + @Model.Id + @"/" + @Model.MainImg }), 130, 136, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card-img-top\", style=\" max-height: 200px; height: auto\" alt=\"...\">\r\n\r\n    <div class=\"card-body\">\r\n        <h5 class=\"card-title\">");
#nullable restore
#line 8 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Home\_NewsCard.cshtml"
                          Write(Html.DisplayFor(m => m.Heading));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n        <p class=\"card-text\">\r\n            ");
#nullable restore
#line 10 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Home\_NewsCard.cshtml"
       Write(Html.Raw(Html.Encode(Model.ShortBody).Replace("\n", "<br />")));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n        ");
#nullable restore
#line 11 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Home\_NewsCard.cshtml"
   Write(Html.ActionLink("Read ...","Details","News", new { Id = Model.Id}, new {@class = "btn btn-link"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Beruwala_Mirror.Models.News.NewsModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
