using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityMovieApp
{
    public class CheckedListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public readonly bool Selectable = false;
    }
}
