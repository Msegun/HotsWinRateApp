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
	public partial class HeroDetailsPage : ContentPage
	{
		public HeroDetailsPage (HeroDetailsViewModel viewModel)
		{
			InitializeComponent ();
            BindingContext = viewModel;
		}

        //Mode dodam On Appearing
	}
}