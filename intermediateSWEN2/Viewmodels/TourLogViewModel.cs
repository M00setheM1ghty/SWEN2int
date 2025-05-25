using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using intermediateSWEN2.Models;
using intermediateSWEN2.Viewmodels;
using intermediateSWEN2.Popups;


namespace SWEN2.ViewModels
{
    public class TourLogViewmodel : BaseViewModel
    {
        public DateTime LogDate { get; set; } = DateTime.Now;
        public string LogComment { get; set; } = "";
        public string LogDifficulty { get; set; } = "Medium";
        public string LogTotalDistance { get; set; } = "0";
        public string LogTotalTime { get; set; } = "00:00:00";
        public string LogRating { get; set; } = "0";
        public string Error { get; private set; } = "";

        public Tour? SelectedTour;

        private TourLog _selectedTourLog;
        public TourLog? SelectedTourLog
        {
            get => _selectedTourLog;
            set
            {
                if (_selectedTourLog != value)
                {
                    _selectedTourLog = value;
                    OnPropertyChanged();
                }
            }
        }

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

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case nameof(LogComment):
                        if (string.IsNullOrWhiteSpace(LogComment))
                            result = "Comment is required.";
                        break;
                    case nameof(LogDifficulty):
                        if (string.IsNullOrWhiteSpace(LogDifficulty))
                            result = "Difficulty is required.";
                        break;
                    case nameof(LogTotalDistance):
                        if (!int.TryParse(LogTotalDistance, out _))
                            result = "Total Distance must be a number.";
                        break;
                    case nameof(LogTotalTime):
                        if (!TimeSpan.TryParse(LogTotalTime, out _))
                            result = "Total Time must be in hh:mm:ss format.";
                        break;
                    case nameof(LogRating):
                        if (string.IsNullOrWhiteSpace(LogRating))
                            result = "Rating is required.";
                        break;
                }
                return result;
            }
        }
        private void AddTourLog(object? parameter)
        {
            var addTourLogWindow = new intermediateSWEN2.Popups.AddTourLog();
            addTourLogWindow.DataContext = this;
            _ = addTourLogWindow.ShowDialog();

            if (!CanAddTourLog(parameter) || Error != "")
            {
                Error = "Please fill in all fields correctly.";
                OnPropertyChanged(nameof(Error));
                return;
            }

            if (SelectedTour != null)
            {
                var newLog = new TourLog
                {
                    DateTime = LogDate,
                    Comment = LogComment,
                    Difficulty = LogDifficulty,
                    TotalDistance = int.TryParse(LogTotalDistance, out var distance) ? distance : 0,
                    TotalTime = TimeSpan.TryParse(LogTotalTime, out var time) ? time : TimeSpan.Zero,
                    Rating = LogRating
                };

                SelectedTour.Logs.Add(newLog);
                SelectedTour.OnPropertyChanged(nameof(SelectedTour.Logs));
                RefreshSelectedTourLogs();
            }
            Error = "";
            OnPropertyChanged(nameof(Error));
        }

        private void DeleteTourLog(object? parameter)
        {
            if (SelectedTour != null && SelectedTourLog != null)
            {
                SelectedTour.Logs.Remove(SelectedTourLog);
                OnPropertyChanged(nameof(SelectedTourLogs));
                RefreshSelectedTourLogs();
            }
        }

        private void ModifyTourLog(object? parameter)
        {
            if (SelectedTour != null && SelectedTourLog != null)
            {
                // Open modify dialog (optional, similar to AddTourLog)
                var modifyTourLogWindow = new intermediateSWEN2.Popups.ModifiyTourLog();
                modifyTourLogWindow.DataContext = this;
                _ = modifyTourLogWindow.ShowDialog();

                // Update only fields that are not empty or default
                if (!string.IsNullOrWhiteSpace(LogComment))
                    SelectedTourLog.Comment = LogComment;
                if (!string.IsNullOrWhiteSpace(LogDifficulty))
                    SelectedTourLog.Difficulty = LogDifficulty;
                if (int.TryParse(LogTotalDistance, out var distance))
                    SelectedTourLog.TotalDistance = distance;
                if (TimeSpan.TryParse(LogTotalTime, out var time))
                    SelectedTourLog.TotalTime = time;
                if (!string.IsNullOrWhiteSpace(LogRating))
                    SelectedTourLog.Rating = LogRating;
                if (LogDate != default)
                    SelectedTourLog.DateTime = LogDate;

                SelectedTour.OnPropertyChanged(nameof(SelectedTour.Logs));
                RefreshSelectedTourLogs();
                Error = "";
                OnPropertyChanged(nameof(Error));
            }
            else
            {
                Error = "No tour log selected for modification.";
                OnPropertyChanged(nameof(Error));
            }
        }


        private bool CanAddTourLog(object? parameter)
        {
            return string.IsNullOrWhiteSpace(this[nameof(LogDate)]) &&
                   string.IsNullOrWhiteSpace(this[nameof(LogComment)]) &&
                   string.IsNullOrWhiteSpace(this[nameof(LogDifficulty)]) &&
                   string.IsNullOrWhiteSpace(this[nameof(LogTotalDistance)]) &&
                   string.IsNullOrWhiteSpace(this[nameof(LogTotalTime)]) &&
                   string.IsNullOrWhiteSpace(this[nameof(LogRating)]);
        }

        private void RefreshSelectedTourLogs()
        {
            if (SelectedTour != null)
            {
                SelectedTourLogs = new ObservableCollection<TourLog>(SelectedTour.Logs);
            }
            else
            {
                SelectedTourLogs = new ObservableCollection<TourLog>();
            }
        }


    }
}
