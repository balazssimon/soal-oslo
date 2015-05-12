using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dataflow;
using SoaMetaModel.MetaInfo;
using OsloExtensions;

namespace SoaMetaModel
{
    public class SourceLocationInfo : ISourceLocation, IMetaInfo {

        public SourceLocationInfo(SourceSpan span, string file = null)
        {
            Span = span;
            FileName = file;
        }

        public SourceLocationInfo(DynamicObjectNode node, string file = null)
            : this(node.GetSourceSpan(), file)
        {
        }

        public SourceLocationInfo(DynamicObjectNode node, SoaLanguageContext context)
            : this(node, context.FileName)
        {
        }

        public string FileName
        {
            get;
            set;
        }

        public SourceSpan Span
        {
            get;
            set;
        }
    }

    public class NodeInfo : IMetaInfo
    {
        public NodeInfo(DynamicObjectNode node)
        {
            Node = node;
        }

        public DynamicObjectNode Node
        {
            get;
            set;
        }
    }
}
