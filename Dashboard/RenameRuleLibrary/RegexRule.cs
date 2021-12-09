using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class RegexRule : IRenameRule
    {
        public string Regex { get; set; }
        public string Value { get; set; }

        public string Name => throw new NotImplementedException();

        public RegexRule()
        {

        }
        public RegexRule(string regex, string value)
        {
            Regex = regex;
            Value = value;
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
