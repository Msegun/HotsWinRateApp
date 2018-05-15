using HotsWinRate.DataBase;
using HotsWinRate.DataBase.Abstract;
using HotsWinRate.Helpers;
using HotsWinRate.ViewModels;
using HotsWinRate.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace HotsWinRate
{
    public partial class App : Application
    {
        private static IHeroRepository _heroRepository;
        private static readonly string _dbName = "herosi.db";
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new HeroPage(new HeroViewModel())); //NavigationPage => dzieki temu mozemy pushowac i popowac ze stron
                                                                              //^ root page
        }

        public static IHeroRepository HeroRepository
        {
            get
            {
                if (_heroRepository == null)
                {
                    var dbPath = DependencyService.Get<IFileHelper>().GetLocalFilePath(_dbName);
                    var dbContext = new DataBaseContext(dbPath);
                    _heroRepository = new HeroRepository(dbContext);
                }
                return _heroRepository;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
