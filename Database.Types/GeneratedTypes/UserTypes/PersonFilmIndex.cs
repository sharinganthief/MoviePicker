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
    public partial class PersonFilmIndex : Database.Types.GeneratedTypes.BaseTypes.PersonFilmIndex
    {
		public PersonFilmIndex()
		{
		}

		public PersonFilmIndex(Database.Types.GeneratedTypes.BaseTypes.PersonFilmIndex personFilmIndex)
		{
			Id = personFilmIndex.Id;
			PersonId = personFilmIndex.PersonId;
			FilmId = personFilmIndex.FilmId;
			Actor = personFilmIndex.Actor;
			Director = personFilmIndex.Director;

			OnInitializeComplete();
		}
		
        public PersonFilmIndex(System.Nullable<global::System.Int32> _Id, global::System.Int32 _PersonId, global::System.Int32 _FilmId, global::System.Boolean _Actor, global::System.Boolean _Director)
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

        public new List<PersonFilmIndex> Find()
        {
            List<PersonFilmIndex> finalResults = new List<PersonFilmIndex>();

            List<Database.Types.GeneratedTypes.BaseTypes.PersonFilmIndex> results = base.Find();

            return ConvertFromBaseTypes(results);
        }		

        public new List<PersonFilmIndex> ToList()
        {
            List<PersonFilmIndex> finalResults = new List<PersonFilmIndex>();

            List<Database.Types.GeneratedTypes.BaseTypes.PersonFilmIndex> results = base.ToList();

            return ConvertFromBaseTypes(results);
        }
        
        private List<PersonFilmIndex> ConvertFromBaseTypes(List<Database.Types.GeneratedTypes.BaseTypes.PersonFilmIndex> baseTypeList)
        {
			List<PersonFilmIndex> typeList = new List<PersonFilmIndex>();
			
            foreach (Database.Types.GeneratedTypes.BaseTypes.PersonFilmIndex baseType in baseTypeList)
            {
                typeList.Add(new PersonFilmIndex(baseType));
            }
            
            return typeList;
        }
    }
}
