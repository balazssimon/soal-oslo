using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    public abstract class Type : Declaration
    {
        public System.Type UnderlyingType
        {
            get;
            set;
        }
    }

    public class NullableType : Type
    {
        private static Dictionary<Type, NullableType> instances = new Dictionary<Type, NullableType>();

        public static NullableType CreateFrom(Type innerType)
        {
            if (!instances.ContainsKey(innerType))
            {
                instances.Add(innerType, new NullableType(innerType));
            }
            return instances[innerType];
        }

        public NullableType(Type innerType = null)
        {
            if (innerType != null)
            {
                InnerType = innerType;
                Name = innerType.Name + "?";
                Namespace = innerType.Namespace;
            }
        }

        public Type InnerType
        {
            get { return (Type)GetValue(InnerTypeProperty); }
            set { SetValue(InnerTypeProperty, value); }
        }
        public static readonly ModelProperty InnerTypeProperty = ModelProperty.Register("InnerType", typeof(Type), typeof(NullableType));
    }

    public abstract class SimpleType : Type
    {
    }

    public abstract class ComplexType : Type
    {
    }

    public class ArrayType : ComplexType
    {
        private static Dictionary<Type, ArrayType> instances = new Dictionary<Type, ArrayType>();

        public static ArrayType CreateFrom(Type itemType)
        {
            if (!instances.ContainsKey(itemType))
            {
                instances.Add(itemType, new ArrayType(itemType));
            }
            return instances[itemType];
        }

        public ArrayType(Type itemType = null)
        {
            if (itemType != null)
            {
                ItemType = itemType;
                Name = "ArrayOf" + itemType.Name;
                Namespace = itemType.Namespace;
            }
        }

        public Type ItemType
        {
            get { return (Type)GetValue(ItemTypeProperty); }
            set { SetValue(ItemTypeProperty, value); }
        }
        public static readonly ModelProperty ItemTypeProperty = ModelProperty.Register("ItemType", typeof(Type), typeof(ArrayType));
    }
}
