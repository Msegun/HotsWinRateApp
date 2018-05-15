using HotsWinRate.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotsWinRate.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HeroPage : ContentPage
	{
		public HeroPage (HeroViewModel viewModel)
		{
			InitializeComponent ();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            var viewModel = this.BindingContext as HeroViewModel;
            if (viewModel == null)
                return;

            viewModel.RefreshHeroes.Execute(null);
        }
    }
}