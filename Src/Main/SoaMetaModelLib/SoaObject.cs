using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    using MetaInfo;

    public class SoaObject : ModelClass
    {
        public SoaObject()
        {
            this.Model = ModelContext<SoaModel>.Current.Model;
            this.Attributes = new ModelList<SoaAttribute>(this, AttributesProperty);
        }

        public int ObjectId
        {
            get
            {
                return this.Model.Instances.IndexOf(this);
            }
        }

        [Opposite(typeof(SoaAttribute), "Object")]
        public ModelList<SoaAttribute> Attributes
        {
            get { return (ModelList<SoaAttribute>)GetValue(AttributesProperty); }
            private set { SetValue(AttributesProperty, value); }
        }
        public static readonly ModelProperty AttributesProperty = ModelProperty.Register("Attributes", typeof(ModelList<SoaAttribute>), typeof(SoaObject));

        public TAttribute FindAttribute<TAttribute>() where TAttribute : SoaAttribute
        {
            foreach (SoaAttribute metaAttribute in this.Attributes)
            {
                TAttribute attribute = metaAttribute as TAttribute;
                if (attribute != null) return attribute;
            }
            return null;
        }


        [Opposite(typeof(SoaModel), "Instances")]
        public SoaModel Model
        {
            get { return (SoaModel)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }
        public static readonly ModelProperty ModelProperty =
            ModelProperty.Register("Model", typeof(SoaModel), typeof(SoaObject));


        private List<IMetaInfo> metaInfos = new List<IMetaInfo>();

        public void AddMetaInfo(IMetaInfo info)
        {
            metaInfos.Add(info);
        }

        public bool HasMetaInfo<TInfo>() where TInfo : IMetaInfo
        {
            return GetMetaInfos<TInfo>().Length > 0;
        }

        public TInfo GetMetaInfo<TInfo>(int index = 0) where TInfo : IMetaInfo
        {
            return GetMetaInfos<TInfo>()[index];
        }

        public TInfo[] GetMetaInfos<TInfo>() where TInfo : IMetaInfo
        {
            return metaInfos.OfType<TInfo>().ToArray();
        }

    }

    public class SoaAttribute : SoaObject
    {
        public string Name
        {
            get;
            set;
        }

        public List<Property> Properties
        {
            get;
            set;
        }


        [Opposite(typeof(SoaObject), "Attributes")]
        public SoaObject Object
        {
            get { return (SoaObject)GetValue(ObjectProperty); }
            set { SetValue(ObjectProperty, value); }
        }
        public static readonly ModelProperty ObjectProperty = ModelProperty.Register("Object", typeof(SoaObject), typeof(SoaAttribute));

    }
}
