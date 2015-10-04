using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Sb.Meta
{
    public class ModelSet<T> : ModelCollectionBase, IModelCollection, ICollection<T>
        where T: class
    {
        private HashSet<T> items;

        public ModelSet(IModelClass owner, ModelProperty ownerProperty)
            : base(owner, ownerProperty)
        {
            this.items = new HashSet<T>();
        }


        #region ICollection<T> Members

        public void Add(T item)
        {
            if (this.items.Add(item))
            {
                this.OnAddValue(item);
            }
        }

        public void Clear()
        {
            HashSet<T> oldItems = this.items;
            this.items = new HashSet<T>();
            foreach (var item in oldItems)
            {
                this.OnRemoveValue(item);
            }
        }

        public bool Contains(T item)
        {
            return this.items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            if (this.items.Remove(item))
            {
                this.OnRemoveValue(item);
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        #endregion


        #region IModelCollection Members

        bool IModelCollection.Add(object item)
        {
            if (!this.Contains((T)item))
            {
                this.Add((T)item);
                return true;
            }
            return false;
        }

        bool IModelCollection.Remove(object item)
        {
            if (this.Contains((T)item))
            {
                this.Remove((T)item);
                return true;
            }
            return false;
        }

        #endregion

        protected void OnAddValue(object value)
        {
            INotifyModelClass notifyOwner = this.Owner as INotifyModelClass;
            if (notifyOwner != null)
            {
                notifyOwner.OnAddValue(this.OwnerProperty, value, true);
            }
        }

        protected void OnRemoveValue(object value)
        {
            INotifyModelClass notifyOwner = this.Owner as INotifyModelClass;
            if (notifyOwner != null)
            {
                notifyOwner.OnRemoveValue(this.OwnerProperty, value, true);
            }
        }
    }
}
