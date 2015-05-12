using OsloExtensions;
using OsloExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoaMetaModel
{
    // Inheritace from 'Generator<List<object>, GeneratorContext>' and constructor is only generated into the main file.
    public partial class VSGenerator
    {
            #region functions from "C:\Users\Balazs\Documents\Visual Studio 2013\Projects\SoaMM\SoaGeneratorLib\VSGeneratorLib.mcg"
            public List<string> Generated_GenerateService(Endpoint endp)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<%@ ServiceHost Language=\"C#\" Debug=\"true\" Service=\"");
                    __printer.Write(endp.Namespace.FullName);
                    __printer.WriteTemplateOutput(".");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("\" CodeBehind=\"~/App_Code/");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput(".cs\" %>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWebConfig()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<configuration>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <system.serviceModel>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.Write(Generated_GenerateBindings());
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.Write(Generated_GenerateBehaviors());
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.Write(Generated_GenerateServices());
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </system.serviceModel>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</configuration>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateClientAppConfig()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<configuration>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <system.serviceModel>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.Write(Generated_GenerateBindings());
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.Write(Generated_GenerateClientBehaviors());
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.Write(Generated_GenerateClientEndpoints());
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </system.serviceModel>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</configuration>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateBindings()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<bindings>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <customBinding>");
                    __printer.WriteLine();
                    int __loop1_iteration = 0;
                    var __loop1_result =
                        (from __loop1_tmp_item___noname1 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop1_tmp_item_binding in EnumerableExtensions.Enumerate((__loop1_tmp_item___noname1).GetEnumerator()).OfType<Binding>()
                        select
                            new
                            {
                                __loop1_item___noname1 = __loop1_tmp_item___noname1,
                                __loop1_item_binding = __loop1_tmp_item_binding,
                            }).ToArray();
                    foreach (var __loop1_item in __loop1_result)
                    {
                        var __noname1 = __loop1_item.__loop1_item___noname1;
                        var binding = __loop1_item.__loop1_item_binding;
                        ++__loop1_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <binding name=\"");
                        __printer.Write(binding.Name);
                        __printer.WriteTemplateOutput("\">");
                        __printer.WriteLine();
                        int __loop2_iteration = 0;
                        var __loop2_result =
                            (from __loop2_tmp_item___noname2 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                            from __loop2_tmp_item_security in EnumerableExtensions.Enumerate((__loop2_tmp_item___noname2).GetEnumerator()).OfType<SecurityProtocolBindingElement>()
                            select
                                new
                                {
                                    __loop2_item___noname2 = __loop2_tmp_item___noname2,
                                    __loop2_item_security = __loop2_tmp_item_security,
                                }).ToArray();
                        foreach (var __loop2_item in __loop2_result)
                        {
                            var __noname2 = __loop2_item.__loop2_item___noname2;
                            var security = __loop2_item.__loop2_item_security;
                            ++__loop2_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("      <security defaultAlgorithmSuite=\"");
                            __printer.Write(security.AlgorithmSuite.ToString());
                            __printer.WriteTemplateOutput("\" securityHeaderLayout=\"");
                            __printer.Write(security.HeaderLayout.ToString());
                            __printer.WriteTemplateOutput("\" messageProtectionOrder=\"");
                            __printer.Write(security.ProtectionOrder.ToString());
                            __printer.WriteTemplateOutput("\" requireSignatureConfirmation=\"");
                            __printer.Write(security.RequireSignatureConfirmation.ToString().ToLower());
                            __printer.WriteTemplateOutput("\" \\");
                            __printer.WriteLine();
                            if (security is MutualCertificateSecurityProtocolBindingElement)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("authenticationMode=\"MutualCertificateDuplex\" messageSecurityVersion=\"WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10\">");
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            if (security is StsSecurityProtocolBindingElement)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("authenticationMode=\"IssuedToken\" messageSecurityVersion=\"WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10\" requireDerivedKeys=\"");
                                __printer.Write(((StsSecurityProtocolBindingElement)security).DerivedKeys.ToString().ToLower());
                                __printer.WriteTemplateOutput("\">");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        ");
                                __printer.Write(Generated_GenerateIssuedTokenParameters(((StsSecurityProtocolBindingElement)security).TokenVersion, ((StsSecurityProtocolBindingElement)security).TokenType, ((StsSecurityProtocolBindingElement)security).TokenIssuer, null));
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            if (security is SamlSecurityProtocolBindingElement)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("authenticationMode=\"IssuedToken\" messageSecurityVersion=\"WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10\" >");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        ");
                                __printer.Write(Generated_GenerateIssuedTokenParameters(((SamlSecurityProtocolBindingElement)security).TokenVersion, ((SamlSecurityProtocolBindingElement)security).TokenType, ((SamlSecurityProtocolBindingElement)security).TokenIssuer, ((SamlSecurityProtocolBindingElement)security).Claims));
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            if (security is SecureConversationSecurityProtocolBindingElement)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("authenticationMode=\"SecureConversation\" messageSecurityVersion=\"WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10\" requireDerivedKeys=\"");
                                __printer.Write(((SecureConversationSecurityProtocolBindingElement)security).DerivedKeys.ToString().ToLower());
                                __printer.WriteTemplateOutput("\">");
                                __printer.WriteLine();
                                if (((SecureConversationSecurityProtocolBindingElement)security).Bootstrap != null)
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("        <secureConversationBootstrap defaultAlgorithmSuite=\"");
                                    __printer.Write(security.AlgorithmSuite.ToString());
                                    __printer.WriteTemplateOutput("\" securityHeaderLayout=\"");
                                    __printer.Write(security.HeaderLayout.ToString());
                                    __printer.WriteTemplateOutput("\" messageProtectionOrder=\"");
                                    __printer.Write(security.ProtectionOrder.ToString());
                                    __printer.WriteTemplateOutput("\"  requireSignatureConfirmation=\"");
                                    __printer.Write(((SecureConversationSecurityProtocolBindingElement)security).Bootstrap.RequireSignatureConfirmation.ToString().ToLower());
                                    __printer.WriteTemplateOutput("\"  requireDerivedKeys=\"");
                                    __printer.Write(((SecureConversationSecurityProtocolBindingElement)security).DerivedKeys.ToString().ToLower());
                                    __printer.WriteTemplateOutput("\" \\");
                                    __printer.WriteLine();
                                    if (((SecureConversationSecurityProtocolBindingElement)security).Bootstrap is MutualCertificateBootstrapProtocolBindingElement)
                                    {
                                        __printer.TrimLine();
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("authenticationMode=\"MutualCertificateDuplex\" messageSecurityVersion=\"WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10\"/>");
                                        __printer.WriteLine();
                                    }
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    if (((SecureConversationSecurityProtocolBindingElement)security).Bootstrap is StsBootstrapProtocolBindingElement)
                                    {
                                        __printer.TrimLine();
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("authenticationMode=\"IssuedToken\" messageSecurityVersion=\"WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10\">");
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("          ");
                                        __printer.Write(Generated_GenerateIssuedTokenParameters(((StsBootstrapProtocolBindingElement)((SecureConversationSecurityProtocolBindingElement)security).Bootstrap).TokenVersion, ((StsBootstrapProtocolBindingElement)((SecureConversationSecurityProtocolBindingElement)security).Bootstrap).TokenType, ((StsBootstrapProtocolBindingElement)((SecureConversationSecurityProtocolBindingElement)security).Bootstrap).TokenIssuer, null));
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("        </secureConversationBootstrap>");
                                        __printer.WriteLine();
                                    }
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    if (((SecureConversationSecurityProtocolBindingElement)security).Bootstrap is SamlBootstrapProtocolBindingElement)
                                    {
                                        __printer.TrimLine();
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("authenticationMode=\"SecureConversation\" messageSecurityVersion=\"WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10\">");
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("          ");
                                        __printer.Write(Generated_GenerateIssuedTokenParameters(((SamlBootstrapProtocolBindingElement)((SecureConversationSecurityProtocolBindingElement)security).Bootstrap).TokenVersion, ((SamlBootstrapProtocolBindingElement)((SecureConversationSecurityProtocolBindingElement)security).Bootstrap).TokenType, ((SamlBootstrapProtocolBindingElement)((SecureConversationSecurityProtocolBindingElement)security).Bootstrap).TokenIssuer, ((SamlBootstrapProtocolBindingElement)((SecureConversationSecurityProtocolBindingElement)security).Bootstrap).Claims));
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("        </secureConversationBootstrap>");
                                        __printer.WriteLine();
                                    }
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                }
                                else
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("        <secureConversationBootstrap />");
                                    __printer.WriteLine();
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        <secureConversationBootstrap />");
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("      </security>");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        int __loop3_iteration = 0;
                        var __loop3_result =
                            (from __loop3_tmp_item___noname3 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                            from __loop3_tmp_item_transaction in EnumerableExtensions.Enumerate((__loop3_tmp_item___noname3).GetEnumerator()).OfType<AtomicTransactionProtocolBindingElement>()
                            select
                                new
                                {
                                    __loop3_item___noname3 = __loop3_tmp_item___noname3,
                                    __loop3_item_transaction = __loop3_tmp_item_transaction,
                                }).ToArray();
                        foreach (var __loop3_item in __loop3_result)
                        {
                            var __noname3 = __loop3_item.__loop3_item___noname3;
                            var transaction = __loop3_item.__loop3_item_transaction;
                            ++__loop3_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            if (transaction.Version == AtomicTransactionVersion.AtomicTransaction10)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      <transactionFlow transactionProtocol=\"WSAtomicTransactionOctober2004\" />");
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        int __loop4_iteration = 0;
                        var __loop4_result =
                            (from __loop4_tmp_item___noname4 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                            from __loop4_tmp_item_reliable in EnumerableExtensions.Enumerate((__loop4_tmp_item___noname4).GetEnumerator()).OfType<ReliableMessagingProtocolBindingElement>()
                            select
                                new
                                {
                                    __loop4_item___noname4 = __loop4_tmp_item___noname4,
                                    __loop4_item_reliable = __loop4_tmp_item_reliable,
                                }).ToArray();
                        foreach (var __loop4_item in __loop4_result)
                        {
                            var __noname4 = __loop4_item.__loop4_item___noname4;
                            var reliable = __loop4_item.__loop4_item_reliable;
                            ++__loop4_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            if (reliable.Version == ReliableMessagingVersion.ReliableMessaging11)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                if (reliable.InOrder == true)
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("      <reliableSession reliableMessagingVersion=\"WSReliableMessaging11\" ordered=\"true\" />");
                                    __printer.WriteLine();
                                }
                                else
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("      <reliableSession reliableMessagingVersion=\"WSReliableMessaging11\" ordered=\"false\" />");
                                    __printer.WriteLine();
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (binding.Encoding is SoapEncodingBindingElement)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            if (((SoapEncodingBindingElement)binding.Encoding).MtomEnabled == true)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      <mtomMessageEncoding messageVersion=\"\\");
                                __printer.WriteLine();
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      <textMessageEncoding messageVersion=\"\\");
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            if (((SoapEncodingBindingElement)binding.Encoding).Version == SoapVersion.Soap11)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("Soap11\\");
                                __printer.WriteLine();
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("Soap12\\");
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            int __loop5_iteration = 0;
                            var __loop5_result =
                                (from __loop5_tmp_item___noname5 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                                from __loop5_tmp_item_addressing in EnumerableExtensions.Enumerate((__loop5_tmp_item___noname5).GetEnumerator()).OfType<AddressingProtocolBindingElement>()
                                select
                                    new
                                    {
                                        __loop5_item___noname5 = __loop5_tmp_item___noname5,
                                        __loop5_item_addressing = __loop5_tmp_item_addressing,
                                    }).ToArray();
                            foreach (var __loop5_item in __loop5_result)
                            {
                                var __noname5 = __loop5_item.__loop5_item___noname5;
                                var addressing = __loop5_item.__loop5_item_addressing;
                                ++__loop5_iteration;
                                __printer.TrimLine();
                                __printer.WriteLine();
                                if (addressing.Version == AddressingVersion.Addressing10)
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("WSAddressing10\\");
                                    __printer.WriteLine();
                                }
                                else
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("WSAddressingAugust2004\\");
                                    __printer.WriteLine();
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("\"  maxReadPoolSize=\"100000000\" maxWritePoolSize=\"100000000\" >");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        <readerQuotas maxArrayLength=\"100000000\" maxDepth=\"200\" maxStringContentLength=\"1000000\" maxBytesPerRead=\"1000000\"/>");
                            __printer.WriteLine();
                            if (((SoapEncodingBindingElement)binding.Encoding).MtomEnabled == true)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      </mtomMessageEncoding>");
                                __printer.WriteLine();
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      </textMessageEncoding>");
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (binding.Transport is HttpTransportBindingElement)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("      <httpTransport maxReceivedMessageSize=\"100000000\" maxBufferSize=\"100000000\" maxBufferPoolSize=\"100000000\"/>");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (binding.Transport is HttpsTransportBindingElement)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            if (((HttpsTransportBindingElement)binding.Transport).ClientAuthentication == HttpsClientAuthentication.Certificate)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      <httpsTransport requireClientCertificate=\"true\" />");
                                __printer.WriteLine();
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      <httpsTransport requireClientCertificate=\"false\" />");
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    </binding>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </customBinding>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</bindings>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateIssuedTokenParameters(IssuedTokenVersion tokenVersion, IssuedTokenType tokenType, IssuedTokenIssuer tokenIssuer, IEnumerable<ClaimsetType> tokenClaims)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<issuedTokenParameters \\");
                    __printer.WriteLine();
                    if (tokenVersion == IssuedTokenVersion.Token10)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("tokenType=\"urn:oasis:names:tc:SAML:1.0:assertion\" \\");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenVersion == IssuedTokenVersion.Token11)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("tokenType=\"http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1\" \\");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenVersion == IssuedTokenVersion.Token20)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("tokenType=\"urn:oasis:names:tc:SAML:2.0:assertion\" \\");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenType == IssuedTokenType.Symmetric128)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("keyType=\"SymmetricKey\" keySize=\"128\">");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenType == IssuedTokenType.Symmetric192)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("keyType=\"SymmetricKey\" keySize=\"192\">");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenType == IssuedTokenType.Symmetric256)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("keyType=\"SymmetricKey\" keySize=\"256\">");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenType == IssuedTokenType.Asymmetric1024)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("keyType=\"AsymmetricKey\" keySize=\"1024\">");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenType == IssuedTokenType.Asymmetric2048)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("keyType=\"AsymmetricKey\" keySize=\"2048\">");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenType == IssuedTokenType.Asymmetric3072)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("keyType=\"AsymmetricKey\" keySize=\"3072\">");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenClaims != null)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  <claimTypeRequirements>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  ");
                        int __loop6_iteration = 0;
                        var __loop6_result =
                            (from __loop6_tmp_item___noname6 in EnumerableExtensions.Enumerate((tokenClaims).GetEnumerator())
                            from __loop6_tmp_item_claim in EnumerableExtensions.Enumerate((__loop6_tmp_item___noname6).GetEnumerator()).OfType<ClaimsetType>()
                            select
                                new
                                {
                                    __loop6_item___noname6 = __loop6_tmp_item___noname6,
                                    __loop6_item_claim = __loop6_tmp_item_claim,
                                }).ToArray();
                        foreach (var __loop6_item in __loop6_result)
                        {
                            var __noname6 = __loop6_item.__loop6_item___noname6;
                            var claim = __loop6_item.__loop6_item_claim;
                            ++__loop6_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    <add claimType=\"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/");
                            __printer.Write(claim.Name);
                            __printer.WriteTemplateOutput("\" isOptional=\"false\" />");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("  ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  </claimTypeRequirements>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenIssuer != null)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  <issuer address=\"");
                        __printer.Write(tokenIssuer.Address);
                        __printer.WriteTemplateOutput("\" />");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  ");
                        if (tokenIssuer.MetadataAddress != null && tokenIssuer.MetadataAddress.Length > 0)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("  <issuerMetadata address=\"");
                            __printer.Write(tokenIssuer.MetadataAddress);
                            __printer.WriteTemplateOutput("\" />");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("  ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</issuedTokenParameters>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateBehaviors()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<behaviors>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <serviceBehaviors>");
                    __printer.WriteLine();
                    int __loop7_iteration = 0;
                    var __loop7_result =
                        (from __loop7_tmp_item___noname7 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop7_tmp_item_endpoint in EnumerableExtensions.Enumerate((__loop7_tmp_item___noname7).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop7_item___noname7 = __loop7_tmp_item___noname7,
                                __loop7_item_endpoint = __loop7_tmp_item_endpoint,
                            }).ToArray();
                    foreach (var __loop7_item in __loop7_result)
                    {
                        var __noname7 = __loop7_item.__loop7_item___noname7;
                        var endpoint = __loop7_item.__loop7_item_endpoint;
                        ++__loop7_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <behavior name=\"");
                        __printer.Write(endpoint.Name);
                        __printer.WriteTemplateOutput("Behavior\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	  <dataContractSerializer maxItemsInObjectGraph=\"2147483647\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("      <serviceMetadata httpGetEnabled=\"true\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("      <serviceDebug includeExceptionDetailInFaults=\"false\" />");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        int __loop8_iteration = 0;
                        var __loop8_result =
                            (from __loop8_tmp_item___noname8 in EnumerableExtensions.Enumerate((endpoint).GetEnumerator())
                            from __loop8_tmp_item_binding in EnumerableExtensions.Enumerate((__loop8_tmp_item___noname8.Binding).GetEnumerator())
                            select
                                new
                                {
                                    __loop8_item___noname8 = __loop8_tmp_item___noname8,
                                    __loop8_item_binding = __loop8_tmp_item_binding,
                                }).ToArray();
                        foreach (var __loop8_item in __loop8_result)
                        {
                            var __noname8 = __loop8_item.__loop8_item___noname8;
                            var binding = __loop8_item.__loop8_item_binding;
                            ++__loop8_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                            int __loop9_iteration = 0;
                            var __loop9_result =
                                (from __loop9_tmp_item___noname9 in EnumerableExtensions.Enumerate((binding).GetEnumerator())
                                from __loop9_tmp_item_Protocols in EnumerableExtensions.Enumerate((__loop9_tmp_item___noname9.Protocols).GetEnumerator())
                                from __loop9_tmp_item_security in EnumerableExtensions.Enumerate((__loop9_tmp_item_Protocols).GetEnumerator()).OfType<SecurityProtocolBindingElement>()
                                select
                                    new
                                    {
                                        __loop9_item___noname9 = __loop9_tmp_item___noname9,
                                        __loop9_item_Protocols = __loop9_tmp_item_Protocols,
                                        __loop9_item_security = __loop9_tmp_item_security,
                                    }).ToArray();
                            foreach (var __loop9_item in __loop9_result)
                            {
                                var __noname9 = __loop9_item.__loop9_item___noname9;
                                var Protocols = __loop9_item.__loop9_item_Protocols;
                                var security = __loop9_item.__loop9_item_security;
                                ++__loop9_iteration;
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      <serviceCredentials>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        <serviceCertificate storeLocation=\"LocalMachine\" storeName=\"My\" x509FindType=\"FindBySubjectName\" findValue=\"WspService\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        <clientCertificate>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("          <authentication certificateValidationMode=\"PeerOrChainTrust\" trustedStoreLocation=\"LocalMachine\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        </clientCertificate>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      </serviceCredentials>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    </behavior>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </serviceBehaviors>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</behaviors>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateClientBehaviors()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<behaviors>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <endpointBehaviors>");
                    __printer.WriteLine();
                    int __loop10_iteration = 0;
                    var __loop10_result =
                        (from __loop10_tmp_item___noname10 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop10_tmp_item_endpoint in EnumerableExtensions.Enumerate((__loop10_tmp_item___noname10).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop10_item___noname10 = __loop10_tmp_item___noname10,
                                __loop10_item_endpoint = __loop10_tmp_item_endpoint,
                            }).ToArray();
                    foreach (var __loop10_item in __loop10_result)
                    {
                        var __noname10 = __loop10_item.__loop10_item___noname10;
                        var endpoint = __loop10_item.__loop10_item_endpoint;
                        ++__loop10_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <behavior name=\"");
                        __printer.Write(endpoint.Name);
                        __printer.WriteTemplateOutput("Behavior\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	  <dataContractSerializer maxItemsInObjectGraph=\"2147483647\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        int __loop11_iteration = 0;
                        var __loop11_result =
                            (from __loop11_tmp_item___noname11 in EnumerableExtensions.Enumerate((endpoint).GetEnumerator())
                            from __loop11_tmp_item_binding in EnumerableExtensions.Enumerate((__loop11_tmp_item___noname11.Binding).GetEnumerator())
                            select
                                new
                                {
                                    __loop11_item___noname11 = __loop11_tmp_item___noname11,
                                    __loop11_item_binding = __loop11_tmp_item_binding,
                                }).ToArray();
                        foreach (var __loop11_item in __loop11_result)
                        {
                            var __noname11 = __loop11_item.__loop11_item___noname11;
                            var binding = __loop11_item.__loop11_item_binding;
                            ++__loop11_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                            int __loop12_iteration = 0;
                            var __loop12_result =
                                (from __loop12_tmp_item___noname12 in EnumerableExtensions.Enumerate((binding).GetEnumerator())
                                from __loop12_tmp_item_Protocols in EnumerableExtensions.Enumerate((__loop12_tmp_item___noname12.Protocols).GetEnumerator())
                                from __loop12_tmp_item_security in EnumerableExtensions.Enumerate((__loop12_tmp_item_Protocols).GetEnumerator()).OfType<SecurityProtocolBindingElement>()
                                select
                                    new
                                    {
                                        __loop12_item___noname12 = __loop12_tmp_item___noname12,
                                        __loop12_item_Protocols = __loop12_tmp_item_Protocols,
                                        __loop12_item_security = __loop12_tmp_item_security,
                                    }).ToArray();
                            foreach (var __loop12_item in __loop12_result)
                            {
                                var __noname12 = __loop12_item.__loop12_item___noname12;
                                var Protocols = __loop12_item.__loop12_item_Protocols;
                                var security = __loop12_item.__loop12_item_security;
                                ++__loop12_iteration;
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      <clientCredentials>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        <clientCertificate storeLocation=\"LocalMachine\" storeName=\"My\" x509FindType=\"FindBySubjectName\" findValue=\"WspClient\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        <serviceCertificate>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("          <defaultCertificate storeLocation=\"LocalMachine\" storeName=\"My\" x509FindType=\"FindBySubjectName\" findValue=\"WspService\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("          <authentication certificateValidationMode=\"PeerOrChainTrust\" trustedStoreLocation=\"LocalMachine\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        </serviceCertificate>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      </clientCredentials>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    </behavior>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </endpointBehaviors>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</behaviors>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateServices()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<services>");
                    __printer.WriteLine();
                    int __loop13_iteration = 0;
                    var __loop13_result =
                        (from __loop13_tmp_item___noname13 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop13_tmp_item_endpoint in EnumerableExtensions.Enumerate((__loop13_tmp_item___noname13).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop13_item___noname13 = __loop13_tmp_item___noname13,
                                __loop13_item_endpoint = __loop13_tmp_item_endpoint,
                            }).ToArray();
                    foreach (var __loop13_item in __loop13_result)
                    {
                        var __noname13 = __loop13_item.__loop13_item___noname13;
                        var endpoint = __loop13_item.__loop13_item_endpoint;
                        ++__loop13_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  <service behaviorConfiguration=\"");
                        __printer.Write(endpoint.Name);
                        __printer.WriteTemplateOutput("Behavior\" name=\"");
                        __printer.Write(endpoint.Interface.Namespace.FullName);
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(endpoint.Name);
                        __printer.WriteTemplateOutput("\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <endpoint binding=\"customBinding\" bindingConfiguration=\"");
                        __printer.Write(endpoint.Binding.Name);
                        __printer.WriteTemplateOutput("\" contract=\"");
                        __printer.Write(endpoint.Interface.Namespace.FullName);
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(endpoint.Interface.Name);
                        __printer.WriteTemplateOutput("\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <endpoint address=\"mex\" binding=\"mexHttpBinding\" contract=\"IMetadataExchange\" />");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  </service>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</services>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateClientEndpoints()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<client>");
                    __printer.WriteLine();
                    int __loop14_iteration = 0;
                    var __loop14_result =
                        (from __loop14_tmp_item___noname14 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop14_tmp_item_endpoint in EnumerableExtensions.Enumerate((__loop14_tmp_item___noname14).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop14_item___noname14 = __loop14_tmp_item___noname14,
                                __loop14_item_endpoint = __loop14_tmp_item_endpoint,
                            }).ToArray();
                    foreach (var __loop14_item in __loop14_result)
                    {
                        var __noname14 = __loop14_item.__loop14_item___noname14;
                        var endpoint = __loop14_item.__loop14_item_endpoint;
                        ++__loop14_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  <endpoint name=\"");
                        __printer.Write(endpoint.Interface.Namespace.FullName);
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(endpoint.Name);
                        __printer.WriteTemplateOutput("\" contract=\"");
                        __printer.Write(endpoint.Interface.Namespace.FullName);
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(endpoint.Interface.Name);
                        __printer.WriteTemplateOutput("\" binding=\"customBinding\" bindingConfiguration=\"");
                        __printer.Write(endpoint.Binding.Name);
                        __printer.WriteTemplateOutput("\" behaviorConfiguration=\"");
                        __printer.Write(endpoint.Name);
                        __printer.WriteTemplateOutput("Behavior\" address=\"");
                        __printer.Write(endpoint.Address.Uri);
                        __printer.WriteTemplateOutput("\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        int __loop15_iteration = 0;
                        var __loop15_result =
                            (from __loop15_tmp_item___noname15 in EnumerableExtensions.Enumerate((endpoint).GetEnumerator())
                            from __loop15_tmp_item_binding in EnumerableExtensions.Enumerate((__loop15_tmp_item___noname15.Binding).GetEnumerator())
                            select
                                new
                                {
                                    __loop15_item___noname15 = __loop15_tmp_item___noname15,
                                    __loop15_item_binding = __loop15_tmp_item_binding,
                                }).ToArray();
                        foreach (var __loop15_item in __loop15_result)
                        {
                            var __noname15 = __loop15_item.__loop15_item___noname15;
                            var binding = __loop15_item.__loop15_item_binding;
                            ++__loop15_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                            int __loop16_iteration = 0;
                            var __loop16_result =
                                (from __loop16_tmp_item___noname16 in EnumerableExtensions.Enumerate((binding).GetEnumerator())
                                from __loop16_tmp_item_Protocols in EnumerableExtensions.Enumerate((__loop16_tmp_item___noname16.Protocols).GetEnumerator())
                                from __loop16_tmp_item_security in EnumerableExtensions.Enumerate((__loop16_tmp_item_Protocols).GetEnumerator()).OfType<SecurityProtocolBindingElement>()
                                select
                                    new
                                    {
                                        __loop16_item___noname16 = __loop16_tmp_item___noname16,
                                        __loop16_item_Protocols = __loop16_tmp_item_Protocols,
                                        __loop16_item_security = __loop16_tmp_item_security,
                                    }).ToArray();
                            foreach (var __loop16_item in __loop16_result)
                            {
                                var __noname16 = __loop16_item.__loop16_item___noname16;
                                var Protocols = __loop16_item.__loop16_item_Protocols;
                                var security = __loop16_item.__loop16_item_security;
                                ++__loop16_iteration;
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("    <identity>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      <dns value=\"WspService\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("    </identity>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  </endpoint>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</client>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateServicesDefaultAspx()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<%@ Page Title=\"Services Home Page\" Language=\"C#\" MasterPageFile=\"~/Site.master\" AutoEventWireup=\"true\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    CodeFile=\"~/Services/Default.aspx.cs\" Inherits=\"Services._Default\" %>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<asp:Content ID=\"HeaderContent\" runat=\"server\" ContentPlaceHolderID=\"HeadContent\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</asp:Content>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<asp:Content ID=\"BodyContent\" runat=\"server\" ContentPlaceHolderID=\"MainContent\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <h2>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        Services");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </h2>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <p>");
                    __printer.WriteLine();
                    int __loop17_iteration = 0;
                    int id = 1;
                    var __loop17_result =
                        (from __loop17_tmp_item___noname17 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop17_tmp_item_endpoint in EnumerableExtensions.Enumerate((__loop17_tmp_item___noname17).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop17_item___noname17 = __loop17_tmp_item___noname17,
                                __loop17_item_endpoint = __loop17_tmp_item_endpoint,
                            }).ToArray();
                    foreach (var __loop17_item in __loop17_result)
                    {
                        var __noname17 = __loop17_item.__loop17_item___noname17;
                        var endpoint = __loop17_item.__loop17_item_endpoint;
                        ++__loop17_iteration;
                        if (__loop17_iteration >= 2)
                        {
                            id = id + 1;
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <asp:HyperLink ID=\"HyperLink");
                        __printer.Write(id);
                        __printer.WriteTemplateOutput("\" runat=\"server\" NavigateUrl=\"~/Services/");
                        __printer.Write(endpoint.Name);
                        __printer.WriteTemplateOutput(".svc\">");
                        __printer.Write(endpoint.Name);
                        __printer.WriteTemplateOutput("</asp:HyperLink><br/>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </p>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</asp:Content>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateServicesDefaultAspxCs()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("using System;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Collections.Generic;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Linq;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Web;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Web.UI;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Web.UI.WebControls;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("namespace Services");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("{");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	public partial class _Default : System.Web.UI.Page");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	{");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		protected void Page_Load(object sender, EventArgs e)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		}");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	}");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            #endregion
        }
    }
    
