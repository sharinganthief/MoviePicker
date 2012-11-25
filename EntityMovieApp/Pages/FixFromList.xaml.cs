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
using WPFPageSwitch;

namespace EntityMovieApp.Pages
{
    /// <summary>
    /// Interaction logic for FixFromList.xaml
    /// </summary>
    public partial class FixFromList : ISwitchable
    {
        protected List<string> MoviesToAdd { get; set; }

        public FixFromList()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MoviesToAdd = fixList.Text.Split(new[] { "\r\n", "\n", Environment.NewLine }, StringSplitOptions.None).ToList();
            Helpers.MovieAppHelpers.FillNewFieldsInMovieTable( MoviesToAdd );
        }


        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
