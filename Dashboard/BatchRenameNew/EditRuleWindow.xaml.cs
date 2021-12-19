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
        public EditRuleWindow()
        {
            InitializeComponent();
        }

        public EditRuleWindow(IRenameRule rule)
        {
            InitializeComponent();
            DataContext = rule;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
