using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    internal class ExtRule : IRenameRule
    {
        public string Extension { get; set; }
        public string NewValue { get; set; }
        public ExtRule(string extension, string newValue)
        {
            Extension = extension;
            NewValue = newValue;
        }
        public string Rename(string original)
        {
            throw new NotImplementedException();
        }
    }
}
