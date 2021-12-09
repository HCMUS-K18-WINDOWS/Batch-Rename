using RenameRuleContract;
using System.Text.Json.Nodes;
namespace BatchRename
{
    internal class AddPostFixRuleParser : IRenameRuleParser
    {
        public IRenameRule Parse(JsonNode obj)
        {
            string value = obj["value"].ToString();
            return new AddPostFixRule(value);
        }
    }
}