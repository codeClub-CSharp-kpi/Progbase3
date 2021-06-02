using EntitiesLibrary;
using Microsoft.Win32;
using MoiveHubSystem.Commands;
using MoiveHubSystem.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MoiveHubSystem.ViewModels
{
	class ExportViewModel : INotifyPropertyChanged
	{
		private ActorRepository _actorRepo = new();
		private FilmActorRepository _filmActorRepository = new();

		public ObservableCollection<Actor> Actors { get; set; } = new();

		public ExportViewModel()
		{
			RefillObservedActors();
		}

		private Actor _selectedActor;
		public Actor SelectedActor
		{
			get
			{
				return _selectedActor;
			}
			set
			{
				_selectedActor = value;
				OnPropertyChanged(nameof(SelectedActor));
			}
		}

		private string _pathToDirectory;
		public string PathToDirectory
		{
			get
			{
				return _pathToDirectory;
			}
			set
			{
				_pathToDirectory = value;
				OnPropertyChanged(nameof(PathToDirectory));
			}
		}


		// Commands
		public ICommand PickDirectory
		{
			get => new RelayCommand(obj =>
			{
				try
				{
					OpenFileDialog ofd = new();

					if (ofd.ShowDialog() == true)
					{
						DirectoryInfo di = new(ofd.FileName);
						PathToDirectory = di.Parent.FullName;
					}
				}
				catch (Exception err)
				{
					MessageBox.Show($"{err.Message}", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
			});
		}
		public ICommand ExecExport
		{
			get => new RelayCommand(obj =>
			{
				try
				{
					Export.ExportFilmsByActor(SelectedActor, PathToDirectory);

					MessageBox.Show("Exported Successfully", "Info",
						MessageBoxButton.OK, MessageBoxImage.Information);
				}
				catch (Exception err)
				{
					MessageBox.Show($"{err.Message}", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}

				(obj as ExportFilmsWindow).Close();
			}, obj=> { return !string.IsNullOrWhiteSpace(PathToDirectory) && SelectedActor != null; });
		}

		// pagination commands
		public ICommand LoadNextPage
		{
			get => new RelayCommand(obj =>
			{
				++_currentPageCounter;
				RefillObservedActors();

			}, obj => (_currentPageCounter < TotalPages));
		}
		public ICommand LoadPrevPage
		{
			get => new RelayCommand(obj =>
			{
				--_currentPageCounter;
				RefillObservedActors();

			}, obj => (1 < _currentPageCounter));
		}

		// pagination attributes
		private int _currentPageCounter = 1;
		const int AmountOfInPageElements = 5;
		private int TotalPages
		{
			get
			{
				int total = _actorRepo.GetAll().Count() / AmountOfInPageElements;
				if (_actorRepo.GetAll().Count() % AmountOfInPageElements != 0)
				{
					return total + 1;
				}
				else
				{
					return total;
				}
			}
		}

		
		// supporting private methods
		private IEnumerable<Actor> GetPageForList(int pageNumber)
		{
			return _actorRepo.GetPage(AmountOfInPageElements, AmountOfInPageElements * (pageNumber - 1));
		}
		private void RefillObservedActors()
		{
			Actors.Clear();
			foreach (var item in GetPageForList(_currentPageCounter))
			{
				Actors.Add(item);
			}
		}

		//
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
