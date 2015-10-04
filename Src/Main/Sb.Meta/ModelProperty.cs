using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Sb.Meta
{
    public class ModelProperty
    {
        private static Dictionary<Type, Dictionary<string, ModelProperty>> properties;

        static ModelProperty()
        {
            ModelProperty.properties = new Dictionary<Type, Dictionary<string, ModelProperty>>();
        }

        private ModelProperty opposite;

        protected ModelProperty(string name, Type type, Type declaringType)
        {
            this.Name = name;
            this.Type = type;
            this.DeclaringType = declaringType;
            this.opposite = null;
        }

        public string Name { get; private set; }
        public Type Type { get; private set; }
        public Type DeclaringType { get; private set; }

        public ModelProperty Opposite
        {
            get
            {
                if (this.opposite == null) 
                {
                    PropertyInfo info = this.DeclaringType.GetProperty(this.Name);
                    foreach (var attribute in info.GetCustomAttributes(typeof(OppositeAttribute), true))
                    {
                        OppositeAttribute oppositeAttribute = attribute as OppositeAttribute;
                        if (oppositeAttribute != null)
                        {
                            this.opposite = ModelProperty.Find(oppositeAttribute.DeclaringType, oppositeAttribute.PropertyName);
                        }
                    }
                }
                return this.opposite;
            }
        }

        public bool IsCollection
        {
            get
            {
                return typeof(IModelCollection).IsAssignableFrom(this.Type);
            }
        }

        public static ModelProperty Register(string name, Type type, Type declaringType)
        {
            return ModelProperty.RegisterProperty(declaringType, name, new ModelProperty(name, type, declaringType));
        }

        public static ModelProperty Find(Type ownerType, string name)
        {
            Dictionary<string, ModelProperty> propertyList;
            if (ModelProperty.properties.TryGetValue(ownerType, out propertyList))
            {
                ModelProperty result;
                if (propertyList.TryGetValue(name, out result))
                {
                    return result;
                }
            }
            return null;
        }

        protected static ModelProperty RegisterProperty(Type declaringType, string name, ModelProperty property)
        {
            Dictionary<string, ModelProperty> propertyList;
            if (!ModelProperty.properties.TryGetValue(declaringType, out propertyList))
            {
                propertyList = new Dictionary<string, ModelProperty>();
                ModelProperty.properties.Add(declaringType, propertyList);
            }
            propertyList.Add(name, property);
            return property;
        }
    }
}
