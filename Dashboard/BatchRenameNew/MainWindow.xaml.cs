using BatchRename;
using Microsoft.Win32;
using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Diagnostics;
using System.ComponentModel;
using WinForms = System.Windows.Forms;

namespace BatchRenameNew
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<IRenameRule> Plugins { get; set; }
        public List<IRenameRuleParser> Parsers { get; set; }
        public BindingList<IRenameRule> Presets { get; set; }
        public BindingList<IRenameRule> Rules { get; set; }
        private FileManager fileManager = new();
        public MainWindow()
        {
            InitializeComponent();
            Plugins = new List<IRenameRule>();
            Parsers = new List<IRenameRuleParser>();
            Presets = new BindingList<IRenameRule>();
            Rules = new BindingList<IRenameRule>();
            RuleListView.ItemsSource = Rules;
        }

        public void LoadFilePreset(string fileName)
        {
            string filePath = $"Presets/{fileName}";
            string jsonString = File.ReadAllText(filePath);
            JsonNode rules = JsonNode.Parse(jsonString)["rules"];
            int length = ((JsonArray)rules).Count;

            for (int i = 0; i < length; i++)
            {
                IRenameRuleParser ruleParser = RuleParserManager.GetInstance().CreateRuleParser(rules[i]["type"].ToString());
                IRenameRule newRule = ruleParser.Parse(rules[i]);
                Rules.Add(newRule);
            }
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
                {
                    //var fileInfo = new RenameRuleContract.FileInfo()
                    //{
                    //    OldName = System.IO.Path.GetFileNameWithoutExtension(filename),
                    //    NewName = System.IO.Path.GetFileNameWithoutExtension(filename),
                    //    OldExtension = System.IO.Path.GetExtension(filename),
                    //    NewExtension = System.IO.Path.GetExtension(filename),
                    //    AbsolutePath = System.IO.Path.GetDirectoryName(filename),
                    //};
                    //fileManager.AddFile(fileInfo);
                    GetFiles(filename);
                    //dataListBoxFile.Items.Add(System.IO.Path.GetFileName(filename));
                }
            }
        }

        private void GetFiles(string directoryName)
        {
            if (Directory.Exists(directoryName))
            {
                var fileNames = Directory.GetFiles(directoryName, "*.*", SearchOption.AllDirectories);
                foreach (var file in fileNames)
                {
                    GetFiles(file);
                }
            } else
            {
                dataListBoxFile.Items.Add(System.IO.Path.GetFileName(directoryName));
                var fileInfo = new RenameRuleContract.FileInfo()
                {
                    OldName = System.IO.Path.GetFileNameWithoutExtension(directoryName),
                    NewName = System.IO.Path.GetFileNameWithoutExtension(directoryName),
                    OldExtension = System.IO.Path.GetExtension(directoryName),
                    NewExtension = System.IO.Path.GetExtension(directoryName),
                    AbsolutePath = System.IO.Path.GetDirectoryName(directoryName),
                };
                fileManager.AddFile(fileInfo);
            }
        }

        private void UploadFolder_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog();
            if (folderDialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                //Lấy địa chỉ thư mục đã chọn
                String sPath = folderDialog.SelectedPath;
                GetFiles((string)sPath);

                //Lưu địa chỉ thư mục vào ListBox
                dataListBoxFolder.Items.Add(sPath);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RuleManager.GetInstance().LoadExternalDll();
            RuleParserManager.GetInstance().LoadExternalDll();
            LoadPresetFolder();
            
        }

        private void LoadPresetFolder()
        {
            var currentFolder = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo d = new DirectoryInfo($"{currentFolder}/Presets");
            System.IO.FileInfo[] files = d.GetFiles("*.json");
            foreach(System.IO.FileInfo file in files)
            {
                PresetsCbb.Items.Add(file.Name);
            }
        }

        private void NewRuleBtn_Click(object sender, RoutedEventArgs e)
        {
            var NewRuleScreen = new NewRuleWindow();
            NewRuleScreen.MyEvent += AddNewRule;
           if (NewRuleScreen.ShowDialog().Value == true)
           {

           }
        }

        private void AddNewRule(IRenameRule rule)
        {
            Rules.Add(rule);
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            if(PresetsCbb.SelectedItem != null)
            {
                LoadFilePreset(PresetsCbb.SelectedItem.ToString());
                foreach(var rule in Rules)
                {
                    Console.WriteLine(rule.Name);
                }
                
            } else
            {
                MessageBox.Show("Please choose a file preset!");
            }
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            fileManager.ApplyRule(Rules.ToList());
            fileManager.BatchRename();
        }
    }
}
