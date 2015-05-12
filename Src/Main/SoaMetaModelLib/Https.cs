using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    [ModelLiteral("HTTPS")]
    public class HttpsTransportBindingElement : TransportBindingElement
    {
        public HttpsTransportBindingElement()
        {
            this.ClientAuthentication = HttpsClientAuthentication.None;
        }

        public HttpsClientAuthentication ClientAuthentication
        {
            get;
            set;
        }
    }

    [ModelLiteral("HttpsAuthentication")]
    public enum HttpsClientAuthentication
    {
        None,
        Certificate
    }
}
