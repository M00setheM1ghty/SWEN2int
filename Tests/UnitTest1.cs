using NUnit.Framework;
using intermediateSWEN2.Viewmodels;
using intermediateSWEN2.Models;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        private TourViewModel vm;

        [SetUp]
        public void Setup()
        {
            vm = new TourViewModel();
        }

        [Test]
        public void AddTourCommand_WithValidInput_AddsTourToCollection()
        {
            vm.NewTourName = "Test Tour";
            vm.NewTourDescription = "A test tour";
            vm.NewTourFrom = "A";
            vm.NewTourTo = "B";
            vm.NewTourTransportType = "Car";

            // Pass 'true' to skip the dialog window
            vm.AddTourCommand.Execute(true);

            Assert.That(vm.Tours.Count, Is.EqualTo(3)); // +2 default tours
            Assert.That(vm.Tours[2].Name, Is.EqualTo("Test Tour"));
        }

        [Test]
        public void AddTourCommand_WithMissingFields_DoesNotAddTour()
        {
            vm.NewTourName = ""; // Missing name
            vm.NewTourDescription = "A test tour";
            vm.NewTourFrom = "A";
            vm.NewTourTo = "B";
            vm.NewTourTransportType = "Car";

            // Pass 'true' to skip the dialog window
            vm.AddTourCommand.Execute(true);

            Assert.That(vm.Tours.Count, Is.EqualTo(2));
            Assert.That(vm.Error, Is.Not.Empty);
        }

        [Test]
        public void DeleteTourCommand_RemovesSelectedTour()
        {
            var tour = new Tour { Name = "To Delete" };
            vm.Tours.Add(tour);
            vm.SelectedTour = tour;

            vm.DeleteTourCommand.Execute(null);

            Assert.That(vm.Tours.Count, Is.EqualTo(2)); // 2 because of default tours
        }

        [Test]
        public void Constructor_PopulatesToursWithDefaults()
        {
            Assert.That(vm.Tours.Count, Is.GreaterThanOrEqualTo(2));
        }
    }
}
