using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dataflow;
using SoaMetaModel.MetaInfo;
using System.Reflection;
using Sb.Meta;

namespace SoaMetaModel
{
    public class BindingValidator : SoaLanguageValidator
    {
        /// <summary>
        /// Constructs the binding validator.
        /// </summary>
        /// <param name="errorReporter">The error reporter instance.</param>
        /// <param name="context">The parser context.</param>
        public BindingValidator(ErrorReporter errorReporter, SoaLanguageContext context)
            : base(errorReporter, context)
        {
        }

        public override void Validate(SoaModel model)
        {
            foreach (Binding binding in model.Instances.OfType<Binding>())
            {
                Validate_BindingElementProperties(binding.Transport);
                Validate_BindingElementProperties(binding.Encoding);

                SecurityProtocolBindingElement security = null;
                BootstrapProtocolBindingElement bootstrap = null;
                foreach (ProtocolBindingElement element in binding.Protocols)
                {
                    if (element is SecurityProtocolBindingElement)
                    {
                        if (security == null)
                        {
                            security = (SecurityProtocolBindingElement)element;
                        }
                        else
                        {
                            Error_MultipleProtocol(element, "security");
                        }
                    }
                    if (element is BootstrapProtocolBindingElement)
                    {
                        if (bootstrap == null)
                        {
                            bootstrap = (BootstrapProtocolBindingElement)element;
                        }
                        else
                        {
                            Error_MultipleProtocol(element, "bootstrap");
                        }
                    }
                    Validate_BindingElementProperties(element);
                }
                if ((security != null) && (security is SecureConversationSecurityProtocolBindingElement))
                {
                    if (bootstrap == null)
                    {
                        Error_MissingProtocol(security, "bootstrap");
                    }
                    else
                    {
                        ((SecureConversationSecurityProtocolBindingElement)security).Bootstrap = bootstrap;
                    }
                }
                if ((bootstrap != null) && ((security == null) || !(security is SecureConversationSecurityProtocolBindingElement)))
                {
                    Error_MissingProtocol(bootstrap, "secure conversation");
                }

                // Set up claims if necessary
                if (security != null && security is SamlSecurityProtocolBindingElement)
                {
                    Validate_Claims(binding, ((SamlSecurityProtocolBindingElement)security).Claims, model);
                }
                if (bootstrap != null && bootstrap is SamlBootstrapProtocolBindingElement)
                {
                    Validate_Claims(binding, ((SamlBootstrapProtocolBindingElement)bootstrap).Claims, model);
                }
            }
        }

        private void Validate_BindingElementProperties(BindingElement element)
        {
            System.Type type = element.GetType();
            foreach (BindingElementProperty property in element.Properties)
            {
                try
                {
                    // Get actual property
                    PropertyInfo p = type.GetProperties().Single(info => AttributeHelpers.GetMemberName(info) == property.Name);
                    System.Type pt = p.PropertyType;

                    // Set up expected type
                    property.Value.ExpectedType = property.Model.Instances.OfType<Type>().Single(t => t.UnderlyingType == pt);

                    // Compute and assign value
                    object v = property.Value.Value;
                    System.Type vt = v.GetType();
                    if (pt.IsAssignableFrom(vt))
                    {
                        p.SetValue(element, v, null);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (InvalidOperationException)
                {
                    Error_InvalidBindingPropertyName(property);
                }
                catch (NameValidationException exception)
                {
                    Error_NameNotFound(exception.Site, (NameNotFoundException)exception.InnerException);
                }
                catch (EvaluationException exception)
                {
                    Error_NotEvaluable(exception);
                }
                catch (Exception)
                {
                    Error_InvalidBindingPropertyValue(property);
                }
            }
        }

        private void Validate_Claims(Binding binding, ModelList<ClaimsetType> claims, SoaModel model)
        {
            foreach (Endpoint endpoint in model.Instances.OfType<Endpoint>().Where(ep => ep.Binding == binding))
            {
                Authorization authorization = endpoint.Authorization;
                if (authorization != null)
                {
                    foreach (OperationAuthorization operation in authorization.OperationAuthorizations)
                    {
                        foreach (Reference reference in operation.References.Where(rf => rf.Object is ClaimsetType))
                        {
                            ClaimsetType claimset = (ClaimsetType)reference.Object;
                            if (!claims.Contains(claimset))
                            {
                                claims.Add(claimset);
                            }
                        }
                        foreach (Reference reference in operation.References.Where(rf => rf.Object is ClaimField))
                        {
                            ClaimsetType claimset = ((ClaimField)reference.Object).Claimset;
                            if (!claims.Contains(claimset))
                            {
                                claims.Add(claimset);
                            }
                        }
                    }
                }
            }
        }
    }
}
