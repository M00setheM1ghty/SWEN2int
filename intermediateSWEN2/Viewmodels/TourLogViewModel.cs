using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using intermediateSWEN2.Models;
using intermediateSWEN2.Viewmodels;


namespace SWEN2.ViewModels
{
    public class TourLogViewmodel : BaseViewModel
    {
        public DateTime LogDate { get; set; } = DateTime.Now;
        public string LogComment { get; set; } = "";
        public string LogDifficulty { get; set; } = "Medium";
        public string LogTotalDistance { get; set; } = "0";
        public string LogTotalTime { get; set; } = "00:00:00";
        public int LogRating { get; set; } = 0;
        public string Error { get; private set; } = "";

        public Tour? SelectedTour;

        private ObservableCollection<TourLog> _selectedTourLogs = new();
        public ObservableCollection<TourLog> SelectedTourLogs
        {
            get => _selectedTourLogs;
            set
            {
                _selectedTourLogs = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddTourLogCommand { get; }
        public ICommand DeleteTourLogCommand { get; }
        public ICommand ModifyTourLogCommand { get; }

        public TourLogViewmodel()
        {
            AddTourLogCommand = new RelayCommand(AddTourLog);
            DeleteTourLogCommand = new RelayCommand(DeleteTourLog);
            ModifyTourLogCommand = new RelayCommand(ModifyTourLog);
        }
        private void AddTourLog(object? parameter)
        {
            if (SelectedTour != null)
            {
                var newLog = new TourLog
                {
                    Id = 1,
                    DateTime = DateTime.Now.AddDays(-1),
                    Comment = "Sunny day, easy drive.",
                    Difficulty = "Easy",
                    TotalDistance = 120,
                    TotalTime = TimeSpan.FromHours(2),
                    Rating = 4
                };
                SelectedTour.Logs.Add(newLog);
            }
        }

        private void DeleteTourLog(object? parameter)
        {
            //if (SelectedTour != null && SelectedTourLog != null)
            //{
            //    SelectedTour.Logs.Remove(SelectedTourLog);
            //    OnPropertyChanged(nameof(SelectedTourLogs));
            //}
        }

        private void ModifyTourLog(object? parameter)
        {

        }


    }
}
