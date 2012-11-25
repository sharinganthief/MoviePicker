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
    public class Film : BusinessBaseType<DB.Film, DB.MovieCollectionEntities>
    {
        public class Where
        {
			public bool FilmIdSet;
			public bool LengthSet;
			public bool YearSet;
			public bool TitleSet;

			private System.Nullable<global::System.Int32> _FilmId;
			private System.Nullable<global::System.Int32> _Length;
			private System.Nullable<global::System.Int32> _Year;
			private global::System.String _Title;


			public System.Nullable<global::System.Int32> FilmId { get { return _FilmId; } set { _FilmId = value; FilmIdSet = true; } }
			public System.Nullable<global::System.Int32> Length { get { return _Length; } set { _Length = value; LengthSet = true; } }
			public System.Nullable<global::System.Int32> Year { get { return _Year; } set { _Year = value; YearSet = true; } }
			public global::System.String Title { get { return _Title; } set { _Title = value; TitleSet = true; } }


            public string MyWhere = "", MyOrderBy = "", MyTop = "";

            public Where()
            {
				FilmId = null;
				Length = null;
				Year = null;
				Title = null;

				FilmIdSet = false;
				LengthSet = false;
				YearSet = false;
				TitleSet = false;

            }
        };

		public global::System.Int32 FilmId { get; set; }
		public global::System.Int32 Length { get; set; }
		public global::System.Int32 Year { get; set; }
		public global::System.String Title { get; set; }

        public Where WhereClause { get; set; }

        #region Constructors

        public Film()
        {
            WhereClause = new Where();
        }

        public Film(System.Nullable<global::System.Int32> _FilmId, global::System.Int32 _Length, global::System.Int32 _Year, global::System.String _Title)
            : this()
        {
            if (_FilmId != null)
            {
                FilmId = (global::System.Int32)_FilmId;
            }

			Length = _Length;
			Year = _Year;
			Title = _Title;

        }

        public Film(DB.Film filmLinq)
            : this()
        {
            Film film = new Film();
            Converter.ConvertToBusinessObject<Film>(film, filmLinq);

            Initialize(film);
        }

        #endregion

        private void Initialize(Film film)
        {
			FilmId = film.FilmId;
			Length = film.Length;
			Year = film.Year;
			Title = film.Title;

            OnInitializeComplete();
        }

        #region Linq Helpers

        public bool Save()
        {
			OnSave();
			
            if (Save<global::System.Int32, Film>(this, FilmId, false))
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
			
            if (Save<global::System.Int32, Film>(this, FilmId, true))
            {
				OnSaveComplete();			
				return true;
			}
			
			return false;
		}
		
        public bool Load(global::System.Int32 _FilmId)
        {
            DB.Film filmLinq = Load<global::System.Int32>(_FilmId);
            
            if (filmLinq == null)
            {
				return false;
            }
            
            Film film = new Film();
            Converter.ConvertToBusinessObject<Film>(film, filmLinq);
            
            Initialize(film);
            
            return true;
        }

        public bool Delete()
        {
	    OnDelete();

            DB.Film filmLinq = Delete<global::System.Int32>(FilmId);

			if (filmLinq == null)
			{
				return false;
			}
			
            Film film = new Film();
            Converter.ConvertToBusinessObject<Film>(film, filmLinq);

            Initialize(film);
            
	    OnDeleteComplete();

            return true;
        }

        public List<Film> Find()
        {
            List<DB.Film> listLinq = Find(WhereClause);
            
            List<Film> list = new List<Film>();
            foreach (DB.Film film in listLinq)
            {
                list.Add(new Film(film));
            }

            return list;
        }

        public new List<Film> ToList()
        {
            List<DB.Film> listLinq = base.ToList();

            List<Film> list = new List<Film>();
            foreach (DB.Film film in listLinq)
            {
                list.Add(new Film(film));
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
			WhereClause.FilmId = FilmId;
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
			WhereClause.FilmId = FilmId;
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
