#pragma checksum "F:\Mars\FreelanceWork\Danish USA\DesiClothing4u\DesiClothing4u.UI\Views\Shared\Welcome.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a69355fa8392881d1f6f6405aa3d1669a751685e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Welcome), @"mvc.1.0.view", @"/Views/Shared/Welcome.cshtml")]
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
#line 1 "F:\Mars\FreelanceWork\Danish USA\DesiClothing4u\DesiClothing4u.UI\Views\_ViewImports.cshtml"
using DesiClothing4u.UI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\Mars\FreelanceWork\Danish USA\DesiClothing4u\DesiClothing4u.UI\Views\_ViewImports.cshtml"
using DesiClothing4u.UI.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a69355fa8392881d1f6f6405aa3d1669a751685e", @"/Views/Shared/Welcome.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6e48719c01b05fdd779a20f756a2eb6aa1e279e1", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Welcome : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DesiClothing4u.Common.Models.Customer>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<h5>This is welcome</h5>\r\n<h2>\r\n    \r\n");
#nullable restore
#line 5 "F:\Mars\FreelanceWork\Danish USA\DesiClothing4u\DesiClothing4u.UI\Views\Shared\Welcome.cshtml"
     if (Model != null && ViewBag.BillingAddress.FirstName != null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <i class=\"Welcome\">Welcome ");
#nullable restore
#line 7 "F:\Mars\FreelanceWork\Danish USA\DesiClothing4u\DesiClothing4u.UI\Views\Shared\Welcome.cshtml"
                              Write(ViewBag.BillingAddress.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</i>\r\n");
#nullable restore
#line 8 "F:\Mars\FreelanceWork\Danish USA\DesiClothing4u\DesiClothing4u.UI\Views\Shared\Welcome.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DesiClothing4u.Common.Models.Customer> Html { get; private set; }
    }
}
#pragma warning restore 1591
