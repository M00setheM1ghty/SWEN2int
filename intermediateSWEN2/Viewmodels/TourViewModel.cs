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


        private Tour? _selectedTour;
        public Tour? SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddTourCommand { get; }
        public ICommand DeleteTourCommand { get; }
        public ICommand ModifyTourCommand { get; }



        public TourViewModel()
        {
            AddTourCommand = new RelayCommand(AddTour);
            DeleteTourCommand = new RelayCommand(DeleteTour);
            ModifyTourCommand = new RelayCommand(ModifyTour);
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
                Logs = new List<TourLog>()
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
