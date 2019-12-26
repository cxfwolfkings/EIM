using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Bow.Enter.Views
{
    public class AccountPageCS : ContentPage
    {
        public AccountPageCS()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Welcome to Account Page!" }
                }
            };
        }
    }
}