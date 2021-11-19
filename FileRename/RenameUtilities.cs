using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileRename
{
    internal class RenameUtilities
    {
        public static string ApplyRuleSet(List<IRenameRule> ruleSet, string filename)
        {
            string newName = filename;
            foreach(var rule in ruleSet)
            {
                newName = rule.Rename(newName);
            }
            return newName;
        }

        public static List<string> GetNewName(List<IRenameRule> ruleSet, String[] paths)
        {
            List<string> newNames = new List<string>();
            foreach(var path in paths)
            {
                string filename = Path.GetFileName(path);
                ApplyRuleSet(ruleSet, filename);
            }
            return newNames;
        }

        public static List<string> GetNewName(List<IRenameRule> ruleSet, List<string> paths)
        {
            List<string> newNames = new List<string>();
            foreach (var path in paths)
            {
                string filename = Path.GetFileName(path);
                ApplyRuleSet(ruleSet, filename);
            }
            return newNames;
        }

        public static bool ApplyNewName(string path, string newName)
        {
            if (File.Exists(path))
            {
                if (Path.IsPathRooted(path))
                {
                    return false;
                }
                string directory = Path.GetDirectoryName(path);
                string newPath = Path.Combine(directory, newName);
                try
                {
                    File.Move(path, newPath);
                } catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.ToString());
                    return false;
                }
                return true;
            }
            return false;
        }

        public static List<string> RenameBatch(List<string> paths, List<string> newNames)
        {
            List<string> errors = new List<string>();
            if (paths.Count != newNames.Count)
            {
                errors.Add("Path and count do not have the same length");
                return errors;
            }
            for (var i = 0; i < paths.Count; i++)
            {
                if (!ApplyNewName(paths[i], newNames[i]))
                {
                    errors.Add(paths[i]);
                }
            }
            return errors;
        }
    }
}
