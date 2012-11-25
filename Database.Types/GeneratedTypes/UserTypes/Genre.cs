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
    public partial class Genre : Database.Types.GeneratedTypes.BaseTypes.Genre
    {
		public Genre()
		{
		}

		public Genre(Database.Types.GeneratedTypes.BaseTypes.Genre genre)
		{
			GenreId = genre.GenreId;
			Name = genre.Name;

			OnInitializeComplete();
		}
		
        public Genre(System.Nullable<global::System.Int32> _GenreId, global::System.String _Name)
        {
            if (_GenreId != null)
            {
                GenreId = (global::System.Int32)_GenreId;
            }

			Name = _Name;

        }

        public new List<Genre> Find()
        {
            List<Genre> finalResults = new List<Genre>();

            List<Database.Types.GeneratedTypes.BaseTypes.Genre> results = base.Find();

            return ConvertFromBaseTypes(results);
        }		

        public new List<Genre> ToList()
        {
            List<Genre> finalResults = new List<Genre>();

            List<Database.Types.GeneratedTypes.BaseTypes.Genre> results = base.ToList();

            return ConvertFromBaseTypes(results);
        }
        
        private List<Genre> ConvertFromBaseTypes(List<Database.Types.GeneratedTypes.BaseTypes.Genre> baseTypeList)
        {
			List<Genre> typeList = new List<Genre>();
			
            foreach (Database.Types.GeneratedTypes.BaseTypes.Genre baseType in baseTypeList)
            {
                typeList.Add(new Genre(baseType));
            }
            
            return typeList;
        }
    }
}
