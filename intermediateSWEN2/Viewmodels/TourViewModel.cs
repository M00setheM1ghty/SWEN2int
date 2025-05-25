using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using intermediateSWEN2.Models;
using System.Windows.Input;

namespace intermediateSWEN2.Viewmodels
{

    public class TourViewModel : BaseViewModel
    {
        public ObservableCollection<Tour> Tours { get; set; } = new();
        public string NewTourName { get; set; } = "";
        public string NewTourDescription { get; set; } = "";
        public string NewTourFrom { get; set; } = "";
        public string NewTourTo { get; set; } = "";
        public string NewTourTransportType { get; set; } = "";

        public string Error { get; private set; } = "";

        private ObservableCollection<Tour> _selectedTourCollection = new();
        public ObservableCollection<Tour> SelectedTourCollection
        {
            get => _selectedTourCollection;
            set
            {
                _selectedTourCollection = value;
                OnPropertyChanged();
            }
        }

        private Tour? _selectedTour;
        public Tour? SelectedTour
        {
            get => _selectedTour;
            set
            {
                if (_selectedTour != value)
                {
                    _selectedTour = value;
                    OnPropertyChanged();
                    UpdateSelectedTourCollection();
                    SelectedTourChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public event EventHandler? SelectedTourChanged;

        private void UpdateSelectedTourCollection()
        {
            SelectedTourCollection.Clear();
            if (SelectedTour != null)
                SelectedTourCollection.Add(SelectedTour);
        }


        public ICommand AddTourCommand { get; }
        public ICommand DeleteTourCommand { get; }
        public ICommand ModifyTourCommand { get; }



        public TourViewModel()
        {
            AddTourCommand = new RelayCommand(AddTour);
            DeleteTourCommand = new RelayCommand(DeleteTour);
            ModifyTourCommand = new RelayCommand(ModifyTour);

            Tours.Add(new Tour
            {
                Name = "City Explorer",
                Description = "A tour through the city's main attractions.",
                From = "Central Station",
                To = "Museum District",
                TransportType = "Walk",
                Logs = new ObservableCollection<TourLog>
    {
        new TourLog
        {
            Id = 1,
            DateTime = DateTime.Now.AddDays(-2),
            Comment = "Great weather, lots of fun.",
            Difficulty = "Easy",
            TotalDistance = 5,
            TotalTime = TimeSpan.FromHours(1.5),
            Rating = "4"
        },
        new TourLog
        {
            Id = 2,
            DateTime = DateTime.Now.AddDays(-1),
            Comment = "Crowded but enjoyable.",
            Difficulty = "Medium",
            TotalDistance = 5,
            TotalTime = TimeSpan.FromHours(2),
            Rating = "4"
        }
    }
            });
            Tours.Add(new Tour
            {
                Name = "Mountain Adventure",
                Description = "A scenic drive to the mountains.",
                From = "Downtown",
                To = "Mountain Peak",
                TransportType = "Car",
                Logs = new ObservableCollection<TourLog>
    {
        new TourLog
        {
            Id = 3,
            DateTime = DateTime.Now.AddDays(-3),
            Comment = "Challenging drive, beautiful views.",
            Difficulty = "Hard",
            TotalDistance = 50,
            TotalTime = TimeSpan.FromHours(3),
            Rating = "1"
        }
    }
            });
        }

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case nameof(NewTourName):
                        if (string.IsNullOrWhiteSpace(NewTourName))
                            result = "Name is required.";
                        break;
                    case nameof(NewTourDescription):
                        if (string.IsNullOrWhiteSpace(NewTourDescription))
                            result = "Description is required.";
                        break;
                    case nameof(NewTourFrom):
                        if (string.IsNullOrWhiteSpace(NewTourFrom))
                            result = "From is required.";
                        break;
                    case nameof(NewTourTo):
                        if (string.IsNullOrWhiteSpace(NewTourTo))
                            result = "To is required.";
                        break;
                    case nameof(NewTourTransportType):
                        if (string.IsNullOrWhiteSpace(NewTourTransportType))
                            result = "Transport type is required.";
                        break;
                }
                return result;
            }
        }

        private void AddTour(object? parameter)
        {
            // Skip the dialog window for testing purposes
            if (parameter is bool skipDialog && skipDialog)
            {
                AddTourCore();
                return;
            }

            var addTourWindow = new Popups.AddTour();
            addTourWindow.DataContext = this;
            _ = addTourWindow.ShowDialog();

            AddTourCore();
        }

        private void AddTourCore()
        {
            // Validate all fields before adding
            if (!string.IsNullOrWhiteSpace(this[nameof(NewTourName)]) ||
                !string.IsNullOrWhiteSpace(this[nameof(NewTourDescription)]) ||
                !string.IsNullOrWhiteSpace(this[nameof(NewTourFrom)]) ||
                !string.IsNullOrWhiteSpace(this[nameof(NewTourTo)]) ||
                !string.IsNullOrWhiteSpace(this[nameof(NewTourTransportType)]))
            {
                Error = "Please fill in all fields correctly.";
                OnPropertyChanged(nameof(Error));
                return;
            }

            var newTour = new Tour
            {
                Name = NewTourName,
                Description = NewTourDescription,
                From = NewTourFrom,
                To = NewTourTo,
                TransportType = NewTourTransportType,
                Logs = new ObservableCollection<TourLog>()
            };
            Tours.Add(newTour);

            // Clear fields after adding
            NewTourName = "";
            NewTourDescription = "";
            NewTourFrom = "";
            NewTourTo = "";
            NewTourTransportType = "";
            Error = "";
            OnPropertyChanged(nameof(NewTourName));
            OnPropertyChanged(nameof(NewTourDescription));
            OnPropertyChanged(nameof(NewTourFrom));
            OnPropertyChanged(nameof(NewTourTo));
            OnPropertyChanged(nameof(NewTourTransportType));
            OnPropertyChanged(nameof(Error));
        }

        private void DeleteTour(object? parameter)
        {
            if (SelectedTour != null)
                Tours.Remove(SelectedTour);
        }

        private void ModifyTour(object? parameter)
        {
            var addTourWindow = new Popups.ModifyTour();
            addTourWindow.DataContext = this;
            _ = addTourWindow.ShowDialog();

            if (SelectedTour == null)
                return;

            // Only update if the new value is not null or whitespace
            if (!string.IsNullOrWhiteSpace(NewTourName))
                SelectedTour.Name = NewTourName;
            if (!string.IsNullOrWhiteSpace(NewTourDescription))
                SelectedTour.Description = NewTourDescription;
            if (!string.IsNullOrWhiteSpace(NewTourFrom))
                SelectedTour.From = NewTourFrom;
            if (!string.IsNullOrWhiteSpace(NewTourTo))
                SelectedTour.To = NewTourTo;
            if (!string.IsNullOrWhiteSpace(NewTourTransportType))
                SelectedTour.TransportType = NewTourTransportType;

            // Notify UI about changes
            OnPropertyChanged(nameof(Tours));
            OnPropertyChanged(nameof(SelectedTour));

            // clear the input fields after modification
            NewTourName = "";
            NewTourDescription = "";
            NewTourFrom = "";
            NewTourTo = "";
            NewTourTransportType = "";
            OnPropertyChanged(nameof(NewTourName));
            OnPropertyChanged(nameof(NewTourDescription));
            OnPropertyChanged(nameof(NewTourFrom));
            OnPropertyChanged(nameof(NewTourTo));
            OnPropertyChanged(nameof(NewTourTransportType));
        }
    }

}
