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
    }
}