using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.TextFormatting;
using STC.WPFMakefile.Model;
using STC.WPFMakefile.TopologicalSort;

namespace STC.WPFMakefile.ViewModel
{
    public class MainViewModel : ObservableCollection<string>
    {

        //!!! ObservableCollectionResults
        //!!! ObservableCollectionTargets
        public MainViewModel()
        {
            Targets = new ObservableCollection<string>();
            AssemblyTaskDependencies = new ObservableCollection<string>();
        }

        private string filePath;
        private ObservableCollection<string> _targets;

        public string FilePath
        {
            get => filePath;
            set
            {
                filePath = value;
                Targets = GetTargetNames();//readonly!1!
                OnPropertyChanged(new PropertyChangedEventArgs("FilePath"));
            }
        }

        public ObservableCollection<string> Targets
        {
            get => _targets;
            private set
            {
                if (_targets == value)
                {
                    return;
                }
                _targets = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Targets"));
            }
        }

        public ObservableCollection<string> AssemblyTaskDependencies { get; set; }

        public string SelectedTarget { get; set; }

        public ObservableCollection<string> GetTargetNames()
        {
            var tasks = ReadTasks();
            var tasksNames = tasks.Select(x => x.Name).ToList();
            //!!!
            tasksNames.Append(DateTime.Now.ToString());
            return new ObservableCollection<string>(tasksNames);
        }

        public void CalculateDependencies()
        {
            var dependencies = GetDependenciesNamesOrder(SelectedTarget);

            AssemblyTaskDependencies.Clear();
            foreach (var d in dependencies)
                AssemblyTaskDependencies.Add(d);
        }

        public List<string> GetDependenciesNamesOrder(string targetName)
        {
            try
            {
                var tasksSorted = GetDependencies(targetName);

                return tasksSorted.SelectMany(x => x.Actions).ToList();
            }
            catch (Exception ex)
            {
                return new List<string> { "Error. " + ex.Message };
            }
        }

        public List<AssemblyTaskWithDependencies> GetDependencies(string targetName)
        {
            try
            {
                if (string.IsNullOrEmpty(targetName))
                    throw new ArgumentException("Target name not specified");

                var tasks = ReadTasks();
                var targetTask = tasks.FirstOrDefault(t => t.Name == targetName);
                if (targetTask == null)
                    throw new ArgumentException("There are no tasks with requested name.");

                var tasksSorted = new TopologicalSort<AssemblyTaskWithDependencies>().DfsSort(tasks, targetTask);
                return tasksSorted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //reads all tasks with all dependencies
        public List<AssemblyTaskWithDependencies> ReadTasks()
        {
            if (string.IsNullOrEmpty(FilePath))
                throw new ArgumentException("File name not specified");//!!! sometimes it throws an error in runtime
            if (!File.Exists(FilePath))
                throw new ArgumentException("File with path <" + FilePath + "> not found");

            return new TasksReader(FilePath).ReadTasks();
        }
    }
}
