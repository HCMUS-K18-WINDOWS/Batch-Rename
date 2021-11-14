using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchRename
{
    public class RuleParserFactory
    {
        public static IRenameRuleParser CreateRuleParser(string strType)
        {
            switch (strType)
            {
                case "STRINGREPLACE":
                    return new ReplaceRuleParser();
                case "ADDPREFIX":
                    return new AddPrefixRuleParser();
                case "REGEX":
                    return new RegexRuleParser();
                case "ORDER":
                    return new OrderRuleParser();
                case "EXT":
                    return new ExtRuleParser();
                case "TRIM":
                    return new TrimRuleParser();
                case "CHARREPLACE":
                    return new CharReplaceRuleParser();
                case "ADDPOSTFIX":
                    return new AddPostFixRuleParser();
                case "CHANGECASE":
                    return new ChangeCaseRuleParser();
                default:
                    return null;
            }
        }
    }
}