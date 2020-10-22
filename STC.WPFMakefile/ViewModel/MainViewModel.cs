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
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged()
        {

        }
    }
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

        public AssemblyTaskDependenciesViewModel()
        {
            //!!!
            Dependencies = CollectionViewSource.GetDefaultView(new List<string>());
        }
    }
    class FileNameViewModel : DependencyObject
    {
        //propdp

        public ICollectionView FileName
        {
            get { return (ICollectionView)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName", typeof(string), typeof(FileNameViewModel), new PropertyMetadata(""));

        public FileNameViewModel()
        {
            FileName = CollectionViewSource.GetDefaultView("");
        }
    }
}
