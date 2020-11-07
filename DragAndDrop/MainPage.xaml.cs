using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DragAndDrop
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel ViewModel => (MainViewModel) BindingContext;

        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainViewModel();
        }

        void DragGestureRecognizer_DragStarting(System.Object sender, Xamarin.Forms.DragStartingEventArgs e)
        {
            var id = ((DragGestureRecognizer)sender).DragStartingCommandParameter.ToString();
            e.Data.Properties.Add("Id", id);

            ViewModel.Started(id);
        }

        void DropGestureRecognizer_Drop(System.Object sender, Xamarin.Forms.DropEventArgs e)
        {
            var id = (string)e.Data.Properties["Id"];

            ViewModel.ToDoDrop(id);
        }

        void InProgressDropGestureRecognizer_Drop(System.Object sender, Xamarin.Forms.DropEventArgs e)
        {
            var id = (string)e.Data.Properties["Id"];

            ViewModel.InProgressDrop(id);
        }

        void DoneDropGestureRecognizer_Drop(System.Object sender, Xamarin.Forms.DropEventArgs e)
        {
            var id = (string)e.Data.Properties["Id"];

            ViewModel.DoneDrop(id);
        }
    }
}
