using Newtonsoft.Json;
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
    /// Interaction logic for SavePresetWindow.xaml
    /// </summary>
    public partial class SavePresetWindow : Window
    {
        public List<IRenameRule> Rules { get; set; }
        public SavePresetWindow(List<IRenameRule> rules)
        {
            InitializeComponent();
            Rules = rules;
        }

        public delegate void MyDelegate();
        public event MyDelegate Handle;

        private void SavePresetBtn_Click(object sender, RoutedEventArgs e)
        {
            var fileContentObject = new
            {
                rules = new List<object>(),
            };
            foreach (var rule in Rules.ToList())
            {
                var ruleParser = RuleParserManager.GetInstance().CreateRuleParser(rule.Name);
                object jsonRule = ruleParser.ParseRuleToFileObject(rule);
                fileContentObject.rules.Add(jsonRule);
            }
            JsonSerializerSettings config = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            string fileContentJson = JsonConvert.SerializeObject(fileContentObject, Formatting.Indented, config);
            string d = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = FileNameTb.Text;
            if(fileName.Equals(""))
            {
                MessageBox.Show($"Please enter filename!");
                return;
            }
            fileName += ".json";
            string filePath = System.IO.Path.Combine(d, "Presets", fileName);
            if (!System.IO.File.Exists(filePath))
            {
                System.IO.File.WriteAllText(filePath, fileContentJson, Encoding.Unicode);
                Handle?.Invoke();
                DialogResult = true;
            }
            else
            {
                MessageBox.Show($"{fileName} already existed!");
            }
        }
    }
}
