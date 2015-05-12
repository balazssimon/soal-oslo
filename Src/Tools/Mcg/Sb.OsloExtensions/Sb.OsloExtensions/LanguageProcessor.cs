using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dataflow;
using System.Reflection;

namespace OsloExtensions
{
    public class LanguageProcessor
    {
        public string MethodNamePattern { get; protected set; }
        public object TargetInstance { get; protected set; }

        public LanguageProcessor(object targetInstance = null, string methodNamePattern = "Process_{0}")
        {
            if (targetInstance != null)
            {
                this.TargetInstance = targetInstance;
            }
            else
            {
                this.TargetInstance = this;
            }
            this.MethodNamePattern = methodNamePattern;
        }

        public virtual object Process(DynamicObjectNode node)
        {
            if (node == null) return null;
            System.Type type = this.TargetInstance.GetType();
            string methodName = string.Format(this.MethodNamePattern, node.GetBrand());
            System.Reflection.MethodInfo method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (method != null)
            {
                return method.Invoke(this.TargetInstance, new object[] { (object)node });
            }
            else
            {
                return this.ProcessChildren(node);
            }
        }

        public virtual object ProcessChildren(DynamicObjectNode node)
        {
            List<object> result = new List<object>();
            foreach (var item in node.GetNode().Edges)
            {
                DynamicObjectNode dynamicNode = DynamicObjectNode.NodeToObject(node.GetParser(), item.Node) as DynamicObjectNode;
                if (dynamicNode != null)
                {
                    object itemResult = this.Process(dynamicNode);
                    if (itemResult != null)
                    {
                        result.Add(itemResult);
                    }
                }
            }
            if (result.Count > 0) return result;
            else return null;
        }
    }
}
