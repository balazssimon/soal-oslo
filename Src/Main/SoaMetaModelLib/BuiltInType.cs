using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoaMetaModel.MetaInfo;

namespace SoaMetaModel
{
    public class BuiltInType : SimpleType
    {
        private static Dictionary<string, BuiltInType> typesByName = new Dictionary<string, BuiltInType>();
        private static Dictionary<BuiltInTypeKind, BuiltInType> typesByKind = new Dictionary<BuiltInTypeKind, BuiltInType>();
        private static Dictionary<System.Type, BuiltInType> typesByType = new Dictionary<System.Type, BuiltInType>();

        protected BuiltInType(string name, BuiltInTypeKind kind, System.Type type = null)
        {
            this.Name = name;
            this.Kind = kind;
            this.UnderlyingType = type;

            typesByName.Add(this.Name, this);
            typesByKind.Add(this.Kind, this);
            if (type != null) typesByType.Add(this.UnderlyingType, this);

            this.AddMetaInfo(new HiddenInfo());
        }

        public BuiltInTypeKind Kind
        {
            get;
            private set;
        }

        public static readonly BuiltInType Bool = new BuiltInType("bool", BuiltInTypeKind.Bool, typeof(bool));
        public static readonly BuiltInType Byte = new BuiltInType("byte", BuiltInTypeKind.Byte, typeof(byte));
        public static readonly BuiltInType Int = new BuiltInType("int", BuiltInTypeKind.Int, typeof(int));
        public static readonly BuiltInType Long = new BuiltInType("long", BuiltInTypeKind.Long, typeof(long));
        public static readonly BuiltInType Float = new BuiltInType("float", BuiltInTypeKind.Float, typeof(float));
        public static readonly BuiltInType Double = new BuiltInType("double", BuiltInTypeKind.Double, typeof(double));
        public static readonly BuiltInType String = new BuiltInType("string", BuiltInTypeKind.String, typeof(string));
        public static readonly BuiltInType Guid = new BuiltInType("Guid", BuiltInTypeKind.Guid, typeof(System.Guid));
        public static readonly BuiltInType Date = new BuiltInType("Date", BuiltInTypeKind.Date);
        public static readonly BuiltInType Time = new BuiltInType("Time", BuiltInTypeKind.Time);
        public static readonly BuiltInType DateTime = new BuiltInType("DateTime", BuiltInTypeKind.DateTime, typeof(System.DateTime));
        public static readonly BuiltInType TimeSpan = new BuiltInType("TimeSpan", BuiltInTypeKind.TimeSpan, typeof(System.TimeSpan));

        public static BuiltInType GetBuiltInType(string name)
        {
            try
            {
                return typesByName[name];
            }
            catch
            {
                return null;
            }
        }

        public static BuiltInType GetBuiltInType(BuiltInTypeKind kind)
        {
            try
            {
                return typesByKind[kind];
            }
            catch
            {
                return null;
            }
        }

        public static BuiltInType GetBuiltInType(System.Type type)
        {
            try
            {
                return typesByType[type];
            }
            catch
            {
                return null;
            }
        }

    }

    public enum BuiltInTypeKind
    {
        Byte,
        Bool,
        Int,
        Long,
        Float,
        Double,
        String,
        DateTime,
        Guid,
        Date,
        Time,
        TimeSpan
    }
}
