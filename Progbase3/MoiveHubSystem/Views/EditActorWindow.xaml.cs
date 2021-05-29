using Generator.models;
using MoiveHubSystem.ViewModels;
using System.Windows;

namespace MoiveHubSystem.Views
{
	/// <summary>
	/// Логика взаимодействия для EditActorWindow.xaml
	/// </summary>
	public partial class EditActorWindow : Window
	{
		public EditActorWindow(Actor actorToEdit)
		{
			InitializeComponent();
			DataContext = new EditActorViewModel(actorToEdit);
		}
	}
}
