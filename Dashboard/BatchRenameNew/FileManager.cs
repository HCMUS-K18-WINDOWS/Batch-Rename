using RenameRuleContract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameNew
{
    public class FileManager
    {
        public BindingList<RenameRuleContract.FileInfo> FileList { get; set;}

        public FileManager()
        {
            FileList = new BindingList<RenameRuleContract.FileInfo>();
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
                file.NewExtension = file.OldExtension;
                file.Status = "";
            }
        }
        public void UpdateName(RenameRuleContract.FileInfo file)
        {
            file.OldName = file.NewName;
            file.OldExtension = file.NewExtension;
        }
        public void ApplyRule(List<IRenameRule> ruleList)
        {
            ResetName();
            foreach(var file in this.FileList)
            {
                foreach(var rule in ruleList)
                {
                    rule.Rename(file);
                }
            }
            CheckFile();
        }
        public void CheckFile()
        {
            if(FileList.Count == 1)
            {
                FileList[0].Status = "OK";
            }
            else
            {
                foreach (var file2Check in this.FileList)
                {
                    foreach (var file in this.FileList)
                    {
                        if (file2Check == file) continue;
                        if (file2Check.NewName == file.NewName && file2Check.NewExtension == file.NewExtension
                            && file2Check.AbsolutePath == file2Check.AbsolutePath)
                        {
                            file2Check.Status = "namesake";
                        }
                        else
                        {
                            file2Check.Status = "OK";
                        }
                    }
                }
            }
        }
        private static string ApplyNewName(RenameRuleContract.FileInfo file)
        {
            if (file.Status != "OK") return file.OldName + ": namesake";
            if ((file.NewName == file.OldName) && (file.OldExtension == file.NewExtension)) {
                return file.OldName + ": no new name";
            }
            var oldFullName = file.OldName + file.OldExtension;
            string oldPath = Path.Combine(file.AbsolutePath, oldFullName);
            if (File.Exists(oldPath))
            {
                var fullName = file.NewName + file.NewExtension;
                if (fullName.Length > 255)
                {
                    return file.OldName + ": new name must not exceed 255 characters";
                }
                string newPath = Path.Combine(file.AbsolutePath, fullName);
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
            } else if(Directory.Exists(oldPath))
            {
                var fullName = file.NewName + file.NewExtension;
                if (fullName.Length > 255)
                {
                    return file.OldName + ": new name must not exceed 255 characters";
                }
                string newPath = Path.Combine(file.AbsolutePath, fullName);
                try
                {
                    Directory.Move(oldPath, newPath);
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


        private static string CopyToNewLocation(RenameRuleContract.FileInfo file, string newLocation)
        {

            if (file.Status != "OK") return file.OldName + ": namesake";
            if ((file.NewName == file.OldName) && (file.OldExtension == file.NewExtension))
            {
                return file.OldName + ": no new name";
            }
            var oldFullName = file.OldName + file.OldExtension;
            string oldPath = Path.Combine(file.AbsolutePath, oldFullName);
            if (File.Exists(oldPath))
            {
                var fullName = file.NewName + file.NewExtension;
                string newPath = Path.Combine(newLocation, fullName);
                try
                {
                    File.Copy(oldPath, newPath);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.ToString());
                    return file.OldName + ": internal error";
                }
                return "";
            } else if(Directory.Exists(oldPath))
            {
                var fullName = file.NewName + file.NewExtension;
                string newPath = Path.Combine(newLocation, fullName);
                try
                {
                    CopyFilesRecursively(oldPath, newPath);
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

        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
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
                    UpdateName(file);
            }
            return errors;
        }
        public List<string> BatchCopy(string newLocation)
        {
            if (!Directory.Exists(newLocation))
            {
                return new List<string> { newLocation + " is not exist on disk"};
            }
            List<string> errors = new List<string>();
            foreach (var file in this.FileList)
            {
                string err = CopyToNewLocation(file, newLocation);
                if (err != "")
                {
                    errors.Add(err);
                }
                else
                    UpdateName(file);
            }
            return errors;
        }
    }
}
