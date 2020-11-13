using System;
using Xamarin.Essentials;
using ToDoApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTodo : ContentPage
    {
        public CreateTodo()
        {
            InitializeComponent();
        }
        async void OnSaveClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoModel)BindingContext;
            await App.Database.SaveItemAsync(todoItem);
            await Navigation.PopAsync();
            
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoModel)BindingContext;
            await App.Database.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
        }

    }
}