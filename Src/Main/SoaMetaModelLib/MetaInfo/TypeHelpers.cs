using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoaMetaModel.MetaInfo
{
    public static class TypeHelpers
    {
        public static bool IsSame(SoaObject typeTo, SoaObject typeFrom)
        {
            if (typeTo == null || typeFrom == null)
            {
                return false;
            }
            else
            {
                return typeTo == typeFrom;
            }
        }

        public static bool IsAssignableFrom(SoaObject typeTo, SoaObject typeFrom)
        {
            if (typeTo == null || typeFrom == null) return false;
            if (typeTo is Declaration && typeFrom is Declaration)
            {
                if (typeTo == PseudoType.Object)
                {
                    return true;
                }
                if (typeFrom is Contract && typeTo is Interface)
                {
                    return TypeHelpers.IsAssignableFrom(typeTo, ((Contract)typeFrom).Interface);
                }
                if (typeFrom is Authorization && typeTo is Interface)
                {
                    return TypeHelpers.IsAssignableFrom(typeTo, ((Authorization)typeFrom).Interface);
                }
                if (typeFrom is EnumType && typeTo is EnumType)
                {
                    return (typeFrom == typeTo);
                }
                if (typeFrom is StructType && typeTo is StructType)
                {
                    return (typeFrom == typeTo) || ((StructType)typeFrom).GetSuperTypes().Contains(typeTo);
                }
                if (typeFrom is ExceptionType && typeTo is ExceptionType)
                {
                    return (typeFrom == typeTo) || ((ExceptionType)typeFrom).GetSuperTypes().Contains(typeTo);
                }
                if (typeFrom is Interface && typeTo is Interface)
                {
                    return (typeFrom == typeTo) || ((Interface)typeFrom).GetSuperTypes().Contains(typeTo);
                }
                if (typeTo is NullableType && typeFrom is NullableType)
                {
                    if (((NullableType)typeTo) == ((NullableType)typeFrom))
                    {
                        return true;
                    }
                }
                if (typeTo is ArrayType && typeFrom is ArrayType)
                {
                    if (((ArrayType)typeTo) == ((ArrayType)typeFrom))
                    {
                        return true;
                    }
                }
                if (typeTo is DelegateType && typeFrom is DelegateType)
                {
                    if (((DelegateType)typeTo) == ((DelegateType)typeFrom))
                    {
                        return true;
                    }
                }
                if (typeTo is BuiltInType && typeFrom is BuiltInType)
                {
                    BuiltInTypeKind kindTo = ((BuiltInType)typeTo).Kind;
                    BuiltInTypeKind kindFrom = ((BuiltInType)typeFrom).Kind;
                    switch (kindTo)
                    {
                        case BuiltInTypeKind.Bool:
                        case BuiltInTypeKind.String:
                        case BuiltInTypeKind.Guid:
                        case BuiltInTypeKind.DateTime:
                        case BuiltInTypeKind.Date:
                        case BuiltInTypeKind.Time:
                        case BuiltInTypeKind.TimeSpan:
                            return kindFrom == kindTo;
                        case BuiltInTypeKind.Byte:
                            if (kindFrom == BuiltInTypeKind.Byte) return true;
                            else
                                if (kindFrom == BuiltInTypeKind.Int ||
                                    kindFrom == BuiltInTypeKind.Long ||
                                    kindFrom == BuiltInTypeKind.Float ||
                                    kindFrom == BuiltInTypeKind.Double) return false;
                            break;
                        case BuiltInTypeKind.Int:
                            if (kindFrom == BuiltInTypeKind.Byte ||
                                kindFrom == BuiltInTypeKind.Int) return true;
                            else
                                if (kindFrom == BuiltInTypeKind.Long ||
                                    kindFrom == BuiltInTypeKind.Float ||
                                    kindFrom == BuiltInTypeKind.Double) return false;
                            break;
                        case BuiltInTypeKind.Long:
                            if (kindFrom == BuiltInTypeKind.Byte ||
                                kindFrom == BuiltInTypeKind.Int ||
                                kindFrom == BuiltInTypeKind.Long) return true;
                            else
                                if (kindFrom == BuiltInTypeKind.Float ||
                                    kindFrom == BuiltInTypeKind.Double) return false;
                            break;
                        case BuiltInTypeKind.Float:
                            if (kindFrom == BuiltInTypeKind.Byte ||
                                kindFrom == BuiltInTypeKind.Int ||
                                kindFrom == BuiltInTypeKind.Long ||
                                kindFrom == BuiltInTypeKind.Float) return true;
                            else
                                if (kindFrom == BuiltInTypeKind.Double) return false;
                            break;
                        case BuiltInTypeKind.Double:
                            if (kindFrom == BuiltInTypeKind.Byte ||
                                kindFrom == BuiltInTypeKind.Int ||
                                kindFrom == BuiltInTypeKind.Long ||
                                kindFrom == BuiltInTypeKind.Float ||
                                kindFrom == BuiltInTypeKind.Double) return true;
                            break;
                        default:
                            break;
                    }
                }
            }
            return false;
        }

        public static SoaObject GetCommonType(SoaObject type1, SoaObject type2)
        {
            if (type1 == null) throw new ArgumentNullException("type1");
            if (type2 == null) throw new ArgumentNullException("type2");
            if (type1 is Declaration && type2 is Declaration)
            {
                if (type1 == PseudoType.Object || type2 == PseudoType.Object)
                {
                    return PseudoType.Object;
                }
                if (type2 is Contract && type1 is Interface)
                {
                    return TypeHelpers.GetCommonType(type1, ((Contract)type2).Interface);
                }
                if (type1 is Contract && type2 is Interface)
                {
                    return TypeHelpers.GetCommonType(type2, ((Contract)type1).Interface);
                }
                if (type2 is Authorization && type1 is Interface)
                {
                    return TypeHelpers.GetCommonType(type1, ((Authorization)type2).Interface);
                }
                if (type1 is Authorization && type2 is Interface)
                {
                    return TypeHelpers.GetCommonType(type2, ((Authorization)type1).Interface);
                }
                if (type1 is StructType && type2 is StructType)
                {
                    if (type1 == type2)
                    {
                        return type1;
                    }
                    if (((StructType)type2).GetSuperTypes().Contains(type1))
                    {
                        return type1;
                    }
                    if (((StructType)type1).GetSuperTypes().Contains(type2))
                    {
                        return type2;
                    }
                }
                if (type1 is ExceptionType && type2 is ExceptionType)
                {
                    if (type1 == type2)
                    {
                        return type1;
                    }
                    if (((ExceptionType)type2).GetSuperTypes().Contains(type1))
                    {
                        return type1;
                    }
                    if (((ExceptionType)type1).GetSuperTypes().Contains(type2))
                    {
                        return type2;
                    }
                }
                if (type1 is Interface && type2 is Interface)
                {
                    if (type1 == type2)
                    {
                        return type1;
                    }
                    if (((Interface)type2).GetSuperTypes().Contains(type1))
                    {
                        return type1;
                    }
                    if (((Interface)type1).GetSuperTypes().Contains(type2))
                    {
                        return type2;
                    }
                }
                if (type1 is NullableType && type2 is NullableType)
                {
                    if (((NullableType)type1) == ((NullableType)type2))
                    {
                        return type1;
                    }
                }
                if (type1 is ArrayType && type2 is ArrayType)
                {
                    if (((ArrayType)type1) == ((ArrayType)type2))
                    {
                        return type1;
                    }
                }
                if (type1 is DelegateType && type2 is DelegateType)
                {
                    if (((DelegateType)type1) == ((DelegateType)type2))
                    {
                        return type1;
                    }
                }
                if (type1 is BuiltInType && type2 is BuiltInType)
                {
                    BuiltInTypeKind kind1 = ((BuiltInType)type1).Kind;
                    BuiltInTypeKind kind2 = ((BuiltInType)type2).Kind;
                    switch (kind1)
                    {
                        case BuiltInTypeKind.Bool:
                        case BuiltInTypeKind.String:
                        case BuiltInTypeKind.Guid:
                        case BuiltInTypeKind.DateTime:
                        case BuiltInTypeKind.Date:
                        case BuiltInTypeKind.Time:
                        case BuiltInTypeKind.TimeSpan:
                            if (kind2 == kind1) return type1;
                            break;
                        case BuiltInTypeKind.Byte:
                            if (kind2 == BuiltInTypeKind.Byte) return type1;
                            else 
                                if (kind2 == BuiltInTypeKind.Int || 
                                    kind2 == BuiltInTypeKind.Long ||
                                    kind2 == BuiltInTypeKind.Float || 
                                    kind2 == BuiltInTypeKind.Double) return type2;
                            break;
                        case BuiltInTypeKind.Int:
                            if (kind2 == BuiltInTypeKind.Byte ||
                                kind2 == BuiltInTypeKind.Int) return type1;
                            else 
                                if (kind2 == BuiltInTypeKind.Long ||
                                    kind2 == BuiltInTypeKind.Float || 
                                    kind2 == BuiltInTypeKind.Double) return type2;
                            break;
                        case BuiltInTypeKind.Long:
                            if (kind2 == BuiltInTypeKind.Byte ||
                                kind2 == BuiltInTypeKind.Int ||
                                kind2 == BuiltInTypeKind.Long) return type1;
                            else 
                                if (kind2 == BuiltInTypeKind.Float || 
                                    kind2 == BuiltInTypeKind.Double) return type2;
                            break;
                        case BuiltInTypeKind.Float:
                            if (kind2 == BuiltInTypeKind.Byte ||
                                kind2 == BuiltInTypeKind.Int ||
                                kind2 == BuiltInTypeKind.Long ||
                                kind2 == BuiltInTypeKind.Float) return type1;
                            else 
                                if (kind2 == BuiltInTypeKind.Double) return type2;
                            break;
                        case BuiltInTypeKind.Double:
                            if (kind2 == BuiltInTypeKind.Byte ||
                                kind2 == BuiltInTypeKind.Int ||
                                kind2 == BuiltInTypeKind.Long ||
                                kind2 == BuiltInTypeKind.Float || 
                                kind2 == BuiltInTypeKind.Double) return type2;
                            break;
                        default:
                            break;
                    }
                }
            }
            throw new ValidationException(string.Format("Incompatible types: '{0}' and '{1}'", type1, type2));
        }
    }
}
