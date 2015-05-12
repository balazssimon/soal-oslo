using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    using MetaInfo;

    public abstract class Declaration : SoaObject
    {
        public string Name
        {
            get;
            set;
        }

        public string FullName
        {
            get
            {
                return (Namespace != null && Namespace.FullName != null) ? (Namespace.FullName + "." + Name) : Name;
            }
        }

        [Opposite(typeof(Namespace), "Declarations")]
        public Namespace Namespace
        {
            get { return (Namespace)GetValue(NamespaceProperty); }
            set { SetValue(NamespaceProperty, value); }
        }
        public static readonly ModelProperty NamespaceProperty = ModelProperty.Register("Namespace", typeof(Namespace), typeof(Declaration));

    }
}
