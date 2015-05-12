using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    [ModelLiteral("Variable")]
    public class Reference : SoaObject
    {
        public string Name
        {
            get;
            set;
        }

        public SoaObject Object
        {
            get { return (SoaObject)GetValue(ObjectProperty); }
            set { SetValue(ObjectProperty, value); }
        }
        public static readonly ModelProperty ObjectProperty = ModelProperty.Register("Object", typeof(SoaObject), typeof(Reference));
    }
}
