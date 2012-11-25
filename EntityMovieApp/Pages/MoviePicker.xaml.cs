using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EntityMovieApp.Classes;
using HelloWorld.DB;
using Helpers;
using SmartFormat;
using WPFPageSwitch;

namespace EntityMovieApp.Pages
{
    /// <summary>
    /// Interaction logic for MoviePicker.xaml
    /// </summary>
    public partial class MoviePicker : UserControl, ISwitchable
    {
        #region Properties
        
        public CommandParameter Parameter { get; set; }

        private List<string> _actors;
        private List<string> Actors
        {
            get { return _actors ?? (_actors = Person.GetAllActors().Select( person => person.FirstName + " " + person.LastName).ToList()); }
        }

        private List<string> _directors;
        private List<string> Directors
        {
            get { return _directors ?? (_directors = Person.GetAllDirectors().Select(person => person.FirstName + " " + person.LastName).ToList()); }
        }


        private List<string> _writers;
        private List<string> Writers
        {
            get { return _writers ?? (_writers = Person.GetAllWriters().Select(person => person.FirstName + " " + person.LastName).ToList()); }
        }

        private List<CheckedListItem> _ratings;
        private List<CheckedListItem> Ratings
        {
            get { return _ratings ?? (_ratings = FillRatingseDropDown()); }
        }

        private string selectedGenres = string.Empty;
        private string selectedRatings = string.Empty;

        private List<string> _titles;
        private List<string> Titles
        {
            get { return _titles ?? (_titles = MovieAppHelpers.RetrieveAllTitles()); }
        }

        private List<CheckedListItem> _availableGenres;
        public List<CheckedListItem> AvailableGenres
        {
            get { return _availableGenres ?? (_availableGenres =  FillGenreDropDown()); }
        }

        #endregion
        
        #region Initialize and Setup
        
        public MoviePicker()
        {
            InitializeComponent();

            // pass an int and if not null or 0 put "try x" in title?
            Window _mainWindow = Application.Current.MainWindow;
            _mainWindow.Title = "Movie Picker";

            //Parameter = new CommandParameter { CanEditBeExecuted = true };
            SetupForm();
            //ActorBox.Populating += ActorBox_Populating_1;
        }

        private void SetupForm()
        {
            // Fill genre
            //GenreDropDown.ItemsSource = FillGenreDropDown();
            //FillGenreDropDown();
            
            // Fill length - math!!!!
            LengthDropDown.ItemsSource = FillLengthDropDown();

            // Fill director - ajax too?
            DirectorsBox.ItemsSource = Directors;
            DirectorsBox.PopulateComplete();

            WritersBox.ItemsSource = Writers;
            WritersBox.PopulateComplete();
            
            // Actors
            ActorBox.ItemsSource = Actors;
            ActorBox.PopulateComplete();
            
            // Titles
            TitleBox.ItemsSource = Titles;
            TitleBox.PopulateComplete();

            RatingsDropDown.SelectionChanged += (cbSender, cbe) =>
            {
                ComboBox cb = cbSender as ComboBox;

                if (cb.SelectedItem is CheckedListItem && ((CheckedListItem)cb.SelectedItem).Selectable == false) cb.SelectedIndex = -1;
            };

            RatingsDropDown.ItemsSource = Ratings;
            

            cmb.SelectionChanged += (cbSender, cbe) =>
            {
                ComboBox cb = cbSender as ComboBox;

                if (cb.SelectedItem is CheckedListItem && ((CheckedListItem)cb.SelectedItem).Selectable == false) cb.SelectedIndex = -1;
            };

            cmb.ItemsSource = AvailableGenres;
        }

        private IEnumerable FillLengthDropDown()
        {
            return MovieAppHelpers.GetLengthsInRanges();
        }

        private List<CheckedListItem> FillGenreDropDown()
        {
            int i = 0;
            return MovieAppHelpers.GetAllGenreNames().Select(genre => new CheckedListItem()
            {
                Id = i++,
                IsChecked = false,
                Name = genre
            }).ToList();
        }

        private List<CheckedListItem> FillRatingseDropDown()
        {
            int i = 0;
            return MovieAppHelpers.GetAllRatings().Select(rating => new CheckedListItem()
            {
                Id = i++,
                IsChecked = false,
                Name = rating
            }).ToList();
        }
        
        private IEnumerable FillDirectorDropDown()
        {
            return
                Person.GetAllDirectors().Select(person => person.FirstName + " " + person.LastName).ToList();
        }

        #endregion

        private void SearchButtonClick(object sender, RoutedEventArgs e)
        {
            QueryBuilder();
        }

        private void QueryBuilder()
        {
            //Title
            Tuple<string, string> actorName = ActorBox.Text.ToString().GetTupleFromName();
            Tuple<string, string> directorName = DirectorsBox.Text.ToString().GetTupleFromName();
            Tuple<string, string> writerName = WritersBox.Text.ToString().GetTupleFromName();

            //string genre = (GenreDropDown.SelectedIndex == -1) ? string.Empty : GenreDropDown.SelectedValue.ToString();
            //List<CheckedListItem> selected = cmb.ItemsSource.Cast<CheckedListItem>().ToList();
            List<string> selectedGenreList = ( selectedGenres == string.Empty) ? new List<string>() : selectedGenres.Split(new[] {','}).ToList() ;
            List<string> selectedRatingsList = (selectedRatings == string.Empty) ? new List<string>() : selectedRatings.Split(new[] { ',' }).ToList();

          Tuple<int, int> length = (LengthDropDown.SelectedIndex == -1)
                                                ? new Tuple<int, int>(0, MovieAppHelpers.GetMaxMovieLength())
                                                : ( Tuple<int, int> ) LengthDropDown.SelectedValue;

            List<Film> results = (List<Film>) SearchHelpers.RunSearch(paramActorFirst: actorName.Item1, paramActorLast: actorName.Item2, 
                                                                paramRatings: selectedRatingsList, paramGenres: selectedGenreList, 
                                                                paramMin: length.Item1, paramMax: length.Item2, 
                                                                paramDirectorFirst: directorName.Item1, paramDirectorLast: directorName.Item2,
                                                                paramWriterFirst: writerName.Item1, paramWriterLast: writerName.Item2
                                                                );

            if (results.Any())
                new Results(results).Switch();
            else
                MessageBox.Show("Your search yielded no results", "No results", MessageBoxButton.OK, MessageBoxImage.Error);
            
        }

        //public AutoCompleteFilterPredicate<MoviePicker> ClientFilter
        //{
        //    get { return (search, item) => item.Titles.Contains(TitleBox.Text); }
        //}
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void cmb_DropDownClosed(object sender, EventArgs e)
        {
            List<CheckedListItem> selected = cmb.ItemsSource.Cast<CheckedListItem>().ToList();
            List<string> genres = selected.Where(item => item.IsChecked).Select(o => o.Name).ToList();
            selectedGenres = string.Join(", ", genres);
            selectedGenreLabel.Content = selectedGenres;
        }
        
        private void RatingsDropDown_DropDownClosed(object sender, EventArgs e)
        {
            List<CheckedListItem> selected = RatingsDropDown.ItemsSource.Cast<CheckedListItem>().ToList();
            List<string> ratings = selected.Where(item => item.IsChecked).Select(o => o.Name).ToList();
            selectedRatings = string.Join(", ", ratings);
            selectedRatingLabel.Content = selectedRatings;
        }

        private void moviePicker_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetPhillipForegroundColors();
        }

        private void LoginTextBlockMouseLeftButtonDown1(object sender, MouseButtonEventArgs e)
        {
            Switcher.Switch(new MainWindow());
        }


    }
}
