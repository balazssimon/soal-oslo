using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Sb.Meta
{
    public class ModelList<T> : ModelCollectionBase, IModelCollection, IList<T>
        where T: class
    {
        private List<T> items;

        public ModelList(IModelClass owner, ModelProperty ownerProperty)
            : base(owner, ownerProperty)
        {
            this.items = new List<T>();
        }

        #region IList<T> Members

        public int IndexOf(T item)
        {
            return this.items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            this.items.Insert(index, item);
            this.OnAddValue(item);
        }

        public void RemoveAt(int index)
        {
            object item = this.items[index];
            this.items.RemoveAt(index);
            this.OnRemoveValue(item);
        }

        public T this[int index]
        {
            get
            {
                return this.items[index];
            }
            set
            {
                object item = this.items[index];
                this.items[index] = null;
                this.OnRemoveValue(item);
                item = value;
                this.items[index] = value;
                this.OnAddValue(item);
            }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            this.items.Add(item);
            this.OnAddValue(item);
        }

        public void Clear()
        {
            List<T> oldItems = this.items;
            this.items = new List<T>();
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

        public void RemoveAll(T item)
        {
            bool removed = false;
            while (this.items.Remove(item))
            {
                removed = true;
            }
            if (removed)
            {
                this.OnRemoveValue(item);
            }
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
            return this.GetEnumerator();
        }

        #endregion

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                this.Add(item);
            }
        }

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
                this.RemoveAll((T)item);
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
