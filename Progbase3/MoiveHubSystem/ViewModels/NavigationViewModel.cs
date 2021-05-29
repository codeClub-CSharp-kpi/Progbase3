using MoiveHubSystem.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MoiveHubSystem.ViewModels
{
	public class NavigationViewModel : INotifyPropertyChanged
	{
		private Commands.RelayCommand _openFilmsWindow;
		public Commands.RelayCommand OpenFilmsWindow
		{
			get
			{
				return _openFilmsWindow ?? new Commands.RelayCommand(obj =>
				{
					(obj as MainWindow).Visibility = System.Windows.Visibility.Collapsed;
					new FilmsWindow().ShowDialog();
					(obj as MainWindow).Visibility = System.Windows.Visibility.Visible;
				});
			}
		}

		private Commands.RelayCommand _openActorsWindow;
		public Commands.RelayCommand OpenActorsWindow
		{
			get
			{
				return _openActorsWindow ?? new Commands.RelayCommand(obj =>
				{
					(obj as MainWindow).Visibility = System.Windows.Visibility.Collapsed;
					new ActorsWindow().ShowDialog();
					(obj as MainWindow).Visibility = System.Windows.Visibility.Visible;
				});
			}
		}

		private Commands.RelayCommand _openReviewsWindow;
		public Commands.RelayCommand OpenReviewsWindow
		{
			get
			{
				return _openReviewsWindow ?? new Commands.RelayCommand(obj =>
				{
					(obj as MainWindow).Visibility = System.Windows.Visibility.Collapsed;
					new ReviewsWindow().ShowDialog();
					(obj as MainWindow).Visibility = System.Windows.Visibility.Visible;
				});
			}
		}

		
		
		public NavigationViewModel()
		{
			
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
