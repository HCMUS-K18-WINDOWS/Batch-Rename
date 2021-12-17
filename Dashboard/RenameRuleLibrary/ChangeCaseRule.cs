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
            Type = "";
        }
        public ChangeCaseRule(string type)
        {
            Type = type;
        }

        public void Rename(FileInfo original)
        {
            switch (this.Type)
            {
                case "Upper":
                    original.NewName = original.OldName.ToUpper();
                    break;
                case "Lower":
                    original.NewName = original.OldName.ToLower();
                    break;
                case "Capital":
                    var OldNameLower = original.OldName.ToLower();
                    var FirstLetter = char.ToUpper(original.OldName[0]);
                    original.NewName = FirstLetter + OldNameLower.Remove(0, 1);
                    break;
                case "camelCase":
                    original.NewName = ToCamelCase(original.OldName);
                    break;
                default:
                    break;
            }
        }

        public bool SetAttribute(string key, object value)
        {
            switch (key)
            {
                case "Type":
                    Type = (string)value;
                    return true;
                default:
                    return false;
            }
        }

        public object? GetAttribute(string key)
        {
            switch (key)
            {
                case "Type":
                    return Type;
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
            var possibles = new string[] {"Upper", "Lower", "Capital", "camelCase"};
            var requirement = new RuleRequirement("Type", RequirementType.String)
            {
                PossibleValues = possibles
            };
            return new RuleRequirement[] { requirement };
        }

        public static string ToCamelCase(string name)
        {
            if (string.IsNullOrEmpty(name) || !name.Contains(" "))
            {
                return name;
            }
            string[] array = name.Split(' ');
            for (int i = 0; i < array.Length; i++)
            {
                string s = array[i];
                string first = string.Empty;
                string rest = string.Empty;
                if (s.Length > 0)
                {
                    first = Char.ToUpperInvariant(s[0]).ToString();
                }
                if (s.Length > 1)
                {
                    rest = s.Substring(1).ToLowerInvariant();
                }
                array[i] = first + rest;
            }
            string newname = string.Join(" ", array);
            if (newname.Length > 0)
            {
                newname = Char.ToLowerInvariant(newname[0]) + newname.Substring(1);
            }
            else
            {
                newname = name;
            }
            return newname;
        }
    }
}
