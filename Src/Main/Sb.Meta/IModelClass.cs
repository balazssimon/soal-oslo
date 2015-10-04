using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sb.Meta
{
    public interface IModelClass
    {
        void SetValue(ModelProperty property, object value);
        object GetValue(ModelProperty property);
    }

    public interface INotifyModelClass
    {
        void OnAddValue(ModelProperty property, object value, bool firstCall);
        void OnRemoveValue(ModelProperty property, object value, bool firstCall);
    }

}
