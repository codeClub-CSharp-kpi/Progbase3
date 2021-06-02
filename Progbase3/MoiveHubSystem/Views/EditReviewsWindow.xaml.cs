using EntitiesLibrary;
using MoiveHubSystem.ViewModels;
using System.Windows;

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
