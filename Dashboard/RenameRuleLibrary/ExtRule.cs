using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class ExtRule : IRenameRule
    {
        public string Extension { get; set; }
        public string NewValue { get; set; }

        public string Name => throw new NotImplementedException();

        public ExtRule()
        {

        }
        public ExtRule(string extension, string newValue)
        {
            Extension = extension;
            NewValue = newValue;
        }
        public string Rename(string original)
        {
            throw new NotImplementedException();
        }

        public bool SetAttribute(string key, object value)
        {
            throw new NotImplementedException();
        }

        public object GetAttribute(string key)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
