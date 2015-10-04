using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Sb.Meta
{
    public interface IModelCollection
    {
        bool Add(object item);
        bool Remove(object item);
    }

    public abstract class ModelCollectionBase
    {
        public object Owner { get; private set; }
        public ModelProperty OwnerProperty { get; private set; }

        public ModelCollectionBase(IModelClass owner, ModelProperty ownerProperty)
        {
            this.Owner = owner;
            this.OwnerProperty = ownerProperty;
        }
    }
}
