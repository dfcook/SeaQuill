using SeaQuill.DataAccess.Queries;
using SeaQuill.ObjectMapping;
using System.Collections.Generic;
using System.Data;

namespace SeaQuill.DataAccess
{
    public static class ObjectSelectStatementExtensions
    {
        private static string ConnectionString =>
            @"Data Source=TGTLND-SVRSQL1\SQL2008R2;Initial Catalog = Broker_Dan; Integrated Security = True";

        public static IEnumerable<T> ExecuteList<T>(this ObjectSelectStatement<T> statement) where T : new()
        {
            var query = new SqlServerQueryObject(ConnectionString, 
                statement.ToString(), CommandType.Text);
            return query.ExecuteList<T>();
        }

        public static T ExecuteSingle<T>(this ObjectSelectStatement<T> statement) where T : new()
        {
            var query = new SqlServerQueryObject(ConnectionString, 
                statement.ToString(), CommandType.Text);
            return query.ExecuteObject<T>();
        }

        public static T ExecuteScalar<T>(this ObjectSelectStatement<T> statement)
        {
            var query = new SqlServerQueryObject(ConnectionString,
                statement.ToString(), CommandType.Text);
            return query.ExecuteScalar<T>();
        }

        public static PagedResult<T> ExecuteList<T>(this PagedObjectSelectStatement<T> statement) where T : new()
        {
            var query = new SqlServerQueryObject(ConnectionString,
                statement.ToString(), CommandType.Text);
            return query.ExecutePagedResult<T>();
        }

        public static T ExecuteSingle<T>(this PagedObjectSelectStatement<T> statement) where T : new()
        {
            var query = new SqlServerQueryObject(ConnectionString,
                statement.ToString(), CommandType.Text);
            return query.ExecuteObject<T>();
        }

        public static T ExecuteScalar<T>(this PagedObjectSelectStatement<T> statement)
        {
            var query = new SqlServerQueryObject(ConnectionString,
                statement.ToString(), CommandType.Text);
            return query.ExecuteScalar<T>();
        }                
    }
}