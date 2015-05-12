using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel.MetaInfo
{
    /// <summary>
    /// Helper class for dealing with attributes and reflection.
    /// </summary>
    public class AttributeHelpers
    {
        /// <summary>
        /// Get model name of the given type.
        /// </summary>
        /// <param name="type">The class or enum type.</param>
        /// <returns>The model name of the given type, specified by the attribute, or the type name itself if attribute is not present.</returns>
        public static string GetTypeName(System.Type type)
        {
            ModelLiteralAttribute[] literals = (ModelLiteralAttribute[])type.GetCustomAttributes(typeof(ModelLiteralAttribute), false);
            if (literals.Length > 0)
            {
                return literals[0].Name;
            }
            else
            {
                return type.Name;
            }
        }

        /// <summary>
        /// Get model name of the given field or property.
        /// </summary>
        /// <param name="member">The enum field or class property.</param>
        /// <returns>The model name of the given member, specified by the attribute, or the mamber name itself if attribute is not present.</returns>
        public static string GetMemberName(System.Reflection.MemberInfo member)
        {
            ModelLiteralAttribute[] literals = (ModelLiteralAttribute[])member.GetCustomAttributes(typeof(ModelLiteralAttribute), false);
            if (literals.Length > 0)
            {
                return literals[0].Name;
            }
            else
            {
                return member.Name;
            }
        }
    }
}
