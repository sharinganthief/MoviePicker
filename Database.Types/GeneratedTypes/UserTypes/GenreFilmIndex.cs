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
    public partial class GenreFilmIndex : Database.Types.GeneratedTypes.BaseTypes.GenreFilmIndex
    {
		public GenreFilmIndex()
		{
		}

		public GenreFilmIndex(Database.Types.GeneratedTypes.BaseTypes.GenreFilmIndex genreFilmIndex)
		{
			Id = genreFilmIndex.Id;
			GenreId = genreFilmIndex.GenreId;
			FilmId = genreFilmIndex.FilmId;

			OnInitializeComplete();
		}
		
        public GenreFilmIndex(System.Nullable<global::System.Int32> _Id, global::System.Int32 _GenreId, global::System.Int32 _FilmId)
        {
            if (_Id != null)
            {
                Id = (global::System.Int32)_Id;
            }

			GenreId = _GenreId;
			FilmId = _FilmId;

        }

        public new List<GenreFilmIndex> Find()
        {
            List<GenreFilmIndex> finalResults = new List<GenreFilmIndex>();

            List<Database.Types.GeneratedTypes.BaseTypes.GenreFilmIndex> results = base.Find();

            return ConvertFromBaseTypes(results);
        }		

        public new List<GenreFilmIndex> ToList()
        {
            List<GenreFilmIndex> finalResults = new List<GenreFilmIndex>();

            List<Database.Types.GeneratedTypes.BaseTypes.GenreFilmIndex> results = base.ToList();

            return ConvertFromBaseTypes(results);
        }
        
        private List<GenreFilmIndex> ConvertFromBaseTypes(List<Database.Types.GeneratedTypes.BaseTypes.GenreFilmIndex> baseTypeList)
        {
			List<GenreFilmIndex> typeList = new List<GenreFilmIndex>();
			
            foreach (Database.Types.GeneratedTypes.BaseTypes.GenreFilmIndex baseType in baseTypeList)
            {
                typeList.Add(new GenreFilmIndex(baseType));
            }
            
            return typeList;
        }
    }
}
