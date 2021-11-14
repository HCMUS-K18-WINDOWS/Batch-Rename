using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace BatchRename
{
    internal class CharReplaceRuleParser : IRenameRuleParser
    {
        public IRenameRule Parse(JsonNode obj)
        {
            List<char> replacements = JsonConvert.DeserializeObject<List<char>>(obj["req"].ToJsonString());
            char value = obj["value"].GetValue<char>();
            return new CharReplaceRule(replacements, value);
        }
    }
}