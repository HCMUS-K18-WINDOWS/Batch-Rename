using RenameRuleContract;
using System.Text.Json.Nodes;

namespace BatchRename
{
    public class RegexRuleParser : IRenameRuleParser
    {
        public string getName()
        {
            return "Regex";
        }

        public IRenameRule Parse(JsonNode obj)
        {
            string req = obj["req"].ToString();
            string value = obj["value"].ToString();
            return new RegexRule(req, value);
        }

        public object ParseRuleToFileObject(IRenameRule rule)
        {
            throw new System.NotImplementedException();
        }
    }
}