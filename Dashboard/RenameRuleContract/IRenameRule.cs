using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RenameRuleContract
{
    public interface IRenameRule : ICloneable
    {
        public string Name { get;}
        string Rename(string original);
        bool SetAttribute(string key, object value);
        object GetAttribute(string key);
    }
}