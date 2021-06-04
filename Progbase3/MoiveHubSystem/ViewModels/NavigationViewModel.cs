using EntitiesLibrary;
using MoiveHubSystem.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MoiveHubSystem.ViewModels
{
	public class NavigationViewModel : INotifyPropertyChanged
	{
		IAccountRepository _accountRepository = new AccountRepository();

		private string _roleName;

		private Commands.RelayCommand _openFilmsWindow;
		public Commands.RelayCommand OpenFilmsWindow
		{
			get
			{
				return _openFilmsWindow ?? new Commands.RelayCommand(obj =>
				{
					var mapWnd = obj as MainWindow;

					mapWnd.Visibility = System.Windows.Visibility.Collapsed;
					FilmsWindow fw = new FilmsWindow();

					switch (IdentifyRoleId(mapWnd))
					{
						case (int)Role_Id.Moderator:
							fw.crudBox.Visibility = System.Windows.Visibility.Visible;
							break;
						case (int)Role_Id.User:
							fw.crudBox.Visibility = System.Windows.Visibility.Collapsed;
							break;
					}
					fw.ShowDialog();

					mapWnd.Visibility = System.Windows.Visibility.Visible;
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
					
					var mapWnd = obj as MainWindow;

					mapWnd.Visibility = System.Windows.Visibility.Collapsed;
					ActorsWindow aw = new ActorsWindow();

					switch (IdentifyRoleId(mapWnd))
					{
						case (int)Role_Id.Moderator:
							aw.crudBox.Visibility = System.Windows.Visibility.Visible;
							break;
						case (int)Role_Id.User:
							aw.crudBox.Visibility = System.Windows.Visibility.Collapsed;
							break;
					}


					aw.ShowDialog();
					mapWnd.Visibility = System.Windows.Visibility.Visible;
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
					var mapWnd = obj as MainWindow;

					mapWnd.Visibility = System.Windows.Visibility.Collapsed;
					ReviewsWindow rw = new ReviewsWindow();
					rw.userName.Text = mapWnd.userName.Text;

					rw.ShowDialog();
					mapWnd.Visibility = System.Windows.Visibility.Visible;
				});
			}
		}

		public ICommand ExecExit
		{
			get => new Commands.RelayCommand(obj =>
			{
				(obj as MainWindow).Close();
			});
		}
		
		public ICommand ExecExportFilms
		{
			get => new Commands.RelayCommand(obj => 
			{
				new ExportFilmsWindow().ShowDialog();
			});
		}

		public ICommand ExecImportFilms
		{
			get => new Commands.RelayCommand(obj =>
			{
				new ImportFilmsWindow().ShowDialog();
			});
		}

		public ICommand ExecGenReport
		{
			get => new Commands.RelayCommand(obj =>
			{
				GenSelectWindow gsw = new();
				if (gsw.ShowDialog() == true)
				{

				}
			});
		}
		private int IdentifyRoleId(MainWindow mapWnd)
		{
			var allAccs = _accountRepository.GetAll();
			var acc = allAccs.Where(obj => obj.Login == mapWnd.userName.Text).FirstOrDefault();

			return acc.RoleId;
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
