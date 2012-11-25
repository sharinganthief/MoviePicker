using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HelloWorld.DB;
using Helpers;

namespace EntityMovieApp
{
    /// <summary>
    /// Interaction logic for MoviePicker.xaml
    /// </summary>
    public partial class MoviePicker
    {
        

        #region Properties
        
        public CommandParameter Parameter { get; set; }

        private List<string> _actors;
        private List<string> Actors
        {
            get { return _actors ?? (_actors = MovieAppHelpers.GetAllActorNames()); }
        }
        
        private List<string> _titles;
        private List<string> Titles
        {
            get { return _titles ?? (_titles = MovieAppHelpers.RetrieveAllTitles()); }
        }

        #endregion


        #region Initialize and Setup

        public MoviePicker2()
        {
            Parameter = new CommandParameter {CanEditBeExecuted = true};
            InitializeComponent();
            
            SetupForm();
        }

        private void SetupForm()
        {
            //fill genre
            GenreDropDown.ItemsSource = FillGenreDropDown();
            //fill length - math!!!!
            LengthDropDown.ItemsSource = FillLengthDropDown();

            //fill director - ajax too?
            DirectorDropDown.ItemsSource = FillDirectorDropDown();
            //actors
        }

        private IEnumerable FillLengthDropDown()
        {
            return MovieAppHelpers.GetLengthsInRanges();
        }

        private IEnumerable FillGenreDropDown()
        {
            return MovieAppHelpers.GetAllGenreNames();
        }
        
        private IEnumerable FillDirectorDropDown()
        {
            return 
                MovieAppHelpers.GetAllDirectorNames();
        }

        #endregion

        private void SearchButtonClick(object sender, RoutedEventArgs e)
        {
            QueryBuilder();
        }

        private void QueryBuilder()
        {
            //Title

            string actorFirst = ActorBox.Text.ToString().Split(' ').FirstOrDefault().Trim();
            string actorLast = ActorBox.Text.ToString().Split(' ').LastOrDefault().Trim();
            
            string genre = (GenreDropDown.SelectedIndex == -1) ? string.Empty : GenreDropDown.SelectedValue.ToString();

            string director = (DirectorDropDown.SelectedIndex == -1) ? string.Empty : DirectorDropDown.SelectedValue.ToString();

            string directorFirst = (DirectorDropDown.SelectedIndex == -1) ? string.Empty : DirectorDropDown.SelectedValue.ToString().Split(' ').FirstOrDefault().Trim();
            string directorLast = (DirectorDropDown.SelectedIndex == -1) ? string.Empty : DirectorDropDown.SelectedValue.ToString().Split(' ').LastOrDefault().Trim();

            Tuple<int, int> length = (LengthDropDown.SelectedIndex == -1)
                                                ? new Tuple<int, int>(0, MovieAppHelpers.GetMaxMovieLength())
                                                : ( Tuple<int, int> ) LengthDropDown.SelectedValue;

            List<Film> results = SearchHelpers.RunSearch(paramActorFirst: actorFirst, paramActorLast: actorLast, paramGenre: genre, 
                                                         paramMin: length.Item1, paramMax: length.Item2, 
                                                         paramDirectorFirst: directorFirst, paramDirectorLast: directorLast);
            WPFHelpers.SwitchToNewWindow<Results>(new object[]{results});
            
        }

        private void TitleBoxPopulating(object sender, PopulatingEventArgs e)
        {
            TitleBox.ItemsSource = Titles;
            TitleBox.PopulateComplete();
        }

        private void ActorBoxPopulating(object sender, PopulatingEventArgs e)
        {
            ActorBox.ItemsSource = Actors;
            ActorBox.PopulateComplete();
        }

        //public AutoCompleteFilterPredicate<MoviePicker> ClientFilter
        //{
        //    get { return (search, item) => item.Titles.Contains(TitleBox.Text); }
        //}
    }
}


#region Archived Code


//if (!string.IsNullOrWhiteSpace(actor))
//{
//    query+="PersonFilmIndexes.Person.Name"
//}

//Tuple<int, int> lengthRange = (Tuple<int, int>)LengthDropDown.SelectedValue;

//List<Film> results = SearchHelpers.RunSearch(paramGenre: "Action");
// = "Length > @0 and Length < @1".RunSearch(x.Item1.ToIntSafe(), x.Item2.ToIntSafe());

//Window ResultsWindow = new Results(results);
//Application.Current.MainWindow = ResultsWindow;
//Close();
//ResultsWindow.Show();

//movie.Cast = new ArrayList{"Kevin Spacey","Matthew Broderick"};
//movie.Genres = new ArrayList { "Drama", "Sci-Fi" };
//movie.Title = "TestMovie";


//add title if exists
//add actor if exists
//add genre if exists
//add length if exists
//add director if exists

#endregion