using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace STC.WPFMakefile.TopologicalSort
{
    class TasksExecutor
    {
        public void ExecuteTasks(List<AssemblyTaskWithDependencies> tasks)
        {
            Process.Start("CMD.exe", GetStringRepresentation(tasks));
        }

        public void PrintTasksConsole(List<AssemblyTaskWithDependencies> tasks)
        {
            Console.WriteLine(GetStringRepresentation(tasks));
        }

        private string GetStringRepresentation(List<AssemblyTaskWithDependencies> tasks)
        {
            return String.Join(Environment.NewLine, tasks.SelectMany(t => t.Actions));
        }
    }
}
