using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    using MetaInfo;
    using System.Reflection;

    public enum ExpressionType
    {
        Add,
        //AddChecked,
        And,
        AndAlso,
        ArrayLength,
        ArrayIndex,
        Call,
        Coalesce,
        Conditional,
        Constant,
        Convert,
        //ConvertChecked,
        Divide,
        Equal,
        ExclusiveOr,
        GreaterThan,
        GreaterThanOrEqual,
        //Invoke,
        Lambda,
        LeftShift,
        LessThan,
        LessThanOrEqual,
        //ListInit,
        MemberAccess,
        MemberInit,
        Modulo,
        Multiply,
        //MultiplyChecked,
        Negate,
        UnaryPlus,
        //NegateChecked,
        New,
        NewArrayInit,
        NewArrayBounds,
        Not,
        NotEqual,
        Or,
        OrElse,
        Parameter,
        //Power,
        //Quote,
        RightShift,
        Subtract,
        //SubtractChecked,
        TypeAs,
        TypeIs,
        Variable,
        //Assign,
        //Block,
        //DebugInfo,
        //Decrement,
        //Dynamic,
        Default,
        //Extension,
        //Goto,
        //Increment,
        //Index,
        //Label,
        //RuntimeVariables,
        //Loop,
        //Switch,
        //Throw,
        //Try,
        //Unbox,
        //AddAssign,
        //AndAssign,
        //DivideAssign,
        //ExclusiveOrAssign,
        //LeftShiftAssign,
        //ModuloAssign,
        //MultiplyAssign,
        //OrAssign,
        //PowerAssign,
        //RightShiftAssign,
        //SubtractAssign,
        //AddAssignChecked,
        //MultiplyAssignChecked,
        //SubtractAssignChecked,
        //PreIncrementAssign,
        //PreDecrementAssign,
        //PostIncrementAssign,
        //PostDecrementAssign,
        //TypeEqual,
        OnesComplement,
        //IsTrue,
        //IsFalse,
        Old
    }

    public static class ExpressionTypeExtensions
    {
        public static int Precedence(this ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.Add: return 4;
                case ExpressionType.And: return 8;
                case ExpressionType.AndAlso: return 11;
                case ExpressionType.ArrayIndex: return 0;
                case ExpressionType.Call: return 0;
                case ExpressionType.Coalesce: return 13;
                case ExpressionType.Conditional: return 14;
                case ExpressionType.Constant: return 0;
                case ExpressionType.Convert: return 2;
                case ExpressionType.Divide: return 3;
                case ExpressionType.Equal: return 7;
                case ExpressionType.ExclusiveOr: return 9;
                case ExpressionType.GreaterThan: return 6;
                case ExpressionType.GreaterThanOrEqual: return 6;
                //case ExpressionType.Invoke: return 0;
                case ExpressionType.Lambda: return 15;
                case ExpressionType.LeftShift: return 5;
                case ExpressionType.LessThan: return 6;
                case ExpressionType.LessThanOrEqual: return 6;
                case ExpressionType.MemberAccess: return 0;
                case ExpressionType.MemberInit: return -1;
                case ExpressionType.Modulo: return 3;
                case ExpressionType.Multiply: return 3;
                case ExpressionType.Negate: return 2;
                case ExpressionType.UnaryPlus: return 2;
                case ExpressionType.New: return 0;
                case ExpressionType.NewArrayInit: return 1;
                case ExpressionType.NewArrayBounds: return 1;
                case ExpressionType.Not: return 2;
                case ExpressionType.NotEqual: return 7;
                case ExpressionType.Or: return 10;
                case ExpressionType.OrElse: return 12;
                case ExpressionType.Parameter: return -1;
                case ExpressionType.RightShift: return 5;
                case ExpressionType.Subtract: return 4;
                case ExpressionType.TypeAs: return 6;
                case ExpressionType.TypeIs: return 6;
                case ExpressionType.Variable: return 0;
                case ExpressionType.Default: return 0;
                case ExpressionType.OnesComplement: return 2;
                case ExpressionType.Old: return 0;
                default: return -1;
            }
        }
    }

    public abstract class Expression : SoaObject
    {
        public Expression(ExpressionType nodeType)
        {
            this.NodeType = nodeType;
            this.Children = new ModelList<Expression>(this, ChildrenProperty);
        }

        public ExpressionType NodeType
        {
            get;
            private set;
        }

        public Declaration Type
        {
            get { return (Declaration)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        public static readonly ModelProperty TypeProperty = ModelProperty.Register("Type", typeof(Declaration), typeof(Expression));

        public Declaration ExpectedType
        {
            get { return (Declaration)GetValue(ExpectedTypeProperty); }
            set { SetValue(ExpectedTypeProperty, value); }
        }
        public static readonly ModelProperty ExpectedTypeProperty = ModelProperty.Register("ExpectedType", typeof(Declaration), typeof(Expression));

        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly ModelProperty ValueProperty = ModelProperty.Register("Value", typeof(object), typeof(Expression));

        [Opposite(typeof(Expression), "Parent")]
        public ModelList<Expression> Children
        {
            get { return (ModelList<Expression>)GetValue(ChildrenProperty); }
            private set { SetValue(ChildrenProperty, value); }
        }
        public static readonly ModelProperty ChildrenProperty = ModelProperty.Register("Children", typeof(ModelList<Expression>), typeof(Expression));

        [Opposite(typeof(Expression), "Children")]
        public Expression Parent
        {
            get { return (Expression)GetValue(ParentProperty); }
            set { SetValue(ParentProperty, value); }
        }
        public static readonly ModelProperty ParentProperty = ModelProperty.Register("Parent", typeof(Expression), typeof(Expression));
    }

    public class UnaryExpression : Expression
    {
        public UnaryExpression(ExpressionType nodeType, Expression operand, Type type = null)
            : base(nodeType)
        {
            this.Operand = operand;

            switch (nodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.UnaryPlus:
                case ExpressionType.OnesComplement:
                    ModelClass.LazyInit(this, TypeProperty, () => this.Operand.Type);
                    ModelClass.LazyInit(this.Operand, ExpectedTypeProperty, () => this.ExpectedType);
                    break;
                case ExpressionType.Not:
                    ModelClass.LazyInit(this, TypeProperty, () => BuiltInType.Bool);
                    ModelClass.LazyInit(this.Operand, ExpectedTypeProperty, () => BuiltInType.Bool);
                    break;
                case ExpressionType.Convert:
                case ExpressionType.TypeAs:
                    ModelClass.LazyInit(this, TypeProperty, () => type);
                    ModelClass.LazyInit(this.Operand, ExpectedTypeProperty, () => PseudoType.Object);
                    break;
                default:
                    throw new SoaModelException("Invalid unary expression type: " + nodeType.ToString());
            }
            ModelClass.LazyInit(this, ValueProperty, () => { throw new EvaluationException(this); });

            this.Children.Add(this.Operand);
        }

        public Expression Operand
        {
            get { return (Expression)GetValue(OperandProperty); }
            set { SetValue(OperandProperty, value); }
        }
        public static readonly ModelProperty OperandProperty = ModelProperty.Register("Operand", typeof(Expression), typeof(UnaryExpression));
    }

    public class NewExpression : Expression
    {
        public NewExpression(IEnumerable<MemberInitExpression> members, Type type = null)
            : base(ExpressionType.New)
        {
            ModelClass.LazyInit(this, TypeProperty, () => type);

            this.Members = new ModelList<MemberInitExpression>(this, MembersProperty);
            foreach (MemberInitExpression member in members)
            {
                this.Members.Add(member);
            }
            ModelClass.LazyInit(this, ValueProperty, () => this.CalculateValue());

            this.Children.AddRange(this.Members);
        }

        [Opposite(typeof(MemberInitExpression), "NewExpression")]
        public ModelList<MemberInitExpression> Members
        {
            get { return (ModelList<MemberInitExpression>)GetValue(MembersProperty); }
            set { SetValue(MembersProperty, value); }
        }
        public static readonly ModelProperty MembersProperty = ModelProperty.Register("Members", typeof(ModelList<MemberInitExpression>), typeof(NewExpression));

        private object CalculateValue()
        {
            if (Type is StructType)
            {
                System.Type type = ((StructType)Type).UnderlyingType;
                try
                {
                    object value = type.GetConstructor(System.Type.EmptyTypes).Invoke(null);
                    foreach (MemberInitExpression member in Members)
                    {
                        foreach (PropertyInfo property in type.GetProperties())
                        {
                            if (AttributeHelpers.GetMemberName(property) == member.Property.Name)
                            {
                                property.SetValue(value, member.Value, null);
                            }
                        }
                    }
                    return value;
                }
                catch (Exception exception)
                {
                    throw new EvaluationException(this, exception);
                }
            }
            throw new EvaluationException(this);
        }
    }

    public class MemberInitExpression : Expression
    {
        public MemberInitExpression(string memberName, Expression value)
            : base(ExpressionType.MemberInit)
        {
            this.ValueExpression = value;

            ModelClass.LazyInit(this, PropertyProperty, () => NameContext.ResolveName(this.NewExpression.Type, memberName, typeof(StructField), typeof(ExceptionField)));
            ModelClass.LazyInit(this, ExpectedTypeProperty, () => this.Property.Type);
            ModelClass.LazyInit(this, TypeProperty, () => this.ValueExpression.Type);
            ModelClass.LazyInit(this, ValueProperty, () => this.ValueExpression.Value);
            ModelClass.LazyInit(this.ValueExpression, ExpectedTypeProperty, () => this.ExpectedType);

            this.Children.Add(this.ValueExpression);
        }

        public Expression ValueExpression
        {
            get;
            private set;
        }

        [Opposite(typeof(NewExpression), "Members")]
        public NewExpression NewExpression
        {
            get { return (NewExpression)GetValue(NewExpressionProperty); }
            set { SetValue(NewExpressionProperty, value); }
        }
        public static readonly ModelProperty NewExpressionProperty = ModelProperty.Register("NewExpression", typeof(NewExpression), typeof(MemberInitExpression));

        public Property Property
        {
            get { return (Property)GetValue(PropertyProperty); }
            set { SetValue(PropertyProperty, value); }
        }
        public static readonly ModelProperty PropertyProperty = ModelProperty.Register("Property", typeof(Property), typeof(MemberInitExpression));
    }

    public class NewArrayExpression : Expression
    {
        public NewArrayExpression(ExpressionType nodeType, IEnumerable<Expression> expressions, Type itemType = null)
            : base(nodeType)
        {
            this.Expressions = new List<Expression>(expressions);

            ModelClass.LazyInit(this, ItemTypeProperty, () =>  itemType);
            switch (nodeType)
            {
                case ExpressionType.NewArrayInit:
                    foreach (var expression in this.Expressions)
                    {
                        ModelClass.LazyInit(expression, ExpectedTypeProperty, () => this.ItemType);
                    }
                    break;
                case ExpressionType.NewArrayBounds:
                    foreach (var expression in this.Expressions)
                    {
                        ModelClass.LazyInit(expression, ExpectedTypeProperty, () => BuiltInType.Int);
                    }
                    break;
                default:
                    throw new SoaModelException("Invalid new array expression type: " + nodeType.ToString());
            }

            ModelClass.LazyInit(this, TypeProperty, () => ArrayType.CreateFrom(this.ItemType));
            ModelClass.LazyInit(this, ValueProperty, () => { throw new EvaluationException(this); });

            this.Children.AddRange(this.Expressions);
        }

        public List<Expression> Expressions
        {
            get;
            private set;
        }

        public Type ItemType
        {
            get { return (Type)GetValue(ItemTypeProperty); }
            set { SetValue(ItemTypeProperty, value); }
        }
        public static readonly ModelProperty ItemTypeProperty = ModelProperty.Register("ItemType", typeof(Type), typeof(NewArrayExpression));
    }

    public class BinaryExpression : Expression
    {
        public BinaryExpression(ExpressionType nodeType, Expression left, Expression right)
            : base(nodeType)
        {
            this.Left = left;
            this.Right = right;

            switch (nodeType)
            {
                case ExpressionType.LeftShift:
                case ExpressionType.RightShift:
                case ExpressionType.Modulo:
                    ModelClass.LazyInit(this, TypeProperty, () => TypeHelpers.GetCommonType(this.Left.Type, this.Right.Type));
                    ModelClass.LazyInit(this.Left, ExpectedTypeProperty, () => BuiltInType.Long);
                    ModelClass.LazyInit(this.Right, ExpectedTypeProperty, () => BuiltInType.Long);
                    break;
                case ExpressionType.Add:
                case ExpressionType.Divide:
                case ExpressionType.Multiply:
                case ExpressionType.Subtract:
                    ModelClass.LazyInit(this, TypeProperty, () => TypeHelpers.GetCommonType(this.Left.Type, this.Right.Type));
                    ModelClass.LazyInit(this.Left, ExpectedTypeProperty, () => this.ExpectedType);
                    ModelClass.LazyInit(this.Right, ExpectedTypeProperty, () => this.ExpectedType);
                    break;
                case ExpressionType.Equal:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.NotEqual:
                    ModelClass.LazyInit(this, TypeProperty, () => BuiltInType.Bool);
                    ModelClass.LazyInit(this.Left, ExpectedTypeProperty, () => this.Right.Type);
                    ModelClass.LazyInit(this.Right, ExpectedTypeProperty, () => this.Left.Type);
                    break;
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.ExclusiveOr:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    ModelClass.LazyInit(this, TypeProperty, () => BuiltInType.Bool);
                    ModelClass.LazyInit(this.Left, ExpectedTypeProperty, () => BuiltInType.Bool);
                    ModelClass.LazyInit(this.Right, ExpectedTypeProperty, () => BuiltInType.Bool);
                    break;
                case ExpressionType.Coalesce:
                    ModelClass.LazyInit(this, TypeProperty, () => TypeHelpers.GetCommonType(((NullableType)this.Left.Type).InnerType, this.Right.Type));
                    ModelClass.LazyInit(this.Left, ExpectedTypeProperty, () => NullableType.CreateFrom((Type)this.ExpectedType));
                    ModelClass.LazyInit(this.Right, ExpectedTypeProperty, () => this.ExpectedType);
                    break;
                default:
                    throw new SoaModelException("Invalid binary expression type: " + nodeType.ToString());
            }
            ModelClass.LazyInit(this, ValueProperty, () => { throw new EvaluationException(this); });

            this.Children.Add(this.Left);
            this.Children.Add(this.Right);
        }

        public Expression Left
        {
            get;
            private set;
        }

        public Expression Right
        {
            get;
            private set;
        }
    }

    public class TypeBinaryExpression : Expression
    {
        public TypeBinaryExpression(Expression expression, Type typeOperand)
            : base(ExpressionType.TypeIs)
        {
            this.Expression = expression;
            this.TypeOperand = typeOperand;

            ModelClass.LazyInit(this, TypeProperty, () => BuiltInType.Bool);
            ModelClass.LazyInit(this.Expression, ExpectedTypeProperty, () => PseudoType.Object);
            ModelClass.LazyInit(this, ValueProperty, () => { throw new EvaluationException(this); });

            this.Children.Add(this.Expression);
        }

        public Expression Expression
        {
            get;
            private set;
        }

        public Type TypeOperand
        {
            get { return (Type)GetValue(TypeOperandProperty); }
            set { SetValue(TypeOperandProperty, value); }
        }
        public static readonly ModelProperty TypeOperandProperty = ModelProperty.Register("TypeOperand", typeof(Type), typeof(TypeBinaryExpression));
    }

    public class ConditionalExpression : Expression
    {
        public ConditionalExpression(Expression test, Expression ifThen, Expression ifElse)
            : base(ExpressionType.Conditional)
        {
            this.Test = test;
            this.IfThen = ifThen;
            this.IfElse = ifElse;

            ModelClass.LazyInit(this.Test, ExpectedTypeProperty, () => BuiltInType.Bool);
            ModelClass.LazyInit(this.IfThen, ExpectedTypeProperty, () => this.ExpectedType);
            ModelClass.LazyInit(this.IfElse, ExpectedTypeProperty, () => this.ExpectedType);
            ModelClass.LazyInit(this, TypeProperty, () => TypeHelpers.GetCommonType(this.IfThen.Type, this.IfElse.Type));
            ModelClass.LazyInit(this, ValueProperty, () => { throw new EvaluationException(this); });

            this.Children.Add(this.Test);
            this.Children.Add(this.IfThen);
            this.Children.Add(this.IfElse);
        }

        public Expression Test
        {
            get;
            private set;
        }

        public Expression IfThen
        {
            get;
            private set;
        }

        public Expression IfElse
        {
            get;
            private set;
        }
    }

    public class LambdaExpression : Expression
    {
        public LambdaExpression()
            : base(ExpressionType.Lambda)
        {
            this.Parameters = new ModelList<LambdaParameter>(this, ParametersProperty);

            ModelClass.LazyInit(this, TypeProperty, () => DelegateType.CreateFrom((Type)this.Body.Type, this.Parameters.Select(param => (Type)param.Type).ToArray()));
            ModelClass.LazyInit(this, ValueProperty, () => { throw new EvaluationException(this); });

        }

        // TODO: temporary fix
        public void BindLazyProperties()
        {
            ModelClass.LazyInit(this.Body, ExpectedTypeProperty, () => ((DelegateType)this.ExpectedType).ReturnType);
            for (int i = 0; i < this.Parameters.Count; i++)
            {
                ModelClass.LazyInit(this.Parameters[i], ExpectedTypeProperty, () => ((DelegateType)this.ExpectedType).ParameterTypes[i]);
            }
            this.Children.AddRange(this.Parameters);
            this.Children.Add(this.Body);
        }

        public Expression Body
        {
            get { return (Expression)GetValue(BodyProperty); }
            set { SetValue(BodyProperty, value); }
        }
        public static readonly ModelProperty BodyProperty = ModelProperty.Register("Body", typeof(Expression), typeof(LambdaExpression));

        [Opposite(typeof(LambdaParameter), "LambdaExpression")]
        public ModelList<LambdaParameter> Parameters
        {
            get { return (ModelList<LambdaParameter>)GetValue(ParametersProperty); }
            private set { SetValue(ParametersProperty, value); }
        }
        public static readonly ModelProperty ParametersProperty = ModelProperty.Register("Parameters", typeof(ModelList<LambdaParameter>), typeof(LambdaExpression));
    }

    public class LambdaParameter : Expression
    {
        public LambdaParameter(string name, Type type = null)
            : base(ExpressionType.Parameter)
        {
            this.Name = name;
            if (type == null)
            {
                ModelClass.LazyInit(this, TypeProperty, () => this.ExpectedType);
            }
            else
            {
                ModelClass.LazyInit(this, TypeProperty, () => type);
            }
            ModelClass.LazyInit(this, ValueProperty, () => { throw new EvaluationException(this); });
        }

        public string Name
        {
            get;
            set;
        }

        [Opposite(typeof(LambdaExpression), "Parameters")]
        public LambdaExpression LambdaExpression
        {
            get { return (LambdaExpression)GetValue(LambdaExpressionProperty); }
            set { SetValue(LambdaExpressionProperty, value); }
        }
        public static readonly ModelProperty LambdaExpressionProperty = ModelProperty.Register("LambdaExpression", typeof(LambdaExpression), typeof(LambdaParameter));
    }

    public class ConstantExpression : Expression
    {
        public ConstantExpression(Type type, object value)
            : base(ExpressionType.Constant)
        {
            ModelClass.LazyInit(this, TypeProperty, () => type);
            ModelClass.LazyInit(this, ValueProperty, () => value);
        }
    }

    public class DefaultExpression : Expression
    {
        public DefaultExpression(Type type = null)
            : base(ExpressionType.Default)
        {
            ModelClass.LazyInit(this, TypeProperty, () => type);
            ModelClass.LazyInit(this, ValueProperty, () => this.CalculateValue());
        }

        private object CalculateValue()
        {
            if (this.Type is BuiltInType)
            {
                switch (((BuiltInType)this.Type).Kind)
                {
                    case BuiltInTypeKind.Bool: return default(bool);
                    case BuiltInTypeKind.Byte: return default(byte);
                    case BuiltInTypeKind.Int: return default(int);
                    case BuiltInTypeKind.Long: return default(long);
                    case BuiltInTypeKind.Float: return default(float);
                    case BuiltInTypeKind.Double: return default(double);
                    case BuiltInTypeKind.String: return default(string);
                    case BuiltInTypeKind.Date: return default(string);
                    case BuiltInTypeKind.Time: return default(string);
                    case BuiltInTypeKind.TimeSpan: return default(System.TimeSpan);
                    case BuiltInTypeKind.DateTime: return default(System.DateTime);
                    default: throw new EvaluationException(this);
                }
            }
            throw new EvaluationException(this);
        }
    }

    public class NameExpression : Expression
    {
        public NameExpression(ExpressionType nodeType, SoaObject @object)
            : base(nodeType)
        {
            this.Object = @object;
            ModelClass.LazyInit(this, TypeProperty, () => this.CalculateType());
            ModelClass.LazyInit(this, ValueProperty, () => { throw new EvaluationException(this); });
        }

        public SoaObject Object
        {
            get { return (SoaObject)GetValue(ObjectProperty); }
            set { SetValue(ObjectProperty, value); }
        }
        public static readonly ModelProperty ObjectProperty = ModelProperty.Register("Object", typeof(SoaObject), typeof(OldExpression));

        protected Declaration CalculateType()
        {
            SoaObject obj = this.Object;
            if (obj is Reference)
            {
                obj = ((Reference)obj).Object;
            }
            if (obj is Declaration)
            {
                return (Declaration)obj;
            }
            if (obj is Property)
            {
                return ((Property)obj).Type;
            }
            throw new TypeInvalidException(this);
        }
    }

    public class OldExpression : NameExpression
    {
        public OldExpression(SoaObject @object)
            : base(ExpressionType.Old, @object)
        {
        }
    }

    public class IdentifierExpression : NameExpression
    {
        public IdentifierExpression(SoaObject @object)
            : base(ExpressionType.Variable, @object)
        {
        }
    }

    public class IndexExpression : Expression
    {
        public IndexExpression(Expression obj, Expression argument)
            : base(ExpressionType.ArrayIndex)
        {
            this.Object = obj;
            this.Argument = argument;

            ModelClass.LazyInit(this, TypeProperty, () => ((ArrayType)this.Object.Type).ItemType);
            ModelClass.LazyInit(this.Argument, ExpectedTypeProperty, () => BuiltInType.Int);
            ModelClass.LazyInit(this, ValueProperty, () => { throw new EvaluationException(this); });

            this.Children.Add(this.Object);
            this.Children.Add(this.Argument);
        }

        public Expression Object
        {
            get;
            private set;
        }

        public Expression Argument
        {
            get;
            private set;
        }
    }
    
    //public class InvocationExpression : Expression
    //{
    //    public InvocationExpression(Expression expression, IEnumerable<Expression> arguments)
    //        : base(ExpressionType.Invoke)
    //    {
    //        this.Expression = expression;
    //        this.Arguments = new List<Expression>(arguments);

    //        ModelClass.LazyInit(this.Expression, ExpectedTypeProperty, () => PseudoType.Delegate);
    //        ModelClass.LazyInit(this, TypeProperty, () => ((DelegateType)this.Expression.Type).ReturnType);
    //        for(int i = 0; i < this.Arguments.Count; i++)
    //        {
    //            ModelClass.LazyInit(this.Arguments[i], ExpectedTypeProperty, () => ((DelegateType)this.Expression.Type).ParameterTypes[i]);
    //        }
    //        ModelClass.LazyInit(this, ValueProperty, () => { throw new EvaluationException(this); });
    //    }

    //    public Expression Expression
    //    {
    //        get;
    //        private set;
    //    }

    //    public List<Expression> Arguments
    //    {
    //        get;
    //        private set;
    //    }
    //}

    public class MemberExpression : Expression
    {
        public MemberExpression(Expression @object, string memberName)
            : base(ExpressionType.MemberAccess)
        {
            this.Object = @object;

            ModelClass.LazyInit(this, MemberProperty, () => this.CalculateMember(memberName));
            ModelClass.LazyInit(this, TypeProperty, () => this.CalculateType());
            ModelClass.LazyInit(this, ValueProperty, () => this.CalculateValue());
            ModelClass.LazyInit(this.Object, ExpectedTypeProperty, () => PseudoType.Object);

            this.Children.Add(this.Object);
        }

        public Expression Object
        {
            get { return (Expression)GetValue(ObjectProperty); }
            set { SetValue(ObjectProperty, value); }
        }
        public static readonly ModelProperty ObjectProperty = ModelProperty.Register("Object", typeof(Expression), typeof(MemberExpression));

        public SoaObject Member
        {
            get { return (SoaObject)GetValue(MemberProperty); }
            set { SetValue(MemberProperty, value); }
        }
        public static readonly ModelProperty MemberProperty = ModelProperty.Register("Member", typeof(SoaObject), typeof(MemberExpression));

        private SoaObject CalculateMember(string memberName)
        {
            try
            {
                return NameContext.ResolveName(this.Object.Type, memberName, typeof(StructField), typeof(EnumType), typeof(EnumValue), typeof(Namespace), typeof(ClaimField));            
            }
            catch (NameException exception)
            {
                throw new NameValidationException(this, exception);
            }
        }

        private Declaration CalculateType()
        {
            if (Member is Field)
            {
                return ((Field)Member).Type;
            }
            else if (Member is Declaration)
            {
                return (Declaration)Member;
            }
            else if(Member is EnumValue)
            {
                return ((EnumValue)Member).Enum;
            }
            throw new TypeInvalidException(this);
        }

        private object CalculateValue()
        {
            if (Member is EnumValue)
            {
                System.Type type = ((EnumValue)Member).Enum.UnderlyingType;
                if (type != null)
                {
                    foreach (FieldInfo field in type.GetFields(BindingFlags.Static | BindingFlags.Public))
                    {
                        if (AttributeHelpers.GetMemberName(field) == ((EnumValue)Member).Name)
                        {
                            return Enum.Parse(type, field.Name);
                        }
                    }
                }
            }
            throw new EvaluationException(this);
        }
    }

    public class MethodCallExpression : Expression
    {
        public MethodCallExpression(Expression obj, string operationName, IEnumerable<Expression> arguments)
            : base(ExpressionType.Call)
        {
            this.Object = obj;
            this.Arguments = new List<Expression>(arguments);

            ModelClass.LazyInit(this, OperationProperty, () => this.CalculateOperation(operationName));
            ModelClass.LazyInit(this, TypeProperty, () => this.Operation.ReturnType);

            for(int i = 0; i < this.Arguments.Count; i++)
            {
                ModelClass.LazyInit(this.Arguments[i], ExpectedTypeProperty, () => this.Operation.Parameters[i].Type);
            }
            ModelClass.LazyInit(this, ValueProperty, () => { throw new EvaluationException(this); });

            this.Children.Add(this.Object);
            this.Children.AddRange(this.Arguments);
        }

        public Expression Object
        {
            get;
            private set;
        }

        public List<Expression> Arguments
        {
            get;
            private set;
        }

        public Operation Operation
        {
            get { return (Operation)GetValue(OperationProperty); }
            set { SetValue(OperationProperty, value); }
        }
        public static readonly ModelProperty OperationProperty = ModelProperty.Register("Operation", typeof(Operation), typeof(MethodCallExpression));

        private Operation CalculateOperation(string operationName)
        {
            try
            {
                return (Operation)NameContext.ResolveOperation(this.Object.Type, operationName, new List<Type>(this.Arguments.Select(expr => (Type)expr.Type)));
            }
            catch (NameException exception)
            {
                throw new NameValidationException(this, exception);
            }
        }
    }
}
