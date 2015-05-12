using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    using MetaInfo;

    public class SoaModel : ModelClass
    {
        public SoaModel()
        {
            this.Instances = new ModelList<SoaObject>(this, InstancesProperty);
        }

        [Opposite(typeof(SoaObject), "Model")]
        public ModelList<SoaObject> Instances
        {
            get { return (ModelList<SoaObject>)GetValue(InstancesProperty); }
            private set { SetValue(InstancesProperty, value); }
        }
        public static readonly ModelProperty InstancesProperty = ModelProperty.Register("Instances", typeof(ModelList<SoaObject>), typeof(SoaModel));

        private Namespace globalNamespace = null;

        public Namespace GlobalNamespace
        {
            get {
                if (globalNamespace == null)
                {
                    this.globalNamespace = new Namespace();
                    this.globalNamespace.Name = null;
                    this.globalNamespace.Prefix = null;
                    this.globalNamespace.Namespace = null;
                    this.globalNamespace.AddMetaInfo(new HiddenInfo());
                }
                return globalNamespace;
            }
        }
    }

    public class SoaModelException : Exception
    {
        public SoaModelException()
            : base()
        {
        }

        public SoaModelException(string message)
            : base(message)
        {
        }

        public SoaModelException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
