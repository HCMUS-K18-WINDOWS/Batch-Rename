using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class OrderRule : IRenameRule
    {
        public int Padding { get; set; }
        private int startValue;
        public int StartValue { get { return startValue; } set { startValue = value; count = value; } }
        private int count = 0;

        public string Name => "Order";

        public OrderRule()
        {
            Padding = 0;
            StartValue = 1;
        }
        public OrderRule(int startValue, int padding)
        {
            StartValue = startValue;
            Padding = padding;
            count = StartValue;
        }

        public bool SetAttribute(string key, object value)
        {
            var intValue = (int) value;
            switch (key)
            {
                case "Start":
                    StartValue = intValue;
                    break;
                case "Number of digit":
                    Padding = intValue;
                    break;
                default:
                    return false;
            }
            return true;
        }

        public object? GetAttribute(string key)
        {
            switch (key)
            {
                case "Start":
                    return StartValue;
                case "Number of digit":
                    return Padding;
                default:
                    return null;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public RuleRequirement[] GetAllAttributesRequirement()
        {
            return new RuleRequirement[] { new RuleRequirement("Start", RequirementType.Number), 
                new RuleRequirement("Number of digit", RequirementType.Number) };
        }

        public void Rename(FileInfo original)
        {
            string padding = "D" + (int)Padding;
            string oldName = original.NewName;
            string newName = oldName + count.ToString(padding);
            original.NewName = newName;
        }
    }
}
