using EntitiesLibrary;
using ImageGenratorLib;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WordGeneratorLib;

namespace MoiveHubSystem.ViewModels
{
	class GenSelectViewModel: INotifyPropertyChanged
	{
		private ActorRepository _actorRepo = new();

		public ObservableCollection<Actor> PickList { get; set; } = new();

		public GenSelectViewModel()
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

		private string _pathToPlace;
		public string PathToPlace
		{
			get
			{
				return _pathToPlace;
			}
			set
			{
				_pathToPlace = value;
				OnPropertyChanged(nameof(PathToPlace));
			}
		}

		public ICommand ExecGen
		{
			get => new RelayCommand(obj =>
			{
				WordGenerator.GenerateWithActorData(PathToPlace, SelectedActor);
				MessageBox.Show("The report has been generated successfully!", "Success!",
							MessageBoxButton.OK, MessageBoxImage.Information);
				
				DiagramGenerator.CompileStatisticsAccordingToActor(PathToPlace, SelectedActor);

				(obj as GenSelectWindow).Close();
			}, obj => !string.IsNullOrEmpty(PathToPlace) && SelectedActor != null);
		}

		public ICommand ChooseReportPlace
		{
			get => new RelayCommand(obj =>
			{
				OpenFileDialog ofd = new OpenFileDialog();
				if (ofd.ShowDialog() == true)
				{
					PathToPlace = Directory.GetParent(ofd.FileName).FullName;
				}
			});
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
			PickList.Clear();
			foreach (var item in GetPageForList(_currentPageCounter))
			{
				PickList.Add(item);
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
