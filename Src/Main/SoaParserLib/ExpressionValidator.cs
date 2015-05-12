using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dataflow;
using SoaMetaModel.MetaInfo;
using System.Reflection;

namespace SoaMetaModel
{
    public class ExpressionValidator : SoaLanguageValidator
    {
        /// <summary>
        /// Constructs the expression validator.
        /// </summary>
        /// <param name="errorReporter">The error reporter instance.</param>
        /// <param name="context">The parser context.</param>
        public ExpressionValidator(ErrorReporter errorReporter, SoaLanguageContext context)
            : base(errorReporter, context)
        {
        }

        public override void Validate(SoaModel model)
        {
            foreach (Expression expression in model.Instances.OfType<Expression>().Where(expr => (expr.Parent == null)))
            {
                this.Validate_Expression(expression);
            }
        }

        private void Validate_Expression(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Constant) return;
            if (TypeHelpers.IsAssignableFrom(expression.ExpectedType, expression.Type))
            {
                foreach (Expression child in expression.Children)
                {
                    this.Validate_Expression(child);
                }
            }
            else
            {
                errorReporter.Error(expression.GetMetaInfo<SourceLocationInfo>(), "Type mismatch, expected \"{0}\", found \"{1}\"", (expression.ExpectedType != null) ? expression.ExpectedType.Name : "?", (expression.Type != null) ? expression.Type.Name : "?");
            }
        }
    }
}
