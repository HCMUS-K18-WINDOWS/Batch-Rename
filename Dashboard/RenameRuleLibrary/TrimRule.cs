using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class TrimRule : IRenameRule
    {
        public string Name => "Trim";

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public RuleRequirement[] GetAllAttributesRequirement()
        {
            return Array.Empty<RuleRequirement>();
        }

        public object? GetAttribute(string key)
        {
            return null;
        }

        public void Rename(FileInfo original)
        {
            string oldName = original.NewName;
            original.NewName = oldName.Trim();
        }

        public bool SetAttribute(string key, object value)
        {
            return false;
        }
    }
}
