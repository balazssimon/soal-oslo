using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Dataflow;
using System.IO;
using System.Collections;

namespace OsloExtensions
{
    public class DynamicObjectNode : DynamicObject
    {
        OsloExtensions.Parser parser;
        Node node;
        // we never want to double-wrap, so all construction
        // goes through NodeToObject, which unwraps atomics
        internal DynamicObjectNode(OsloExtensions.Parser parser, Node node)
        {
            this.parser = parser;
            this.node = node;
        }

        // this method is the only way to go from the low-level
        // node world to the world of (dynamic) objects
        public static object NodeToObject(OsloExtensions.Parser parser, Node node)
        {
            if (node.NodeKind == NodeKind.Atomic)
                return node.AtomicValue;
            else
                return new DynamicObjectNode(parser, node);
        }

        public static IEnumerable<dynamic> ReadValues(OsloExtensions.Parser parser, TextReader reader)
        {
            foreach (var edge in Node.ReadFrom(reader))
            {
                yield return NodeToObject(parser, edge.Node);
            }
        }

        public static IEnumerable<dynamic> ReadValuesFromString(OsloExtensions.Parser parser, string text)
        {
            foreach (var edge in Node.ReadFromString(text))
            {
                yield return NodeToObject(parser, edge.Node);
            }
        }

        // implement the obvious three...
        public override bool Equals(object obj)
        {
            return node.Equals(obj);
        }

        public override int GetHashCode()
        {
            return node.GetHashCode();
        }

        public override string ToString()
        {
            return node.ToString();
        }

        public Identifier GetBrand() { return node.Brand; }
        public Node GetNode() { return this.node; }
        public Parser GetParser() { return this.parser; }
        public SourceSpan GetSourceSpan() 
        {
            return this.parser.GetSourceSpan(this.node); 
        }

        public virtual IEnumerator<dynamic> GetEnumerator()
        {
            IEnumerable result = null;
            switch (node.NodeKind)
            {
                case NodeKind.Collection:
                    result = WrapCollectionNodeAsEnumerable(node);
                    break;
                case NodeKind.List:
                    result = WrapListNodeAsEnumerable(node);
                    break;
            }
            if (result != null)
            {
                foreach (var item in result)
                {
                    yield return item;
                }
            }
            else
            {
                yield return NodeToObject(this.parser, node);
            }
        }

        // special case conversion to IEnumerable to support foreach
        // over lists and collections
        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            result = null;
            if (binder.Type != typeof(IEnumerable))
                return false;
            switch (node.NodeKind)
            {
                case NodeKind.Collection:
                    result = WrapCollectionNodeAsEnumerable(node);
                    return true;
                case NodeKind.List:
                    result = WrapListNodeAsEnumerable(node);
                    return true;
                default:
                    return false;
            }
        }

        IEnumerable WrapCollectionNodeAsEnumerable(Node node)
        {
            foreach (var item in node.Edges)
            {
                yield return NodeToObject(this.parser, item.Node);
            }
        }

        IEnumerable WrapListNodeAsEnumerable(Node node)
        {
            foreach (var item in node.ViewAsList())
            {
                yield return NodeToObject(this.parser, item);
            }
        }

        // support [int] over list and [string] over records
        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            result = null;
            if (indexes.Length > 1)
                return false;
            if (node.NodeKind == NodeKind.List && indexes[0] is int)
            {
                int index = (int)indexes[0];
                var list = node.ViewAsList();
                if (index >= list.Count)
                    throw new IndexOutOfRangeException("Indexed past end of M list node");
                result = NodeToObject(this.parser, list[index]);
                return true;
            }
            string name = indexes[0] as string;
            if (name != null)
            {
                return TryGetMember(name, true, out result);
            }
            return false;
        }

        // support . over records and .Count/.IsReadOnly over collections/lists
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return TryGetMember(binder.Name, binder.IgnoreCase, out result);
        }

        bool TryGetMember(string name, bool ignoreCase, out object result)
        {
            result = null;
            switch (node.NodeKind)
            {
                case NodeKind.List:
                    if (name == "Count")
                    {
                        result = node.ViewAsList().Count;
                        return true;
                    }
                    else if (name == "IsReadOnly")
                    {
                        result = true;
                        return true;
                    }
                    return false;
                case NodeKind.Collection:
                    if (name == "Count")
                    {
                        result = node.Edges.Count;
                        return true;
                    }
                    else if (name == "IsReadOnly")
                    {
                        result = true;
                        return true;
                    }
                    return false;
                case NodeKind.Record:
                    if (node.Edges.ContainsLabel(name))
                    {
                        result = NodeToObject(this.parser, node.Edges[name]);
                        return true;
                    }
                    else if (ignoreCase)
                    {
                        foreach (var e in node.Edges)
                        {
                            if (string.Compare(e.Label.Text, name, StringComparison.InvariantCultureIgnoreCase) == 0)
                            {
                                result = NodeToObject(this.parser, e.Node);
                                return true;
                            }
                        }
                    }
                    return false;
                default:
                    return false;
            }
        }
    }
}
