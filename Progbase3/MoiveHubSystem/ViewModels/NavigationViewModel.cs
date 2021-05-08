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
					_controledMapWin.Visibility = System.Windows.Visibility.Collapsed;
					new FilmsWindow().ShowDialog();
					_controledMapWin.Visibility = System.Windows.Visibility.Visible;
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
					_controledMapWin.Visibility = System.Windows.Visibility.Collapsed;
					new ActorsWindow().ShowDialog();
					_controledMapWin.Visibility = System.Windows.Visibility.Visible;
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
					_controledMapWin.Visibility = System.Windows.Visibility.Collapsed;
					new ReviewsWindow().ShowDialog();
					_controledMapWin.Visibility = System.Windows.Visibility.Visible;
				});
			}
		}

		
		private MainWindow _controledMapWin;
		public NavigationViewModel(MainWindow mapWin)
		{
			_controledMapWin = mapWin;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
