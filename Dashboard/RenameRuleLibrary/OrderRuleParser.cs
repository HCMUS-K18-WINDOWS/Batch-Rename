using RenameRuleContract;
using System.Text.Json.Nodes;

namespace BatchRename
{
    public class OrderRuleParser : IRenameRuleParser
    {
        public string getName()
        {
            return "Order";
        }

        public IRenameRule Parse(JsonNode obj)
        {
            string type = obj["type"].ToString();
            return new OrderRule(type);
        }
    }
}