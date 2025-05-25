using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intermediateSWEN2.Models
{
    public class TourLog
    {
        public int? Id { get; set; }
        public DateTime DateTime { get; set; }
        public string? Comment { get; set; }
        public string? Difficulty { get; set; }
        public int TotalDistance { get; set; }
        public TimeSpan TotalTime { get; set; }
        public string? Rating { get; set; }

    }
}
