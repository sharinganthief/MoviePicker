using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

namespace Database.Types.GeneratedTypes
{
    [Serializable]
    public partial class Person : Database.Types.GeneratedTypes.BaseTypes.Person
    {
		public Person()
		{
		}

		public Person(Database.Types.GeneratedTypes.BaseTypes.Person person)
		{
			PersonId = person.PersonId;
			FirstName = person.FirstName;
			LastName = person.LastName;

			OnInitializeComplete();
		}
		
        public Person(System.Nullable<global::System.Int32> _PersonId, global::System.String _FirstName, global::System.String _LastName)
        {
            if (_PersonId != null)
            {
                PersonId = (global::System.Int32)_PersonId;
            }

			FirstName = _FirstName;
			LastName = _LastName;

        }

        public new List<Person> Find()
        {
            List<Person> finalResults = new List<Person>();

            List<Database.Types.GeneratedTypes.BaseTypes.Person> results = base.Find();

            return ConvertFromBaseTypes(results);
        }		

        public new List<Person> ToList()
        {
            List<Person> finalResults = new List<Person>();

            List<Database.Types.GeneratedTypes.BaseTypes.Person> results = base.ToList();

            return ConvertFromBaseTypes(results);
        }
        
        private List<Person> ConvertFromBaseTypes(List<Database.Types.GeneratedTypes.BaseTypes.Person> baseTypeList)
        {
			List<Person> typeList = new List<Person>();
			
            foreach (Database.Types.GeneratedTypes.BaseTypes.Person baseType in baseTypeList)
            {
                typeList.Add(new Person(baseType));
            }
            
            return typeList;
        }
    }
}
