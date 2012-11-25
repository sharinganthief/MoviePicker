using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HelloWorld.DB;
using Helpers;
using WPFPageSwitch;

namespace EntityMovieApp.Pages
{
    /// <summary>
    /// Interaction logic for AddResults.xaml
    /// </summary>
    public partial class AddResults : ISwitchable
    {
        //List<Film> addResults = new List<Film>();

        public AddResults(List<IMDB> films )
        {
            InitializeComponent();
            //resultsGrid.DataContext = films;
            FillGrid(films);
        }

        private void FillGrid(List<IMDB> films)
        {
            DataTable filmData = new DataTable();
            filmData.Columns.Add(new DataColumn("Title", typeof(string)));
            filmData.Columns.Add(new DataColumn("Year", typeof(string)));
            filmData.Columns.Add(new DataColumn("Runtime", typeof(string)));
            filmData.Columns.Add(new DataColumn("Inserted", typeof(Image)));
            
            foreach (IMDB film in films)
            {
                var row = filmData.NewRow();

                row["Game Name"] = film.Title;
                row["Creator"] = film.Year;
                row["Publisher"] = film.Runtime;
                //row["Inserted"] = ;

                filmData.Rows.Add(row);
                    
            }
            
            
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
