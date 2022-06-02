#pragma checksum "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5e396aabcdbe1b3a52ae15d1a7e98b47f166024d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_ManageUsers), @"mvc.1.0.view", @"/Views/Admin/ManageUsers.cshtml")]
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
#line 1 "C:\Users\abdo\source\repos\GpProject\Views\_ViewImports.cshtml"
using GpProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\abdo\source\repos\GpProject\Views\_ViewImports.cshtml"
using GpProject.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml"
using AspNetCoreHero.ToastNotification.Abstractions;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5e396aabcdbe1b3a52ae15d1a7e98b47f166024d", @"/Views/Admin/ManageUsers.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"97de66dcb621b0dae117d96685adaa060e03e0d1", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Admin_ManageUsers : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<GpProject.ViewModels.UserMangerViewModel>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/vendor/download.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("avatar avatar-sm me-3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("user1"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml"
  
    ViewData["Title"] = "ManageUsers";
    Layout = "~/Views/Shared/UserManager.cshtml";


#line default
#line hidden
#nullable disable
            WriteLiteral(@"



<div class=""row"">
    <div class=""col-12"">
        <div class=""card mb-4"">
            <div class=""card-header pb-0"">
                <h6>Users table</h6>
            </div>
            <div class=""card-body px-0 pt-0 pb-2"">
                <div class=""table-responsive p-0"">
                    <table class=""table align-items-center mb-0"">
                        <thead>
                            <tr>
                                <th class=""text-uppercase text-secondary text-xxs font-weight-bolder opacity-7"">User</th>
                                <th class=""text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7"">Role</th>

                                <th class=""text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7"">Status</th>
                                <th class=""text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2"">Function</th>
                                <th class=""text-secondary opacity-7"">");
            WriteLiteral("</th>\r\n                            </tr>\r\n                        </thead>\r\n\r\n                        <tbody>\r\n");
#nullable restore
#line 35 "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml"
                             foreach (var user in Model)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\r\n                                    <td>\r\n                                        <div class=\"d-flex px-2 py-1\">\r\n                                            <div>\r\n");
#nullable restore
#line 41 "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml"
                                                 if (user.User.Img == null)
                                                {


#line default
#line hidden
#nullable disable
            WriteLiteral("                                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "5e396aabcdbe1b3a52ae15d1a7e98b47f166024d6747", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 45 "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml"

                                                }
                                                else
                                                {
                                                    var Userbase64 = Convert.ToBase64String(user.User.Img);
                                                    var UserimgSrc = String.Format("data:image/png;base64,{0}", Userbase64);

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                    <img");
            BeginWriteAttribute("src", " src=\"", 2392, "\"", 2409, 1);
#nullable restore
#line 51 "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml"
WriteAttributeValue("", 2398, UserimgSrc, 2398, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"avatar avatar-sm me-3\" alt=\"user1\">\r\n");
#nullable restore
#line 52 "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml"

                                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            </div>\r\n                                            <div class=\"d-flex flex-column justify-content-center\">\r\n                                                <h6 class=\"mb-0 text-sm\">");
#nullable restore
#line 56 "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml"
                                                                    Write(user.User.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n                                                <p class=\"text-xs text-secondary mb-0\">");
#nullable restore
#line 57 "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml"
                                                                                  Write(user.User.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                                            </div>
                                        </div>
                                    </td>
                                    <td class=""align-middle text-center"">
                                        <span class=""text-secondary text-xs font-weight-bold"">");
#nullable restore
#line 62 "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml"
                                                                                         Write(user.Role);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                    </td>\r\n                                    <td class=\"align-middle text-center text-sm\">\r\n");
#nullable restore
#line 65 "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml"
                                         if (user.User.Status)
                                        {



#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <span class=\"badge badge-sm bg-gradient-success\">Online</span>\r\n");
#nullable restore
#line 70 "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml"
                                        }
                                        else
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <span class=\"badge badge-sm bg-gradient-secondary\">Offline</span>\r\n");
#nullable restore
#line 74 "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                                    </td>\r\n\r\n                                    <td>\r\n                                        <p class=\"text-xs font-weight-bold mb-0\">");
#nullable restore
#line 80 "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml"
                                                                            Write(user.User.City);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                        <p class=\"text-xs text-secondary mb-0\">");
#nullable restore
#line 81 "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml"
                                                                          Write(user.User.District);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                    </td>\r\n\r\n                                    <td class=\"align-middle\">\r\n                                        <a href=\"javascript:;\" class=\"text-secondary font-weight-bold text-xs js-delete\" data-id=\"");
#nullable restore
#line 85 "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml"
                                                                                                                             Write(user.User.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n                                            Delete\r\n                                        </a>\r\n                                    </td>\r\n                                </tr>\r\n");
#nullable restore
#line 90 "C:\Users\abdo\source\repos\GpProject\Views\Admin\ManageUsers.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </tbody>\r\n\r\n\r\n                    </table>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
        $(document).ready(function () {
            $("".js-delete"").on(""click"", function () {
                var btn = $(this);
                console.log(""done"");
                bootbox.confirm({
                    message: ""This is a confirm with custom button text and color! Do you like it?"",
                    buttons: {
                        confirm: {
                            label: 'Yes',
                            className: 'btn-danger'
                        },
                        cancel: {
                            label: 'No',
                            className: 'btn-success'
                        }
                    },
                    callback: function (result) {
                        if (result) {
                            $.ajax({
                                url: ""/api/AdminApi/?id="" + btn.data(""id""),
                                method: ""DELETE"",
                                success: function (data) {
           ");
                WriteLiteral(@"                         btn.parents(""tr"").fadeOut();
                                },
                                error: function () {
                                    alert(""error happend"");
                                }
                            });
                        }
                    }
                });
            });
        });

    </script>
");
            }
            );
            WriteLiteral("\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public INotyfService _notyfService { get; private set; } = default!;
        #nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<GpProject.ViewModels.UserMangerViewModel>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591