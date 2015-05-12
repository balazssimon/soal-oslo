using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoaMetaModel
{
    public class RuleStatement : SoaObject
    {
        public string Text
        {
            get;
            set;
        }

        public Expression Rule
        {
            get;
            set;
        }
    }
}
