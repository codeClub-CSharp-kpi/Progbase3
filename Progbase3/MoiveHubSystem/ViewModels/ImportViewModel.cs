using EntitiesLibrary;
using Microsoft.Win32;
using MoiveHubSystem.Commands;
using MoiveHubSystem.Views;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MoiveHubSystem.ViewModels
{
	class ImportViewModel : INotifyPropertyChanged
	{
		private string _pathOfXML;
		public string PathOfXML
		{
			get
			{
				return _pathOfXML;
			}
			set
			{
				_pathOfXML = value;
				OnPropertyChanged(nameof(PathOfXML));
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

					ofd.Filter = "(*.xml)|*.xml";
					if (ofd.ShowDialog() == true)
					{
						DirectoryInfo di = new(ofd.FileName);
						if (di.Extension == ".xml")
						{
							PathOfXML = di.FullName;
						}
						else
						{
							throw new Exception("Sorry! File should be of XML type");
						}
					}
				}
				catch (Exception err)
				{
					MessageBox.Show($"{err.Message}", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
			});
		}

		public ICommand ExecImport
		{
			get => new RelayCommand(obj =>
			{
				try
				{
					var importedEntities = Import.ImportExportedFilms(PathOfXML);
					if (importedEntities.Count > 0)
					{
						MessageBox.Show($"Success! Imported entities amount: {importedEntities.Count}", "Info",
						MessageBoxButton.OK, MessageBoxImage.Information);
					}
					else
					{
						MessageBox.Show("Ignored! XML contains already existing in database films!", "Info",
						MessageBoxButton.OK, MessageBoxImage.Exclamation);
					}
					
				}
				catch (Exception err)
				{
					MessageBox.Show($"{err.Message}", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}

				(obj as ImportFilmsWindow).Close();
			}, obj => { return !string.IsNullOrWhiteSpace(PathOfXML); });
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
