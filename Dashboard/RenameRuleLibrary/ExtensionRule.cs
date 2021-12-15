using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameRuleLibrary
{
    public class ExtensionRule : IRenameRule
    {
        public string Name => "Extension";
        public string OldExtension { get; set; }
        public string NewExtension { get; set; }

        public ExtensionRule()
        {
            NewExtension = "";
        }
        public ExtensionRule(string oldExtension, string newExtension)
        {
            OldExtension = oldExtension;
            NewExtension = newExtension;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public RuleRequirement[] GetAllAttributesRequirement()
        {
            var requirement1 = new RuleRequirement("Old extension", RequirementType.String);
            var requirement2 = new RuleRequirement("New extension", RequirementType.String);
            return new RuleRequirement[] { requirement1, requirement2 };
        }

        public object? GetAttribute(string key)
        {
            switch (key)
            {
                case "New extension":
                    return NewExtension;
                case "Old extension":
                    return OldExtension;
                default:
                    return null;
            }
        }

        public void Rename(FileInfo original)
        {
            if (original.OldExtension == OldExtension || original.OldExtension == "*")
            {
                original.NewExtension = NewExtension;
            }
        }

        public bool SetAttribute(string key, object value)
        {
            string strValue = (string)value;
            switch (key)
            {
                case "New extension":
                    NewExtension = strValue;
                    break;
                case "Old extension":
                    OldExtension = strValue;
                    break;
                default:
                    return false;
            }
            return true;
        }
    }
}
