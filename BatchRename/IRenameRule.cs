using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchRename
{
    public interface IRenameRule
    {
        string Rename(string original);
    }
}