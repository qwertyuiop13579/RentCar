#pragma checksum "A:\Progs\Labs\7Sem\CarInternet\Labs\Labs\Views\Sale\IndexByCar.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "573236234b1943b9312bceba2e6767691fca0b1a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sale_IndexByCar), @"mvc.1.0.view", @"/Views/Sale/IndexByCar.cshtml")]
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
#line 1 "A:\Progs\Labs\7Sem\CarInternet\Labs\Labs\Views\_ViewImports.cshtml"
using Labs;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "A:\Progs\Labs\7Sem\CarInternet\Labs\Labs\Views\_ViewImports.cshtml"
using Labs.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"573236234b1943b9312bceba2e6767691fca0b1a", @"/Views/Sale/IndexByCar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8e37a103f5ef95a743edc470bbf4e7f638cdfd68", @"/Views/_ViewImports.cshtml")]
    public class Views_Sale_IndexByCar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Labs.Models.Sale>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "A:\Progs\Labs\7Sem\CarInternet\Labs\Labs\Views\Sale\IndexByCar.cshtml"
  
    ViewBag.Title = "Список заказов автомобиля";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<table class=\"table\">\r\n    <tr><th>ID Клиента</th><th>Начало аренды</th><th>Конец аренды</th><th>Цена</th><th>Статус</th></tr>\r\n");
#nullable restore
#line 8 "A:\Progs\Labs\7Sem\CarInternet\Labs\Labs\Views\Sale\IndexByCar.cshtml"
     foreach (var sale in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 11 "A:\Progs\Labs\7Sem\CarInternet\Labs\Labs\Views\Sale\IndexByCar.cshtml"
           Write(sale.id_client);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 12 "A:\Progs\Labs\7Sem\CarInternet\Labs\Labs\Views\Sale\IndexByCar.cshtml"
           Write(sale.date2.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 13 "A:\Progs\Labs\7Sem\CarInternet\Labs\Labs\Views\Sale\IndexByCar.cshtml"
           Write(sale.date3.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 14 "A:\Progs\Labs\7Sem\CarInternet\Labs\Labs\Views\Sale\IndexByCar.cshtml"
           Write(sale.price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 15 "A:\Progs\Labs\7Sem\CarInternet\Labs\Labs\Views\Sale\IndexByCar.cshtml"
           Write(sale.status);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n");
#nullable restore
#line 17 "A:\Progs\Labs\7Sem\CarInternet\Labs\Labs\Views\Sale\IndexByCar.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Labs.Models.Sale>> Html { get; private set; }
    }
}
#pragma warning restore 1591