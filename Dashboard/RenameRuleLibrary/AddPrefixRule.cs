using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchRename
{
    public class AddPrefixRule : IRenameRule
    {
        public string Prefix { get; set; }
        public string Name { get => "AddPrefix"; set{ } }

        public AddPrefixRule()
        {
            Prefix = "";
        }

        public AddPrefixRule(string prefix)
        {
            Prefix = prefix;
        }

        public void Rename(FileInfo original)
        {
            string result = $"{Prefix}{original.OldName}";
            original.NewName = result;
        }

        public bool SetAttribute(string key, object value)
        {
            switch(key)
            {
                case "Prefix":
                    Prefix = (string)value;
                    return true;
                default:
                    return false;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public object? GetAttribute(string key)
        {
            switch (key)
            {
                case "Prefix":
                    return Prefix;
                default:
                    return null;
            }
        }

        public RuleRequirement[] GetAllAttributesRequirement()
        {
            return new RuleRequirement[] { new RuleRequirement("Prefix", RequirementType.String) };
        }
    }
}