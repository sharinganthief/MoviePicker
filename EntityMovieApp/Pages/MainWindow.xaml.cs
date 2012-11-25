using System.Collections.Generic;
using System.Windows;
using EntityMovieApp.Classes;
using HelloWorld.DB;
using WPFPageSwitch;

namespace EntityMovieApp.Pages
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ISwitchable
    {


        public List<Film> Results { get; set; }
        public List<Film> Maybes { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            //Helpers.MovieAppHelpers.FixMoviesInDB();
            //Helpers.WPFHelpers.SetPhillipBackgroundColors(Application.Current.MainWindow);
            //Helpers.WPFHelpers.SetBackground(this, SystemColors.DesktopBrushKey);
            //Helpers.WPFHelpers.SetPhillipForegroundColors(Application.Current.MainWindow);
            //this.Background = new SolidColorBrush(Colors.Black);
        }

        private void Button1Click(object sender, RoutedEventArgs e)
        {
            new ManageCollection().Switch();
        }

        private void Button2Click(object sender, RoutedEventArgs e)
        {
            new Netflix().Switch();
        }

        private void Button3Click(object sender, RoutedEventArgs e)
        {
            //WPFHelpers.SwitchToNewWindow<MoviePicker>();
            new MoviePicker().Switch();
        }

        public void UtilizeState(object state)
        {
            throw new System.NotImplementedException();
        }

        private void mainMenuLayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
    public class CommandParameter
    {
        public bool CanEditBeExecuted { get; set; }
    }
}
