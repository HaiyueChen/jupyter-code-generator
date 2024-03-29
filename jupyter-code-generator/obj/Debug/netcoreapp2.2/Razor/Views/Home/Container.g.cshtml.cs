#pragma checksum "C:\Users\chenh\Desktop\jupyter-code-generator\jupyter-code-generator\Views\Home\Container.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3457536db20f4ac3c5f67ac34396a88786ba8c9a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Container), @"mvc.1.0.view", @"/Views/Home/Container.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Container.cshtml", typeof(AspNetCore.Views_Home_Container))]
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
#line 1 "C:\Users\chenh\Desktop\jupyter-code-generator\jupyter-code-generator\Views\_ViewImports.cshtml"
using jupyter_code_generator;

#line default
#line hidden
#line 2 "C:\Users\chenh\Desktop\jupyter-code-generator\jupyter-code-generator\Views\_ViewImports.cshtml"
using jupyter_code_generator.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3457536db20f4ac3c5f67ac34396a88786ba8c9a", @"/Views/Home/Container.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"82bdd13f5b83b863b4dedc23fac346802eed01cb", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Container : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\Users\chenh\Desktop\jupyter-code-generator\jupyter-code-generator\Views\Home\Container.cshtml"
  
    ViewData["Title"] = "Container";

#line default
#line hidden
            BeginContext(47, 77, true);
            WriteLiteral("\r\n<h1>Select a container to view</h1>\r\n\r\n<select id=\"container-select\">\r\n    ");
            EndContext();
            BeginContext(124, 47, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3457536db20f4ac3c5f67ac34396a88786ba8c9a3920", async() => {
                BeginContext(141, 21, true);
                WriteLiteral("Select a container...");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(171, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 10 "C:\Users\chenh\Desktop\jupyter-code-generator\jupyter-code-generator\Views\Home\Container.cshtml"
       
        List<Container> containers = (List<Container>) ViewData["Containers"];
        foreach(var container in containers)
        {

#line default
#line hidden
            BeginContext(319, 12, true);
            WriteLiteral("            ");
            EndContext();
            BeginContext(333, 16, true);
            WriteLiteral(" <option value=\"");
            EndContext();
            BeginContext(350, 19, false);
#line 14 "C:\Users\chenh\Desktop\jupyter-code-generator\jupyter-code-generator\Views\Home\Container.cshtml"
                         Write(container.reference);

#line default
#line hidden
            EndContext();
            BeginContext(369, 2, true);
            WriteLiteral("\">");
            EndContext();
            BeginContext(372, 24, false);
#line 14 "C:\Users\chenh\Desktop\jupyter-code-generator\jupyter-code-generator\Views\Home\Container.cshtml"
                                               Write(container.metaData.title);

#line default
#line hidden
            EndContext();
            BeginContext(396, 11, true);
            WriteLiteral("</option>\r\n");
            EndContext();
#line 15 "C:\Users\chenh\Desktop\jupyter-code-generator\jupyter-code-generator\Views\Home\Container.cshtml"
        }
    

#line default
#line hidden
            BeginContext(423, 361, true);
            WriteLiteral(@";
</select>


<div class=""row"">
    <div class=""col-8"">
        <div id=""files"" class=""border-right"">



        </div>

    </div>

    <div class=""col-4"">
        <div id=""selected"">
            <button class=""btn-sm btm-secondary"" type=""button"" id=""submit-files"">Generate jupyter notebook file</button>
        </div>
    </div>
</div>

");
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
