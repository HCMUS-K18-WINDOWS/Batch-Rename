using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class CharReplaceRule : IRenameRule
    {
        public List<char> Replacements { get; set; }
        public char Value { get; set; }

        public CharReplaceRule(List<char> req, char value)
        {
            Replacements = req;
            Value = Value;
        }
        public string Rename(string original)
        {
            throw new NotImplementedException();
        }
    }
}
