using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MoiveHubSystem
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			MessageBox.Show($"An unhandled exception just occured: {e.Exception.Message}", Process.GetCurrentProcess().ProcessName, MessageBoxButton.OK, MessageBoxImage.Error);
			e.Handled = true;
		}
	}
}
