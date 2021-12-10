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

        public string Name => throw new NotImplementedException();

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
