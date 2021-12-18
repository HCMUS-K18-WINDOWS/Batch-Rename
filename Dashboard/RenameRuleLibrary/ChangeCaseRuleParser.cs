using RenameRuleContract;
using System.Text.Json.Nodes;

namespace BatchRename
{
    public class ChangeCaseRuleParser : IRenameRuleParser
    {
        public string getName()
        {
            return "ChangeCase";
        }

        public IRenameRule Parse(JsonNode obj)
        {
            string req = obj["req"].ToString();
            return new ChangeCaseRule(req);
        }

        public object ParseRuleToFileObject(IRenameRule rule)
        {
            throw new System.NotImplementedException();
        }
    }
}