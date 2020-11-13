using System;
using ToDoApp.Models;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace ToDoApp.Views
{
    public class CreateTodoViewModel : ContentPage
    {
        public CreateTodoViewModel()
        {
            Title = "Add New Task";

            var nameEntry = new Entry();
            nameEntry.SetBinding(Entry.TextProperty, "Name");
            nameEntry.Placeholder = "Task";
            
            var descriptionEntry = new Entry();
            descriptionEntry.SetBinding(Entry.TextProperty, "Description");
            descriptionEntry.Placeholder = "Details";
            
            var statusEntry = new Entry();
            statusEntry.SetBinding(Entry.TextProperty, "Status");
            statusEntry.Placeholder = "Done?";

            var saveButton = new Button { Text = "Add Task" };
                saveButton.BackgroundColor = (Color)App.Current.Resources["champagne"];
                saveButton.TextColor = (Color)App.Current.Resources["pinkish"];
                saveButton.Clicked += async (sender, e) =>
                {
                     //Check to see that user entered anything   
                     
                    if (nameEntry.Text == null)
                    {
                        await DisplayAlert ("Nothing to add", "Please enter something!", "OK");
                        System.Diagnostics.Debug.WriteLine("Nothing to add!");
                    }
                    else
                    {
                        try
                        {
                            Vibration.Vibrate();
                            System.Diagnostics.Debug.WriteLine("SPARAT VIBRERAR!");
                        }
                        catch (FeatureNotSupportedException ex)
                        {
                            System.Diagnostics.Debug.WriteLine("Feature not supported on device");

                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine("Other error has occurred.");
                        }
                        
                        var todoItem = (TodoModel) BindingContext;
                        await App.Database.SaveItemAsync(todoItem);
                        await Navigation.PopAsync();

                        System.Diagnostics.Debug.WriteLine("Added");
                    }
                };

            var deleteButton = new Button { Text = "Remove Task" };
                deleteButton.BackgroundColor = (Color)App.Current.Resources["champagne"];
                deleteButton.TextColor = (Color)App.Current.Resources["pinkish"];
                deleteButton.Clicked += async (sender, e) =>
                {
                    
                    // Xamarin Essentials Vibration vid Borttagning av entry
                    try
                    {
                        Vibration.Vibrate();
                        System.Diagnostics.Debug.WriteLine("DELETE VIBRERAR!");
                    }
                    catch (FeatureNotSupportedException ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Feature not supported on device");

                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Other error has occurred.");
                    }

                    var todoItem = (TodoModel) BindingContext;
                    await App.Database.DeleteItemAsync(todoItem);
                    await Navigation.PopAsync();
                    
                    System.Diagnostics.Debug.WriteLine("Deleted");
                };

                Content = new StackLayout
                {
                    Margin = new Thickness(35),
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    Children =
                    {

                        nameEntry,
                        descriptionEntry,
                        statusEntry,
                        saveButton,
                        deleteButton
                    }
                };
        }
    }
}