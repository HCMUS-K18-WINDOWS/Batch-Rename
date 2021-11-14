using System.Text.Json.Nodes;

namespace BatchRename
{
    internal class ChangeCaseRuleParser : IRenameRuleParser
    {
        public IRenameRule Parse(JsonNode obj)
        {
            string req = obj["req"].ToString();
            return new ChangeCaseRule(req);
        }
    }
}