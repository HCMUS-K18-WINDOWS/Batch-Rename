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

        public string Name => "ChangeCase";

        public ChangeCaseRule()
        {

        }
        public ChangeCaseRule(string type)
        {
            Type = type;
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

        public object? Clone()
        {
            return this.MemberwiseClone();
        }

        public RuleRequirement[] GetAllAttributesRequirement()
        {
            var possibles = new string[] {"Upper", "Lower", "Capital", "camelCase"};
            var requirement = new RuleRequirement("Type", RequirementType.String)
            {
                PossibleValues = possibles
            };
            return new RuleRequirement[] { requirement };
        }

        public void Rename(FileInfo original)
        {
            throw new NotImplementedException();
        }
    }
}
