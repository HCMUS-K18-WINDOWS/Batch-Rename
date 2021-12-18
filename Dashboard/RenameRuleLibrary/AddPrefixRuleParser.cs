using Newtonsoft.Json;
using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;

namespace BatchRename
{
    public class AddPrefixRuleParser : IRenameRuleParser
    {
        public string getName()
        {
            return "AddPrefix";
        }
        public IRenameRule Parse(JsonNode obj)
        {
            string value = obj["value"].ToString();
            return new AddPrefixRule(value);
        }

        public object ParseRuleToFileObject(IRenameRule rule)
        {
            var addPrefix = rule as AddPrefixRule;
            var obj = new
            {
                type = addPrefix?.Name,
                value = addPrefix?.Prefix
            };
            return obj;
        }
    }
}