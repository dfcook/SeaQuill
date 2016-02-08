using System.Data;

namespace SeaQuill.DataAccess.Queries
{
    public enum QueryType
    {
        StoredProcedure,
        Adhoc
    }

    public enum DatabaseType
    {
        SqlServer,
        Oracle,
        MySql
    }

    public interface IQueryObjectFactory
    {        
        IQueryObject GetQuery(string commandText);         
    }
}
