﻿using Microsoft.Maui.Controls;

namespace dtMauiAPp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}