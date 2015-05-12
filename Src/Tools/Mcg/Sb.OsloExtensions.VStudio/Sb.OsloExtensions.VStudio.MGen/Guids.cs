// Guids.cs
// MUST match guids.h
using System;

namespace OsloExtensions.VisualStudio
{
    static class GuidList
    {
        public const string guidSb_OsloExtensions_VStudio_MGenPkgString = "15574803-1643-4e67-8b29-3f8fa7de6436";
        public const string guidSb_OsloExtensions_VStudio_MGenCmdSetString = "65db15c1-aa07-473b-8d6c-b2b2d1b1fee6";
        public const string GuidOsloLanguageService = "01FB45B3-8DEF-42AB-8780-C10F0DA92362";
        public const string GuidOsloGeneratorService = "0B70BD55-7625-49F3-B95F-77014736787A";

        //[Guid("34593464-10CD-4DD2-9CA4-68A1F9636BA9")]
        //"01FB45B3-8DEF-42AB-8780-C10F0DA92362";

        public static readonly Guid guidSb_OsloExtensions_VStudio_MGenCmdSet = new Guid(guidSb_OsloExtensions_VStudio_MGenCmdSetString);
    };
}