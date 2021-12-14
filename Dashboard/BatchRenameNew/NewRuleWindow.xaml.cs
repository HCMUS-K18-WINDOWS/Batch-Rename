using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BatchRenameNew
{
    /// <summary>
    /// Interaction logic for NewRuleWindow.xaml
    /// </summary>
    public partial class NewRuleWindow : Window
    {
        public NewRuleWindow()
        {
            InitializeComponent();
        }

        public delegate void myDelegate(IRenameRule rule);
        public event myDelegate MyEvent;

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = RulesCbb.SelectedItem;
            canvasField.Children.Clear();
            StackPanel mainPanel = new StackPanel();
            mainPanel.Orientation = Orientation.Vertical;
            string[] fieldNames = RuleManager.GetInstance().GetAllFieldName(selectedItem.ToString());
            foreach(string name in fieldNames)
            {
                TextBlock label = new TextBlock();
                label.Text = name;
                TextBox input = new TextBox();
                input.Width = 150;
                StackPanel panel = new StackPanel();
                //panel.Orientation = Orientation.Horizontal;
                panel.Children.Add(label);
                panel.Children.Add(input);
                mainPanel.Children.Add(panel);
            }
            canvasField.Children.Add(mainPanel);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RulesCbb.ItemsSource = RuleManager.GetInstance().GetAllRulesName();
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            IRenameRule newRule = RuleManager.GetInstance().CreateRule(RulesCbb.SelectedItem.ToString());
            MyEvent?.Invoke(newRule);
        }
    }
}
