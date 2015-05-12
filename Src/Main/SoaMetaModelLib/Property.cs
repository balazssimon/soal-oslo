using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    public class Property : SoaObject
    {
        public string Name
        {
            get;
            set;
        }

        public Type Type
        {
            get { return (Type)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        public static readonly ModelProperty TypeProperty = ModelProperty.Register("Type", typeof(Type), typeof(Property));

    }

    public class Variable : Property
    {
    }

    public class Field : Property
    {
    }
}
