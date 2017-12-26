using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StyleCop.CSharp;
using StyleCop;

namespace StyleCopCustomRules
{
    [SourceAnalyzer(typeof(CsParser))]
    public class ControllerSuffixRule : SourceAnalyzer
    {
        private const string ruleName = "ControllerSuffixRule";

        public override void AnalyzeDocument(CodeDocument document)
        {
            CsDocument doc = (CsDocument)document;
            doc.WalkDocument(new CodeWalkerElementVisitor<object>(this.VisitElement));
        }

        private bool VisitElement(CsElement element, CsElement parentElement, object context)
        {
            var classElement = element as Class;

            if (classElement != null)
            {
                if (classElement.BaseClass == "Controller" || classElement.BaseClass == "System.Web.Controller")
                {
                    if (!classElement.Name.EndsWith("Controller", System.StringComparison.Ordinal))
                    {
                        this.AddViolation(element, ruleName);
                    }
                }
            }

            return true;
        }
    }
}
