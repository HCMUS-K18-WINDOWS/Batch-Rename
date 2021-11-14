using System.Text.Json.Nodes;

namespace BatchRename
{
    internal class RegexRuleParser : IRenameRuleParser
    {
        public IRenameRule Parse(JsonNode obj)
        {
            string req = obj["req"].ToString();
            string value = obj["value"].ToString();
            return new RegexRule(req, value);
        }
    }
}