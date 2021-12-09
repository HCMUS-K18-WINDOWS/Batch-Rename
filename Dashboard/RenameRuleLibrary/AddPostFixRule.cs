using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class AddPostFixRule : IRenameRule
    {
        public string Postfix { get; set; }

        public string Name => "AddPostFix";

        public AddPostFixRule()
        {

        }
        public AddPostFixRule(string postfix)
        {
            Postfix = postfix;
        }
        

        public bool SetAttribute(string key, object value)
        {
            switch (key)
            {
                case "Prefix":
                    Postfix = (string)value;
                    return true;
                default:
                    return false;
            }
        }

        public object GetAttribute(string key)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public string[] GetAllAttributesName()
        {
            return new string[] { "Postfix" };
        }

        public void Rename(FileInfo original)
        {
            throw new NotImplementedException();
        }
    }
}
