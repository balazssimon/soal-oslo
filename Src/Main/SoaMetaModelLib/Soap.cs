using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    [ModelLiteral("SOAP")]
    public class SoapEncodingBindingElement : EncodingBindingElement
    {
        public SoapEncodingBindingElement()
        {
            this.Version = SoapVersion.Soap11;
            this.MtomEnabled = false;
        }

        public SoapVersion Version
        {
            get;
            set;
        }

        [ModelLiteral("MTOM")]
        public bool MtomEnabled
        {
            get;
            set;
        }
    }

    [ModelLiteral("SoapVersion")]
    public enum SoapVersion
    {
        [ModelLiteral("Soap11")]
        Soap11,
        [ModelLiteral("Soap12")]
        Soap12
    }
}
