using ToDoApp.DbConnection;
using ToDoApp.ViewModels;
using Xamarin.Forms;


namespace ToDoApp
{
    public partial class App : Application
    {
        static SQLiteDB database;
        public App()
        {
            InitializeComponent();

            var nav = new NavigationPage(new TodoListPage());
            nav.BarBackgroundColor = (Color)App.Current.Resources["champagne"];
            nav.BarTextColor = (Color)App.Current.Resources["pinkish"];
            

            MainPage = nav;
        }
        public static SQLiteDB Database
        {
            get
            {
                if (database == null)
                {database = new SQLiteDB();}
                
                return database;
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}