using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dataflow;

namespace OsloExtensions
{
    public class Parser
    {
        private GraphStore store;
        private System.Dataflow.Parser parser;
        private Dictionary<object, SourceSpan> locations;

        public Parser(ParserFactory parserFactory)
        {
            this.parser = parserFactory.Create();
            this.store = new DefaultGraphStore();
            this.locations = new Dictionary<object, SourceSpan>();
            this.parser.GraphBuilder = new NodeGraphBuilder(store);

            this.parser.SyntaxMatched += new EventHandler<SyntaxMatchedEventArgs>(parser_SyntaxMatched);
        }

        void parser_SyntaxMatched(object sender, SyntaxMatchedEventArgs e)
        {
            if (e.ReturnValue != null)
            {
                this.locations[e.ReturnValue] = e.Span;
            }
        }

        dynamic NormalizeResult(object result)
        {
            if (result is Node)
                return DynamicObjectNode.NodeToObject(this, (Node)result);
            return result;
        }

        public dynamic Parse(ITextStream stream, ErrorReporter errors)
        {
            object result = this.parser.Parse(stream, errors);
            return this.NormalizeResult(result);
        }

        public dynamic Parse(ITextStream stream, ErrorReporter errors, SourcePoint startLocation)
        {
            object result = this.parser.Parse(stream, errors, startLocation);
            return this.NormalizeResult(result);
        }

        public SourceSpan GetSourceSpan(object node)
        {
            SourceSpan result;
            this.locations.TryGetValue(node, out result);
            return result;
        }
    }
}
