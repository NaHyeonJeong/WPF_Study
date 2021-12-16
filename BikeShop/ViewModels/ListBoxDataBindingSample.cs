using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace BikeShop
{
    public class ListBoxDataBindingSample : ObservableCollection<TodoItem>
    {
        public ListBoxDataBindingSample()
        {
            this.Add(new TodoItem() { Title = "Complete this WPF tutorial", Completion = 45 });
            this.Add(new TodoItem() { Title = "Learn C#", Completion = 80 });
            this.Add(new TodoItem() { Title = "Wash the car", Completion = 0 });
        }
    }

    public class TodoItem
    {
        public string Title { get; set; }
        public int Completion { get; set; }
    }
}
