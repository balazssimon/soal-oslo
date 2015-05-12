using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell.Interop;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.OLE.Interop;
using System.ComponentModel;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Package;
using System.Dataflow;

namespace OsloExtensions.VisualStudio
{
    [ComVisible(true)]
    [Guid(GuidList.GuidOsloGeneratorService)]
    public class OsloCodeGeneratorGenerator : IVsSingleFileGenerator, IObjectWithSite
    {
        private object site;

        #region IVsSingleFileGenerator Members

        public int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".Generated.cs";
            return Microsoft.VisualStudio.VSConstants.S_OK;
        }

        public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out uint pcbOutput, IVsGeneratorProgress pGenerateProgress)
        {
            rgbOutputFileContents[0] = IntPtr.Zero;
            pcbOutput = 0;
            string fileName = "";
            try
            {
                if (wszInputFilePath != null)
                {
                    fileName = Path.GetFileName(wszInputFilePath);
                }
                bool error = false;
                string className = null;
                if (wszInputFilePath != null)
                {
                    className = Path.GetFileNameWithoutExtension(wszInputFilePath).Trim();
                    int length = 0;
                    while (length < className.Length && (char.IsLetterOrDigit(className[length]) || className[length] == '_'))
                    {
                        ++length;
                    }
                    className = className.Substring(0, length);
                }
                OsloErrorReporter errorReporter = new OsloErrorReporter();
                OsloCodeGeneratorInfo info = null;
                try
                {
                    info = new OsloCodeGeneratorInfo(wszInputFilePath, new StringReader(bstrInputFileContents), true, errorReporter);
                    if (info.ClassName == null && !string.IsNullOrEmpty(className))
                    {
                        info.ClassName = className;
                    }
                    if (info.NamespaceName == null)
                    {
                        info.NamespaceName = wszDefaultNamespace;
                    }
                }
                catch (Exception ex)
                {
                    error = true;
                    info = null;
                    pGenerateProgress.GeneratorError(0, 0, string.Format("[OsloCodeGeneratorGenerator] Could not process input file: {0}. Error: {1}", fileName, ex.ToString()), 0, 0);
                }
                MemoryStream memory = new MemoryStream();
                if (info != null && !error)
                {
                    using (TemplatePrinter printer = new TemplatePrinter(new StreamWriter(memory, Encoding.UTF8)))
                    {
                        info.GenerateTemporaryCode(printer, (uint current, uint total) => pGenerateProgress.Progress(current, total));
                        printer.ForcedWriteLine();
                        printer.Flush();
                    }
                }
                memory.Flush();
                byte[] contents = new byte[memory.Length];
                memory.Position = 0;
                memory.Read(contents, 0, contents.Length);
                rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(contents.Length);
                Marshal.Copy(contents, 0, rgbOutputFileContents[0], contents.Length);
                pcbOutput = (uint)contents.Length;
                foreach (var errorMessage in errorReporter.Errors)
                {
                    int lineNumber = errorMessage.LineNumber - 1;
                    int columnNumber = errorMessage.ColumnNumber - 1;
                    if (lineNumber < 0) lineNumber = 0;
                    if (columnNumber < 0) columnNumber = 0;
                    if (errorMessage.Level == ErrorLevel.Error)
                    {
                        error = true;
                        pGenerateProgress.GeneratorError(0, 0, errorMessage.ToString(), (uint)lineNumber, (uint)columnNumber);
                    }
                    else
                    {
                        pGenerateProgress.GeneratorError(1, 0, errorMessage.ToString(), (uint)lineNumber, (uint)columnNumber);
                    }
                }
                if (!error)
                {
                    return Microsoft.VisualStudio.VSConstants.S_OK;
                }
            }
            catch(Exception ex)
            {
                pGenerateProgress.GeneratorError(0, 0, string.Format("[OsloCodeGeneratorGenerator] Could not process input file: {0}. Error: {1}", fileName, ex.ToString()), 0, 0);
            }
            return Microsoft.VisualStudio.VSConstants.E_FAIL;
        }

        #endregion

        #region IObjectWithSite Members

        public void GetSite(ref Guid riid, out IntPtr ppvSite)
        {
            if (this.site == null)
            {
                throw new Win32Exception(-2147467259);
            }

            IntPtr objectPointer = Marshal.GetIUnknownForObject(this.site);

            try
            {
                Marshal.QueryInterface(objectPointer, ref riid, out ppvSite);
                if (ppvSite == IntPtr.Zero)
                {
                    throw new Win32Exception(-2147467262);
                }
            }
            finally
            {
                if (objectPointer != IntPtr.Zero)
                {
                    Marshal.Release(objectPointer);
                    objectPointer = IntPtr.Zero;
                }
            }
        }

        public void SetSite(object pUnkSite)
        {
            this.site = pUnkSite;
        }

        #endregion
    }
}
