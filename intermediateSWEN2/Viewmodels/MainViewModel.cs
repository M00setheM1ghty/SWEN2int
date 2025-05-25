using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using intermediateSWEN2.Models;
using SWEN2.ViewModels;

namespace intermediateSWEN2.Viewmodels
{
    public class MainViewModel : BaseViewModel
    {
        public TourViewModel TourViewModel { get; }
        public TourLogViewmodel TourLogViewModel { get; }

        private Tour? _selectedTour;
        public Tour? SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;
                OnPropertyChanged();
                TourViewModel.SelectedTour = value;
                TourLogViewModel.SelectedTour = value;
                TourLogViewModel.SelectedTourLogs = _selectedTour?.Logs != null
                    ? new ObservableCollection<TourLog>(_selectedTour.Logs)
                    : new ObservableCollection<TourLog>();
                // Debug output
                if (_selectedTour != null)
                {
                    System.Diagnostics.Debug.WriteLine($"SelectedTour: Name={_selectedTour.Name}, From={_selectedTour.From}, To={_selectedTour.To}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("SelectedTour is null");
                }
            }
        }
        public MainViewModel()
        {
            TourViewModel = new TourViewModel();
            TourLogViewModel = new TourLogViewmodel();

            // Subscribe to selection changes in TourViewModel
            TourViewModel.SelectedTourChanged += (s, e) =>
            {
                SelectedTour = TourViewModel.SelectedTour;
            };
        }
    }

}
