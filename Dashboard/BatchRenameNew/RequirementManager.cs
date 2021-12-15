using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BatchRenameNew
{
    public class RequirementManager
    {
        private RuleRequirement[] RuleRequirements 
        { 
            get 
            { 
                return _requirements.Values.ToArray(); 
            } 
            set 
            {
                _uiElementDic.Clear();
                foreach(var item in value)
                {
                    _requirements[item.Name] = item;
                }
            } 
        }
        private Dictionary<string, RuleRequirement> _requirements = new();
        private Dictionary<string, UIElement> _uiElementDic = new();
        private string _name = string.Empty;
        public RequirementManager(string ruleName)
        {
            _name = ruleName;
            var re = RuleManager.GetInstance().GetAllFieldRequirement(ruleName);
            if (re != null)
            {
                this.RuleRequirements = re;
            } else
            {
                throw new KeyNotFoundException("Rule is not exist");
            }
        }
        public UIElement BuildElement()
        {
            _uiElementDic.Clear();
            var wrapper = new StackPanel
            {
                Orientation = Orientation.Vertical
            };
            foreach (var requirement in this.RuleRequirements)
            {
                var reqS = new StackPanel();
                UIElement uiElement;
                var label = new Label
                {
                    Content = requirement.Name
                };
                if (requirement.Type == RequirementType.Boolean)
                {
                    var checker = new CheckBox();
                    _uiElementDic[requirement.Name] = checker;
                    uiElement = checker;
                } else if (requirement.PossibleValues == null)
                {
                    var textbox = new TextBox();
                    _uiElementDic[requirement.Name] = textbox;
                    uiElement = textbox;
                } else
                {
                    var combobox = new ComboBox();
                    var possible = (string[]) requirement.PossibleValues;
                    combobox.ItemsSource = possible;
                    uiElement = combobox;
                }
                reqS.Children.Add(label);
                reqS.Children.Add(uiElement);
                wrapper.Children.Add(reqS);
            }
            return wrapper;
        }
        public IRenameRule CreateRule()
        {
            var newRule = RuleManager.GetInstance().CreateRule(_name);
            foreach(var requirement in this.RuleRequirements)
            {
                var field = _uiElementDic[requirement.Name];
                if (requirement.Type == RequirementType.Boolean)
                {
                    var checker = ((CheckBox)field).IsChecked;
                    newRule.SetAttribute(requirement.Name, checker);
                } else if (requirement.PossibleValues == null)
                {
                    switch (requirement.Type)
                    {
                        case RequirementType.String:
                            var text = ((TextBox)field).Text;
                            newRule.SetAttribute(requirement.Name, text);
                            break;
                        case RequirementType.Number:
                            var number = int.Parse(((TextBox)field).Text);
                            newRule.SetAttribute(requirement.Name, number);
                            break;
                    }
                } else
                {
                    switch (requirement.Type)
                    {
                        case RequirementType.String:
                            var text = (string)((ComboBox)field).SelectedItem;
                            newRule.SetAttribute(requirement.Name, text);
                            break;
                        case RequirementType.Number:
                            var number = int.Parse((string)((ComboBox)field).SelectedItem);
                            newRule.SetAttribute(requirement.Name, number);
                            break;
                    }
                }
                
            }
            return newRule;
        }
    }

}
