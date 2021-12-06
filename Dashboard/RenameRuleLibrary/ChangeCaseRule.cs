using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class ChangeCaseRule : IRenameRule
    {
        public string Type { get; set; }
        public ChangeCaseRule(string type)
        {
            Type = type;
        }
        public string Rename(string original)
        {
            throw new NotImplementedException();
        }
    }
}
