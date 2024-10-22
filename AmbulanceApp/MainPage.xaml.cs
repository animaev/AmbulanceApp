﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AmbulanceApp
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnCall_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CallPage());
        }

        private void btnEmpl_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EmplPage());
        }

        private void btnMachine_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new MachinePage());
        }
    }
}
