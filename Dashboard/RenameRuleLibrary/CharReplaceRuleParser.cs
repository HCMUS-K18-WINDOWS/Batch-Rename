using Newtonsoft.Json;
using RenameRuleContract;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace BatchRename
{
    public class CharReplaceRuleParser : IRenameRuleParser
    {
        public string getName()
        {
            return "CharReplace";
        }

        public IRenameRule Parse(JsonNode obj)
        {
            //List<char> replacements = JsonConvert.DeserializeObject<List<char>>(obj["req"].ToJsonString());
            char CharBefore = obj["CharBefore"].GetValue<char>();
            char CharAfter = obj["CharAfter"].GetValue<char>();
            return new CharReplaceRule(CharBefore, CharAfter);
        }
    }
}