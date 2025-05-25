using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intermediateSWEN2.Viewmodels
{
    public class MainViewModel : BaseViewModel
    {
        public TourViewModel TourViewModel { get; }

        public MainViewModel()
        {
            TourViewModel = new TourViewModel();
        }
    }

}
