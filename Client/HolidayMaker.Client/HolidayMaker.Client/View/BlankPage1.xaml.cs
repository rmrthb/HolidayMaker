﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HolidayMaker.Client.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            this.InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string userEmail = EmailTextbox.Text;

            string password = PasswordTextbox.Password;

            string confirmPassword = ConfirmPasswordTextbox.Password;

            if(password == confirmPassword)
            {
                PasswordTextBlock.Text = "";
                ConfirmPasswordTextBlock.Text = "";
            }
            else
            {
                PasswordTextBlock.Text = "Passwords don't match";
                ConfirmPasswordTextBlock.Text = "Passwords don't match";
            }
        }
    }
}