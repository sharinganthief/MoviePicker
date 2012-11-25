using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Helpers;
using WPFPageSwitch;

namespace EntityMovieApp.Pages
{
    /// <summary>
    /// Interaction logic for Analytics.xaml
    /// </summary>
    public partial class Analytics : ISwitchable
    {
        public Analytics()
        {
            InitializeComponent();
            SetUpChart();
        }

        private void SetUpChart()
        {
            genreChart.DataContext = MovieAppHelpers.MoviesByGenre();
            //actorChart.DataContext = MovieAppHelpers.MoviesByActor();
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
