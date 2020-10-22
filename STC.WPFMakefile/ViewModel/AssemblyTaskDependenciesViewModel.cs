using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace STC.WPFMakefile.ViewModel
{
    class AssemblyTaskDependenciesViewModel : DependencyObject
    {
        //propdp

        public ICollectionView Dependencies
        {
            get { return (ICollectionView)GetValue(DependenciesProperty); }
            set { SetValue(DependenciesProperty, value); }
        }

        public static readonly DependencyProperty DependenciesProperty =
            DependencyProperty.Register("Dependencies", typeof(ICollectionView), typeof(AssemblyTaskDependenciesViewModel), new PropertyMetadata(null));

        public AssemblyTaskDependenciesViewModel(List<string> args)
        {
            Dependencies = CollectionViewSource.GetDefaultView(args);
        }
        public AssemblyTaskDependenciesViewModel(string filePath, string targetName)
        {
            var a = (new STC.WPFMakefile.ViewModel.DependentTasksDisplay()).GetDependenciesNamesOrder(filePath, targetName);
            Dependencies = CollectionViewSource.GetDefaultView(a);
        }
    }
}
