using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class OrderRule : IRenameRule
    {
        public string Type { get; set; }

        public string Name => throw new NotImplementedException();

        public OrderRule()
        {

        }
        public OrderRule(string type)
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

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public string[] GetAllAttributesName()
        {
            throw new NotImplementedException();
        }
    }
}
