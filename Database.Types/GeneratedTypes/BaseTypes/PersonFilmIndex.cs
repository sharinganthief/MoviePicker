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
    public class PersonFilmIndex : BusinessBaseType<DB.PersonFilmIndex, DB.MovieCollectionEntities>
    {
        public class Where
        {
			public bool IdSet;
			public bool PersonIdSet;
			public bool FilmIdSet;
			public bool ActorSet;
			public bool DirectorSet;

			private System.Nullable<global::System.Int32> _Id;
			private System.Nullable<global::System.Int32> _PersonId;
			private System.Nullable<global::System.Int32> _FilmId;
			private System.Nullable<global::System.Boolean> _Actor;
			private System.Nullable<global::System.Boolean> _Director;


			public System.Nullable<global::System.Int32> Id { get { return _Id; } set { _Id = value; IdSet = true; } }
			public System.Nullable<global::System.Int32> PersonId { get { return _PersonId; } set { _PersonId = value; PersonIdSet = true; } }
			public System.Nullable<global::System.Int32> FilmId { get { return _FilmId; } set { _FilmId = value; FilmIdSet = true; } }
			public System.Nullable<global::System.Boolean> Actor { get { return _Actor; } set { _Actor = value; ActorSet = true; } }
			public System.Nullable<global::System.Boolean> Director { get { return _Director; } set { _Director = value; DirectorSet = true; } }


            public string MyWhere = "", MyOrderBy = "", MyTop = "";

            public Where()
            {
				Id = null;
				PersonId = null;
				FilmId = null;
				Actor = null;
				Director = null;

				IdSet = false;
				PersonIdSet = false;
				FilmIdSet = false;
				ActorSet = false;
				DirectorSet = false;

            }
        };

		public global::System.Int32 Id { get; set; }
		public global::System.Int32 PersonId { get; set; }
		public global::System.Int32 FilmId { get; set; }
		public global::System.Boolean Actor { get; set; }
		public global::System.Boolean Director { get; set; }

        public Where WhereClause { get; set; }

        #region Constructors

        public PersonFilmIndex()
        {
            WhereClause = new Where();
        }

        public PersonFilmIndex(System.Nullable<global::System.Int32> _Id, global::System.Int32 _PersonId, global::System.Int32 _FilmId, global::System.Boolean _Actor, global::System.Boolean _Director)
            : this()
        {
            if (_Id != null)
            {
                Id = (global::System.Int32)_Id;
            }

			PersonId = _PersonId;
			FilmId = _FilmId;
			Actor = _Actor;
			Director = _Director;

        }

        public PersonFilmIndex(DB.PersonFilmIndex personFilmIndexLinq)
            : this()
        {
            PersonFilmIndex personFilmIndex = new PersonFilmIndex();
            Converter.ConvertToBusinessObject<PersonFilmIndex>(personFilmIndex, personFilmIndexLinq);

            Initialize(personFilmIndex);
        }

        #endregion

        private void Initialize(PersonFilmIndex personFilmIndex)
        {
			Id = personFilmIndex.Id;
			PersonId = personFilmIndex.PersonId;
			FilmId = personFilmIndex.FilmId;
			Actor = personFilmIndex.Actor;
			Director = personFilmIndex.Director;

            OnInitializeComplete();
        }

        #region Linq Helpers

        public bool Save()
        {
			OnSave();
			
            if (Save<global::System.Int32, PersonFilmIndex>(this, Id, false))
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
			
            if (Save<global::System.Int32, PersonFilmIndex>(this, Id, true))
            {
				OnSaveComplete();			
				return true;
			}
			
			return false;
		}
		
        public bool Load(global::System.Int32 _Id)
        {
            DB.PersonFilmIndex personFilmIndexLinq = Load<global::System.Int32>(_Id);
            
            if (personFilmIndexLinq == null)
            {
				return false;
            }
            
            PersonFilmIndex personFilmIndex = new PersonFilmIndex();
            Converter.ConvertToBusinessObject<PersonFilmIndex>(personFilmIndex, personFilmIndexLinq);
            
            Initialize(personFilmIndex);
            
            return true;
        }

        public bool Delete()
        {
	    OnDelete();

            DB.PersonFilmIndex personFilmIndexLinq = Delete<global::System.Int32>(Id);

			if (personFilmIndexLinq == null)
			{
				return false;
			}
			
            PersonFilmIndex personFilmIndex = new PersonFilmIndex();
            Converter.ConvertToBusinessObject<PersonFilmIndex>(personFilmIndex, personFilmIndexLinq);

            Initialize(personFilmIndex);
            
	    OnDeleteComplete();

            return true;
        }

        public List<PersonFilmIndex> Find()
        {
            List<DB.PersonFilmIndex> listLinq = Find(WhereClause);
            
            List<PersonFilmIndex> list = new List<PersonFilmIndex>();
            foreach (DB.PersonFilmIndex personFilmIndex in listLinq)
            {
                list.Add(new PersonFilmIndex(personFilmIndex));
            }

            return list;
        }

        public new List<PersonFilmIndex> ToList()
        {
            List<DB.PersonFilmIndex> listLinq = base.ToList();

            List<PersonFilmIndex> list = new List<PersonFilmIndex>();
            foreach (DB.PersonFilmIndex personFilmIndex in listLinq)
            {
                list.Add(new PersonFilmIndex(personFilmIndex));
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
