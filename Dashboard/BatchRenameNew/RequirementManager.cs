using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BatchRenameNew
{
    public class RequirementManager
    {

        Regex specialChar = new Regex(@"[/\\*:?<>|]");
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
        private IRenameRule? _renameRule;
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
        public RequirementManager(IRenameRule renameRule)
        {
            _name = renameRule.Name;
            _renameRule = renameRule;
            RuleRequirements = renameRule.GetAllAttributesRequirement();
        }
        public UIElement BuildEditElement()
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
                var content = _renameRule?.GetAttribute(requirement.Name);
                if (requirement.Type == RequirementType.Boolean)
                {
                    var checker = new CheckBox();
                    var isChecked = (bool) content!;
                    checker.IsChecked = isChecked;
                    _uiElementDic[requirement.Name] = checker;
                    uiElement = checker;
                }
                else if (requirement.PossibleValues == null)
                {
                    var textbox = new TextBox();
                    string text = content!.ToString();
                    textbox.Text = text;
                    _uiElementDic[requirement.Name] = textbox;
                    uiElement = textbox;
                }
                else
                {
                    var combobox = new ComboBox();
                    var possible = (string[])requirement.PossibleValues;
                    combobox.ItemsSource = possible;
                    var selectedItem = (string) content!;
                    combobox.SelectedItem = selectedItem;
                    _uiElementDic[requirement.Name] = combobox;
                    uiElement = combobox;
                }
                reqS.Children.Add(label);
                reqS.Children.Add(uiElement);
                wrapper.Children.Add(reqS);
            }
            return wrapper;
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
                    _uiElementDic[requirement.Name] = combobox;
                    uiElement = combobox;
                }
                reqS.Children.Add(label);
                reqS.Children.Add(uiElement);
                wrapper.Children.Add(reqS);
            }
            return wrapper;
        }
        public bool CheckText(string text)
        {
            MatchCollection matched = specialChar.Matches((string)text);
            if (matched.Count > 0)
            {
                MessageBox.Show("Don't use special char include: \\ / : * ? \" < > |");
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool SetRule()
        {
            var backupRule = (IRenameRule) _renameRule?.Clone()!;
            foreach (var requirement in this.RuleRequirements)
            {
                var field = _uiElementDic[requirement.Name];
                if (requirement.Type == RequirementType.Boolean)
                {
                    var checker = ((CheckBox)field).IsChecked;
                    _renameRule?.SetAttribute(requirement.Name, checker!);
                }
                else if (requirement.PossibleValues == null)
                {
                    switch (requirement.Type)
                    {
                        case RequirementType.String:
                            var text = ((TextBox)field).Text;
                            if (!CheckText(text))
                            {
                                _renameRule = backupRule;
                                return false;
                            }
                            _renameRule?.SetAttribute(requirement.Name, text);
                            break;
                        case RequirementType.Number:
                            if (((TextBox)field).Text.Equals("")) break;
                            var number = int.Parse(((TextBox)field).Text);
                            _renameRule?.SetAttribute(requirement.Name, number);
                            break;
                    }
                }
                else
                {
                    switch (requirement.Type)
                    {
                        case RequirementType.String:
                            var text = (string)((ComboBox)field).SelectedItem;
                            _renameRule?.SetAttribute(requirement.Name, text);
                            break;
                        case RequirementType.Number:
                            var number = int.Parse((string)((ComboBox)field).SelectedItem);
                            _renameRule?.SetAttribute(requirement.Name, number);
                            break;
                    }
                }

            }
            return true;
        }
        public IRenameRule? CreateRule()
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
                            if (!CheckText(text))
                            {
                                return null;
                            }
                            newRule.SetAttribute(requirement.Name, text);
                            break;
                        case RequirementType.Number:
                            if (((TextBox)field).Text.Equals("")) break;
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
