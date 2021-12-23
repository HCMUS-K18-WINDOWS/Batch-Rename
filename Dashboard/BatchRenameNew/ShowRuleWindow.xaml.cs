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
    /// Interaction logic for ShowRuleWindow.xaml
    /// </summary>
    public partial class ShowRuleWindow : Window
    {
        private IRenameRule _renameRule;
        private RequirementManager _requirementManager;
        public ShowRuleWindow()
        {
            InitializeComponent();
        }

        public ShowRuleWindow(IRenameRule rule)
        {
            InitializeComponent();
            this._renameRule = rule;
            this._requirementManager = new RequirementManager(_renameRule);
            DataContext = rule;
        }
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var panel = _requirementManager.BuildShowElement();
            canvasField.Children.Add(panel);
        }
    }
}
