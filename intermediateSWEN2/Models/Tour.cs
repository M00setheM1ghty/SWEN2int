using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace intermediateSWEN2.Models
{
    public class Tour : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id { get; set; }

        private string _name = "";
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        private string _description = "";
        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        private string _from = "";
        public string From
        {
            get => _from;
            set { _from = value; OnPropertyChanged(); }
        }

        private string _to = "";
        public string To
        {
            get => _to;
            set { _to = value; OnPropertyChanged(); }
        }

        private string _transportType = "";
        public string TransportType
        {
            get => _transportType;
            set { _transportType = value; OnPropertyChanged(); }
        }

        public double Distance { get; set; } = 0.0;
        public TimeSpan EstimatedTime { get; set; } = TimeSpan.Zero;
        public string RouteImagePath { get; set; } = "placeholder.png";

        public ObservableCollection<TourLog> Logs { get; set; } = new();

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}

