using RenameRuleContract;
using System.Text.Json.Nodes;
namespace BatchRename
{
    public class AddPostfixRuleParser : IRenameRuleParser
    {
        public string getName()
        {
            return "AddPostfix";
        }

        public IRenameRule Parse(JsonNode obj)
        {
            string value = obj["value"].ToString();
            return new AddPostfixRule(value);
        }
    }
}