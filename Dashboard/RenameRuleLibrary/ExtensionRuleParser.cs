using BatchRename;
using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace RenameRuleLibrary
{
    public class ExtensionRuleParser : IRenameRuleParser
    {
        public string getName()
        {
            return "Extension";
        }

        public IRenameRule Parse(JsonNode obj)
        {
            string req = obj["req"].ToString();
            string value = obj["value"].ToString();
            return new ExtensionRule(req, value);
        }

        public object ParseRuleToFileObject(IRenameRule rule)
        {
            throw new NotImplementedException();
        }
    }
}
