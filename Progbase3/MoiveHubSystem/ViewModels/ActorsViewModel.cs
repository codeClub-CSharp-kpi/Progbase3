using Generator.models;
using Generator.Repostitories.implementations;
using Generator.Repostitories.interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MoiveHubSystem.ViewModels
{
	class ActorsViewModel: INotifyPropertyChanged
	{
		const int AmountOfInPageElements = 5;
		
		private ActorRepository _repo;

		public ObservableCollection<Actor> Actors { get; set; }

		public ActorsViewModel()
		{
			_repo = new ActorRepository();
			Actors = new ObservableCollection<Actor>();

			foreach (var item in GetPageForList(_currentPageCounter))
			{
				Actors.Add(item);
			}
		}

		// current page counter
		private int _currentPageCounter = 1;

		// supporting private methods
		private IEnumerable<Actor> GetPageForList(int pageNumber)
		{
			return _repo.GetPage(AmountOfInPageElements, AmountOfInPageElements * (pageNumber - 1));
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
