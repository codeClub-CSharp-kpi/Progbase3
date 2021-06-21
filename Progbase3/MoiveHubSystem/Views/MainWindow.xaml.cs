using MoiveHubSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace MoiveHubSystem.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			try
			{
				DirectoryInfo di = new("../../../../../images");
				Icon = new BitmapImage(new Uri(di.FullName + @"/icons/favicon.ico", UriKind.Absolute));
				this.filmsImage.Source = new BitmapImage(new Uri(di.FullName + @"/pictures/filmsLogo.png", UriKind.Absolute));
				this.actorsImage.Source = new BitmapImage(new Uri(di.FullName + @"/pictures/actorsLogo.png", UriKind.Absolute));
				this.reviewsImage.Source = new BitmapImage(new Uri(di.FullName + @"/pictures/reviewsLogo.png", UriKind.Absolute));

				this.DataContext = new NavigationViewModel();
			}
			catch (Exception err)
			{
				throw new Exception(err.Message);
				//MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				//Process.GetCurrentProcess().Kill();
			}
		}
	}
}
