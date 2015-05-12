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
            #region functions from "C:\Users\sb\Documents\svn\soamm\SoaMM\VisualStudio\SoaMM\SoaGeneratorLib\VSGeneratorEx.mcg"
            public string Generated_GenerateExpression(Expression expr)
            {
                if (expr.Parent != null && expr.Parent.NodeType > expr.NodeType && expr.NodeType != ExpressionType.Variable && expr.NodeType != ExpressionType.Constant)
                {
                    return "(" + Generated_GenerateExpressionWB(expr) + ")";
                }
                else
                {
                    return Generated_GenerateExpressionWB(expr);
                }
            }
            
            public string Generated_GenerateExpressionWB(Expression expr)
            {
                if (expr.NodeType == ExpressionType.Negate)
                {
                    return "-" + Generated_GenerateExpression(((UnaryExpression)expr).Operand);
                }
                else if (expr.NodeType == ExpressionType.UnaryPlus)
                {
                    return Generated_GenerateExpression(((UnaryExpression)expr).Operand);
                }
                else if (expr.NodeType == ExpressionType.OnesComplement)
                {
                    return "~" + Generated_GenerateExpression(((UnaryExpression)expr).Operand);
                }
                else if (expr.NodeType == ExpressionType.Not)
                {
                    return "!" + Generated_GenerateExpression(((UnaryExpression)expr).Operand);
                }
                else if (expr.NodeType == ExpressionType.Convert)
                {
                    return "(" + Generated_PrintType((Type)((UnaryExpression)expr).Type) + ")" + Generated_GenerateExpression(((UnaryExpression)expr).Operand);
                }
                else if (expr.NodeType == ExpressionType.TypeAs)
                {
                    return Generated_GenerateExpression(((UnaryExpression)expr).Operand) + " as " + Generated_PrintType((Type)((UnaryExpression)expr).Type);
                }
                else if (expr.NodeType == ExpressionType.New)
                {
                    if (((NewExpression)expr).Members.Count == 0)
                    {
                        return "new " + Generated_PrintType((Type)((NewExpression)expr).Type) + "()";
                    }
                    else
                    {
                        StringBuilder s = new StringBuilder("new " + Generated_PrintType((Type)((NewExpression)expr).Type) + "() {");
                        s.Append("}");
                        return s.ToString();
                    }
                }
                else if (expr.NodeType == ExpressionType.NewArrayInit)
                {
                    StringBuilder s = new StringBuilder("new " + Generated_PrintType((Type)((NewArrayExpression)expr).Type) + "[] {");
                    string comma = "";
                    int __loop1_iteration = 0;
                    var __loop1_result =
                        (from __loop1_tmp_item___noname1 in EnumerableExtensions.Enumerate((((NewArrayExpression)expr).Expressions).GetEnumerator())
                        from __loop1_tmp_item_ex in EnumerableExtensions.Enumerate((__loop1_tmp_item___noname1).GetEnumerator()).OfType<Expression>()
                        select
                            new
                            {
                                __loop1_item___noname1 = __loop1_tmp_item___noname1,
                                __loop1_item_ex = __loop1_tmp_item_ex,
                            }).ToArray();
                    foreach (var __loop1_item in __loop1_result)
                    {
                        var __noname1 = __loop1_item.__loop1_item___noname1;
                        var ex = __loop1_item.__loop1_item_ex;
                        ++__loop1_iteration;
                        s.Append(comma + Generated_GenerateExpression(ex));
                        comma = ", ";
                    }
                    s.Append("}");
                    return s.ToString();
                }
                else if (expr.NodeType == ExpressionType.NewArrayBounds)
                {
                    return "new " + Generated_PrintType((Type)((NewArrayExpression)expr).Type) + "[" + Generated_GenerateExpression(((NewArrayExpression)expr).Expressions.ElementAt(0)) + "]";
                }
                else if (expr.NodeType == ExpressionType.LeftShift)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " << " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.RightShift)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " >> " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.Modulo)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " % " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.Add)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " + " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.Divide)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " / " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.Multiply)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " * " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.Subtract)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " - " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.And)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " & " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.AndAlso)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " && " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.Equal)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " == " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.ExclusiveOr)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " ^ " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.GreaterThan)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " > " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.GreaterThanOrEqual)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " >= " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.LessThan)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " < " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.LessThanOrEqual)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " <= " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.NotEqual)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " != " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.Or)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " | " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.OrElse)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " || " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.Coalesce)
                {
                    return Generated_GenerateExpression(((BinaryExpression)expr).Left) + " ?? " + Generated_GenerateExpression(((BinaryExpression)expr).Right);
                }
                else if (expr.NodeType == ExpressionType.TypeIs)
                {
                    return Generated_GenerateExpression(((TypeBinaryExpression)expr).Expression) + " is " + Generated_PrintType(((TypeBinaryExpression)expr).TypeOperand);
                }
                else if (expr.NodeType == ExpressionType.Conditional)
                {
                    return "(" + Generated_GenerateExpression(((ConditionalExpression)expr).Test) + ") ? " + Generated_GenerateExpression(((ConditionalExpression)expr).IfThen) + " : " + Generated_GenerateExpression(((ConditionalExpression)expr).IfElse);
                }
                else if (expr.NodeType == ExpressionType.Lambda)
                {
                    return "";
                }
                else if (expr.NodeType == ExpressionType.Constant)
                {
                    if (((ConstantExpression)expr).Type == PseudoType.Object)
                    {
                        return "null";
                    }
                    else if (((ConstantExpression)expr).Type == BuiltInType.Bool)
                    {
                        return ((ConstantExpression)expr).Value.ToString().ToLower();
                    }
                    else if (((ConstantExpression)expr).Type == BuiltInType.Long || ((ConstantExpression)expr).Type == BuiltInType.Double)
                    {
                        return ((ConstantExpression)expr).Value.ToString();
                    }
                    else if (((ConstantExpression)expr).Type == BuiltInType.String || ((ConstantExpression)expr).Type == BuiltInType.Guid)
                    {
                        return "\"" + ((ConstantExpression)expr).Value.ToString() + "\"";
                    }
                    else if (((ConstantExpression)expr).Type == BuiltInType.Date || ((ConstantExpression)expr).Type == BuiltInType.Time || ((ConstantExpression)expr).Type == BuiltInType.DateTime || ((ConstantExpression)expr).Type == BuiltInType.TimeSpan)
                    {
                        return "";
                    }
                }
                else if (expr.NodeType == ExpressionType.Default)
                {
                    return "";
                }
                else if (expr.NodeType == ExpressionType.Old)
                {
                    return "";
                }
                else if (expr.NodeType == ExpressionType.Variable)
                {
                    if (((IdentifierExpression)expr).Object is Reference)
                    {
                        return ((Reference)(((IdentifierExpression)expr).Object)).Name;
                    }
                    else
                    {
                        return ((Declaration)(((IdentifierExpression)expr).Object)).Name;
                    }
                }
                else if (expr.NodeType == ExpressionType.ArrayIndex)
                {
                    return Generated_GenerateExpression(((IndexExpression)expr).Object) + "[" + Generated_GenerateExpression(((IndexExpression)expr).Argument) + "]";
                }
                else if (expr.NodeType == ExpressionType.MemberAccess)
                {
                    if (((MemberExpression)expr).Member is Declaration)
                    {
                        return Generated_GenerateExpression(((MemberExpression)expr).Object) + "." + ((Declaration)((MemberExpression)expr).Member).Name;
                    }
                    else if (((MemberExpression)expr).Member is ClaimField)
                    {
                        return Generated_GenerateExpression(((MemberExpression)expr).Object) + "." + ((ClaimField)((MemberExpression)expr).Member).Name;
                    }
                    else if (((MemberExpression)expr).Member is EnumValue)
                    {
                        return Generated_GenerateExpression(((MemberExpression)expr).Object) + "." + ((EnumValue)((MemberExpression)expr).Member).Name.ToUpper();
                    }
                }
                else if (expr.NodeType == ExpressionType.Call)
                {
                    StringBuilder s = new StringBuilder(Generated_GenerateExpression(((MethodCallExpression)expr).Object) + "." + ((MethodCallExpression)expr).Operation.Name + "(");
                    string comma = "";
                    int __loop2_iteration = 0;
                    var __loop2_result =
                        (from __loop2_tmp_item___noname2 in EnumerableExtensions.Enumerate((((MethodCallExpression)expr).Arguments).GetEnumerator())
                        from __loop2_tmp_item_ex in EnumerableExtensions.Enumerate((__loop2_tmp_item___noname2).GetEnumerator()).OfType<Expression>()
                        select
                            new
                            {
                                __loop2_item___noname2 = __loop2_tmp_item___noname2,
                                __loop2_item_ex = __loop2_tmp_item_ex,
                            }).ToArray();
                    foreach (var __loop2_item in __loop2_result)
                    {
                        var __noname2 = __loop2_item.__loop2_item___noname2;
                        var ex = __loop2_item.__loop2_item_ex;
                        ++__loop2_iteration;
                        s.Append(comma + Generated_GenerateExpression(ex));
                        comma = ", ";
                    }
                    s.Append(")");
                    return s.ToString();
                }
                return "";
            }
            
            #endregion
        }
    }
    
