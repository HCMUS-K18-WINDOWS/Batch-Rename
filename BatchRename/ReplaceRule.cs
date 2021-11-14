﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchRename
{
    public class ReplaceRule : IRenameRule
    {
        public List<string> Needles { get; set; }
        public string Replacer { get; set; }

        public ReplaceRule(List<string> needle, string replacer)
        {
            Needles = needle;
            Replacer = replacer;
        }
        public string Rename(string original)
        {
            string result = original;
            foreach(var needle in Needles)
            {
                result = result.Replace(needle, Replacer);
            }
            return result;
        }
    }
}