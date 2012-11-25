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
        public Maybes(ICollection<Film> maybes )
        {
            InitializeComponent();
            this.SetPhillipBackgroundColors();
            Window _mainWindow = Application.Current.MainWindow;
            _mainWindow.Title = Smart.Format("{0} Maybe{0|s} selected", maybes.Count);
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
    }
}
