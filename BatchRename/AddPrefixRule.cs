using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchRename
{
    public class AddPrefixRule : IRenameRule
    {
        public string Prefix { get; set; }

        public AddPrefixRule(string prefix)
        {
            Prefix = prefix;
        }
        public string Rename(string original)
        {
            string result = "";
            return $"{Prefix}{result}";
        }
    }
}