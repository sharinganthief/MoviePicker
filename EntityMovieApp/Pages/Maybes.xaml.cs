using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HelloWorld.DB;
using Helpers;
using SmartFormat;

namespace EntityMovieApp.Pages
{
    /// <summary>
    /// Interaction logic for Maybes.xaml
    /// </summary>
    public partial class Maybes
    {
        private Window _mainWindow { get; set; }
        private int _maybeCount { get; set; }

        public Maybes(ICollection<Film> maybes )
        {
            InitializeComponent();
            this.SetPhillipBackgroundColors();
            //_mainWindow = Application.Current.MainWindow;
            _mainWindow = Application.Current.Windows[1];
            _maybeCount = maybes.Count;
            if (_mainWindow != null) _mainWindow.Title = Smart.Format("{0} Maybe{0:|s} selected", _maybeCount);
            ShowMaybes(maybes);
        }

        public void ShowMaybes(ICollection<Film> maybes)
        {
            foreach ( TreeViewItem node in maybes.GetTreeItems())
            {
                node.IsExpanded = false;
                maybeTree.Items.Add( node );
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = maybeTree.SelectedItem as TreeViewItem;
            if (item != null)
            {
                maybeTree.Items.Remove(item);
                maybeTree.Items.Refresh();
                _maybeCount--;
                _mainWindow.Title = Smart.Format("{0}: Maybe{0|s} selected", _maybeCount);

            }

        }
    }
}
