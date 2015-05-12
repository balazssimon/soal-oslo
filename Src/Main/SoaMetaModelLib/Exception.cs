using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    using MetaInfo;

    [ModelLiteral("Exception")]
    public class ExceptionType : ComplexType
    {
        public ExceptionType()
        {
            this.Fields = new ModelList<ExceptionField>(this, FieldsProperty);
        }

        [Opposite(typeof(ExceptionField), "Exception")]
        public ModelList<ExceptionField> Fields
        {
            get { return (ModelList<ExceptionField>)GetValue(FieldsProperty); }
            private set { SetValue(FieldsProperty, value); }
        }
        public static readonly ModelProperty FieldsProperty = ModelProperty.Register("Fields", typeof(ModelList<ExceptionField>), typeof(ExceptionType));

        public ExceptionType SuperType
        {
            get { return (ExceptionType)GetValue(SuperTypeProperty); }
            set { SetValue(SuperTypeProperty, value); }
        }
        public static readonly ModelProperty SuperTypeProperty = ModelProperty.Register("SuperType", typeof(ExceptionType), typeof(ExceptionType));


        public virtual IEnumerable<ExceptionType> GetSuperTypes(bool throws = false)
        {
            List<ExceptionType> superTypes = new List<ExceptionType>();
            this.GetSuperTypes(superTypes, new Stack<ExceptionType>(), throws);
            return superTypes;
        }

        protected virtual void GetSuperTypes(List<ExceptionType> superTypes, Stack<ExceptionType> branch, bool throws = false)
        {
            branch.Push(this);
            ExceptionType superType = this.SuperType;
            if (superType != null)
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
    public class ExceptionField : Field
    {

        [Opposite(typeof(ExceptionType), "Fields")]
        public ExceptionType Exception
        {
            get { return (ExceptionType)GetValue(ExceptionProperty); }
            set { SetValue(ExceptionProperty, value); }
        }
        public static readonly ModelProperty ExceptionProperty = ModelProperty.Register("Exception", typeof(ExceptionType), typeof(ExceptionField));

    }
}
