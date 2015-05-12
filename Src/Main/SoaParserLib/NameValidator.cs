using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dataflow;

namespace SoaMetaModel
{
    public class NameValidator : SoaLanguageValidator
    {
        /// <summary>
        /// Constructs the name validator.
        /// </summary>
        /// <param name="errorReporter">The error reporter instance.</param>
        /// <param name="context">The parser context.</param>
        public NameValidator(ErrorReporter errorReporter, SoaLanguageContext context)
            : base(errorReporter, context)
        {
        }

        /// <summary>
        /// The main validator method.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        public override void Validate(SoaModel model)
        {
            this.Validate_Collision(model);
            this.Validate_Casing(model);
        }

        /// <summary>
        /// Validate declaration name collision.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        private void Validate_Collision(SoaModel model)
        {
            // Types involved in possible name collision
            HashSet<System.Type> collidable = new HashSet<System.Type> { typeof(StructType), typeof(EnumType), typeof(ExceptionType), typeof(ClaimsetType), typeof(Interface), typeof(Authorization), typeof(Contract), typeof(Endpoint) };

            // The scope of name collision is a namespace
            foreach (Namespace ns in model.Instances.OfType<Namespace>())
            {
                Dictionary<string, Declaration> declarations = new Dictionary<string, Declaration>();
                foreach (Declaration declaration in ns.Declarations.Where(decl => collidable.Contains(decl.GetType())))
                {
                    if (declarations.ContainsKey(declaration.Name))
                    {
                        Error_NameCollision(declaration, declarations[declaration.Name]);
                    }
                    else
                    {
                        declarations.Add(declaration.Name, declaration);
                    }
                }
            }
        }

        /// <summary>
        /// Validate declaration and field name casing.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        private void Validate_Casing(SoaModel model)
        {
            foreach (Declaration declaration in model.Instances.Where(obj => !obj.HasMetaInfo<SoaMetaModel.MetaInfo.HiddenInfo>()).OfType<Declaration>())
            {
                if (!(declaration is ArrayType || declaration is NullableType))
                {
                    if (Char.IsLower(declaration.Name[0]))
                    {
                        Console.WriteLine(declaration.Name);
                        Warning_NameLowerCase(declaration, declaration.Name);
                    }
                }
            }
            foreach (Field field in model.Instances.OfType<Field>())
            {
                if (Char.IsLower(field.Name[0]))
                {
                    Warning_NameLowerCase(field, field.Name);
                }
            }
        }

    }
}
