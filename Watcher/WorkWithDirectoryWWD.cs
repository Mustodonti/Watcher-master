using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace ASD
{
    class WorkWithDirectoryWWD
    {
        static public string WayToFolder()
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                return FBD.SelectedPath;
            }
            else
            {
                MessageBox.Show("Вы не выбрали директорию");
                return "";
            }
        }
        static public string CreateDirectoryToTargetPathWWD001(string Target_Path)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Target_Path);//Работа с Директорией
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                    return ("\nCreate destination folder for old files\n------------------------------Successful---------------------------------\n");
                }
                else return ("Папка уже существует,прежде удалите ее, для того чтобы продолжить");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "ErrorIn#WWD001");
                return (ex.Message + "\nErrorIn#WWD001\n");
            }
        }
        static public List<string> GetDirectoriesOlderDatetimeWWD002(string Path, int Period_Days)
        {
            List<string> Old_Path = new List<string>();
            try
            {
                string[] dir = Directory.GetDirectories(Path);
                foreach (string path in dir)
                {
                    DateTime dt = Directory.GetLastWriteTime(path);
                    if (dt < DateTime.Now.AddDays(-Period_Days))
                    {
                        Old_Path.Add(path);
                    }
                }
                return Old_Path;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nErrorIn#WWD002\n");
                return Old_Path;
            }
        }

        static public List<string> GetFilesOlderDatetimeWWD003(string Path, int Period_Days)
        {
            List<string> Old_Path = new List<string>();
            try
            {

                string[] files = Directory.GetFiles(Path);
                foreach (string path in files)
                {
                    string date = DateTime.Now.ToString("dd.MM.yyyy");//Работа с датой
                    DateTime dt = Directory.GetLastWriteTime(path);
                    if (dt < DateTime.Now.AddDays(-Period_Days))
                    {
                        Old_Path.Add(path);


                    }

                }
                return Old_Path;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nErrorIn#WWD003\n");
                return Old_Path;
            }
        }

        static public void MovementDirectoriesWWD004(string targetPath, List<string> PathOfDirectories)
        {
            try
            {
                DirectoryInfo targetDir = new DirectoryInfo(targetPath);
                if (!targetDir.Exists) targetDir.Create();
                var DirectoriesInTargetDir = targetDir.GetDirectories().Select(f => f.Name);
                foreach (string Dir in PathOfDirectories)
                {
                    DirectoryInfo F = new DirectoryInfo(Dir);
                    string DirName = F.Name;
                    int i = 1;
                    while (DirectoriesInTargetDir.Contains(DirName))
                    {
                        MessageBox.Show("2");
                        i++;
                        DirName = string.Format("{0} ({1})", DirName, i);
                    }
                    F.MoveTo(Path.Combine(targetPath, DirName));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nErrorIn#WWD005\n");
            }

        }
        static public void MovementFilesWWD005(string targetPath, List<string> PathOfFiles)
        {
            try
            {
                DirectoryInfo targetDir = new DirectoryInfo(targetPath);
                if (!targetDir.Exists) targetDir.Create();
                var filesInTargetDir = targetDir.GetFiles().Select(f => f.Name);
                foreach (string sms in filesInTargetDir)
                {
                    MessageBox.Show(sms);
                }

                foreach (string file in PathOfFiles)
                {
                    string fileName = Path.GetFileName(file);
                    int i = 1;
                    FileInfo F = new FileInfo(file);
                    while (filesInTargetDir.Contains(fileName))
                    {
                        i++;
                        fileName = string.Format("{0} ({1}) {2}", Path.GetFileNameWithoutExtension(F.FullName), i, F.Extension);
                    }
                    F.MoveTo(Path.Combine(targetPath, fileName));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nErrorIn#WWD005\n");
            }
        }
        static public List<string> GetDirectoriesInDyrectoriesWWD006(string Path)
        {
            List<string> DerectoriesAreDeleted_Path = new List<string>();
            try
            {
                string[] dir = Directory.GetDirectories(Path);
                foreach (string path in dir)
                {
                    DerectoriesAreDeleted_Path.Add(path);
                    Directory.Delete(path, true);
                }
                return DerectoriesAreDeleted_Path;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nErrorIn#WWD006\n");
                return DerectoriesAreDeleted_Path;
            }
        }

        static public List<string> GetFilesInDirectoriesWWD007(string Path)
        {
            List<string> FilesAreDeleted_Path = new List<string>();
            try
            {

                string[] files = Directory.GetFiles(Path);
                foreach (string path in files)
                {
                    FilesAreDeleted_Path.Add(path);
                    FileInfo fl = new FileInfo(path);
                    fl.Delete();
                }
                return FilesAreDeleted_Path;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nErrorIn#WWD007\n");
                return FilesAreDeleted_Path;
            }
        }
    }

}