using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoaMetaModel
{
    public class SecurityProtocolBindingElement : ProtocolBindingElement
    {
        public SecurityProtocolBindingElement()
        {
            this.AlgorithmSuite = SecurityAlgorithmSuite.Basic128;
            this.HeaderLayout = SecurityHeaderLayout.Strict;
            this.ProtectionOrder = SecurityProtectionOrder.SignBeforeEncrypt;
            this.RequireSignatureConfirmation = false;
        }

        public SecurityAlgorithmSuite AlgorithmSuite
        {
            get;
            set;
        }

        public SecurityHeaderLayout HeaderLayout
        {
            get;
            set;
        }

        public SecurityProtectionOrder ProtectionOrder
        {
            get;
            set;
        }

        public bool RequireSignatureConfirmation
        {
            get;
            set;
        }
    }

    public enum SecurityAlgorithmSuite
    {
        Basic128,
        Basic192,
        Basic256,
        TripleDes,
        Basic128Rsa15,
        Basic192Rsa15,
        Basic256Rsa15,
        TripleDesRsa15,
        Basic128Sha256,
        Basic192Sha256,
        Basic256Sha256,
        TripleDesSha256,
        Basic128Sha256Rsa15,
        Basic192Sha256Rsa15,
        Basic256Sha256Rsa15,
        TripleDesSha256Rsa15
    }

    public enum SecurityHeaderLayout
    {
        Strict,
        Lax,
        LaxTimestampFirst,
        LaxTimestampLast
    }

    public enum SecurityProtectionOrder
    {
        EncryptBeforeSign,
        SignBeforeEncrypt,
        SignBeforeEncryptAndEncryptSignature
    }
}
