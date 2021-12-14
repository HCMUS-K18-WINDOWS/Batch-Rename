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

        public string[] GetAllAttributesName()
        {
            return new string[] {"NewExtension"};
        }

        public object? GetAttribute(string key)
        {
            switch (key)
            {
                case "NewExtension":
                    return NewExtension;
                default:
                    return null;
            }
        }

        public void Rename(FileInfo original)
        {
            original.NewExtension = NewExtension;
        }

        public bool SetAttribute(string key, object value)
        {
            string strValue = (string)value;
            switch (key)
            {
                case "NewExtension":
                    NewExtension = strValue;
                    break;
                default:
                    return false;
            }
            return true;
        }
    }
}
