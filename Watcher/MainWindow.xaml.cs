using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using ASD;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        string _selectPathToWatch;
        string _selectPathToDelete;
        int _periodDays;
        string _targetPath;
        public MainWindow()
        {
            InitializeComponent();
            //DatePicker.SelectedDate = new DateTime(2020, 1, 23);
            //browsePathTB.Text = "";
            //browsePathToDeleteTB.Text = "";
            //CopyMove(new object(), new RoutedEventArgs());
            //Delete(new object(), new RoutedEventArgs());
        }

        void BrowseForWatch(object sender, RoutedEventArgs e) => browsePathTB.Text = WorkWithDirectoryWWD.WayToFolder();

        void BrowseForDelete(object sender, RoutedEventArgs e) => browsePathToDeleteTB.Text = WorkWithDirectoryWWD.WayToFolder();

        void CopyMove(object sender, RoutedEventArgs e)
        {
            TextBlock_Column2.Text = "";
            var date = DatePicker.SelectedDate;
            _periodDays = DatePicker.SelectedDate.HasValue ? (int)(DateTime.Now - DatePicker.SelectedDate.Value).TotalDays : Convert.ToInt32(periodDaysTB.Text);
            if (periodDaysTB.Text != "" && browsePathTB.Text != "")
            {
                _selectPathToWatch = browsePathTB.Text;

                _targetPath = "C:\\Будет удалено";// + DateTime.Now.AddDays(_periodDays).ToString("dd.MM.yyyy");
                WorkWithDirectoryWWD.CreateDirectoryToTargetPathWWD001(_targetPath);

                TextBlock_Column2.Text += "\nУстаревшие Каталоги:\n";
                List<string> DirectoriesList = WorkWithDirectoryWWD.GetDirectoriesOlderDatetimeWWD002(_selectPathToWatch, _periodDays);
                foreach (string i in DirectoriesList)
                {
                    TextBlock_Column2.Text += i + "\n";
                }

                TextBlock_Column2.Text += "\nУстаревшие Файлы:\n";//+string.Join("",Work_With_DirectoryWWD.GetFiles_Older_Datetime_WWD003(SPstr, Period_Days));
                List<string> FilesList = WorkWithDirectoryWWD.GetFilesOlderDatetimeWWD003(_selectPathToWatch, _periodDays);
                FilesList.ForEach(i => TextBlock_Column2.Text += i + "\n");

                /*Movement old files to destination folder */
                WorkWithDirectoryWWD.MovementFilesWWD005(_targetPath, FilesList);
                TextBlock_Column2.Text += $"\nФайлы успешно перемещены: \n{_targetPath}";
                WorkWithDirectoryWWD.MovementDirectoriesWWD004(_targetPath, DirectoriesList);
                TextBlock_Column2.Text += $"\nКаталоги успешно перемещены: \n{_targetPath}";
            }
        }

        void Delete(object sender, RoutedEventArgs e)
        {
            //TextBlock_Column2.Text = "\nУдаленные Каталоги:\n";
            //WorkWithDirectoryWWD.GetDirectoriesInDyrectoriesWWD006(_selectPathToDelete).ForEach(i => TextBlock_Column2.Text += i + "\n");

            //TextBlock_Column2.Text += "\nУдаленные Файлы:\n";
            //WorkWithDirectoryWWD.GetFilesInDirectoriesWWD007(_selectPathToDelete).ForEach(i => TextBlock_Column2.Text += i + "\n"); ;
            //Directory.Delete(_selectPathToDelete, true);
            //new FileInfo(@"C:\ti\Log.txt").Create();
            // Open the contact_list file for reading. File is placed in the debug folder.
            StreamReader sr = new StreamReader(@"C:\ti\Log.txt");
            // Variable to store each row.
            string dataRow = "";
            // String array needed to determin number of columns to use. 
            string[] headerRow;

            int columns = 0;
            // Object array used to store each line of data from the text file.
            object[] row;

            // Read the first line of the text file. Then split the data using a comma character
            headerRow = sr.ReadLine().Split('.');
            // Store the length of headerRow string array, this will tell us how many columns we need.
            columns = headerRow.Length;
            TextBlock_Column2.Text += "\nЧто в файле хранится:\n";
            foreach (string i in headerRow)
            {
                TextBlock_Column2.Text += i + "\n";
            }
            try
            {
                DateTime dt = new DateTime(Convert.ToInt32(headerRow[2]), Convert.ToInt32(headerRow[1]), Convert.ToInt32(headerRow[0]));
                MessageBox.Show(dt.ToString("dd.MM.yyyy"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                DateTime dt = new DateTime(Convert.ToInt32(headerRow[2]), Convert.ToInt32(headerRow[1]), Convert.ToInt32(headerRow[0]));
                if ((int)(DateTime.Now - dt).TotalDays == 0)
                {
                    MessageBox.Show("Yes");
                }
                else MessageBox.Show("No");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // "For Loop" below is used to add column headers to the DataGridView control. The name of each column
            // header begins with "Header" followed by a number. 



            // At this point, column headers have been added.
            // Now all that remains is to add the rows. Read the file line by line, using a while loop
            // and the ReadLine() method. When reading each line make sure it is not a blank line. If it is a blank line
            // skip the line and read the next line. 
        }
    }
}