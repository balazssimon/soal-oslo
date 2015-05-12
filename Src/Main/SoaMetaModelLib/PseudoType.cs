using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoaMetaModel.MetaInfo;

namespace SoaMetaModel
{
    public class PseudoType : Type
    {
        protected PseudoType(PseudoTypeKind kind)
        {
            Kind = kind;

            this.AddMetaInfo(new HiddenInfo());
        }

        public PseudoTypeKind Kind
        {
            get;
            set;
        }

        public static readonly PseudoType Void = new PseudoType(PseudoTypeKind.Void);
        public static readonly PseudoType Async = new PseudoType(PseudoTypeKind.Async);
        public static readonly PseudoType Object = new PseudoType(PseudoTypeKind.Object);
    }

    public enum PseudoTypeKind
    {
        Void,
        Async,
        Object
    }
}
