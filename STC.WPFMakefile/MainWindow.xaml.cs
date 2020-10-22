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
using STC.WPFMakefile.ViewModel;

namespace STC.WPFMakefile
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AssemblyTaskDependenciesViewModel tmp;
        public MainWindow()
        {
            InitializeComponent();
            // DataContext = new MyVm(new MyModel);
            //DataContext = new AssemblyTaskDependenciesViewModel();
            this.DataContext = new AssemblyTaskDependenciesViewModel(filePath.Text, targetName.Text);
            tmp = new AssemblyTaskDependenciesViewModel(filePath.Text, targetName.Text);
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.Filter = "All documents |*.*";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                filePath.Text = filename;
            }
        }

        private void ShowDependencies_Click(object sender, RoutedEventArgs e)
        {
            tmp.CalculateDependencies(filePath.Text, targetName.Text);
            this.DataContext = tmp;
            //DataContext = new AssemblyTaskDependenciesViewModel(filePath.Text, targetName.Text);
            //            ((AssemblyTaskDependenciesViewModel)DataContext).CalculateDependencies(filePath.Text, targetName.Text);
        }
    }
}
