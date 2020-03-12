using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using ASD;

namespace WpfApp2
{
    public partial class Window : System.Windows.Window
    {
        string _selectPathToWatch;
        int _periodDays=0;
        string _targetPath;
        public Window()
        {
            InitializeComponent();  
            DirectoryInfo selectPathToDelete = new DirectoryInfo(@"C:\Будет удалено");
            if (selectPathToDelete.Exists)
            {
                try
                {
                    StreamReader sr = new StreamReader(@"C:\Будет удалено\Log.txt");
                    string[] headerRow = sr.ReadLine().Split('.');
                    sr.Close();
                    DateTime dt = new DateTime(Convert.ToInt32(headerRow[2]), Convert.ToInt32(headerRow[1]), Convert.ToInt32(headerRow[0]));
                    if ((int)(DateTime.Now - dt).TotalDays >= 0)
                    {
                        Directory.Delete(@"C:\Будет удалено", true);
                    }
                    else this.Close();                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void BrowseForWatch(object sender, RoutedEventArgs e) => browsePathTB.Text = WorkWithDirectoryWWD.WayToFolder();

        void BrowseForDelete(object sender, RoutedEventArgs e) => browsePathToDeleteTB.Text = WorkWithDirectoryWWD.WayToFolder();

        void CopyMove(object sender, RoutedEventArgs e)
        {
            TextBlock_Column2.Text = "";
            var date = DateFilterFiles.SelectedDate.Value;
            try
            {
                _periodDays = (int)(DateTime.Now - date).TotalDays;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (browsePathTB.Text != "")
            {
                _selectPathToWatch = browsePathTB.Text;
                _targetPath = "C:\\Будет удалено";
                WorkWithDirectoryWWD.CreateDirectoryToTargetPathWWD001(_targetPath);

                TextBlock_Column2.Text += "\nУстаревшие Каталоги:\n";
                List<string> DirectoriesList = WorkWithDirectoryWWD.GetDirectoriesOlderDatetimeWWD002(_selectPathToWatch, _periodDays);
                DirectoriesList.ForEach(i=> TextBlock_Column2.Text += i + "\n");

                TextBlock_Column2.Text += "\nУстаревшие Файлы:\n";
                List<string> FilesList = WorkWithDirectoryWWD.GetFilesOlderDatetimeWWD003(_selectPathToWatch, _periodDays);
                FilesList.ForEach(i => TextBlock_Column2.Text += i + "\n");

                /*Movement old files to destination folder */
                WorkWithDirectoryWWD.MovementFilesWWD005(_targetPath, FilesList);
                TextBlock_Column2.Text += $"\nФайлы успешно перемещены: \n{_targetPath}";
                WorkWithDirectoryWWD.MovementDirectoriesWWD004(_targetPath, DirectoriesList);
                TextBlock_Column2.Text += $"\nКаталоги успешно перемещены: \n{_targetPath}";

                /*Create Log.txt*/
                try
                {
                    using (StreamWriter sw = new StreamWriter(@"C:\Будет удалено\Log.txt", false, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(DatePicker.SelectedDate.Value.ToString("dd.MM.yyyy"));
                        sw.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Не заполнен путь к директории");
            }
        }
        void Delete(object sender, RoutedEventArgs e)
        {
            if (browsePathToDeleteTB.Text != "")
            {
                WorkWithDirectoryWWD.GetDirectoriesInDyrectoriesWWD006(browsePathToDeleteTB.Text).ForEach(i => TextBlock_Column2.Text += i + "\n");
                WorkWithDirectoryWWD.GetFilesInDirectoriesWWD007(browsePathToDeleteTB.Text).ForEach(i => TextBlock_Column2.Text += i + "\n"); ;
                Directory.Delete(browsePathToDeleteTB.Text, true);
            }
            else MessageBox.Show("Вы не ввели путь для удаления директории");
        }
    }
}