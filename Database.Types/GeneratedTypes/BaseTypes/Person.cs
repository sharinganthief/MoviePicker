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
    public class Person : BusinessBaseType<DB.Person, DB.MovieCollectionEntities>
    {
        public class Where
        {
			public bool PersonIdSet;
			public bool FirstNameSet;
			public bool LastNameSet;

			private System.Nullable<global::System.Int32> _PersonId;
			private global::System.String _FirstName;
			private global::System.String _LastName;


			public System.Nullable<global::System.Int32> PersonId { get { return _PersonId; } set { _PersonId = value; PersonIdSet = true; } }
			public global::System.String FirstName { get { return _FirstName; } set { _FirstName = value; FirstNameSet = true; } }
			public global::System.String LastName { get { return _LastName; } set { _LastName = value; LastNameSet = true; } }


            public string MyWhere = "", MyOrderBy = "", MyTop = "";

            public Where()
            {
				PersonId = null;
				FirstName = null;
				LastName = null;

				PersonIdSet = false;
				FirstNameSet = false;
				LastNameSet = false;

            }
        };

		public global::System.Int32 PersonId { get; set; }
		public global::System.String FirstName { get; set; }
		public global::System.String LastName { get; set; }

        public Where WhereClause { get; set; }

        #region Constructors

        public Person()
        {
            WhereClause = new Where();
        }

        public Person(System.Nullable<global::System.Int32> _PersonId, global::System.String _FirstName, global::System.String _LastName)
            : this()
        {
            if (_PersonId != null)
            {
                PersonId = (global::System.Int32)_PersonId;
            }

			FirstName = _FirstName;
			LastName = _LastName;

        }

        public Person(DB.Person personLinq)
            : this()
        {
            Person person = new Person();
            Converter.ConvertToBusinessObject<Person>(person, personLinq);

            Initialize(person);
        }

        #endregion

        private void Initialize(Person person)
        {
			PersonId = person.PersonId;
			FirstName = person.FirstName;
			LastName = person.LastName;

            OnInitializeComplete();
        }

        #region Linq Helpers

        public bool Save()
        {
			OnSave();
			
            if (Save<global::System.Int32, Person>(this, PersonId, false))
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
			
            if (Save<global::System.Int32, Person>(this, PersonId, true))
            {
				OnSaveComplete();			
				return true;
			}
			
			return false;
		}
		
        public bool Load(global::System.Int32 _PersonId)
        {
            DB.Person personLinq = Load<global::System.Int32>(_PersonId);
            
            if (personLinq == null)
            {
				return false;
            }
            
            Person person = new Person();
            Converter.ConvertToBusinessObject<Person>(person, personLinq);
            
            Initialize(person);
            
            return true;
        }

        public bool Delete()
        {
	    OnDelete();

            DB.Person personLinq = Delete<global::System.Int32>(PersonId);

			if (personLinq == null)
			{
				return false;
			}
			
            Person person = new Person();
            Converter.ConvertToBusinessObject<Person>(person, personLinq);

            Initialize(person);
            
	    OnDeleteComplete();

            return true;
        }

        public List<Person> Find()
        {
            List<DB.Person> listLinq = Find(WhereClause);
            
            List<Person> list = new List<Person>();
            foreach (DB.Person person in listLinq)
            {
                list.Add(new Person(person));
            }

            return list;
        }

        public new List<Person> ToList()
        {
            List<DB.Person> listLinq = base.ToList();

            List<Person> list = new List<Person>();
            foreach (DB.Person person in listLinq)
            {
                list.Add(new Person(person));
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
			WhereClause.PersonId = PersonId;
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
			WhereClause.PersonId = PersonId;
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
