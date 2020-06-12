#pragma checksum "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "62069bdae7eb2320e456c47988064e7f002d5640"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_News), @"mvc.1.0.view", @"/Views/Admin/News.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"62069bdae7eb2320e456c47988064e7f002d5640", @"/Views/Admin/News.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a2393882dc5344e9946d8de621b629c415a1220d", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_News : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Beruwala_Mirror.Models.Admin.CreateNewsViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
  
    ViewData["Title"] = "Create new News feed";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Create a new news feed</h1>\r\n\r\n");
#nullable restore
#line 8 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
 using (Html.BeginForm("News", "Admin", FormMethod.Post, new { enctype = "multipart/form-data" }))
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
Write(Html.HiddenFor(m => m.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 12 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
   Write(Html.LabelFor(m => m.Heading));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 13 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
   Write(Html.TextBoxFor(m => m.Heading, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 16 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
   Write(Html.LabelFor(m => m.NewsBody));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 17 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
   Write(Html.TextAreaFor(m => m.NewsBody, new { rows = "10", @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 20 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
   Write(Html.LabelFor(m => m.MainImage));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 21 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
   Write(Html.TextBoxFor(m => m.MainImage, new { Type = "file" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n    </div>\r\n    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 25 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
   Write(Html.LabelFor(m => m.SubImage1));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 26 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
   Write(Html.TextBoxFor(m => m.SubImage1, new { Type = "file" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 29 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
   Write(Html.LabelFor(m => m.SubImage2));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 30 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
   Write(Html.TextBoxFor(m => m.SubImage2, new { Type = "file" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 33 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
   Write(Html.LabelFor(m => m.SubImage3));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 34 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
   Write(Html.TextBoxFor(m => m.SubImage3, new { Type = "file" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
            WriteLiteral("    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 38 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
   Write(Html.LabelFor(m => m.YouTubLink));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 39 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
   Write(Html.TextBoxFor(m => m.YouTubLink, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
            WriteLiteral("    <div class=\"form-group\">\r\n        ");
#nullable restore
#line 43 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
   Write(Html.LabelFor(m => m.TopNewsForDays));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 44 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
   Write(Html.TextBoxFor(m => m.TopNewsForDays, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
            WriteLiteral("    <button type=\"submit\" class=\"btn btn-primary\">Submit</button>\r\n    <div class=\"form-group\">\r\n        <small id=\"emailHelp\" class=\"form-text text-muted\">Created By :   ");
#nullable restore
#line 49 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
                                                                     Write(Model.CreatedBy);

#line default
#line hidden
#nullable disable
            WriteLiteral("   Created On :  ");
#nullable restore
#line 49 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"
                                                                                                      Write(Model.CreatedDate);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </small>\r\n    </div>\r\n");
#nullable restore
#line 51 "C:\Learning\Beruwala_Mirror\Beruwala.Mirror\Beruwala_Mirror\Views\Admin\News.cshtml"


}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Beruwala_Mirror.Models.Admin.CreateNewsViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
