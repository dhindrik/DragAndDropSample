using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace DragAndDrop
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            Todo.Add(new Item("Go fishing"));
            Todo.Add(new Item("Talk about Xamarin"));
        }

        public ObservableCollection<Item> Todo { get; set; } = new ObservableCollection<Item>();
        public ObservableCollection<Item> InProgress { get; set; } = new ObservableCollection<Item>();
        public ObservableCollection<Item> Done { get; set; } = new ObservableCollection<Item>();

        

        private string newTitle;
        public string NewTitle
        {
            get => newTitle;
            set => Set(ref newTitle, value);
        }

        public ICommand Save => new Command(() =>
        {
            Todo.Add(new Item(NewTitle));
            NewTitle = string.Empty;
        });

        public void Started(string id)
        {
            var item = Get(id);

            if (item != null)
            {
                item.IsVisible = false;
            }
        }

        public void InProgressDrop(string id)
        {
            var item = Get(id);
            Remove(id);
            InProgress.Add(item);
            item.IsVisible = true;
        }

        public void DoneDrop(string id)
        {
            var item = Get(id);
            Remove(id);
            Done.Add(item);
            item.IsVisible = true;
        }


        public void ToDoDrop(string id)
        {
            var item = Get(id);
            Remove(id);
            Todo.Add(item);
            item.IsVisible = true;            
        }

        private void Remove(string id)
        {
           

            var item = Todo.SingleOrDefault(x => x.Id == id);

            if(item != null)
            {
                Todo.Remove(item);
                return;
            }

            item = InProgress.SingleOrDefault(x => x.Id == id);

            if (item != null)
            {
                InProgress.Remove(item);
                return;
            }

            item = Done.SingleOrDefault(x => x.Id == id);

            if (item != null)
            {
                Done.Remove(item);
                return;
            }

        }

        private Item Get(string id)
        {
            Item item = null;

            item = Todo.SingleOrDefault(x => x.Id == id);

            if (item == null)
            {
                Done.SingleOrDefault(x => x.Id == id);
            }

            if (item == null)
            {
                Done.SingleOrDefault(x => x.Id == id);
            }

            return item;
        }
    }

    public class Item : ObservableObject
    {
        public Item(string text)
        {
            Text = text;
            Id = Guid.NewGuid().ToString();
        }

        public string Text { get; set; }
        public string Id { get; private set; }

        private bool isVisible = true;
        public bool IsVisible
        {
            get => isVisible;
            set => Set(ref isVisible, value);
        }
    }

    public class ObservableObject : INotifyPropertyChanged
    {
        protected void Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
