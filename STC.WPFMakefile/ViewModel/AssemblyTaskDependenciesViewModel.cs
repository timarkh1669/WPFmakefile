using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace STC.WPFMakefile.ViewModel
{
    class AssemblyTaskDependenciesViewModel : ObservableCollection<string>
    {
        private DependentTasksDisplay d;
        public ICollectionView Dependencies { get ; set ; }
        public ObservableCollection<string> tasks;

        public AssemblyTaskDependenciesViewModel()
        {
            d = new STC.WPFMakefile.ViewModel.DependentTasksDisplay();
        }

        public AssemblyTaskDependenciesViewModel(string filePath, string targetName)
        {
            d = new STC.WPFMakefile.ViewModel.DependentTasksDisplay();

            var dependencies = d.GetDependenciesNamesOrder(filePath, targetName);
            Dependencies = CollectionViewSource.GetDefaultView(dependencies);
            //!!! ObservableCollectionResults
            //!!! ObservableCollectionTargets
        }

        public void CalculateDependencies(string filePath, string targetName)
        {
            var dependencies = d.GetDependenciesNamesOrder(filePath, targetName);
            Dependencies = CollectionViewSource.GetDefaultView(dependencies);
        }
    }
}
