using HotsWinRate.Models;
using HotsWinRate.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using Xamarin.Forms;


namespace HotsWinRate.ViewModels
{
    public class HeroViewModel : BaseViewModel
    {
        public class ApiHero
        {
            public string PrimaryName { get; set; }
            public string ImageURL { get; set; }
            public string AttributeName { get; set; }
            public string Group { get; set; }
            public string SubGroup { get; set; }
            public string Translations { get; set; }
        }

        private string _heroName;
        public string HeroName
        {
            get
            {
                return _heroName;
            }
            set
            {
                _heroName = value;
                OnPropertyChanged(nameof(HeroName));
            }
        }

        private ObservableCollection<String> _heroList;
        public ObservableCollection<String> HeroList
        {
            get
            {
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://api.hotslogs.com/Public/Data/Heroes"));

                WebReq.Method = "GET";

                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                string jsonString;
                using (Stream stream = WebResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                    jsonString = reader.ReadToEnd();
                }

                List<ApiHero> items = JsonConvert.DeserializeObject<List<ApiHero>>(jsonString);

                ObservableCollection<string> ApiHeroList = new ObservableCollection<string>();

                foreach (ApiHero b in items)
                {
                    ApiHeroList.Add(b.PrimaryName);
                }

                return ApiHeroList;
            }
            set
            {
                _heroList = value;
                OnPropertyChanged("HeroList");
            }
        }


        private ObservableCollection<String> _classList;
        public ObservableCollection<String> ClassList
        {
            get { return new ObservableCollection<string>
            {
                "Assasin",
                "Tank",
                "Support",
                "Multiclass",
                "Other"
            }; }
            set
            {
                _classList = value;
                OnPropertyChanged("ClassList");
            }
        }

        private string _heroClass;
        public string HeroClass
        {
            get
            {
                return _heroClass;
            }
            set
            {
                _heroClass = value;
                OnPropertyChanged(nameof(HeroClass));
            }
        }

        private Hero _selectedHero;
        public Hero SelectedHero
        {
            get
            {
                return _selectedHero;
            }
            set
            {
                _selectedHero = value;
                if (_selectedHero == null)
                    return;
                SelectHero.Execute(_selectedHero);
                _selectedHero = null;
                OnPropertyChanged(nameof(SelectedHero));
            }
        }

        private ObservableCollection<Hero> _heroes;
        public ObservableCollection<Hero> Heroes
        {
            get
            {
                return _heroes;
            }
            set
            {
                _heroes = value;
                OnPropertyChanged(nameof(Heroes));
            }
        }

        public Command AddHero
        {
            get
            {
                return new Command(async () =>
                {
                    if (HeroName == null)
                        HeroName =  "";
                    if (HeroName.Length > 2)
                    {
                        var hero = new Hero()
                        {
                            Namaewa = HeroName,
                            Type = HeroClass,
                            Win = 0,
                            Played = 0
                        };
                        HeroName = String.Empty;
                        HeroClass = String.Empty;
                        await App.HeroRepository.AddHeroAsync(hero);
                        RefreshHeroes.Execute(null);
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Error!", "Hero Name cannot be shorter than three letters", "Ok");
                });
            }
        }

        public Command RefreshHeroes
        {
            get
            {
                return new Command(async () =>
                {
                    var heroes = await App.HeroRepository.GetHeroesAsync();
                    Heroes = new ObservableCollection<Hero>(heroes);
                });
            }
        }

        public Command SelectHero
        {
            get
            {
                return new Command(async () =>
                {
                    var heroDetailsViewModel = new HeroDetailsViewModel()
                    {
                        HeroId = SelectedHero.Id,
                        HeroName = SelectedHero.Namaewa,
                        HeroWin = SelectedHero.Win,
                        HeroPlayed = SelectedHero.Played
                    };

                    var heroDetailsPage = new HeroDetailsPage(heroDetailsViewModel);
                    await Application.Current.MainPage.Navigation.PushAsync(heroDetailsPage);
                });
            }
        }
    }

}

