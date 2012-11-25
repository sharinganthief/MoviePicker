using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloWorld.DB
{
    public partial class Person 
    {
        public static IList<Person> GetAllActors()
        {
            using (MovieCollectionEntities entity = new MovieCollectionEntities())
            {
                return
                    entity.PersonFilmIndexes.Where(pfi => pfi.RoleID == FilmRole.ActorRole.RoleID).Select(
                        pfi => pfi.Person).Distinct().ToList();
            }
        }

        public static IList<Person> GetAllDirectors()
        {
            using (MovieCollectionEntities entity = new MovieCollectionEntities())
            {
                return
                    entity.PersonFilmIndexes.Where(pfi => pfi.RoleID == FilmRole.DirectorRole.RoleID).Select(
                        pfi => pfi.Person).Distinct().ToList();
            }
        }

        public static IList<Person> GetAllWriters()
        {
            using (MovieCollectionEntities entity = new MovieCollectionEntities())
            {
                return
                    entity.PersonFilmIndexes.Where(pfi => pfi.RoleID == FilmRole.WriterRole.RoleID).Select(
                        pfi => pfi.Person).Distinct().ToList();
            }
        }
    }
}
