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
using System.Windows.Navigation;
using System.Windows.Shapes;
using STC.WPFMakefile.TopologicalSort;

namespace STC.WPFMakefile
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
            }
        }

        private void ShowDependencies_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var filename = "";
                
                var tasks = new TasksReader(filename).ReadTasks();
                var targetTask = GetTaskFromArgs(tasks, args);

                var tasksSorted = (new TopologicalSort<AssemblyTaskWithDependencies>()).DfsSort(tasks, targetTask);
                (new TasksExecutor()).PrintTasksConsole(tasksSorted);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error. " + e.Message);
            }
        }

        //!!!
        private static AssemblyTaskWithDependencies GetTaskFromArgs(
    List<AssemblyTaskWithDependencies> tasks,
    string[] args)
        {
            if (args == null || args.Length == 0)
                throw new ArgumentException("Task name wasn't specified.");

            string targetTaskName = args[0];
            var targetTask = tasks.FirstOrDefault(t => t.Name == targetTaskName);
            if (targetTask == null)
                throw new ArgumentException("There is no task with requested name.");

            return targetTask;
        }

    }
}
