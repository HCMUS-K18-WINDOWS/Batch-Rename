using BatchRename;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BatchRenameNew
{
    public class RuleParserManager
    {
        private Dictionary<string, IRenameRuleParser> rules = new Dictionary<string, IRenameRuleParser>();
        private static RuleParserManager? instance;

        private RuleParserManager()
        {

        }

        public static RuleParserManager GetInstance()
        {
            if (instance == null)
            {
                instance = new RuleParserManager();
            }
            return instance;
        }


        public void Add(IRenameRuleParser rule)
        {
            rules.Add(rule.getName(), rule);
        }

        public IRenameRuleParser CreateRuleParser(string strRuleName)
        {
            if (rules.ContainsKey(strRuleName))
            {
                return (IRenameRuleParser)rules[strRuleName];
            }
            return null;
        }


        public void LoadExternalDll()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string folder = Path.GetDirectoryName(exePath);
            var infos = new DirectoryInfo(folder).GetFiles("*.dll");

            // Nạp vào bộ nhớ từng file đl
            foreach (var fi in infos)
            {
                Assembly assembly = Assembly.LoadFrom((fi.FullName));
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (type.IsClass &&
                        typeof(IRenameRuleParser).IsAssignableFrom(type))
                    {
                        try
                        {
                            IRenameRuleParser newRule = Activator.CreateInstance(type) as IRenameRuleParser;
                            rules.Add(newRule.getName(), newRule);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
            }
        }
    }
}