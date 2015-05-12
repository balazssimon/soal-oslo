using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;
using SoaMetaModel.MetaInfo;

namespace SoaMetaModel
{
    public class DelegateType : Type
    {
        private static List<DelegateType> instances = new List<DelegateType>();

        public static DelegateType CreateFrom(Type returnType, Type[] paramTypes)
        {
            // Find matching instance if exists
            foreach (DelegateType instance in instances)
            {
                if (instance.ReturnType != returnType) break;
                if (instance.ParameterTypes.Count != paramTypes.Length) break;
                bool match = true;
                for (int i = 0; i < instance.ParameterTypes.Count; i++)
                {
                    if (instance.ParameterTypes[i] != paramTypes[i]) {
                        match = false;
                        break;
                    }
                }
                if (match) return instance;
            }

            // Create new instance
            DelegateType result = new DelegateType();
            result.ReturnType = returnType;
            foreach (Type paramType in paramTypes)
            {
                result.ParameterTypes.Add(paramType);
            }
            instances.Add(result);
            return result;
        }

        public DelegateType()
        {
            this.ParameterTypes = new List<Type>();
        }

        public Type ReturnType
        {
            get;
            set;
        }

        public List<Type> ParameterTypes
        {
            get;
            set;
        }
    }
}
