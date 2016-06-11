using System;
using System.Data;

namespace SeaQuill.DataAccess.Queries
{
    public class QueryObjectFactory : IQueryObjectFactory
    {
        private string ConnectionString { get; set; }
        private QueryType QueryType { get; set; }
        private DatabaseType DatabaseType { get; set; }

        public QueryObjectFactory(string connectionString, QueryType queryType, DatabaseType databaseType)
        {
            ConnectionString = connectionString;
            DatabaseType = databaseType;
            QueryType = queryType;
        }

        public IQueryObject GetQuery(string commandText)
        {
            var commandType = QueryType == QueryType.Adhoc ? CommandType.Text : CommandType.StoredProcedure;

            switch (DatabaseType)
            {
                case DatabaseType.SqlServer:
                    return new SqlServerQueryObject(ConnectionString, commandText, commandType);

                default:
                    throw new ArgumentException($"Unknown DatabaseType: {DatabaseType}");
            }
        }
    }
}
