using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using STC.WPFMakefile.Annotations;
using STC.WPFMakefile.TopologicalSort;
using STC.WPFMakefile.ViewModel;

namespace STC.WPFMakefile
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainViewModel AssemblyTaskDependencies { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            AssemblyTaskDependencies = new MainViewModel();
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.Filter = "All documents |*.*";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
                AssemblyTaskDependencies.FilePath = dlg.FileName;
        }

        private void ShowDependencies_Click(object sender, RoutedEventArgs e)
        {
            AssemblyTaskDependencies.CalculateDependencies();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
