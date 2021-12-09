using RenameRuleContract;
using System.Text.Json.Nodes;
namespace BatchRename
{
    public class AddPostFixRuleParser : IRenameRuleParser
    {
        public string getName()
        {
            return "AddPostFix";
        }

        public IRenameRule Parse(JsonNode obj)
        {
            string value = obj["value"].ToString();
            return new AddPostFixRule(value);
        }
    }
}