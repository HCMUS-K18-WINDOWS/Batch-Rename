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
            string strValue = (string) value;
            if (strValue == null || strValue.Length == 0)
            {
                return false;
            }
            switch (key)
            {
                case "Start":
                    StartValue = int.Parse(strValue);
                    break;
                case "Number of digit":
                    Padding = int.Parse(strValue);
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

        public string[] GetAllAttributesName()
        {
            return new string[] {"Start", "Number of digit"};
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
