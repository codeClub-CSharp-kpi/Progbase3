using Generator.models;
using System.Windows;
using System.Windows.Controls;

namespace MoiveHubSystem.Views
{
	/// <summary>
	/// Логика взаимодействия для ActorWindow.xaml
	/// </summary>
	public partial class ActorsWindow : Window
	{
		public ActorsWindow()
		{
			InitializeComponent();
		}

		private void actorsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (actorsList.SelectedItem is Actor selAct)
			{
				var actorCityId = selAct.CityId;
				int counter = 0;
				foreach (var a in cityField.ItemsSource)
				{
					if ((a as City).Id == actorCityId)
					{
						cityField.SelectedIndex = counter;
						break;
					}
					counter++;
				}
			}
		}
	}
}
