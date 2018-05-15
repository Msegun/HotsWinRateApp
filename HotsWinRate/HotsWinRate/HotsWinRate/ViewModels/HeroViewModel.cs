using HotsWinRate.Models;
using HotsWinRate.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace HotsWinRate.ViewModels
{
    public class HeroViewModel : BaseViewModel
    {
        private string _heroName; //pole entry do dodania nazwy bohatera
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
                    var hero = new Hero()
                    {
                        Namaewa = HeroName,
                        Win = 0,
                        Played = 0
                    };
                    await App.HeroRepository.AddHeroAsync(hero);
                    RefreshHeroes.Execute(null);
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

