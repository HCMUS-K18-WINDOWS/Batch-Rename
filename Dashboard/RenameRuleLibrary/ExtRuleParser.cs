using RenameRuleContract;
using System.Text.Json.Nodes;

namespace BatchRename
{
    public class ExtRuleParser : IRenameRuleParser
    {
        public string getName()
        {
            return "Ext";
        }

        public IRenameRule Parse(JsonNode obj)
        {
            string ext = obj["req"].ToString();
            string newValue = obj["value"].ToString();
            return new ExtRule(ext, newValue);
        }
    }
}