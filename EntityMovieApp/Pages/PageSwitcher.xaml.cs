using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EntityMovieApp.Classes;
using Helpers;
using WPFPageSwitch;
using WPF.Themes;


namespace EntityMovieApp.Pages
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class PageSwitcher : Window
    {
        public PageSwitcher()
        {
            InitializeComponent();
            Window main = Application.Current.MainWindow;
            main.Title = "Movie App";
            this.SetPhillipBackgroundColors();
            //this.ApplyTheme("ExpressionDark");
            Switcher.pageSwitcher = this;
            new MainWindow().Switch();            
        }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextPage.Name.ToString());
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.SetPhillipForegroundColors();
        }
    }
}
