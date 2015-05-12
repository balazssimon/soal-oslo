using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OsloExtensions;
using Microsoft.M;
using System.Reflection;
using System.Dataflow;

namespace SoaMetaModel
{
    public class SoaLanguage
    {
        private static Language soaLanguage = null;

        public static Language Load()
        {
            if (SoaLanguage.soaLanguage == null)
            {
                //MImage image = MImage.LoadFromAssembly(Assembly.GetExecutingAssembly());
                SoaLanguage.soaLanguage = Language.LoadFromCurrentAssembly("Soa", "Services");
            }
            return SoaLanguage.soaLanguage;
        }
    }

    /// <summary>
    /// Parser context shared among multiple parsers.
    /// </summary>
    public class SoaLanguageContext
    {
        /// <summary>
        /// Mappings of source nodes to metamodel objects.
        /// </summary>
        private Dictionary<Node, SoaObject> objects = new Dictionary<Node, SoaObject>();

        public void AddObject(DynamicObjectNode node, SoaObject @object)
        {
            try
            {
                objects.Add(node.GetNode(), @object);
                @object.AddMetaInfo(new NodeInfo(node));
            }
            catch (ArgumentException exception)
            {
                throw new SoaLanguageException("The node had been already processed", exception);
            }
        }

        public SoaObject GetObject(DynamicObjectNode node)
        {
            try
            {
                return objects[node.GetNode()];
            }
            catch (KeyNotFoundException exception)
            {
                throw new SoaLanguageException("The node had not been processed yet", exception);
            }
        }

        public string FileName
        {
            get;
            set;
        }

        public SoaLanguageContext(string file = null)
        {
            FileName = file;
        }

    }

}
