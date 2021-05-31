using Generator.models;
using MoiveHubSystem.ViewModels;
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
using System.Windows.Shapes;

namespace MoiveHubSystem.Views
{
	/// <summary>
	/// Логика взаимодействия для EditReviewsWindow.xaml
	/// </summary>
	public partial class EditReviewsWindow : Window
	{
		public EditReviewsWindow(Review reviewToEdit)
		{
			InitializeComponent();
			DataContext = new EditReviewViewModel(reviewToEdit);
		}
	}
}
