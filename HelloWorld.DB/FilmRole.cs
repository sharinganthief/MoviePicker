using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace HelloWorld.DB
{
    public partial class FilmRole
    {
        public static FilmRole _actorRole;

        public static FilmRole ActorRole
        {
            get
            {
                if (_actorRole != null)
                {
                    return _actorRole;
                }

                string s = RoleNames.Actor.ToString();
                using (MovieCollectionEntities entity = new MovieCollectionEntities())
                {
                    _actorRole = (entity.FilmRoles.FirstOrDefault(r => r.RoleName == s));
                }
                return _actorRole;

            }
        }

        public static FilmRole _directorRole;

        public static FilmRole DirectorRole
        {
            get
            {
                if (_directorRole != null)
                {
                    return _directorRole;
                }

                string s = RoleNames.Director.ToString();
                using (MovieCollectionEntities entity = new MovieCollectionEntities())
                {
                    _directorRole = (entity.FilmRoles.FirstOrDefault(r => r.RoleName == s));
                }
                return _directorRole;

            }
        }

        public static FilmRole _writerRole;

        public static FilmRole WriterRole
        {
            get
            {
                if (_writerRole != null)
                {
                    return _writerRole;
                }

                string s = RoleNames.Writer.ToString();
                using (MovieCollectionEntities entity = new MovieCollectionEntities())
                {
                    _writerRole = (entity.FilmRoles.FirstOrDefault(r => r.RoleName == s));
                }
                return _writerRole;

            }
        }
    }
}
