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

        public RegexRule(string regex, string value)
        {
            Regex = regex;
            Value = value;
        }
        public string Rename(string original)
        {
            throw new NotImplementedException();
        }
    }
}
