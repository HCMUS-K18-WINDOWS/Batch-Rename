using Newtonsoft.Json;
using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;

namespace BatchRename
{
    public class ReplaceRuleParser : IRenameRuleParser
    {
        public string getName()
        {
            return "Replace";
        }

        public IRenameRule Parse(JsonNode obj)
        {
            List<string> needles = new List<string>();
            needles = JsonConvert.DeserializeObject<List<string>>(obj["req"].ToJsonString());
            string value = obj["value"].ToString();
            return new ReplaceRule(needles, value);
        }

        public object ParseRuleToFileObject(IRenameRule rule)
        {
            var replaceRule = rule as ReplaceRule;
            var obj = new
            {
                type = replaceRule?.Name,
                req = replaceRule?.Needles,
                value = replaceRule?.Replacer,
            };
            return obj;
        }
    }
}