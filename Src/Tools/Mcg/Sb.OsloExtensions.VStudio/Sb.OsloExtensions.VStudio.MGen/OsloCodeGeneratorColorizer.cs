using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TextManager.Interop;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Package;

namespace OsloExtensions.VisualStudio
{
    public class OsloCodeGeneratorColorizer : Colorizer
    {
        public OsloCodeGeneratorColorizer(LanguageService svc, IVsTextLines buffer, IScanner scanner)
            : base(svc, buffer, scanner)
        {
        }

        #region IVsColorizer Members

        public override int ColorizeLine(int line, int length, IntPtr ptr, int state, uint[] attrs)
        {
            int linepos = 0;
            if (this.Scanner != null)
            {
                try
                {
                    string text = Marshal.PtrToStringUni(ptr, length);

                    this.Scanner.SetSource(text, 0);

                    TokenInfo tokenInfo = new TokenInfo();

                    tokenInfo.EndIndex = -1;

                    while (this.Scanner.ScanTokenAndProvideInfoAboutIt(tokenInfo, ref state))
                    {
                        if (attrs != null)
                        {
                            for (; linepos < tokenInfo.StartIndex; linepos++)
                                attrs[linepos] = (uint)TokenColor.Text;

                            for (; linepos <= tokenInfo.EndIndex; linepos++)
                                attrs[linepos] = (uint)tokenInfo.Color;
                        }
                    }
                }
                catch (Exception)
                {
                    // Ignore exceptions
                }
            }
            if (attrs != null)
            {
                // Must initialize the colors in all cases, otherwise you get 
                // random color junk on the screen.
                for (; linepos < length; linepos++)
                    attrs[linepos] = (uint)TokenColor.Text;
            }
            return state;
        }

        public override int GetStartState(out int piStartState)
        {
            piStartState = 0;
            return Microsoft.VisualStudio.VSConstants.S_OK;
        }

        public override int GetStateAtEndOfLine(int iLine, int iLength, IntPtr pText, int iState)
        {
            string text = Marshal.PtrToStringUni(pText, iLength);
            if (text != null)
            {
                //text = text.Trim();
                if (iState == 0 && text.StartsWith("template"))
                {
                    return 2;
                }
                else if ((iState == 1 || iState == 2) && text == "end template")
                {
                    return 0;
                }
                else
                {
                    this.Scanner.SetSource(text, 0);
                    ((OsloCodeGeneratorScanner)this.Scanner).ScanLine(ref iState);
                }
            }
            return iState;
        }

        public override int GetStateMaintenanceFlag(out int pfFlag)
        {
            pfFlag = 1;
            return Microsoft.VisualStudio.VSConstants.S_OK;
        }

        #endregion
    }
}
