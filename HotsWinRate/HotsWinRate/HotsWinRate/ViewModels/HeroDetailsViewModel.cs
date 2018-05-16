using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HotsWinRate.ViewModels
{
    public class HeroDetailsViewModel : BaseViewModel
    {
        private int _heroId;
        public int HeroId
        {
            get => _heroId;
            set { _heroId = value; OnPropertyChanged(nameof(HeroId));  }
        }

        private string _heroName;
        public string HeroName
        {
            get => _heroName;
            set { _heroName = value; OnPropertyChanged(nameof(HeroName)); }
        }

        private int _heroWin;
        public int HeroWin
        {
            get => _heroWin;
            set { _heroWin = value; OnPropertyChanged(nameof(HeroWin)); }
        }

        private int _heroPlayed;
        public int HeroPlayed
        {
            get => _heroPlayed;
            set { _heroPlayed = value; OnPropertyChanged(nameof(HeroPlayed)); }
        }

        public Command HeroHasWon
        {
            get
            {
                return new Command(async () =>
                {
                    var hero = await App.HeroRepository.GetHeroByIdAsync(HeroId);
                    hero.Win = hero.Win + 1;
                    hero.Played = hero.Played + 1;
                    await App.HeroRepository.UpdateHeroAsync(hero);
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                });
            }
        }

        public Command HeroHasLost
        {
            get
            {
                return new Command(async () =>
                {
                    var hero = await App.HeroRepository.GetHeroByIdAsync(HeroId);
                    hero.Played = hero.Played + 1;
                    await App.HeroRepository.UpdateHeroAsync(hero);
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                });
            }
        }

        public Command HeroRestart
        {
            get
            {
                return new Command(async () =>
                {
                    var hero = await App.HeroRepository.GetHeroByIdAsync(HeroId);
                    hero.Played = 0;
                    hero.Win = 0;
                    await App.HeroRepository.UpdateHeroAsync(hero);
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                });
            }
        }

        public Command DeleteHero
        {
            get
            {
                return new Command(async () =>                 
                {
                    var answer = await Application.Current.MainPage.DisplayAlert("Deleting", "Do you want to delete this hero?", "Yes", "No");
                    if (answer)
                    {
                        await App.HeroRepository.DeleteHeroAsync(HeroId);
                        await Application.Current.MainPage.Navigation.PopToRootAsync();
                    }
                });
            }
        }
    }
}
