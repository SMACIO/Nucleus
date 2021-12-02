using System;
using System.Collections.Generic;

namespace Nucleus.Data
{
    public class NucleusDbConnectionOptions
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        
        public NucleusDatabaseInfoDictionary Databases { get; set; }

        public NucleusDbConnectionOptions()
        {
            ConnectionStrings = new ConnectionStrings();
            Databases = new NucleusDatabaseInfoDictionary();
        }

        public string GetConnectionStringOrNull(
            string connectionStringName,
            bool fallbackToDatabaseMappings = true,
            bool fallbackToDefault = true)
        {
            var connectionString = ConnectionStrings.GetOrDefault(connectionStringName);
            if (!connectionString.IsNullOrEmpty())
            {
                return connectionString;
            }

            if (fallbackToDatabaseMappings)
            {
                var database = Databases.GetMappedDatabaseOrNull(connectionStringName);
                if (database != null)
                {
                    connectionString = ConnectionStrings.GetOrDefault(database.DatabaseName);
                    if (!connectionString.IsNullOrEmpty())
                    {
                        return connectionString;
                    }
                }
            }

            if (fallbackToDefault)
            {
                connectionString = ConnectionStrings.Default;
                if (!connectionString.IsNullOrWhiteSpace())
                {
                    return connectionString;
                }
            }

            return null;
        }
    }
}




