using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace MVVMDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = new Window();
            var viewModel = new ViewModel();
            mainWindow.DataContext = viewModel;
            mainWindow.Show();
            viewModel.GetCourseIdFromDB();
        }
    }
}
