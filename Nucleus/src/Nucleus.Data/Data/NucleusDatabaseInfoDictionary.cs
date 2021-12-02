using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Nucleus.Data
{
    public class NucleusDatabaseInfoDictionary : Dictionary<string, NucleusDatabaseInfo>
    {
        private Dictionary<string, NucleusDatabaseInfo> ConnectionIndex { get; set; }

        public NucleusDatabaseInfoDictionary()
        {
            ConnectionIndex = new Dictionary<string, NucleusDatabaseInfo>();
        }
        
        [CanBeNull]
        public NucleusDatabaseInfo GetMappedDatabaseOrNull(string connectionStringName)
        {
            return ConnectionIndex.GetOrDefault(connectionStringName);
        }

        public NucleusDatabaseInfoDictionary Configure(string databaseName, Action<NucleusDatabaseInfo> configureAction)
        {
            var databaseInfo = this.GetOrAdd(
                databaseName,
                () => new NucleusDatabaseInfo(databaseName)
            );
            
            configureAction(databaseInfo);
            
            return this;
        }

        /// <summary>
        /// This method should be called if this dictionary changes.
        /// It refreshes indexes for quick access to the connection informations.
        /// </summary>
        public void RefreshIndexes()
        {
            ConnectionIndex = new Dictionary<string, NucleusDatabaseInfo>();
            
            foreach (var databaseInfo in Values)
            {
                foreach (var mappedConnection in databaseInfo.MappedConnections)
                {
                    if (ConnectionIndex.ContainsKey(mappedConnection))
                    {
                        throw new NucleusException(
                            $"A connection name can not map to multiple databases: {mappedConnection}."
                        );
                    }

                    ConnectionIndex[mappedConnection] = databaseInfo;
                }
            }
        }
    }
}





