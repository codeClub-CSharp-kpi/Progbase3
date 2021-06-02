using EntitiesLibrary;
using MoiveHubSystem.ViewModels;
using System.Windows;

namespace MoiveHubSystem.Views
{
	/// <summary>
	/// Логика взаимодействия для EditFilmWindow.xaml
	/// </summary>
	public partial class EditFilmWindow : Window
	{
		public EditFilmWindow(Film filmToEdit)
		{
			InitializeComponent();
			DataContext = new EditFilmViewModel(filmToEdit);
		}
	}
}
