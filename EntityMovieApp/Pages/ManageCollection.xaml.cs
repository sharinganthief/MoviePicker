using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EntityMovieApp.Classes;
using HelloWorld.DB;
using Helpers;
using WPFPageSwitch;

namespace EntityMovieApp.Pages
{
    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class ManageCollection : UserControl, ISwitchable
    {

        public CommandParameter Parameter { get; set; }
        public IMDB Movie { get; set; }

        public ManageCollection()
        {
            Parameter = new CommandParameter {CanEditBeExecuted = true};
            InitializeComponent();
        }

        public IMDB GetIMDBMovie(string titleToSearchOn)
        {
            return new IMDB(titleToSearchOn);
        }

        private void ButtonClick1(object sender, RoutedEventArgs e)
        {
            //Find Movie Clicked
            string movieToRetrieve = TitleToSearch.Text;
            if (!string.IsNullOrWhiteSpace(movieToRetrieve))
            {
                Movie = GetIMDBMovie(movieToRetrieve);
                retrievedTitle.Content = Movie.Title;
                retreievedLeadActor.Content = Movie.Cast.ToArray().ToList().First();
                retrievedYear.Content = Movie.Year;
            }
            else
                "You have to enter a title to search for".ShowMessage();
        }

        private void ButtonClick2(object sender, RoutedEventArgs e)
        {
            try
            {
                bool inserted = Movie.AddMovieToCollection();

                Film moviePulledFromDB = Movie.Title.ReadMovieFromCollection();

                if (inserted)
                    "Movie successfully inserted: [ Title: {0} ] ".ShowMessage(new object[] { moviePulledFromDB.Title });
            }
            catch (Exception ex)
            {
                ex.ThrowFormattedException();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new AddFromList().Switch();
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void LoginTextBlockMouseLeftButtonDown1(object sender, MouseButtonEventArgs e)
        {
            new MainWindow().Switch();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new DeleteFromCollection().Switch();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new FixFromList().Switch();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            
            new AddFromListofURLs().Switch();
        }
    }
}

