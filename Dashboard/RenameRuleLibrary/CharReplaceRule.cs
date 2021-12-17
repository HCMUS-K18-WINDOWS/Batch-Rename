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

        public char CharBefore { get; set; }
        public char CharAfter { get; set; }

        public string Name => "CharReplace";

        public CharReplaceRule()
        {
            //Value = '';
        }

        public CharReplaceRule(char value1, char value2)
        {
            CharBefore = value1;
            CharAfter = value2;
        }
        public void Rename(FileInfo original)
        {
            throw new NotImplementedException();
        }

        public bool SetAttribute(string key, object value)
        {
            var strValue = (string)value;
            if (strValue.Length == 0) return false;
            switch (key)
            {
                case "CharBefore":
                    CharBefore = strValue.ElementAt(0);
                    return true;
                case "CharAfter":
                    CharAfter = strValue.ElementAt(0);
                    return true;
                default:
                    return false;
            }
        }

        public object? GetAttribute(string key)
        {
            switch (key)
            {
                case "CharBefore":
                    return CharBefore;
                case "CharAfter":
                    return CharAfter;
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
            return new RuleRequirement[] { new RuleRequirement("CharBefore", RequirementType.String), 
                new RuleRequirement("CharAfter", RequirementType.String) };
        }


    }
}
