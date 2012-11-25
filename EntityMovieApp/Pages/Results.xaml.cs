using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using EntityMovieApp.Classes;
using HelloWorld.DB;
using Helpers;
using SmartFormat;
using WPFPageSwitch;

namespace EntityMovieApp.Pages
{
    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class Results : ISwitchable
    {
        private readonly List<Film> _films;
        
        private readonly List<Film> _maybes = new List<Film>();

        private Film _currentlyShowing;
        private readonly Window _mainWindow;

        public Results(IEnumerable<Film> results )
        {
            InitializeComponent();
            _films = results.ToList();
            _mainWindow = Application.Current.MainWindow;
            _mainWindow.Title = Smart.Format("{0} {0:result|results} left", _films.Count());
            ShowMovie();

        }

        public Results()
        {
            InitializeComponent();
            ShowMovie();
        }

        private void ShowMovie()
        {
            
                _currentlyShowing = _films.First();
                resultTree.Items.Add( _currentlyShowing.GetTreeItem());

                foreach (TreeViewItem tn in from TreeViewItem root in resultTree.Items 
                                            from TreeViewItem tn in root.Items 
                                            where (tn.Header.ToString().Contains("QuickInfo")) 
                                            || (tn.Header.ToString().Contains("Plot")) select tn)
                {
                    tn.ExpandSubtree();
                }


                
        }

        private void NoButtonClicked(object sender, RoutedEventArgs e) //NO
        {
            
            _films.Remove(_currentlyShowing);
            if (!CheckForLast())
            ShowMovie();
            _mainWindow.Title = Smart.Format("{0} {0:result|results} left", _films.Count());
            
        }

        private bool CheckForLast()
        {
            resultTree.Items.Clear();
            
            if (!_films.Any())
            {
                maybeButton.IsEnabled = false;
                noButton.IsEnabled = false;
                ShowMaybesButtonClicked(new object(), new RoutedEventArgs());
                return true;
            }
            return false;
        }

        private void MaybeButtonClicked(object sender, RoutedEventArgs e)
        {
            
            showMaybeButton.IsEnabled = true;
            _maybes.Add(_currentlyShowing);
            _films.Remove(_currentlyShowing);
            if (!CheckForLast())
                ShowMovie();
            _mainWindow.Title = Smart.Format("{0} {0:result|results} left", _films.Count());
        }

        private void ShowMaybesButtonClicked(object sender, RoutedEventArgs e)
        {
            Window maybesWindow = new Maybes(_maybes);
            maybesWindow.ShowDialog();

        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void LoginTextBlockMouseLeftButtonDown1(object sender, MouseButtonEventArgs e)
        {
            new MoviePicker().Switch();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            resultTree.Background = new SolidColorBrush(Colors.Black);
        }
    }
}
