using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.TextManager.Interop;

namespace OsloExtensions.VisualStudio
{
    public class OsloCodeGeneratorSource : Microsoft.VisualStudio.Package.Source
    {
        public OsloCodeGeneratorSource(LanguageService service, IVsTextLines textLines, Colorizer colorizer)
            : base(service, textLines, colorizer)
        {
            
        }

        private dynamic parseResult;
        public dynamic ParseResult
        {
            get { return parseResult; }
            set { parseResult = value; }
        }
    }
}
