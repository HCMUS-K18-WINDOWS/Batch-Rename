using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchRename
{
    public class ReplaceRule : IRenameRule
    {
        public List<string> Needles { get; set; }
        public string Replacer { get; set; }

        public string Name => "Replace";

        public ReplaceRule()
        {
            Needles = new List<string>();
            Replacer = "";
        }

        public ReplaceRule(List<string> needle, string replacer)
        {
            Needles = needle;
            Replacer = replacer;
        }

        public bool SetAttribute(string key, object value)
        {
            if (value == null) return false;
            string strValue = (string) value;
            switch (key)
            {
                case "Needles":
                    {
                        var needles = strValue.Split(new char[] { ',' });
                        for (int i = 0; i < needles.Length; i++)
                        {
                            Needles.Add(needles[i].Trim());
                        }
                        break;
                    }
                case "Replacer":
                    {
                        Replacer = strValue;
                        break;
                    }
                default:
                    return false;
            }
            return true;
        }

        public object? GetAttribute(string key)
        {
            switch (key)
            {
                case "Needles":
                    return String.Join(", ", Needles);
                case "Replacer":
                    return Replacer;
                default:
                    return null;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public string[] GetAllAttributesName()
        {
            return new string[] {"Needles", "Replacer"};
        }

        public void Rename(FileInfo original)
        {
            string result = original.NewName;
            foreach (var needle in Needles)
            {
                result = result.Replace(needle, Replacer);
            }
            original.NewName = result;
        }
    }
}