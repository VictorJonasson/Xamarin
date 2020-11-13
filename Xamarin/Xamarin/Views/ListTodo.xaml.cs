using System;
using ToDoApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListTodo : ContentPage
    {
        public ListTodo()
        {
            InitializeComponent();
        }
        async void OnSaveClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoModel)BindingContext;
            await App.Database.SaveItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateTodo
            {
                BindingContext = new TodoModel()
            });
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoModel)BindingContext;
            await App.Database.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new CreateTodoViewModel
                {
                    BindingContext = e.SelectedItem as TodoModel
                });
            }
        }

    }
}