using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    [ModelLiteral("Enum")]
    public class EnumType : SimpleType
    {
        public EnumType()
        {
            this.Values = new ModelList<EnumValue>(this, ValuesProperty);
        }

        [Opposite(typeof(EnumValue), "Enum")]
        public ModelList<EnumValue> Values
        {
            get { return (ModelList<EnumValue>)GetValue(ValuesProperty); }
            private set { SetValue(ValuesProperty, value); }
        }
        public static readonly ModelProperty ValuesProperty = ModelProperty.Register("Values", typeof(ModelList<EnumValue>), typeof(EnumType));
    }

    [ModelLiteral("Value")]
    public class EnumValue : SoaObject
    {
        public string Name
        {
            get;
            set;
        }

        [Opposite(typeof(EnumType), "Values")]
        public EnumType Enum
        {
            get { return (EnumType)GetValue(EnumProperty); }
            set { SetValue(EnumProperty, value); }
        }
        public static readonly ModelProperty EnumProperty = ModelProperty.Register("Enum", typeof(EnumType), typeof(EnumValue));
    }
}
