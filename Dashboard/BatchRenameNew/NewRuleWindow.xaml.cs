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
        private RequirementManager _requirementManager;
        public NewRuleWindow()
        {
            InitializeComponent();
        }

        public delegate void myDelegate(IRenameRule rule);
        public event myDelegate MyEvent;

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (string) RulesCbb.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }
            try
            {
                _requirementManager = new RequirementManager(selectedItem);
                canvasField.Children.Clear();
                canvasField.Children.Add(_requirementManager.BuildElement());
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RulesCbb.ItemsSource = RuleManager.GetInstance().GetAllRulesName();
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_requirementManager == null) return;
            var newRule = _requirementManager.CreateRule();
            MyEvent?.Invoke(newRule);
        }
    }
}
