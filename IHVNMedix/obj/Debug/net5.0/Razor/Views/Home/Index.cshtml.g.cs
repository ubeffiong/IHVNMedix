#pragma checksum "D:\Development\IHVNMedix\IHVNMedix\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a4585cd10a25e3d954f5804fedd54e441fce719e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "D:\Development\IHVNMedix\IHVNMedix\Views\_ViewImports.cshtml"
using IHVNMedix;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Development\IHVNMedix\IHVNMedix\Views\_ViewImports.cshtml"
using IHVNMedix.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a4585cd10a25e3d954f5804fedd54e441fce719e", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"367336915c48ca9be8597206f1ff531929c3c14b", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\Development\IHVNMedix\IHVNMedix\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4\">IHVN Medical Diagnosis App</h1>\r\n</div>\r\n<div class=\"text-center\">\r\n    <a");
            BeginWriteAttribute("href", " href=\"", 174, "\"", 214, 1);
#nullable restore
#line 9 "D:\Development\IHVNMedix\IHVNMedix\Views\Home\Index.cshtml"
WriteAttributeValue("", 181, Url.Action("Create", "Patients"), 181, 33, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary\">Register a New Patient</a>\r\n</div>\r\n<br />\r\n<div class=\"text-center\">\r\n    <a");
            BeginWriteAttribute("href", " href=\"", 317, "\"", 359, 1);
#nullable restore
#line 13 "D:\Development\IHVNMedix\IHVNMedix\Views\Home\Index.cshtml"
WriteAttributeValue("", 324, Url.Action("Create", "Encounters"), 324, 35, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary\">Create an Encounter</a>\r\n</div>\r\n<br />\r\n<div class=\"text-center\">\r\n    <a");
            BeginWriteAttribute("href", " href=\"", 459, "\"", 503, 1);
#nullable restore
#line 17 "D:\Development\IHVNMedix\IHVNMedix\Views\Home\Index.cshtml"
WriteAttributeValue("", 466, Url.Action("Create", "Appointments"), 466, 37, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary\">Book Appointment</a>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591