using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoaMetaModel
{
    public enum NetbeansVersion
    {
        Netbeans6,
        Netbeans8
    }

    public static class GeneratorLibExtensions
    {
        /*
         * Returns Uri, if null returns http://default
        */
        public static string GetUri(Namespace ns)
        {
            if (ns.Uri == null)
                return "http://default";
            else
                return ns.Uri;
        }

        /*
         * Returns the not null Uri with slash at end
         */
        public static string GetUriWithSlash(Namespace ns)
        {
            return GetUri(ns).EndsWith("/") ? GetUri(ns) : GetUri(ns) + "/";
        }

        public static bool HasNonArrayFields(this StructType type)
        {
            return type.Fields.Where(fi => !(fi.Type is ArrayType)).Count() > 0;
        }

        public static bool HasNonArrayFields(this ExceptionType type)
        {
            return type.Fields.Where(fi => !(fi.Type is ArrayType)).Count() > 0;
        }

        public static bool HasNonArrayFields(this ClaimsetType type)
        {
            return type.Fields.Where(fi => !(fi.Type is ArrayType)).Count() > 0;
        }

        /*
         * Get package name from namespace uri, splitted at : / and . characters, in reverse order
        */
        public static string GetPackage(Namespace ns)
        {
            return ns.FullName;
            //string[] withoutprefix = GetUriWithSlash(ns).Split(new char[] { ':', '/', '.' });
            //string retVal = "";
            //for (int i = withoutprefix.Length - 2; ; i--)
            //{
            //    if (withoutprefix[i] == "")
            //    {
            //        break;
            //    }
            //    retVal += withoutprefix[i] + ".";
            //}
            //return retVal.Remove(retVal.Length - 1);
        }

        public static bool HasDeclarations(this Namespace ns)
        {
            return ns.Declarations.Count(d => !(d is Namespace)) > 0;
        }

        public static bool HasInterfaces(this Namespace ns)
        {
            return ns.Declarations.Count(d => d is Interface) > 0;
        }

        public static bool HasEndpoints(this Namespace ns)
        {
            return ns.Declarations.Count(d => d is Endpoint) > 0;
        }

        public static IEnumerable<StructType> GetAllDescendants(this StructType type)
        {
            List<StructType> result = new List<StructType>();
            result.Add(type);
            bool foundNew = true;
            while (foundNew)
            {
                foundNew = false;
                foreach (var st in type.Model.Instances.OfType<StructType>())
                {
                    StructType sup = st.SuperType;
                    if (sup != null && result.Contains(sup))
                    {
                        if (!result.Contains(st))
                        {
                            foundNew = true;
                            result.Add(st);
                        }
                    }
                }
            }
            result.Remove(type);
            return result;
        }

        public static IEnumerable<ExceptionType> GetAllDescendants(this ExceptionType type)
        {
            List<ExceptionType> result = new List<ExceptionType>();
            result.Add(type);
            bool foundNew = true;
            while (foundNew)
            {
                foundNew = false;
                foreach (var st in type.Model.Instances.OfType<ExceptionType>())
                {
                    ExceptionType sup = st.SuperType;
                    if (sup != null && result.Contains(sup))
                    {
                        if (!result.Contains(st))
                        {
                            foundNew = true;
                            result.Add(st);
                        }
                    }
                }
            }
            result.Remove(type);
            return result;
        }

        public static string GetRootDirForJava(this Namespace ns)
        {
            int dotCount = ns.FullName.Count(c => c == '.');
            string result = "";
            for (int i = 0; i <= dotCount; i++)
            {
                if (result != "") result += "/";
                result += "..";
            }
            return result;
        }

        public static bool HasAddressing(this Endpoint endpoint)
        {
            return endpoint.Binding.Protocols.Count(pbe => pbe is AddressingProtocolBindingElement) > 0;
        }

        public static bool HasSecurity(this Endpoint endpoint)
        {
        	return endpoint.Binding.Protocols.Count(pbe => pbe is SecurityProtocolBindingElement) > 0;
        }

        public static bool HasReliableMessaging(this Endpoint endpoint)
        {
            return endpoint.Binding.Protocols.Count(pbe => pbe is ReliableMessagingProtocolBindingElement) > 0;
        }

        public static ReliableMessagingProtocolBindingElement GetReliableMessaging(this Endpoint endpoint)
        {
            return (ReliableMessagingProtocolBindingElement)endpoint.Binding.Protocols.FirstOrDefault(pbe => pbe is ReliableMessagingProtocolBindingElement);
        }

        public static bool HasPolicy(this Binding binding)
        {
            return binding.Protocols.Count > 0 || binding.Transport.GetType() == typeof(HttpsTransportBindingElement) || ((binding.Encoding is SoapEncodingBindingElement) && ((SoapEncodingBindingElement)binding.Encoding).MtomEnabled); 
        }
    }
}
