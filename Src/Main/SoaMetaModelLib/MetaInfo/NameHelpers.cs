using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoaMetaModel.MetaInfo
{
    public static class NameHelpers
    {
        public static void CheckName(IEnumerable<SoaObject> objects, SoaObject scope, string name, params System.Type[] types)
        {
            List<SoaObject> objs = new List<SoaObject>(objects);
            if (objs.Count > 0)
            {
                throw new NameCollisionException(scope, name, types, objects);
            }
        }

        public static SoaObject SelectName(IEnumerable<SoaObject> objects, SoaObject scope, string name, params System.Type[] types)
        {
            List<SoaObject> objs = new List<SoaObject>(objects);
            if (objs.Count == 1)
            {
                return objs[0];
            }
            else if (objs.Count > 1)
            {
                throw new NameCollisionException(scope, name, types, objects);
            }
            else
            {
                throw new NameNotFoundException(scope, name, types);
            }
        }

        public static IEnumerable<SoaObject> LookupNameMany(SoaObject scope, string name, params System.Type[] types)
        {
            if (scope is Namespace)
            {
                return
                    from d in ((Namespace)scope).Declarations
                    where d.Name == name && types.Contains(d.GetType())
                    select d;
            }
            if (scope is Authorization)
            {
                return
                    from opa in ((Authorization)scope).OperationAuthorizations
                    where (opa.Operation != null) && (opa.Operation.Name == name) && types.Contains(opa.GetType())
                    select opa;
            }
            if (scope is Contract)
            {
                return
                    from opc in ((Contract)scope).OperationContracts
                    where (opc.Operation != null) && (opc.Operation.Name == name) && types.Contains(opc.GetType())
                    select opc;
            }
            if (scope is Operation)
            {
                return
                    from p in ((Operation)scope).Parameters
                    where p.Name == name && types.Contains(p.GetType())
                    select p;
            }
            if (scope is OperationAuthorization)
            {
                return
                    from r in ((OperationAuthorization)scope).References
                    where r.Name == name && types.Contains(r.GetType())
                    select r;
            }
            if (scope is OperationContract)
            {
                return
                    from r in ((OperationContract)scope).References
                    where r.Name == name && types.Contains(r.GetType())
                    select r;
            }
            if (scope is Binding)
            {
                return
                    from p in ((Binding)scope).Protocols
                    where p.Name == name && types.Contains(typeof(ProtocolBindingElement))
                    select p;
            }
            if (scope is BindingElement)
            {
                return
                    from p in ((BindingElement)scope).Properties
                    where p.Name == name && types.Contains(p.GetType())
                    select p;
            }
            if (scope is ClaimsetType)
            {
                return
                    from field in ((ClaimsetType)scope).Fields
                    where field.Name == name && types.Contains(field.GetType())
                    select field;
            }
            if (scope is EnumType)
            {
                return
                    from v in ((EnumType)scope).Values
                    where v.Name == name && types.Contains(v.GetType())
                    select v;
            }
            if (scope is StructType)
            {
                StructType structType = scope as StructType;

                List<StructType> structs = new List<StructType>();
                structs.Add(structType);
                structs.AddRange(structType.GetSuperTypes());

                List<SoaObject> result = new List<SoaObject>();
                foreach (StructType strct in structs)
                {
                    foreach (StructField field in strct.Fields)
                    {
                        if (types.Contains(field.GetType()) && field.Name == name)
                        {
                            result.Add(field);
                        }
                    }
                }
                return result;
            }
            if (scope is ExceptionType)
            {
                ExceptionType exceptionType = scope as ExceptionType;

                List<ExceptionType> exceptions = new List<ExceptionType>();
                exceptions.Add(exceptionType);
                exceptions.AddRange(exceptionType.GetSuperTypes());

                List<SoaObject> result = new List<SoaObject>();
                foreach (ExceptionType exception in exceptions)
                {
                    foreach (ExceptionField field in exception.Fields)
                    {
                        if (types.Contains(field.GetType()) && field.Name == name)
                        {
                            result.Add(field);
                        }
                    }
                }
                return result;
            }
            if (scope is Interface)
            {
                Interface intf = scope as Interface;

                List<Interface> interfaces = new List<Interface>();
                interfaces.Add(intf);
                interfaces.AddRange(intf.GetSuperTypes());

                List<SoaObject> result = new List<SoaObject>();
                foreach (Interface iface in interfaces)
                {
                    foreach (Operation operation in iface.Operations)
                    {
                        if (types.Contains(operation.GetType()) && operation.Name == name)
                        {
                            result.Add(operation);
                        }
                    }
                }
                return result;
            }
            return new List<SoaObject>();
        }

        public static IEnumerable<SoaObject> LookupOperationMany(SoaObject scope, string name, List<Type> paramTypes, bool exact)
        {
            if (scope is Interface)
            {
                Interface intf = scope as Interface;

                List<Interface> interfaces = new List<Interface>();
                interfaces.Add(intf);
                interfaces.AddRange(intf.GetSuperTypes());

                List<SoaObject> result = new List<SoaObject>();
                foreach (Interface iface in interfaces)
                {
                    foreach (Operation operation in iface.Operations)
                    {
                        if (operation.Name == name && operation.Parameters.Count == paramTypes.Count)
                        {
                            int i = 0;
                            while (i < operation.Parameters.Count)
                            {
                                if (exact ? TypeHelpers.IsSame(operation.Parameters[i].Type, paramTypes[i]) : TypeHelpers.IsAssignableFrom(operation.Parameters[i].Type, paramTypes[i]))
                                {
                                    ++i;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (i == operation.Parameters.Count)
                            {
                                result.Add(operation);
                            }
                        }
                    }
                }
                return result;
            }
            if (scope is Authorization)
            {
                return NameHelpers.LookupOperationMany(((Authorization)scope).Interface, name, paramTypes, exact);
            }
            if (scope is Contract)
            {
                return NameHelpers.LookupOperationMany(((Contract)scope).Interface, name, paramTypes, exact);
            }
            return new List<SoaObject>();
        }

    }
}
