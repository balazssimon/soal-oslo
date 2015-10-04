using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sb.Meta
{
    public class ModelContext<TModel>
    {
        internal static Stack<ModelContext<TModel>> contexts;

        static ModelContext()
        {
            ModelContext<TModel>.contexts = new Stack<ModelContext<TModel>>();
        }

        internal ModelContext(TModel model)
        {
            this.Model = model;
        }

        public TModel Model { get; private set; }

        public static ModelContext<TModel> Current
        {
            get
            {
                if (ModelContext<TModel>.contexts.Count > 0) return ModelContext<TModel>.contexts.Peek();
                else throw new ModelException("No model context is defined. Surround your code with: using(new ModelContextScope(model)) { ... }");
            }
        }

    }


    public class ModelContextScope<TModel> : IDisposable
    {
        public ModelContextScope(TModel model)
        {
            ModelContext<TModel>.contexts.Push(new ModelContext<TModel>(model));
        }

        public void Dispose()
        {
            ModelContext<TModel>.contexts.Pop();
        }
    }
}
