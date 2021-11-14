using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;

namespace BatchRename
{
    public class AddPrefixRuleParser : IRenameRuleParser
    {
        public IRenameRule Parse(JsonNode obj)
        {
            string value = obj["value"].ToString();
            return new AddPrefixRule(value);
        }
    }
}