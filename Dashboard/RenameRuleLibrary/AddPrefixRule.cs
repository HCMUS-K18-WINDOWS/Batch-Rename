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

        public string Name => throw new NotImplementedException();

        public AddPrefixRule()
        {

        }

        public AddPrefixRule(string prefix)
        {
            Prefix = prefix;
        }
        public string Rename(string original)
        {
            string result = "";
            return $"{Prefix}{result}";
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
            throw new NotImplementedException();
        }

        public string[] GetAllAttributesName()
        {
            throw new NotImplementedException();
        }

        public void Rename(FileInfo original)
        {
            throw new NotImplementedException();
        }
    }
}