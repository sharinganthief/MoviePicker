using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using DB = HelloWorld.DB;

/*
Primary Objects Linquify - Business Class Generator
Copyright (C) 2010 Kory Becker
http://www.primaryobjects.com

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.
*/

namespace Database.Types.GeneratedTypes.BaseTypes
{
    [Serializable]
    public class Genre : BusinessBaseType<DB.Genre, DB.MovieCollectionEntities>
    {
        public class Where
        {
			public bool GenreIdSet;
			public bool NameSet;

			private System.Nullable<global::System.Int32> _GenreId;
			private global::System.String _Name;


			public System.Nullable<global::System.Int32> GenreId { get { return _GenreId; } set { _GenreId = value; GenreIdSet = true; } }
			public global::System.String Name { get { return _Name; } set { _Name = value; NameSet = true; } }


            public string MyWhere = "", MyOrderBy = "", MyTop = "";

            public Where()
            {
				GenreId = null;
				Name = null;

				GenreIdSet = false;
				NameSet = false;

            }
        };

		public global::System.Int32 GenreId { get; set; }
		public global::System.String Name { get; set; }

        public Where WhereClause { get; set; }

        #region Constructors

        public Genre()
        {
            WhereClause = new Where();
        }

        public Genre(System.Nullable<global::System.Int32> _GenreId, global::System.String _Name)
            : this()
        {
            if (_GenreId != null)
            {
                GenreId = (global::System.Int32)_GenreId;
            }

			Name = _Name;

        }

        public Genre(DB.Genre genreLinq)
            : this()
        {
            Genre genre = new Genre();
            Converter.ConvertToBusinessObject<Genre>(genre, genreLinq);

            Initialize(genre);
        }

        #endregion

        private void Initialize(Genre genre)
        {
			GenreId = genre.GenreId;
			Name = genre.Name;

            OnInitializeComplete();
        }

        #region Linq Helpers

        public bool Save()
        {
			OnSave();
			
            if (Save<global::System.Int32, Genre>(this, GenreId, false))
            {
				OnSaveComplete();			
				return true;
			}
			
			return false;
        }

		public bool Insert()
		{
			// Allows you to force an Insert via the Save function, rather than an Update. Aka, explicit insert.
			// Useful for cases where you want to assign a primary key before inserting, rather than auto-assigning.
			OnSave();
			
            if (Save<global::System.Int32, Genre>(this, GenreId, true))
            {
				OnSaveComplete();			
				return true;
			}
			
			return false;
		}
		
        public bool Load(global::System.Int32 _GenreId)
        {
            DB.Genre genreLinq = Load<global::System.Int32>(_GenreId);
            
            if (genreLinq == null)
            {
				return false;
            }
            
            Genre genre = new Genre();
            Converter.ConvertToBusinessObject<Genre>(genre, genreLinq);
            
            Initialize(genre);
            
            return true;
        }

        public bool Delete()
        {
	    OnDelete();

            DB.Genre genreLinq = Delete<global::System.Int32>(GenreId);

			if (genreLinq == null)
			{
				return false;
			}
			
            Genre genre = new Genre();
            Converter.ConvertToBusinessObject<Genre>(genre, genreLinq);

            Initialize(genre);
            
	    OnDeleteComplete();

            return true;
        }

        public List<Genre> Find()
        {
            List<DB.Genre> listLinq = Find(WhereClause);
            
            List<Genre> list = new List<Genre>();
            foreach (DB.Genre genre in listLinq)
            {
                list.Add(new Genre(genre));
            }

            return list;
        }

        public new List<Genre> ToList()
        {
            List<DB.Genre> listLinq = base.ToList();

            List<Genre> list = new List<Genre>();
            foreach (DB.Genre genre in listLinq)
            {
                list.Add(new Genre(genre));
            }

            return list;
        }

        #endregion

        #region Overridables

        /// <summary>
        /// Fired after the object is created or loaded.
        /// </summary>
        protected virtual void OnInitializeComplete()
        {
			WhereClause.GenreId = GenreId;
        }

        /// <summary>
        /// Fired before the object is inserted or updated.
        /// </summary>
        protected virtual void OnSave()
        {
        }

        /// <summary>
        /// Fired after the object is inserted or updated.
        /// </summary>
        protected virtual void OnSaveComplete()
        {
			WhereClause.GenreId = GenreId;
        }

        /// <summary>
        /// Fired before the object is deleted.
        /// </summary>
	protected virtual void OnDelete()
	{
	}

        /// <summary>
        /// Fired after the object is deleted.
        /// </summary>
	protected virtual void OnDeleteComplete()
	{
	}

        #endregion
    }
}
