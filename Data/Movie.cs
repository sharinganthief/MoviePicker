using System.Collections.Generic;
using Database.Types.GeneratedTypes.BaseTypes;

namespace Data
{
    public class Movie
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Runtime { get; set; }
        public string ReleaseYear { get; set; }
        public List<Person> People { get; set; }
        public IList<Genre> Genres { get; set; }

    }
}
