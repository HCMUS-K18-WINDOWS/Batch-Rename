using System.Text.Json.Nodes;

namespace BatchRename
{
    internal class TrimRuleParser : IRenameRuleParser
    {
        public IRenameRule Parse(JsonNode obj)
        {
            return new TrimRule();
        }
    }
}