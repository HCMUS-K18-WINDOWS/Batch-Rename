using System.Text.Json.Nodes;

namespace BatchRename
{
    internal class OrderRuleParser : IRenameRuleParser
    {
        public IRenameRule Parse(JsonNode obj)
        {
            string type = obj["type"].ToString();
            return new OrderRule(type);
        }
    }
}