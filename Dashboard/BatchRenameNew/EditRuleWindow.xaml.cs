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
using RenameRuleContract;

namespace BatchRenameNew
{
    /// <summary>
    /// Interaction logic for EditRuleWindow.xaml
    /// </summary>
    public partial class EditRuleWindow : Window
    {
        private IRenameRule _renameRule;
        private RequirementManager _requirementManager;
        public EditRuleWindow()
        {
            InitializeComponent();
        }

        public EditRuleWindow(IRenameRule rule)
        {
            InitializeComponent();
            this._renameRule = rule;
            this._requirementManager = new RequirementManager(_renameRule);
            DataContext = rule;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var panel = _requirementManager.BuildEditElement();
            canvasField.Children.Add(panel);
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if(_requirementManager.SetRule())
            MessageBox.Show("Edit successful");
            //if (_requirementManager.SetRule())
            //{
            //    Close();
            //} else
            //{
            //    MessageBox.Show("Invalid rule");
            //}
        }
    }
}
