using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;

namespace BatchRename
{
    public interface IRenameRuleParser
    {
        IRenameRule Parse(JsonNode obj);
    }
}