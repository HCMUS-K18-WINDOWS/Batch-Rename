using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace BatchRename
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<IRenameRule> listRules = new List<IRenameRule>();

            string fileName = "preset.json";
            string jsonString = File.ReadAllText(fileName);
            JsonNode rules = JsonNode.Parse(jsonString)["rules"];
            int length = ((JsonArray)rules).Count;
            
            for (int i = 0; i < length; i++)
            {
                IRenameRuleParser ruleParser = RuleParserFactory.CreateRuleParser(rules[i]["type"].ToString());
                IRenameRule newRule = ruleParser.Parse(rules[i]);
                listRules.Add(newRule);
            }
        }
    }
}
