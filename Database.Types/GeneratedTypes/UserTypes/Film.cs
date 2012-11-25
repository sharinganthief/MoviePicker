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
    public partial class Film : Database.Types.GeneratedTypes.BaseTypes.Film
    {
		public Film()
		{
		}

		public Film(Database.Types.GeneratedTypes.BaseTypes.Film film)
		{
			FilmId = film.FilmId;
			Length = film.Length;
			Year = film.Year;
			Title = film.Title;

			OnInitializeComplete();
		}
		
        public Film(System.Nullable<global::System.Int32> _FilmId, global::System.Int32 _Length, global::System.Int32 _Year, global::System.String _Title)
        {
            if (_FilmId != null)
            {
                FilmId = (global::System.Int32)_FilmId;
            }

			Length = _Length;
			Year = _Year;
			Title = _Title;

        }

        public new List<Film> Find()
        {
            List<Film> finalResults = new List<Film>();

            List<Database.Types.GeneratedTypes.BaseTypes.Film> results = base.Find();

            return ConvertFromBaseTypes(results);
        }		

        public new List<Film> ToList()
        {
            List<Film> finalResults = new List<Film>();

            List<Database.Types.GeneratedTypes.BaseTypes.Film> results = base.ToList();

            return ConvertFromBaseTypes(results);
        }
        
        private List<Film> ConvertFromBaseTypes(List<Database.Types.GeneratedTypes.BaseTypes.Film> baseTypeList)
        {
			List<Film> typeList = new List<Film>();
			
            foreach (Database.Types.GeneratedTypes.BaseTypes.Film baseType in baseTypeList)
            {
                typeList.Add(new Film(baseType));
            }
            
            return typeList;
        }
    }
}
