using BatchRename;
using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BatchRenameNew
{
    public class RuleManager
    {
        private Dictionary<string, IRenameRule> rules = new Dictionary<string, IRenameRule>();
        private static RuleManager? instance;
        private RuleManager()
        {

        }

        public static RuleManager GetInstance()
        {
            if(instance == null)
            {
                instance = new RuleManager();
            }
            return instance;
        }

       
        public void Add(IRenameRule rule)
        {
            rules.Add(rule.Name, rule);
        }

        public IRenameRule CreateRule(string strRuleName)
        {
            if(rules.ContainsKey(strRuleName))
            {
                return (IRenameRule)rules[strRuleName].Clone();
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
                Assembly assembly = Assembly.LoadFile((fi.FullName));
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (type.IsClass &&
                        typeof(IRenameRule).IsAssignableFrom(type))
                    {
                        try
                        {
                            IRenameRule newRule = Activator.CreateInstance(type) as IRenameRule;
                            rules.Add(newRule.Name, newRule);
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