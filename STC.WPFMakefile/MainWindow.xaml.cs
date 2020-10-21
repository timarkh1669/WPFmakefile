﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            //dlg.DefaultExt = ".txt";
            dlg.Filter = "All documents |*.*";

            Nullable<bool> result = dlg.ShowDialog();

            if (result.HasValue && result.Value)
            {
                string filename = dlg.FileName;
                filePath.Text = filename;
            }
        }

        private void ShowDependencies_Click(object sender, RoutedEventArgs e)
        {
            var temp = (new STC.WPFMakefile.ViewModel.DependentTasksDisplay()).GetDependenciesNamesOrder(filePath.Text, targetName.Text);
            actionsListView.Items.Clear();
            foreach(var t in temp)
                actionsListView.Items.Add(t);//!!!
        }
    }
}
