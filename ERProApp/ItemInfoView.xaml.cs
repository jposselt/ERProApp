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
using System.Windows.Shapes;

namespace ERProApp
{
    /// <summary>
    /// Interaction logic for ItemInfoView.xaml
    /// </summary>
    public partial class ItemInfoView : Window
    {
        public ItemInfoView()
        {
            App.Log.Debug("Initialisiere Buchinformationsfenster");
            InitializeComponent();
        }

        private void Button_OkClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
