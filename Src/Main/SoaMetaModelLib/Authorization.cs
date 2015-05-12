using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    using MetaInfo;

    public class Authorization : Declaration
    {
        public Authorization()
        {
            this.OperationAuthorizations = new ModelList<OperationAuthorization>(this, OperationAuthorizationsProperty);
        }

        public Interface Interface
        {
            get { return (Interface)GetValue(InterfaceProperty); }
            set { SetValue(InterfaceProperty, value); }
        }
        public static readonly ModelProperty InterfaceProperty = ModelProperty.Register("Interface", typeof(Interface), typeof(Authorization));

        [Opposite(typeof(OperationAuthorization), "Authorization")]
        public ModelList<OperationAuthorization> OperationAuthorizations
        {
            get { return (ModelList<OperationAuthorization>)GetValue(OperationAuthorizationsProperty); }
            private set { SetValue(OperationAuthorizationsProperty, value); }
        }
        public static readonly ModelProperty OperationAuthorizationsProperty = ModelProperty.Register("OperationAuthorizations", typeof(ModelList<OperationAuthorization>), typeof(Authorization));
    }

    [ModelLiteral("Operation")]
    public class OperationAuthorization : OperationImplementation
    {
        public OperationAuthorization()
            : base()
        {
            this.OperationAuthorizationStatements = new ModelList<OperationAuthorizationStatement>(this, OperationAuthorizationStatementsProperty);
        }

        [Opposite(typeof(Authorization), "OperationAuthorizations")]
        public Authorization Authorization
        {
            get { return (Authorization)GetValue(AuthorizationProperty); }
            set { SetValue(AuthorizationProperty, value); }
        }
        public static readonly ModelProperty AuthorizationProperty = ModelProperty.Register("Authorization", typeof(Authorization), typeof(OperationAuthorization));

        [Opposite(typeof(OperationAuthorizationStatement), "OperationAuthorization")]
        public ModelList<OperationAuthorizationStatement> OperationAuthorizationStatements
        {
            get { return (ModelList<OperationAuthorizationStatement>)GetValue(OperationAuthorizationStatementsProperty); }
            private set { SetValue(OperationAuthorizationStatementsProperty, value); }
        }
        public static readonly ModelProperty OperationAuthorizationStatementsProperty = ModelProperty.Register("OperationAuthorizationStatements", typeof(ModelList<OperationAuthorizationStatement>), typeof(OperationAuthorization));
    }

    public class OperationAuthorizationStatement : RuleStatement
    {
        [Opposite(typeof(OperationAuthorization), "OperationAuthorizationStatements")]
        public OperationAuthorization OperationAuthorization
        {
            get { return (OperationAuthorization)GetValue(OperationAuthorizationProperty); }
            set { SetValue(OperationAuthorizationProperty, value); }
        }
        public static readonly ModelProperty OperationAuthorizationProperty = ModelProperty.Register("OperationAuthorization", typeof(OperationAuthorization), typeof(OperationAuthorizationStatement));
    }

    public class Demand : OperationAuthorizationStatement
    {
    }
}
