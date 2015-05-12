using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dataflow;
using SoaMetaModel.MetaInfo;

namespace SoaMetaModel
{
    /// <summary>
    /// The base class of validators.
    /// </summary>
    public abstract class SoaLanguageValidator
    {
        /// <summary>
        /// The error reporter instance.
        /// </summary>
        protected ErrorReporter errorReporter;

        /// <summary>
        /// The parser context instance.
        /// </summary>
        protected SoaLanguageContext context;

        /// <summary>
        /// Constructs the validator.
        /// </summary>
        /// <param name="errorReporter">The error reporter instance.</param>
        /// <param name="context">The parser context.</param>
        public SoaLanguageValidator(ErrorReporter errorReporter, SoaLanguageContext context)
            : base()
        {
            this.errorReporter = errorReporter;
            this.context = context;
        }

        /// <summary>
        /// The main validator method.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        public abstract void Validate(SoaModel model);

        protected void Error_NameNotFound(SoaObject site, NameNotFoundException exception)
        {
            // Build type string
            StringBuilder sb = new StringBuilder();
            sb.Append(AttributeHelpers.GetTypeName(exception.Types[0]));
            for (int i = 1; i < exception.Types.Length - 1; i++) sb.Append(", " + AttributeHelpers.GetTypeName(exception.Types[i]).ToLower());
            if (exception.Types.Length > 1) sb.Append(" or " + AttributeHelpers.GetTypeName(exception.Types[exception.Types.Length - 1]).ToLower());

            errorReporter.Error(site.GetMetaInfo<SourceLocationInfo>(), "{0} \"{1}\" is not declared.", sb.ToString(), exception.Name);
        }

        protected void Error_NameCollision(Declaration site, Declaration with)
        {
            errorReporter.Error(site.GetMetaInfo<SourceLocationInfo>(), "Name of {0} \"{2}\" is already declared to a \"{1}\".", AttributeHelpers.GetTypeName(site.GetType()).ToLower(), AttributeHelpers.GetTypeName(with.GetType()).ToLower(), site.Name);
        }

        protected void Warning_NameLowerCase(SoaObject site, string name)
        {
            errorReporter.Warning(site.GetMetaInfo<SourceLocationInfo>(), "Name of {0} \"{1}\" starts with a lowercase letter.", AttributeHelpers.GetTypeName(site.GetType()).ToLower(), name);
        }

        protected void Error_NotEvaluable(EvaluationException exception)
        {
            errorReporter.Error(exception.Site.GetMetaInfo<SourceLocationInfo>(), "Expression cannot be evaluated compile-time.");
        }

        protected void Error_InvalidBindingPropertyName(BindingElementProperty property)
        {
            errorReporter.Error(property.GetMetaInfo<SourceLocationInfo>(), "Invalid binding property \"{0}\".", property.Name);
        }

        protected void Error_InvalidBindingPropertyValue(BindingElementProperty property)
        {
            errorReporter.Error(property.GetMetaInfo<SourceLocationInfo>(), "Incompatible value for property \"{0}\".", property.Name);
        }

        protected void Error_MultipleProtocol(SoaObject site, string protocol)
        {
            errorReporter.Error(site.GetMetaInfo<SourceLocationInfo>(), "A {0} protocol is already included.", protocol);
        }

        protected void Error_MissingProtocol(SoaObject site, string protocol)
        {
            errorReporter.Error(site.GetMetaInfo<SourceLocationInfo>(), "A {0} protocol is missing.", protocol);
        }
    }

}
