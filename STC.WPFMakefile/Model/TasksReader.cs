using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.WPFMakefile.Model;

namespace STC.WPFMakefile.Model
{
    class TasksReader
    {
        private string fileName;
        public TasksReader(string fileName)
        {
            this.fileName = fileName;
        }

        public List<AssemblyTaskWithDependencies> ReadTasks()
        {
            var tasksAll = new Dictionary<string, AssemblyTaskWithDependencies>();

            AssemblyTaskWithDependencies currentTask = null;
            foreach (var line in File.ReadLines(fileName))
            {
                if (line.Length > 0 && !char.IsWhiteSpace(line, 0))
                {
                    var currentTaskName = line.Contains(':') ? line.Substring(0, line.IndexOf(':')) : line;
                    var taskDependenciesStr = line.Contains(':') ? line.Substring(currentTaskName.Length + 1) : "";
                    var taskDependencies = taskDependenciesStr.Split(' ').Where(x => !string.IsNullOrEmpty(x));

                    currentTask = new AssemblyTaskWithDependencies(currentTaskName, taskDependencies.ToList(), null);
                    tasksAll.Add(currentTaskName, currentTask);
                }
                else if (char.IsWhiteSpace(line, 0) && currentTask != null)
                {
                    currentTask.AddAction(line.Trim());
                }
                else
                {
                    throw new ApplicationException("incorrect input data: empty line");
                }
            }

            foreach (var t in tasksAll.Values)
            {
                foreach (var d in t.DependenciesNames)
                    tasksAll[t.Name].AddDependency(tasksAll[d]);
            }
            var tasks = tasksAll.Values.ToList();
            if (!CheckDependencies(tasks, tasksAll))
                throw new ApplicationException("Dependency name + " + "doesn't exist");

            return tasksAll.Values.ToList();
        }

        public bool CheckDependencies(List<AssemblyTaskWithDependencies> tasks, Dictionary<string, AssemblyTaskWithDependencies> tasksDict)
        {
            foreach(var d in tasks.SelectMany(x => x.DependenciesNames))
            {
                if (!tasksDict.ContainsKey(d))
                    return false;
            }
            return true;
        }
    }
}
