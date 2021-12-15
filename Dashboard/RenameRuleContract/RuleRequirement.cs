using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameRuleContract
{
    public enum RequirementType
    {
        String,
        Number,
        Boolean
    }

    public class RuleRequirement
    {
        public string Name { get; set; }
        public RequirementType Type { get; set; }
        public object[]? PossibleValues { get; set; }
        public RuleRequirement()
        {
            Name = "";
            Type = RequirementType.String;
        }
        public RuleRequirement(string name, RequirementType type)
        {
            Name = name;
            Type = type;
        }
    }
}
