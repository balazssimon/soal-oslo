using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sb.Meta
{
    public class ModelClass : IModelClass, INotifyModelClass
    {
        private Dictionary<ModelProperty, object> values;
        private Dictionary<ModelProperty, Func<object>> initializers;

        public ModelClass()
        {
            this.values = new Dictionary<ModelProperty, object>();
            this.initializers = new Dictionary<ModelProperty, Func<object>>();
        }

        public void LazyInit(ModelProperty property, Func<object> value)
        {
            this.initializers.Add(property, value);
        }

        public static void LazyInit(ModelClass obj, ModelProperty property, Func<object> value)
        {
            obj.initializers.Add(property, value);
        }

        #region IMetaClass Members

        public void SetValue(ModelProperty property, object newValue)
        {
            object oldValue;
            if (this.values.TryGetValue(property, out oldValue))
            {
                if (newValue == oldValue) return;
            }
            if (property.IsCollection)
            {
                this.values[property] = newValue;
            }
            else
            {
                (this as INotifyModelClass).OnAddValue(property, newValue, true);
            }
        }

        public object GetValue(ModelProperty property)
        {
            object value;
            if (this.values.TryGetValue(property, out value))
            {
                return value;
            }
            else
            {
                Func<object> initializer;
                if (this.initializers.TryGetValue(property, out initializer))
                {
                    value = initializer();
                    this.values[property] = value;
                    if (!property.IsCollection)
                    {
                        (this as INotifyModelClass).OnAddValue(property, value, true);
                    }
                    return value;
                }
            }
            return null;
        }

        #endregion

        #region INotifyModelClass Members

        void INotifyModelClass.OnAddValue(ModelProperty property, object value, bool firstCall)
        {
            bool added = false;
            if (property.IsCollection)
            {
                IModelCollection collection = this.GetValue(property) as IModelCollection;
                if (collection != null)
                {
                    if (value != null && collection.Add(value))
                    {
                        added = true;
                    }
                    else if (value != null && firstCall)
                    {
                        added = true;
                    }
                }
            }
            else
            {
                object oldValue = this.GetValue(property);
                if (value != oldValue)
                {
                    if (oldValue != null)
                    {
                        (this as INotifyModelClass).OnRemoveValue(property, oldValue, false);
                    }
                    this.values[property] = value;
                    added = value != null;
                }
                else
                {
                    added = value != null && firstCall;
                }
            }
            if (added)
            {
                ModelProperty oppositeProperty = property.Opposite;
                if (oppositeProperty != null)
                {
                    INotifyModelClass oppositeObject = value as INotifyModelClass;
                    if (oppositeObject != null)
                    {
                        oppositeObject.OnAddValue(oppositeProperty, this, false);
                    }
                }
            }
        }

        void INotifyModelClass.OnRemoveValue(ModelProperty property, object value, bool firstCall)
        {
            bool removed = false;
            if (property.IsCollection)
            {
                IModelCollection collection = this.GetValue(property) as IModelCollection;
                if (collection != null)
                {
                    if (value != null && collection.Remove(value))
                    {
                        removed = true;
                    }
                    else if (value != null && firstCall)
                    {
                        removed = true;
                    }
                }
            }
            else
            {
                object oldValue = this.GetValue(property);
                if (value == oldValue) 
                {
                    this.values[property] = null;
                    removed = value != null;
                }
            }
            if (removed)
            {
                ModelProperty oppositeProperty = property.Opposite;
                if (oppositeProperty != null)
                {
                    INotifyModelClass oppositeObject = value as INotifyModelClass;
                    if (oppositeObject != null)
                    {
                        oppositeObject.OnRemoveValue(oppositeProperty, this, false);
                    }
                }
            }
        }

        #endregion
    }
}
