using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameRuleContract
{
    public class FileInfo
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
        public string OldExtension { get; set; }
        public string NewExtension { get; set; }
        public string AbsolutePath { get; set; }
        public string Status { get; set; }

        public string GetFullOldNameString()
        {
            return OldName + OldExtension;
        }

        public string GetFullNewNameString()
        {
            return NewName + NewExtension;
        }
    }
}
