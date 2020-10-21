﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using STC.WPFMakefile.TopologicalSort;

namespace STC.WPFMakefile.ViewModel
{
    class DependentTasksDisplay
    {
        public List<string> GetDependenciesNames(string fileName, string targetName)
        {
            try
            {
                var tasksSorted = GetDependencies(fileName, targetName);
                return tasksSorted.Select(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                return new List<string> { "Error. " + ex.Message };
            }
        }

        public List<AssemblyTaskWithDependencies> GetDependencies(string fileName, string targetName)
        {
            try
            {
                if (string.IsNullOrEmpty(targetName))
                    throw new ArgumentException("Target name not specified");
                if (string.IsNullOrEmpty(fileName))
                    throw new ArgumentException("Target name not specified");

                var tasks = new TasksReader(fileName).ReadTasks();
                var targetTask = tasks.FirstOrDefault(t => t.Name == targetName);
                if (targetTask == null)
                    throw new ArgumentException("There are no tasks with requested name.");

                var tasksSorted = (new TopologicalSort<AssemblyTaskWithDependencies>()).DfsSort(tasks, targetTask);
                return tasksSorted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}