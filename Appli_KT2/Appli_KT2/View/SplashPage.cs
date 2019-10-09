﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Appli_KT2.View
{
	public class SplashPage : ContentPage
	{
        Image splashImage;
		public SplashPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "ic_sices.png",
                WidthRequest = 300,
                HeightRequest = 300
            };
            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);

            // this.BackgroundColor = Color.FromHEx("#");
            this.BackgroundColor = Color.White;
            this.Content = sub;
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await splashImage.ScaleTo(1,2000);
            await splashImage.ScaleTo(0.5,1500, Easing.Linear);
            //await splashImage.ScaleTo(150,1200, Easing.Linear);
            Application.Current.MainPage = new NavigationPage(new MainTabbedPage());

        }

    }
}