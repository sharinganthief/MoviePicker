using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Configuration;
using HelloWorld.DB;

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
    public abstract class BusinessBaseType<TEntity, TContext>
        where TEntity : class, new()
        where TContext : MovieCollectionEntities, new()
    {
        protected TContext Context = null;
		private Exception _lastException;
        private bool _transaction = false;
		
        protected BusinessBaseType()
        {
			#region Context Initialization
            try
            {
                Context = new TContext();
            }
            catch (Exception excep)
            {
				string errorMessage = "Could not instantiate data context.";
				
				if (excep.InnerException.GetType() == typeof(ArgumentException))
				{
					errorMessage = "Could not locate database connection string.";
				}
				
                throw new Exception(errorMessage + " Please verify the Entity Framework connection string has been copied into the main project's App.Config or Web.Config file. Try dragging the App.Config from your DBML or EDMX project into your main project's folder or copy the <connectionStrings> section into your project's Web.Config.\n\n" + excep.InnerException);
            }
			#endregion
			

        }

        public void SetTransactionContext(TContext context)
        {
            Context = context;
            _transaction = true;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        protected TEntity Load<TValue>(TValue pk)
        {
			try
			{
				Type typeOfEntity = typeof(TEntity);

				var primaryKey = (from p in typeOfEntity.GetProperties()
								  from attr in p.GetCustomAttributes(false)
                                  where attr is System.Data.Objects.DataClasses.EdmScalarPropertyAttribute && (attr as System.Data.Objects.DataClasses.EdmScalarPropertyAttribute).EntityKeyProperty
								  select p).FirstOrDefault();

                string tableName = GetTableName<TEntity>();
	            
                string Sql = "SELECT VALUE " + tableName + " FROM " + tableName + " WHERE " + tableName + "." + primaryKey.Name + " = @" + primaryKey.Name;

				IEnumerable<TEntity> entityList = Context.CreateQuery<TEntity>(Sql, new System.Data.Objects.ObjectParameter(primaryKey.Name, pk));
	            
				TEntity entity = entityList.Single();

				return entity;
            }
            catch (Exception excep)
            {
				_lastException = excep;
				return null;
            }
        }

        protected TEntity Delete<TValue>(TValue pk)
        {
            TEntity entity = Load<TValue>(pk);

			if (entity != null)
			{
                Context.DeleteObject(entity);

                if (!_transaction)
                {
                    Context.SaveChanges();
                }
			}
			else
			{
				throw new Exception("Error in Delete(): Verify the primary key property has been set on the object before calling.\n" + _lastException.Message);
			}
			
            return entity;
        }

        protected bool Save<TValue, TBusiness>(TBusiness businessObject, TValue pk, bool explicitInsert)
        {
            TEntity entity = new TEntity();

            // Get the name of the Primary Key.
            Type typeOfEntity = typeof(TEntity);

			var primaryKey = (from p in typeOfEntity.GetProperties()
							  from attr in p.GetCustomAttributes(false)
                              where attr is System.Data.Objects.DataClasses.EdmScalarPropertyAttribute && (attr as System.Data.Objects.DataClasses.EdmScalarPropertyAttribute).EntityKeyProperty
							  select p).FirstOrDefault();

            bool isNew = explicitInsert;
            object primaryKeyValue = Converter.GetPropertyValue(businessObject, primaryKey.Name);

            if (primaryKey.PropertyType.Name.ToLower() == "int32")
            {
                if ((int)primaryKeyValue == 0)
                {
                    isNew = true;
                }
            }
            else if (primaryKey.PropertyType.Name.ToLower() == "guid")
            {
                if ((Guid)primaryKeyValue == Guid.Empty)
                {
                    isNew = true;

                    // Auto-generate a GUID.
					PropertyInfo pi = businessObject.GetType().GetProperty(primaryKey.Name);
					pi.SetValue(businessObject, Guid.NewGuid(), null);
                }
            }
            else if (primaryKey.PropertyType.Name.ToLower() == "string")
            {
                if ((string)primaryKeyValue == "")
                {
                    isNew = true;
                }
            }

            if (isNew)
            {
                Context.AddObject(GetTableName<TBusiness>(), entity);
            }
            else
            {
                // Updating an existing entity. First, load the LINQ context to update.
                entity = Load<TValue>(pk);
                if (entity == null)
                {
					throw new Exception("Error in Save(): Verify the primary key property has been set before updating. If you supplied the primary key value and wish to do an explicit Insert, try calling BusinessType.Insert() instead of BusinessType.Save().\n" + _lastException.Message);
                }
            }

            // Set the new values.
            Converter.ConvertToBusinessObject<TEntity>(entity, businessObject);

            if (!_transaction)
            {
               Context.SaveChanges();
            }
            
            // If inserting, load primary key value into businessObject to return.
            if (isNew)
            {
                object primaryKeyValueLinq = Converter.GetPropertyValue(entity, primaryKey.Name);

                PropertyInfo pi = businessObject.GetType().GetProperty(primaryKey.Name);
                pi.SetValue(businessObject, primaryKeyValueLinq, null);
            }
            
            return true;
        }

        protected List<TEntity> Find<TBusinessWhere>(TBusinessWhere entity)
        {
            // Form a dynamic LINQ query using the LINQ object.
            string MyWhere = "", MyTop = "", MyOrderBy = "";

            FieldInfo fi = entity.GetType().GetField("MyTop");
            if (fi != null)
            {
                MyTop = (string)fi.GetValue(entity);
            }

            fi = entity.GetType().GetField("MyWhere");
            if (fi != null)
            {
                MyWhere = (string)fi.GetValue(entity);
            }

            fi = entity.GetType().GetField("MyOrderBy");
            if (fi != null)
            {
                MyOrderBy = (string)fi.GetValue(entity);
            }

            string tableName = GetTableName<TEntity>();

            string sql = "SELECT VALUE " + tableName + " FROM ";

            List<System.Data.Objects.ObjectParameter> propertyValues = new List<System.Data.Objects.ObjectParameter>();
            List<bool> propertyValuesSet = new List<bool>();
            int propertyUseCount = 0;

            sql += tableName + " WHERE ";

            if (MyWhere.Length > 0)
            {
                sql += MyWhere.Replace("'", "''") + " AND ";
                propertyUseCount = 1;
            }

            sql += "1=1";

            // First, get the boolean indicator values so we know which fields to use.
            FieldInfo[] fieldsInfo = entity.GetType().GetFields();
            foreach (FieldInfo field in fieldsInfo)
            {
                string propertyName = field.Name;
                object propertyValue = field.GetValue(entity);

                if (propertyName.EndsWith("Set") && propertyValue.GetType().Name.ToLower() == "boolean")
                {
                    // Boolean flag indicating if we should use this field or not. Allows us to use null in query.
                    propertyValuesSet.Add((bool)propertyValue);
                }
            }

            // Next, go through the actual properties and if the boolean indicator is true, use it in the query.
            int propertyIndex = 0;
            PropertyInfo[] propInfo = entity.GetType().GetProperties();
            foreach (PropertyInfo property in propInfo)
            {
                string propertyName = property.Name;
                object propertyValue = null;

                propertyValue = property.GetValue(entity, null);

                // Actual property value to use in query.
                if (propertyValuesSet[propertyIndex] == true)
                {
                    // This value was set by the user. Use this value in the query.
                    sql += " AND " + tableName + "." + propertyName;

                    if (propertyValue == null)
                    {
                        sql += " IS NULL";
                    }
                    else
                    {
                        sql += " = @p" + propertyValues.Count;

                        propertyValues.Add(new System.Data.Objects.ObjectParameter("p" + propertyValues.Count.ToString(), propertyValue));
                    }

                    propertyUseCount++;
                }

                propertyIndex++;
            }

            if (MyOrderBy.Length > 0)
            {
                sql += " " + MyOrderBy;
            }

            IEnumerable<TEntity> entityList = new List<TEntity>();

            if (propertyUseCount > 0)
            {
                entityList = Context.CreateQuery<TEntity>(sql, propertyValues.ToArray());
            }

            if (MyTop.Length > 0)
            {
                char[] seps = {' '};
                string[] parts = MyTop.Split(seps);
                if (parts.Length >= 2)
                {
                    entityList = entityList.Take(Convert.ToInt32(parts[1]));
                }
            }

            return entityList.ToList();
        }

        protected List<TEntity> ToList()
        {
            string tableName = GetTableName<TEntity>();

            string Sql = "SELECT VALUE " + tableName + " FROM " + tableName;
            IEnumerable<TEntity> entityList = Context.CreateQuery<TEntity>(Sql, new System.Data.Objects.ObjectParameter[]{});

            return entityList.ToList();
        }

        private string GetTableName<T>()
        {
            // Get the name of the entity's table. (old format: typeOfEntity.Name);
            return Context.MetadataWorkspace.GetEntityContainer(Context.DefaultContainerName, System.Data.Metadata.Edm.DataSpace.CSpace).BaseEntitySets.Where(bes => bes.ElementType.Name == typeof(T).Name).FirstOrDefault().Name;
        }
    }

    public class Converter
    {
        public static object ConvertToBusinessObject<TEntity1>(TEntity1 originalObject, object abusinessType)
        {
            // Convert originalObject to a businessType object.
            object businessObject = null;
            Type businessType = originalObject.GetType();
            PropertyInfo lastProperty = null;
            bool foreignKeyReference = false;

            // Find the class
            Type obj = Type.GetType(businessType.AssemblyQualifiedName);

            // Get it's constructor
            ConstructorInfo constructor = obj.GetConstructor(new Type[] { });

            // Invoke it's constructor, which returns an instance of businessType.
            businessObject = constructor.Invoke(null);

            PropertyInfo[] propInfo = abusinessType.GetType().GetProperties();
            foreach (PropertyInfo property in propInfo)
            {
                string key = property.Name;
                object value = null;

                foreignKeyReference = false;

                if (!property.PropertyType.IsArray || property.PropertyType.Name.ToLower() == "byte[]")
                {
                    if (property.PropertyType.Name.IndexOf("EntityReference") > -1)
                    {
                        // This is an EntityFramework foreign key. Set the value, if one exists. Otherwise, it defaults to null.
                        System.Data.EntityKey entityKey = ((System.Data.Objects.DataClasses.EntityReference)(property.GetValue(abusinessType, null))).EntityKey;
                        if (entityKey != null)
                        {
                            foreignKeyReference = true;
                            key = entityKey.EntityKeyValues[0].Key;
                            value = ((System.Data.Objects.DataClasses.EntityReference)(property.GetValue(abusinessType, null))).EntityKey.EntityKeyValues[0].Value;
                        }
                    }
                    else
                    {
                        value = property.GetValue(abusinessType, null);
                    }

                    // Grab any AlternateName attribute from the property.
                    string alternateKey = "";
                    string linqContextPropertyName = "";
                    bool foreignKeyReferenceProcessed = false;

                    // If we're doing a Set (for example: table.ForeignId = N) then setup the proper mapping.
                    object[] attributes = property.GetCustomAttributes(false);
                    foreach (object attribute in attributes)
                    {
                        if (attribute is AlternateMappingAttribute)
                        {
                            alternateKey = ((AlternateMappingAttribute)attribute).ForeignKeyName;
                            linqContextPropertyName = ((AlternateMappingAttribute)attribute).LinqPropertyName;
                            foreignKeyReferenceProcessed = true;

						    // For referenced foreign keys, make sure it has a value to set, otherwise leave null.
                            if (value != null &&
                               (value.ToString().Length == 0 || value.ToString() == "0" || value.ToString() == Guid.Empty.ToString()))
                            {
                                value = null;
                            }
                        }
                    }

                    // If we're doing a Get (for example: table.Load(N) where table contains a ForeignId) then setup the proper mapping.
                    if (foreignKeyReference && !foreignKeyReferenceProcessed)
                    {
                        bool doneMapping = false;
                        
                        // Find the foreign key property name in our business type that also has an AlternateMappingAttribute.
                        PropertyInfo[] propInfo2 = businessType.GetProperties();
                        foreach (PropertyInfo property2 in propInfo2)
                        {
                            object[] attributes2 = property2.GetCustomAttributes(false);
                            foreach (object attribute in attributes2)
                            {
                                if (attribute is AlternateMappingAttribute)
                                {
                                    if (((AlternateMappingAttribute)attribute).LinqPropertyName == lastProperty.Name)
                                    {
										// We've found a mapping property. Change the key to set to be the alternateKey.
                                        alternateKey = property2.Name;
                                        linqContextPropertyName = ((AlternateMappingAttribute)attribute).LinqPropertyName;
                                        key = alternateKey;

                                        // For referenced foreign keys, make sure it has a value to set, otherwise leave null.
                                        if (value != null &&
                                           (value.ToString().Length == 0 || value.ToString() == "0" || value.ToString() == Guid.Empty.ToString()))
                                        {
                                            value = null;
                                        }

                                        doneMapping = true;
                                        break;
                                    }
                                }
                            }

                            if (doneMapping)
                                break;
                        }
                    }

                    SetPropertyValue(originalObject, key, value, alternateKey, linqContextPropertyName);
                }

                // Record the last property to use with foreign key lookups.
                lastProperty = property;
            }

            return businessObject;
        }
        
        public static bool SetPropertyValue(object obj, string linqPropertyName, object propertyValue, string alternateLinqPropertyName, string linqContextPropertyName)
        {
            PropertyInfo[] propInfo = obj.GetType().GetProperties();
            PropertyInfo lastProperty = null;

            // Get an auto-incrementing primary key, if one exists, so we know not to set its value.
			var primaryKeyAutoIncrement = (from p in propInfo
							  from attr in p.GetCustomAttributes(false)
                              where attr is System.Data.Objects.DataClasses.EdmScalarPropertyAttribute && 
                                ( (attr as System.Data.Objects.DataClasses.EdmScalarPropertyAttribute).EntityKeyProperty &&
                                  !(attr as System.Data.Objects.DataClasses.EdmScalarPropertyAttribute).IsNullable )
							  select p).FirstOrDefault();

            foreach (PropertyInfo property in propInfo)
            {
                string propertyName = property.Name;

                if (property.PropertyType.Name.IndexOf("EntityReference") > -1)
                {
                    // This is an EntityFramework foreign key.
                    if (lastProperty != null)
                    {
                        // If the property has a different name than the foreign key table's primary key, then set the appropriate linq context property name.
                        if (alternateLinqPropertyName.Length > 0 && lastProperty.Name == linqContextPropertyName)
                        {
                            if (SetPropertyValueEntityReference(obj, alternateLinqPropertyName, propertyValue, lastProperty, property))
                            {
                                // We successfully set the foreign key value for a property with a different name than the foreign key's primary key name.
                                return true;
                            }
                        }
                        else
                        {
                            if (SetPropertyValueEntityReference(obj, linqPropertyName, propertyValue, lastProperty, property))
                            {
                                // We successfully set the foreign key value.
                                return true;
                            }
                        }
                    }
                }

                bool isPrimaryKeyAutoIncrement = false;
                if (primaryKeyAutoIncrement != null && propertyName.ToLower() == primaryKeyAutoIncrement.Name.ToLower() && (primaryKeyAutoIncrement.PropertyType == typeof(Int32)))
                {
                    // Auto-incrementing primary key integer or an already existing Guid primary key (which can not be changed), so do not assign a value.
                    try
                    {
                        // If a value was supplied for the primary key, assume we're not auto-incrementing, and try to use it.
                        if ((primaryKeyAutoIncrement.PropertyType == typeof(Int32) && Convert.ToInt32(propertyValue) != 0) || (primaryKeyAutoIncrement.PropertyType == typeof(Guid) && new Guid(propertyValue.ToString()) != Guid.Empty))
                        {
                            // A value was supplied, so assume this is not an auto-increment property.
                            isPrimaryKeyAutoIncrement = false;
                        }
                        else
                        {
                            isPrimaryKeyAutoIncrement = true;
                        }
                    }
                    catch
                    {
                        isPrimaryKeyAutoIncrement = true;
                    }
                }

                if ((!property.PropertyType.IsArray || property.PropertyType.Name.ToLower() == "byte[]") && !isPrimaryKeyAutoIncrement && propertyName.ToLower() == linqPropertyName.ToLower())
                {
                    if (primaryKeyAutoIncrement != null && propertyName.ToLower() == primaryKeyAutoIncrement.Name.ToLower() && primaryKeyAutoIncrement.PropertyType == typeof(Guid) &&
                        (Guid)GetPropertyValue(obj, propertyName) != Guid.Empty)
                    {
                        // Do not set the value for a Guid primary key which is already set.
                        return true;
                    }
                    else
                    {
                        object currentValue = property.GetValue(obj, null);
                        if (currentValue == propertyValue || (currentValue != null && (currentValue.Equals(propertyValue) || currentValue.ToString() == propertyValue.ToString()) ))
                        {
                            // Value has not changed.
                        }
                        else
                        {
                            property.SetValue(obj, propertyValue, null);
                        }

                        return true;
                    }
                }

                // Record the last property to use with foreign key lookups.
                lastProperty = property;
            }

            return false;
        }

        public static object GetPropertyValue(object obj, string linqPropertyName)
        {
            PropertyInfo[] propInfo = obj.GetType().GetProperties();
            foreach (PropertyInfo property in propInfo)
            {
                string propertyName = property.Name;

                if ((!property.PropertyType.IsArray || property.PropertyType.Name.ToLower() == "byte[]") && propertyName.ToLower() == linqPropertyName.ToLower())
                {
                    return property.GetValue(obj, null);
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if a property is the key we want to set the value on.
        /// Do this by instantiating the foreign key reference class and getting its primary key.
        /// We then compare the primary key name with linqPropertyName to see if this is the key we want to set the value on.
        /// </summary>
        /// <param name="obj">Entity Framework class containing the foreign key references</param>
        /// <param name="linqPropertyName">Name of foreign key field to set value on (Foreign Class's primary key)</param>
        /// <param name="propertyValue">Value to set for foreign key</param>
        /// <param name="entityObjectProperty">Foreign key Entity Framework class, ie: MyForeignTable</param>
        /// <param name="entityReferenceProperty">Foreign key EntityReference class, ie: MyForeignTableReference</param>
        /// <returns>true if value was successfully set for foreign key</returns>
        private static bool SetPropertyValueEntityReference(object obj, string linqPropertyName, object propertyValue, PropertyInfo entityObjectProperty, PropertyInfo entityReferenceProperty)
        {
            // Get the EntityReference object (ForeignKeyReference).
            object val = entityReferenceProperty.GetValue(obj, null);

            // Get the existing value for the foreign key.
            System.Data.EntityKey entityKey = ((System.Data.Objects.DataClasses.EntityReference)(val)).EntityKey;

            // Get the type of the foreign key class.
            Type foreignKeyType = entityObjectProperty.PropertyType;

            // Get it's constructor
            ConstructorInfo foreignKeyConstructor = foreignKeyType.GetConstructor(new Type[] { });

            // Invoke it's constructor, which returns an instance of the foreign key class.
            object foreignKey = foreignKeyConstructor.Invoke(new object[] { });

            var primaryKeyForeignKey = (from p in foreignKey.GetType().GetProperties()
                                        from attr in p.GetCustomAttributes(false)
                                        where attr is System.Data.Objects.DataClasses.EdmScalarPropertyAttribute &&
                                          ((attr as System.Data.Objects.DataClasses.EdmScalarPropertyAttribute).EntityKeyProperty &&
                                            !(attr as System.Data.Objects.DataClasses.EdmScalarPropertyAttribute).IsNullable)
                                        select p).FirstOrDefault();

            if (primaryKeyForeignKey.Name == linqPropertyName)
            {
                // This is the property we want to set the value on.
                // Get the EntityKey for the foreign key.
                PropertyInfo entityKeyProperty = ((System.Data.Objects.DataClasses.EntityReference)(val)).GetType().GetProperty("EntityKey");

                object currentValue = null;
                
                System.Data.EntityKey entityKeyObj = (System.Data.EntityKey)entityKeyProperty.GetValue(val, null);
                if (entityKeyObj != null)
                {
                    currentValue = entityKeyObj.EntityKeyValues[0].Value;
                }

                if (currentValue == propertyValue || (currentValue != null && currentValue.Equals(propertyValue)))
                {
                    // Value has not changed.
                }
                else
                {
                    // If the foreign key has a value, we need to set it to null before assigning a new value.
                    if (entityKey != null)
                    {
                        // The foreign key already has a value. First set it to null, then we'll assign the new value.
                        entityKey = null;

                        entityKeyProperty.SetValue(val, entityKey, null);
                    }

                    // If the foreign key is being assigned a null value, then we're done. Otherwise, set the value.
                    if (propertyValue != null)
                    {
                        // Create an EntityKey to hold the foreign key's value. Instantiate EntityKey.
                        Type entityType = typeof(System.Data.EntityKey);

                        // Get it's constructor
                        ConstructorInfo constructor = entityType.GetConstructor(new Type[] { typeof(string), typeof(string), typeof(object) });

                        // Setup the naming structure for the EntityKey.
                        string dbContextName = ((System.Data.Objects.DataClasses.EntityObject)(obj)).EntityKey.EntityContainerName;
                        string entityKeyName = ((System.Data.Objects.DataClasses.RelatedEnd)(val)).TargetRoleName;
                        string targetKey = dbContextName + "." + entityKeyName;

                        // Invoke it's constructor, which returns an instance of the EntityKey.
                        object entityRef = constructor.Invoke(new object[] { targetKey, linqPropertyName, propertyValue });

                        // Set the value for the foreign key.
                        entityKeyProperty.SetValue(val, entityRef, null);
                    }
                }

                return true;
            }

            return false;
        }
    }
    
    public class AlternateMappingAttribute : Attribute
    {
        private string _linqPropertyName;
        private string _foreignKeyName;

        public string LinqPropertyName
        {
            get
            {
                return _linqPropertyName;
            }
        }

        public string ForeignKeyName
        {
            get
            {
                return _foreignKeyName;
            }
        }

        /// <summary>
        /// Creates an AlternateMappingAttribute
        /// </summary>
        /// <param name="linqPropertyName">The name of the LINQ context property</param>
        /// <param name="foreignPrimaryKeyName">The name of the foreign key table's primary key to map the linq property to</param>
        public AlternateMappingAttribute(string linqPropertyName, string foreignPrimaryKeyName)
        {
            _linqPropertyName = linqPropertyName;
            _foreignKeyName = foreignPrimaryKeyName;
        }
    }    
}
