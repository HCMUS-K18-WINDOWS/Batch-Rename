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
        public List<char> Replacements { get; set; }
        public char Value { get; set; }

        public string Name => throw new NotImplementedException();

        public CharReplaceRule()
        {

        }

        public CharReplaceRule(List<char> req, char value)
        {
            Replacements = req;
            Value = Value;
        }
        public string Rename(string original)
        {
            throw new NotImplementedException();
        }

        public bool SetAttribute(string key, object value)
        {
            throw new NotImplementedException();
        }

        public object? GetAttribute(string key)
        {
            throw new NotImplementedException();
        }

        public object Clone()
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
