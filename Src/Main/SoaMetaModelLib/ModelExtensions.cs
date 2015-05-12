using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoaMetaModel
{
    public class ModelExtensions
    {
        public static void CreateOperationInputOutputTypes(Namespace ns)
        {
            foreach (Interface intf in ns.Declarations.OfType<Interface>())
            {
                foreach(Operation operation in intf.Operations)
                {
                    StructType inputType =
                        new StructType()
                        {
                            Name = operation.Name,
                            Namespace = intf.Namespace
                        };
                    foreach (OperationParameter param in operation.Parameters)
                    {
                        inputType.Fields.Add(
                            new StructField()
                            {
                                Name = param.Name,
                                Type = param.Type
                            });
                    }
                    StructType outputType =
                        new StructType()
                        {
                            Name = operation.Name + "Response",
                            Namespace = intf.Namespace
                        };
                    outputType.Fields.Add(
                        new StructField()
                        {
                            Name = operation.Name + "Result",
                            Type = operation.ReturnType
                        });

                    operation.InputType = inputType;
                    operation.OutputType = outputType;
                }
            }
        }
    }
}
