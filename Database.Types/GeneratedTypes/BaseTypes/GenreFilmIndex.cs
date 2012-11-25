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
    public class GenreFilmIndex : BusinessBaseType<DB.GenreFilmIndex, DB.MovieCollectionEntities>
    {
        public class Where
        {
			public bool IdSet;
			public bool GenreIdSet;
			public bool FilmIdSet;

			private System.Nullable<global::System.Int32> _Id;
			private System.Nullable<global::System.Int32> _GenreId;
			private System.Nullable<global::System.Int32> _FilmId;


			public System.Nullable<global::System.Int32> Id { get { return _Id; } set { _Id = value; IdSet = true; } }
			public System.Nullable<global::System.Int32> GenreId { get { return _GenreId; } set { _GenreId = value; GenreIdSet = true; } }
			public System.Nullable<global::System.Int32> FilmId { get { return _FilmId; } set { _FilmId = value; FilmIdSet = true; } }


            public string MyWhere = "", MyOrderBy = "", MyTop = "";

            public Where()
            {
				Id = null;
				GenreId = null;
				FilmId = null;

				IdSet = false;
				GenreIdSet = false;
				FilmIdSet = false;

            }
        };

		public global::System.Int32 Id { get; set; }
		public global::System.Int32 GenreId { get; set; }
		public global::System.Int32 FilmId { get; set; }

        public Where WhereClause { get; set; }

        #region Constructors

        public GenreFilmIndex()
        {
            WhereClause = new Where();
        }

        public GenreFilmIndex(System.Nullable<global::System.Int32> _Id, global::System.Int32 _GenreId, global::System.Int32 _FilmId)
            : this()
        {
            if (_Id != null)
            {
                Id = (global::System.Int32)_Id;
            }

			GenreId = _GenreId;
			FilmId = _FilmId;

        }

        public GenreFilmIndex(DB.GenreFilmIndex genreFilmIndexLinq)
            : this()
        {
            GenreFilmIndex genreFilmIndex = new GenreFilmIndex();
            Converter.ConvertToBusinessObject<GenreFilmIndex>(genreFilmIndex, genreFilmIndexLinq);

            Initialize(genreFilmIndex);
        }

        #endregion

        private void Initialize(GenreFilmIndex genreFilmIndex)
        {
			Id = genreFilmIndex.Id;
			GenreId = genreFilmIndex.GenreId;
			FilmId = genreFilmIndex.FilmId;

            OnInitializeComplete();
        }

        #region Linq Helpers

        public bool Save()
        {
			OnSave();
			
            if (Save<global::System.Int32, GenreFilmIndex>(this, Id, false))
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
			
            if (Save<global::System.Int32, GenreFilmIndex>(this, Id, true))
            {
				OnSaveComplete();			
				return true;
			}
			
			return false;
		}
		
        public bool Load(global::System.Int32 _Id)
        {
            DB.GenreFilmIndex genreFilmIndexLinq = Load<global::System.Int32>(_Id);
            
            if (genreFilmIndexLinq == null)
            {
				return false;
            }
            
            GenreFilmIndex genreFilmIndex = new GenreFilmIndex();
            Converter.ConvertToBusinessObject<GenreFilmIndex>(genreFilmIndex, genreFilmIndexLinq);
            
            Initialize(genreFilmIndex);
            
            return true;
        }

        public bool Delete()
        {
	    OnDelete();

            DB.GenreFilmIndex genreFilmIndexLinq = Delete<global::System.Int32>(Id);

			if (genreFilmIndexLinq == null)
			{
				return false;
			}
			
            GenreFilmIndex genreFilmIndex = new GenreFilmIndex();
            Converter.ConvertToBusinessObject<GenreFilmIndex>(genreFilmIndex, genreFilmIndexLinq);

            Initialize(genreFilmIndex);
            
	    OnDeleteComplete();

            return true;
        }

        public List<GenreFilmIndex> Find()
        {
            List<DB.GenreFilmIndex> listLinq = Find(WhereClause);
            
            List<GenreFilmIndex> list = new List<GenreFilmIndex>();
            foreach (DB.GenreFilmIndex genreFilmIndex in listLinq)
            {
                list.Add(new GenreFilmIndex(genreFilmIndex));
            }

            return list;
        }

        public new List<GenreFilmIndex> ToList()
        {
            List<DB.GenreFilmIndex> listLinq = base.ToList();

            List<GenreFilmIndex> list = new List<GenreFilmIndex>();
            foreach (DB.GenreFilmIndex genreFilmIndex in listLinq)
            {
                list.Add(new GenreFilmIndex(genreFilmIndex));
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
			WhereClause.Id = Id;
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
			WhereClause.Id = Id;
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
