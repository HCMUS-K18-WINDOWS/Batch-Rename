using System.Text.Json.Nodes;

namespace BatchRename
{
    internal class ExtRuleParser : IRenameRuleParser
    {
        public IRenameRule Parse(JsonNode obj)
        {
            string ext = obj["req"].ToString();
            string newValue = obj["value"].ToString();
            return new ExtRule(ext, newValue);
        }
    }
}