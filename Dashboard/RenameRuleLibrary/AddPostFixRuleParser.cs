using Newtonsoft.Json;
using RenameRuleContract;
using System.Collections.Generic;
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

        public object ParseRuleToFileObject(IRenameRule rule)
        {
            var addPostFix = rule as AddPostfixRule;
            var obj = new
            {
                type = addPostFix?.Name,
                value = addPostFix?.Postfix
            };
            return obj;
        }
    }
}