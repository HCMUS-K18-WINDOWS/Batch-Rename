using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


using Microsoft.Win32; //FileDialog
using WinForms = System.Windows.Forms; //FolderDialog // To do this, add a reference to System.Windows.Forms under Project-> References with Add Reference.
using System.IO; //Folder, Directory
using System.Diagnostics; //Debug.WriteLine

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using Path = System.IO.Path;
using RenameRuleContract;

namespace Dashboard1
{
    public partial class MainWindow : Window
    {
        public List<IRenameRule> plugins { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            plugins = new List<IRenameRule>();
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
            LoadExternalDll();
        }

        public void LoadExternalDll()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string folder = Path.GetDirectoryName(exePath);
            var infos = new DirectoryInfo(folder).GetFiles("*.dll");

            // Nạp vào bộ nhớ từng file đl
            foreach (var fi in infos)
            {
                Assembly assembly = Assembly.LoadFile((fi.FullName));
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (type.IsClass &&
                        typeof(IRenameRule).IsAssignableFrom(type))
                    {
                        try
                        {
                            plugins.Add(Activator.CreateInstance(type) as IRenameRule);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
            }

            foreach (var plugin in plugins)
            {
                Console.WriteLine("Plugin: " + plugin.ToString());
            }
        }
    }
}
