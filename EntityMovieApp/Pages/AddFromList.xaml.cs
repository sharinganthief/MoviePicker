using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using EntityMovieApp.Classes;
using Helpers;
using WPFPageSwitch;
using MessageBox = System.Windows.MessageBox;
using MessageBoxOptions = System.Windows.MessageBoxOptions;

namespace EntityMovieApp.Pages
{
    /// <summary>
    /// Interaction logic for AddFromList.xaml
    /// </summary>
    public partial class AddFromList : ISwitchable
    {
        public List<string> MoviesToAdd { get; set; }
        //public List<IMDB> processedMovies = new List<IMDB>();
        private int _moviesInsertedSuccessfully;
        private int _movieCount;


    //Stores the value of the ProgressBar

        private delegate void UpdateProgressBarDelegate(
    DependencyProperty dp, Object value);

    //Create a new instance of our ProgressBar Delegate that points
    // to the ProgressBar's SetValue method.
        private readonly UpdateProgressBarDelegate _updatePbDelegate;

        //private UpdateProgressBarDelegate UpdatePbDelegate { get; set; }

        public AddFromList()
        {
            InitializeComponent();

            _updatePbDelegate = progressBar.SetValue;
            progressBar.Visibility = Visibility.Hidden;
            
            
        }

        private void ButtonClick1(object sender, RoutedEventArgs e)
        {
            _moviesInsertedSuccessfully = 0;
            MoviesToAdd = listOfMovies.Text.Split(new[] { "\r\n", "\n", Environment.NewLine }, StringSplitOptions.None).ToList();
            progressBar.Minimum = 0;
            progressBar.Maximum = MoviesToAdd.Count;
            progressBar.Value = 0;
            progressBar.Visibility = Visibility.Visible;

            Stopwatch timer = new Stopwatch();
            timer.Start();
            foreach (string movieToRetrieve in MoviesToAdd)
            {

                _movieCount++;

                if (!string.IsNullOrWhiteSpace(movieToRetrieve))
                {
                    IMDB Movie;
                    if (movieToRetrieve.Contains(@"http://www.imdb.com/title"))
                    {
                        Movie = new IMDB(movieToRetrieve, true);
                        AddMovie(Movie);
                    }
                    else
                    {
                        if (!MovieAppHelpers.CheckDBForMovie( movieToRetrieve ))
                        {
                            try
                            {
                                Movie = new IMDB(movieToRetrieve);

                                if (Movie.Title.ToLower() != movieToRetrieve.ToLower())
                                {
                                    string message = string.Format(
                                        "Retrieved title [ {1} ] did not match entered title [ {2} ], add anyway?{0}{0}[ Title: {1}, Year: {3}, Lead: {4} ]",
                                            Environment.NewLine, Movie.Title, movieToRetrieve, Movie.Year ?? string.Empty,
                                            (Movie.Cast != null) ? Movie.Cast.ToArray().FirstOrDefault() : string.Empty);
                                    
                                    MessageBoxResult msgBoxResult = MessageBox.Show(message, "Ambiguous Match Found", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly);

                                    if (msgBoxResult == MessageBoxResult.Yes) AddMovie(Movie);

                                    if (msgBoxResult == MessageBoxResult.No)
                                    {
                                        Log.Trace(
                                            "Retrieved Movie: [ Title: {0}, Year: {1}, Lead: {2} ] Entered Movie: [ Title: {3} ]",
                                            new object[]
                                                {
                                                    Movie.Title, Movie.Year ?? string.Empty,
                                                    (Movie.Cast != null)
                                                        ? Movie.Cast.ToArray().FirstOrDefault()
                                                        : string.Empty, movieToRetrieve
                                                });
                                        Dispatcher.Invoke(_updatePbDelegate,
                                                          System.Windows.Threading.DispatcherPriority.Background,
                                                          new object[]
                                                              {
                                                                  RangeBase.ValueProperty, (double) _movieCount
                                                              });
                                    }
                                
                            
                        }
                                else // Titles Match
                                {
                                    AddMovie(Movie);
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Trace("Could not complete movie retrieval and insert", ex, movieToRetrieve);
                                //Log.Error("Could not complete movie insert, exception: [ {0} ]", ex.ToString());
                            }
                        }
                    }
                }
            }

            //Switcher.Switch(new AddResults( processedMovies));
            timer.Stop();
            "Inserted {0} of {1} movies successfully, Total run time: {2}, (see log for any failures)".ShowMessage(new object[] { _moviesInsertedSuccessfully, MoviesToAdd.Count, timer.Elapsed.ToReadableString() });
            
        }

        public void AddMovie(IMDB movie)
        {
            try
            {
                //progressLabel.Content = string.Format("Adding movie [ {0} ]...", Movie.Title);
                bool inserted = movie.AddMovieToCollection();
                if (inserted) _moviesInsertedSuccessfully++;
                movie.Inserted = inserted;
                //processedMovies.Add( movie );
                //Film moviePulledFromDB = movieToRetrieve.ReadMovieFromCollection();
                Dispatcher.Invoke(_updatePbDelegate,
                System.Windows.Threading.DispatcherPriority.Background, new object[]
                {
                    RangeBase.ValueProperty, (double) _movieCount
                });
                
            }
            catch (Exception ex)
            {
                Log.Trace("Could not complete movie insert", movie.Title);
                Log.Error(ex);
                ex.ThrowFormattedException();

            }           
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void LoginTextBlockMouseLeftButtonDown1(object sender, MouseButtonEventArgs e)
        {
            new MainWindow().Switch();
        }
    }
}


