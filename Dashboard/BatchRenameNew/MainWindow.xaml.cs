using BatchRename;
using Microsoft.Win32;
using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using WinForms = System.Windows.Forms;

namespace BatchRenameNew
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<IRenameRule> plugins { get; set; }
        public List<IRenameRuleParser> parsers { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            plugins = new List<IRenameRule>();
            parsers = new List<IRenameRuleParser>();
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); //Để khi bấm vào icon shutdown, chương trình sẽ kết thúc
        }

        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove(); //Giúp duy chuyển cửa sổ hộp thoại
        }

        private void UploadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true; //Giúp đọc được nhiểu file cùng lúc
            if (openFileDialog.ShowDialog() == true)
            {
                //Lọc qua mảng các file và lưu tên vào ListBox
                foreach (string filename in openFileDialog.FileNames)
                    dataListBoxFile.Items.Add(System.IO.Path.GetFileName(filename));
            }
        }

        private void UploadFolder_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog();
            if (folderDialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                //Lấy địa chỉ thư mục đã chọn
                String sPath = folderDialog.SelectedPath;

                //Lưu địa chỉ thư mục vào ListBox
                dataListBoxFolder.Items.Add(sPath);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RuleManager.GetInstance().LoadExternalDll();
            RuleParserManager.GetInstance().LoadExternalDll();
        }
    }
}
