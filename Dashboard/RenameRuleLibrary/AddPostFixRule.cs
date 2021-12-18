using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class AddPostfixRule : IRenameRule
    {
        public string Postfix { get; set; }

        public string Name => "AddPostfix";

        public AddPostfixRule()
        {
            Postfix = "";
        }
        public AddPostfixRule(string postfix)
        {
            Postfix = postfix;
        }
        

        public bool SetAttribute(string key, object value)
        {
            switch (key)
            {
                case "Postfix":
                    Postfix = (string)value;
                    return true;
                default:
                    return false;
            }
        }

        public object? GetAttribute(string key)
        {
            switch (key)
            {
                case "Postfix":
                    return Postfix;
                default:
                    return null;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public RuleRequirement[] GetAllAttributesRequirement()
        {
            return new RuleRequirement[] { new RuleRequirement("Postfix", RequirementType.String) };
        }

        public void Rename(FileInfo original)
        {
            string result = $"{original.OldName}{Postfix}";
            original.NewName = result;
        }
    }
}
