using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.TextManager.Interop;
using System.IO;
using System.Dataflow;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using VSOLE = Microsoft.VisualStudio.OLE.Interop;
using System.Diagnostics;
using OsloExtensions;

namespace OsloExtensions.VisualStudio
{
    [ComVisible(true)]
    [Guid(GuidList.GuidOsloLanguageService)]
    public class OsloCodeGeneratorLanguageService : LanguageService
    {
        private LanguagePreferences preferences;
        private OsloCodeGeneratorScanner scanner;
        private Language codeParser;
        private Language templateParser;
        private EventLogger logger;

        public OsloCodeGeneratorLanguageService()
        {
            this.logger = EventLogger.CreateLogger("OsloCodeGeneratorLanguageService");
            this.logger.Log(EventLogEntryType.Information, "Entering: OsloCodeGeneratorLanguageService()");
            this.codeParser = OsloCodeGeneratorLanguages.GetOsloCodeGeneratorParser();
            this.templateParser = OsloCodeGeneratorLanguages.GetOsloCodeGeneratorTemplateParser();
            this.logger.Log(EventLogEntryType.Information, "Leaving: OsloCodeGeneratorLanguageService()");
        }

        public override string GetFormatFilterList()
        {
            this.logger.Log(EventLogEntryType.Information, "Entering: GetFormatFilterList()");
            return OsloCodeGeneratorConfiguration.FormatList;
        }

        #region Custom Colors
        public override int GetColorableItem(int index, out IVsColorableItem item)
        {
            this.logger.Log(EventLogEntryType.Information, "Entering: GetColorableItem()");
            if (index <= OsloCodeGeneratorConfiguration.ColorableItems.Count)
            {
                item = OsloCodeGeneratorConfiguration.ColorableItems[index - 1];
                return Microsoft.VisualStudio.VSConstants.S_OK;
            }
            else
            {
                throw new ArgumentNullException("index");
            }
        }

        public override int GetItemCount(out int count)
        {
            this.logger.Log(EventLogEntryType.Information, "Entering: GetItemCount()");
            count = OsloCodeGeneratorConfiguration.ColorableItems.Count;
            return Microsoft.VisualStudio.VSConstants.S_OK;
        }
        #endregion

        public override LanguagePreferences GetLanguagePreferences()
        {
            this.logger.Log(EventLogEntryType.Information, "Entering: GetLanguagePreferences()");
            if (preferences == null)
            {
                preferences = new LanguagePreferences(this.Site, typeof(OsloCodeGeneratorLanguageService).GUID, this.Name);
                preferences.Init();
            }
            return preferences;
        }

        public override IScanner GetScanner(IVsTextLines buffer)
        {
            this.logger.Log(EventLogEntryType.Information, "Entering: GetScanner()");
            if (scanner == null)
            {
                scanner = new OsloCodeGeneratorScanner();
            }
            return scanner;
        }

        public override Microsoft.VisualStudio.Package.Source CreateSource(IVsTextLines buffer)
        {
            this.logger.Log(EventLogEntryType.Information, "Entering: CreateSource()");
            return new OsloCodeGeneratorSource(this, buffer, this.GetColorizer(buffer));
        }


        public override void OnIdle(bool periodic)
        {
            // from IronPythonLanguage sample
            // this appears to be necessary to get a parse request with ParseReason = Check?
            OsloCodeGeneratorSource src = (OsloCodeGeneratorSource)GetSource(this.LastActiveTextView);
            if (src != null && src.LastParseTime >= Int32.MaxValue >> 12)
            {
                src.LastParseTime = 0;
            }
            base.OnIdle(periodic);
        }

        public override string Name
        {
            get { return OsloCodeGeneratorConfiguration.LanguageName; }
        }

        public override AuthoringScope ParseSource(ParseRequest req)
        {
            this.logger.Log(EventLogEntryType.Information, "Entering: ParseSource()");
            OsloCodeGeneratorSource source = (OsloCodeGeneratorSource)this.GetSource(req.FileName);
            switch (req.Reason)
            {
                case ParseReason.Check:
                    // This is where you perform your syntax highlighting.
                    // Parse entire source as given in req.Text.
                    // Store results in the AuthoringScope object.
                    OsloErrorReporter errorReporter = new OsloErrorReporter();
                    dynamic program = this.codeParser.Parse(new StringReader(req.Text), errorReporter);
                    source.ParseResult = program;
                    foreach (var error in errorReporter.Errors)
                    {
                        TextSpan span = new TextSpan();
                        span.iStartLine = error.LineNumber - 1;
                        span.iEndLine = error.EndLineNumber - 1;
                        span.iStartIndex = error.ColumnNumber - 1;
                        span.iEndIndex = error.EndColumnNumber - 1;
                        Severity severity = Severity.Error;
                        switch (error.Level)
                        {
                            case ErrorLevel.DeprecationWarning:
                                severity = Severity.Warning;
                                break;
                            case ErrorLevel.Error:
                                severity = Severity.Error;
                                break;
                            case ErrorLevel.Message:
                                severity = Severity.Hint;
                                break;
                            case ErrorLevel.Warning:
                                severity = Severity.Warning;
                                break;
                        }
                        req.Sink.AddError(req.FileName, error.ToString(), span, severity);
                    }
                    break;
            }
            return new OsloCodeGeneratorAuthoringScope();
        }

        public override ViewFilter CreateViewFilter(CodeWindowManager mgr, IVsTextView newView)
        {
            this.logger.Log(EventLogEntryType.Information, "Entering: CreateViewFilter()");
            return new OsloCodeGeneratorViewFilter(mgr, newView);
        }

        public override Colorizer GetColorizer(IVsTextLines buffer)
        {
            this.logger.Log(EventLogEntryType.Information, "Entering: GetColorizer()");
            return new OsloCodeGeneratorColorizer(this, buffer, this.GetScanner(buffer));
        }
    }
}
