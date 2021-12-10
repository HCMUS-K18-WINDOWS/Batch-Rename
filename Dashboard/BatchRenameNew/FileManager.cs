using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameNew
{
    public class FileManager
    {
        public List<RenameRuleContract.FileInfo> FileList { get; set;}

        public FileManager()
        {
            FileList = new List<RenameRuleContract.FileInfo>();
        }
        public void AddFile(RenameRuleContract.FileInfo file)
        {
            FileList.Add(file);
        }
        public void ResetName()
        {
            foreach (RenameRuleContract.FileInfo file in FileList)
            {
                file.NewName = file.OldName;
                file.NewExtension = file.NewExtension;
                file.Status = "";
            }
        }
        public void ApplyRule(List<IRenameRule> ruleList)
        {
            ResetName();
            foreach(var file in this.FileList)
            {
                foreach (var rule in ruleList)
                {
                    rule.Rename(file);
                }
            }
            CheckFile();
        }
        public void CheckFile()
        {
            foreach(var file2Check in this.FileList)
            {
                foreach (var file in this.FileList)
                {
                    if (file2Check == file) continue;
                    if (file2Check.NewName == file.NewName && file2Check.NewExtension == file.NewExtension
                        && file2Check.AbsolutePath == file2Check.AbsolutePath)
                    {
                        file2Check.Status = "namesake";
                    } else
                    {
                        file2Check.Status = "OK";
                    }
                }
            }
        }
        private static string ApplyNewName(RenameRuleContract.FileInfo file)
        {
            if (file.Status != "OK") return file.OldName + ": namesake";
            if ((file.OldName == file.NewExtension) || (file.OldExtension == file.NewExtension)) {
                return file.OldName + ": no new name";
            }
            string oldPath = Path.Combine(file.AbsolutePath, file.OldName);
            if (File.Exists(oldPath))
            {
                if (Path.IsPathRooted(oldPath))
                {
                    return file.OldName + ": path is rooted";
                }
                string directory = Path.GetDirectoryName(file.AbsolutePath);
                string newPath = Path.Combine(directory, file.NewName);
                try
                {
                    File.Move(oldPath, newPath);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.ToString());
                    return file.OldName + ": internal error";
                }
                return "";
            }
            return file.OldName + ": does not exist";
        }
        public void ClearAllFile()
        {
            FileList.Clear();
        }
        public List<string> BatchRename()
        {
            List<string> errors = new List<string>();
            foreach (var file in this.FileList)
            {
                string err = ApplyNewName(file);
                if (err != "")
                {
                    errors.Add(err);
                } else
                {
                    file.OldExtension = file.NewExtension;
                    file.OldName = file.NewName;
                }
            }
            return errors;
        }
    }
}
