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
using Newtonsoft.Json;
using System.Text.Encodings.Web;
using System.Web;
using System.Threading;

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
        private FileManager folderManager = new();
        public string LastChosenPreset { get; set; }
        private bool isFileFeature = true;
        public MainWindow()
        {
            InitializeComponent();
            Plugins = new List<IRenameRule>();
            Parsers = new List<IRenameRuleParser>();
            Presets = new BindingList<IRenameRule>();
            Rules = new BindingList<IRenameRule>();
            RuleListView.ItemsSource = Rules;
            LastChosenPreset = "";

            RuleManager.GetInstance().LoadExternalDll();
            RuleParserManager.GetInstance().LoadExternalDll();
            LoadProjectConfig();
        }

        public async void Backup()
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));
            while (await timer.WaitForNextTickAsync())
            {
                SaveLastTimeState();
            }
        }

        private void LoadProjectConfig()
        {
            var d = AppDomain.CurrentDomain.BaseDirectory;
            var fileConfig = Path.Combine(d, "project.json");
            if(File.Exists(fileConfig))
            {
                JsonNode? config = JsonNode.Parse(File.ReadAllText(fileConfig));
                if(config != null) { 
                    LastChosenPreset = config["last_chosen_preset"].ToString();
                    PresetsCbb.SelectedItem = LastChosenPreset;
                    this.Left = config["position"]["x"].GetValue<double>();
                    this.Top = config["position"]["y"].GetValue<double>();
                    this.Width = config["size"]["width"].GetValue<double>();
                    this.Height = config["size"]["height"].GetValue<double>();
                    this.isFileFeature = config["is_file_feature"].GetValue<bool>();
                    List<RenameRuleContract.FileInfo> files = JsonConvert.DeserializeObject<List<RenameRuleContract.FileInfo>>(config["files"].ToJsonString());
                    if (isFileFeature)
                    {
                        
                        fileManager.FileList = new BindingList<RenameRuleContract.FileInfo>(files);
                    } else
                    {
                        folderManager.FileList = new BindingList<RenameRuleContract.FileInfo>(files);

                    }
                    foreach (var file in files)
                    {
                        // Đang hiển thị tên cũ và k có extention của file khi load file trong file backup project.json
                        // => Khi đổi danh sách file thành bảng thì cần chỉnh lại dòng này!
                        dataListBoxFile.Items.Add(file.OldName);
                    }
                    JsonNode? rules = config["rules"];
                    int ruleLength = ((JsonArray)rules).Count;
                    for (int i = 0; i < ruleLength; i++)
                    {
                        var parser = RuleParserManager.GetInstance().CreateRuleParser(rules[i]["type"].ToString());
                        var rule = parser.Parse(rules[i]);
                        Rules.Add(rule);
                    }
                }
                 
            }
        }

        public void LoadFilePreset(string fileName)
        {
            LastChosenPreset = fileName;
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
        private void listBoxFiles_DragDrop(object sender, DragEventArgs e)
        {
            string[] directoryName = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in directoryName)
            {
                GetFiles(file);
            }
            
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
                    GetFiles(filename);
                }
            }
        }

        private void GetFolder(string directoryName)
        {
            if (Directory.Exists(directoryName))
            {
                var fileNames = Directory.GetDirectories(directoryName);
                foreach (var file in fileNames)
                {
                    GetFolder(file);
                }
                var fileInfo = new RenameRuleContract.FileInfo()
                {
                    OldName = System.IO.Path.GetFileNameWithoutExtension(directoryName),
                    NewName = System.IO.Path.GetFileNameWithoutExtension(directoryName),
                    OldExtension = "",
                    NewExtension = "",
                    AbsolutePath = System.IO.Path.GetDirectoryName(directoryName)
                };
                foreach (var file in folderManager.FileList)
                {
                    if (file.AbsolutePath == fileInfo.AbsolutePath && file.NewName == fileInfo.NewName)
                    {
                        MessageBox.Show($"Folder: {fileInfo.NewName} exist");
                        return;
                    }
                }
                folderManager.AddFile(fileInfo);
                dataListBoxFile.Items.Add(fileInfo.GetFullOldNameString());
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
                var fileInfo = new RenameRuleContract.FileInfo()
                {
                    OldName = System.IO.Path.GetFileNameWithoutExtension(directoryName),
                    NewName = System.IO.Path.GetFileNameWithoutExtension(directoryName),
                    OldExtension = System.IO.Path.GetExtension(directoryName),
                    NewExtension = System.IO.Path.GetExtension(directoryName),
                    AbsolutePath = System.IO.Path.GetDirectoryName(directoryName),
                };
                foreach(var file in fileManager.FileList)
                {
                    if(file.AbsolutePath == fileInfo.AbsolutePath && file.NewName == fileInfo.NewName)
                    {
                        MessageBox.Show($"File: {fileInfo.NewName} exist");
                        return;
                    }
                }
                fileManager.AddFile(fileInfo);
                dataListBoxFile.Items.Add(System.IO.Path.GetFileName(directoryName));
            }
        }

        private void UploadFolder_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog();
            if (folderDialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                //Lấy địa chỉ thư mục đã chọn
                String sPath = folderDialog.SelectedPath;
                if(isFileFeature)
                {

                GetFiles((string)sPath);
                } else
                {
                    GetFolder(sPath);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPresetFolder();
            Backup();
            //dataListBoxFolder.ItemsSource = fileManager.FileList;
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
            if(isFileFeature)
            {
                fileManager.ApplyRule(Rules.ToList());
                if (fileManager.FileList.Count == 0)
                {
                    return;
                }
                var errors = fileManager.BatchRename();
                dataListBoxFile.Items.Clear();
                foreach (var file in fileManager.FileList)
                {
                    dataListBoxFile.Items.Add(file.GetFullNewNameString());
                }
                if (errors.Count == 0)
                {
                    MessageBox.Show("Rename successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(string.Join("\n", errors.ToArray()), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } else
            {
                folderManager.ApplyRule(Rules.ToList());
                if (folderManager.FileList.Count == 0)
                {
                    return;
                }
                var errors = folderManager.BatchRename();
                dataListBoxFile.Items.Clear();
                foreach (var file in folderManager.FileList)
                {
                    dataListBoxFile.Items.Add(file.GetFullNewNameString());
                }
                if (errors.Count == 0)
                {
                    MessageBox.Show("Rename successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                }
                else
                {
                    MessageBox.Show(string.Join("\n", errors.ToArray()), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                dataListBoxFolder.Items.Clear();
                folderManager.FileList.Clear();
                dataListBoxFile.Items.Clear();
            }
            
        }

        private void SavePresetBtn_Click(object sender, RoutedEventArgs e)
        {
            var savePresetScreen = new SavePresetWindow(Rules.ToList());
            savePresetScreen.Handle += ReloadPresetsFolder;
            savePresetScreen.ShowDialog();
        }

        public void ReloadPresetsFolder()
        {
            PresetsCbb.Items.Clear();
            LoadPresetFolder();
        }
        
        public void SaveLastTimeState()
        {
            var listRules = new List<object>();
            foreach(var rule in Rules.ToList())
            {
                var parser = RuleParserManager.GetInstance().CreateRuleParser(rule.Name);
                var ruleObj = parser.ParseRuleToFileObject(rule);
                listRules.Add(ruleObj);
            }
            var config = new
            {
                position = new { x = this.Left, y = this.Top },
                size = new { width = this.Width, height = this.Height },
                last_chosen_preset = LastChosenPreset != null ? LastChosenPreset : "",
                is_file_feature = isFileFeature,
                files = isFileFeature ? fileManager.FileList : folderManager.FileList,
                rules = listRules
            };

            var d = AppDomain.CurrentDomain.BaseDirectory;
            var projectPath = Path.Combine(d, "project.json");
            if (File.Exists(projectPath))
            {
                string jsonConfigString = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(projectPath, jsonConfigString);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            SaveLastTimeState();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var index = RuleListView.SelectedIndex;
            var _rule = Rules[index];

            var screen = new EditRuleWindow(_rule);
            screen.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var _rule = (IRenameRule)RuleListView.SelectedItem;
            Rules.Remove(_rule);
        }

        private void DeleteAllRule_Click(object sender, RoutedEventArgs e)
        {
            
            string message = "Are you sure to Delete All Rules?";
            string caption = "Notification";
            WinForms.MessageBoxButtons buttons = WinForms.MessageBoxButtons.YesNo;
            WinForms.DialogResult result;
            if(Rules.Count() == 0)
            {
                return;
            }
            result = WinForms.MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                Rules.Clear();
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var index = RuleListView.SelectedIndex;
            var _rule = Rules[index];
            var screen = new ShowRuleWindow(_rule);
            screen.ShowDialog();
        }

        private void PreviewBtn_Click(object sender, RoutedEventArgs e)
        {
            dataListBoxFolder.Items.Clear();
            if(isFileFeature)
            {
                fileManager.ApplyRule(Rules.ToList());
                foreach (var file in fileManager.FileList)
                {
                    dataListBoxFolder.Items.Add(file.GetFullNewNameString());
                }
            } else
            {
                folderManager.ApplyRule(Rules.ToList());
                foreach (var file in folderManager.FileList)
                {
                    dataListBoxFolder.Items.Add(file.GetFullNewNameString());
                }
            }
            
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog();
            if (folderDialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                //Lấy địa chỉ thư mục đã chọn
                String sPath = folderDialog.SelectedPath;
                fileManager.ApplyRule(Rules.ToList());
                var errors = fileManager.BatchCopy(sPath);
                if (errors.Count == 0)
                {
                    MessageBox.Show("Copy successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(string.Join("\n", errors.ToArray()), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Clear_File_Click(object sender, RoutedEventArgs e)
        {
            string message = "Are you sure to DELETE ALL?";
            string caption = "Notification";
            WinForms.MessageBoxButtons buttons = WinForms.MessageBoxButtons.YesNo;
            WinForms.DialogResult result;
            if(isFileFeature)
            {
                if (fileManager.FileList.Count() == 0)
                {
                    return;
                }
                result = WinForms.MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    fileManager.FileList.Clear();
                    dataListBoxFile.Items.Clear();
                }
            } else
            {
                if (folderManager.FileList.Count() == 0)
                {
                    return;
                }
                result = WinForms.MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    folderManager.FileList.Clear();
                    dataListBoxFile.Items.Clear();
                }
            }
            
        }

        private void SwitchBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!isFileFeature)
            {
                dataListBoxFile.Items.Clear();
                if (fileManager.FileList.Count() > 0)
                {
                    foreach (var file in fileManager.FileList.ToList())
                    {
                        dataListBoxFile.Items.Add(file.GetFullOldNameString());
                    }
                }
                UploadFileBtn.Visibility = Visibility.Visible;
                CopyBtn.Visibility = Visibility.Visible;
                PathHeader.Text = "File";
                isFileFeature = true;
            } else
            {
                dataListBoxFile.Items.Clear();
                if(folderManager.FileList.Count() > 0)
                {
                    foreach(var folder in folderManager.FileList.ToList())
                    {
                        dataListBoxFile.Items.Add(folder.GetFullOldNameString());
                    }
                }
                UploadFileBtn.Visibility = Visibility.Hidden;
                CopyBtn.Visibility = Visibility.Hidden;
                PathHeader.Text = "Folder";
                isFileFeature = false;
            }
        }
    }
}
