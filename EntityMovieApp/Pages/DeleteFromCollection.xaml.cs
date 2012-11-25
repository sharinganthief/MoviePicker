using System;
using System.Collections.Generic;
using System.Windows;
using Helpers;
using WPFPageSwitch;

namespace EntityMovieApp.Pages
{
    /// <summary>
    /// Interaction logic for DeleteFromCollection.xaml
    /// </summary>
    public partial class DeleteFromCollection : ISwitchable
    {
        private List<string> _titles;
        private List<string> Titles
        {
            get { return _titles ?? (_titles = MovieAppHelpers.RetrieveAllTitles()); }
        }

        public DeleteFromCollection()
        {
            InitializeComponent();
            TitleBox.ItemsSource = Titles;
            TitleBox.PopulateComplete();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string movieToDelete = TitleBox.Text;
            if (!string.IsNullOrWhiteSpace(movieToDelete))
            {
                bool deleted = movieToDelete.RemoveMovieFomCollection();
                if (deleted)
                {
                    TitleBox.Text = string.Empty;
                    MessageBox.Show(string.Format("Successfully deleted: {0}", movieToDelete), "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(string.Format("Error while deleting: {0}", movieToDelete), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
