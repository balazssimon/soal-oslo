using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsloExtensions
{
    public static class OsloCodeGeneratorLanguages
    {
        private static Language osloCodeGeneratorScanner;
        private static Language osloCodeGeneratorTemplateScanner;
        private static Language osloCodeGeneratorParser;
        private static Language osloCodeGeneratorTemplateParser;

        public static Language GetOsloCodeGeneratorScanner()
        {
            if (OsloCodeGeneratorLanguages.osloCodeGeneratorScanner != null)
            {
                return OsloCodeGeneratorLanguages.osloCodeGeneratorScanner;
            }
            OsloCodeGeneratorLanguages.osloCodeGeneratorScanner = Language.LoadFromCurrentAssembly("OsloExtensions", "OsloCodeGeneratorScanner");
            return OsloCodeGeneratorLanguages.osloCodeGeneratorScanner;
        }

        public static Language GetOsloCodeGeneratorTemplateScanner()
        {
            if (OsloCodeGeneratorLanguages.osloCodeGeneratorTemplateScanner != null)
            {
                return OsloCodeGeneratorLanguages.osloCodeGeneratorTemplateScanner;
            }
            OsloCodeGeneratorLanguages.osloCodeGeneratorTemplateScanner = Language.LoadFromCurrentAssembly("OsloExtensions", "OsloCodeGeneratorTemplateScanner");
            return OsloCodeGeneratorLanguages.osloCodeGeneratorTemplateScanner;
        }

        public static Language GetOsloCodeGeneratorParser()
        {
            if (OsloCodeGeneratorLanguages.osloCodeGeneratorParser != null)
            {
                return OsloCodeGeneratorLanguages.osloCodeGeneratorParser;
            }
            OsloCodeGeneratorLanguages.osloCodeGeneratorParser = Language.LoadFromCurrentAssembly("OsloExtensions", "OsloCodeGenerator");
            return OsloCodeGeneratorLanguages.osloCodeGeneratorParser;
        }

        public static Language GetOsloCodeGeneratorTemplateParser()
        {
            if (OsloCodeGeneratorLanguages.osloCodeGeneratorTemplateParser != null)
            {
                return OsloCodeGeneratorLanguages.osloCodeGeneratorTemplateParser;
            }
            OsloCodeGeneratorLanguages.osloCodeGeneratorTemplateParser = Language.LoadFromCurrentAssembly("OsloExtensions", "OsloCodeGeneratorTemplate");
            return OsloCodeGeneratorLanguages.osloCodeGeneratorTemplateParser;
        }
    }
}
