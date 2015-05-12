using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dataflow;

namespace OsloExtensions
{
    public class OsloErrorReporter : System.Dataflow.ErrorReporter
    {
        public List<ErrorInformation> Errors { get; protected set; }

        public OsloErrorReporter()
        {
            this.Errors = new List<ErrorInformation>();
        }

        protected override void OnError(ErrorInformation errorInformation)
        {
            this.Errors.Add(errorInformation);
        }
    }
}
