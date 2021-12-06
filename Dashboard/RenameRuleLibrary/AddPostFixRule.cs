using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class AddPostFixRule : IRenameRule
    {
        public string Value { get; set; }
        public AddPostFixRule(string value)
        {
            Value = value;
        }
        public string Rename(string original)
        {
            throw new NotImplementedException();
        }
    }
}
