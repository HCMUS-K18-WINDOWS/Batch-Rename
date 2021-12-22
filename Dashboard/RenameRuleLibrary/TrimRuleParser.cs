using RenameRuleContract;
using System.Text.Json.Nodes;

namespace BatchRename
{
    public class TrimRuleParser : IRenameRuleParser
    {
        public string getName()
        {
            return "Trim";
        }

        public IRenameRule Parse(JsonNode obj)
        {
            return new TrimRule();
        }

        public object ParseRuleToFileObject(IRenameRule rule)
        {
            var trimRule = rule as TrimRule;
            var obj = new
            {
                type = trimRule?.Name,
            };
            return obj;
        }
    }
}