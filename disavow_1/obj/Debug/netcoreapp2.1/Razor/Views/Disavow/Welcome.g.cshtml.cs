#pragma checksum "C:\GitHub\VScrawler\disavow_1\Views\Disavow\Welcome.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "09c9c006597568abcafb2eab72aa0d37aff9dd43"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Disavow_Welcome), @"mvc.1.0.view", @"/Views/Disavow/Welcome.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Disavow/Welcome.cshtml", typeof(AspNetCore.Views_Disavow_Welcome))]
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
#line 1 "C:\GitHub\VScrawler\disavow_1\Views\_ViewImports.cshtml"
using disavow_1;

#line default
#line hidden
#line 2 "C:\GitHub\VScrawler\disavow_1\Views\_ViewImports.cshtml"
using disavow_1.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"09c9c006597568abcafb2eab72aa0d37aff9dd43", @"/Views/Disavow/Welcome.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e309c9741032f046bf006c29ec7cd0280a754353", @"/Views/_ViewImports.cshtml")]
    public class Views_Disavow_Welcome : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\GitHub\VScrawler\disavow_1\Views\Disavow\Welcome.cshtml"
  
    ViewData["Title"] = "Welcome";

#line default
#line hidden
            BeginContext(43, 28, true);
            WriteLiteral("\r\n<h2>Welcome</h2>\r\n\r\n<ul>\r\n");
            EndContext();
#line 8 "C:\GitHub\VScrawler\disavow_1\Views\Disavow\Welcome.cshtml"
     for (int i = 0; i < (int)ViewData["NumTimes"]; i++)
    {

#line default
#line hidden
            BeginContext(136, 12, true);
            WriteLiteral("        <li>");
            EndContext();
            BeginContext(149, 19, false);
#line 10 "C:\GitHub\VScrawler\disavow_1\Views\Disavow\Welcome.cshtml"
       Write(ViewData["Message"]);

#line default
#line hidden
            EndContext();
            BeginContext(168, 7, true);
            WriteLiteral("</li>\r\n");
            EndContext();
#line 11 "C:\GitHub\VScrawler\disavow_1\Views\Disavow\Welcome.cshtml"
    }

#line default
#line hidden
            BeginContext(182, 5, true);
            WriteLiteral("</ul>");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
