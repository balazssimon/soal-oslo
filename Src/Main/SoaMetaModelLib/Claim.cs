using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    using MetaInfo;

    [ModelLiteral("Claimset")]
    public class ClaimsetType : ComplexType
    {
        public ClaimsetType()
        {
            this.Fields = new ModelList<ClaimField>(this, FieldsProperty);
        }

        public string Uri
        {
            get;
            set;
        }

        [Opposite(typeof(ClaimField), "Claimset")]
        public ModelList<ClaimField> Fields
        {
            get { return (ModelList<ClaimField>)GetValue(FieldsProperty); }
            private set { SetValue(FieldsProperty, value); }
        }
        public static readonly ModelProperty FieldsProperty = ModelProperty.Register("Fields", typeof(ModelList<ClaimField>), typeof(ClaimsetType));

    }

    [ModelLiteral("Field")]
    public class ClaimField : Field
    {
        public string Uri
        {
            get;
            set;
        }

        [Opposite(typeof(ClaimsetType), "Fields")]
        public ClaimsetType Claimset
        {
            get { return (ClaimsetType)GetValue(ClaimsetProperty); }
            set { SetValue(ClaimsetProperty, value); }
        }
        public static readonly ModelProperty ClaimsetProperty = ModelProperty.Register("Claimset", typeof(ClaimsetType), typeof(ClaimField));

    }
}
