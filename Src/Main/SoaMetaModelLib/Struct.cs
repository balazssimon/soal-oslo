using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    using MetaInfo;

    [ModelLiteral("Struct")]
    public class StructType : ComplexType
    {
        public StructType()
        {
            this.Fields = new ModelList<StructField>(this, FieldsProperty);
        }

        [Opposite(typeof(StructField), "Struct")]
        public ModelList<StructField> Fields
        {
            get { return (ModelList<StructField>)GetValue(FieldsProperty); }
            private set { SetValue(FieldsProperty, value); }
        }
        public static readonly ModelProperty FieldsProperty = ModelProperty.Register("Fields", typeof(ModelList<StructField>), typeof(StructType));

        public StructType SuperType
        {
            get { return (StructType)GetValue(SuperTypeProperty); }
            set { SetValue(SuperTypeProperty, value); }
        }
        public static readonly ModelProperty SuperTypeProperty = ModelProperty.Register("SuperType", typeof(StructType), typeof(StructType));


        public virtual IEnumerable<StructType> GetSuperTypes(bool throws = false)
        {
            List<StructType> superTypes = new List<StructType>();
            this.GetSuperTypes(superTypes, new Stack<StructType>(), throws);
            return superTypes;
        }

        protected virtual void GetSuperTypes(List<StructType> superTypes, Stack<StructType> branch, bool throws = false)
        {
            branch.Push(this);
            StructType superType = this.SuperType;
            if(superType != null)
            {
                // Circular inheritance condition
                if (!branch.Contains(superType))
                {
                    // Multiple-path inheritance condition
                    if (!superTypes.Contains(superType))
                    {
                        superTypes.Add(superType);
                        superType.GetSuperTypes(superTypes, branch, throws);
                    }
                }
                else
                {
                    if (throws)
                    {
                        throw new InheritanceValidationException(branch.Last());
                    }
                }
            }
            branch.Pop();
        }
    }

    [ModelLiteral("Field")]
    public class StructField : Field
    {

        [Opposite(typeof(StructType), "Fields")]
        public StructType Struct
        {
            get { return (StructType)GetValue(StructProperty); }
            set { SetValue(StructProperty, value); }
        }
        public static readonly ModelProperty StructProperty = ModelProperty.Register("Struct", typeof(StructType), typeof(StructField));

    }
}
